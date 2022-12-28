using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Fields
    Rigidbody2D rigidBody;
    Animator animator;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

    [Header("Player Status")]
    [SerializeField] FloatVariable playerHp;
    [SerializeField] FloatVariable playerMaximumHp;
    [SerializeField] IntVariable playerLevel;
    [SerializeField] FloatVariable playerExp;
    [SerializeField] FloatVariable playerMaximumExp;
    [SerializeField] FloatVariable playerInvincibleTime;
    [SerializeField] Vector3Variable playerPosition;
    [SerializeField] bool isDead;
    [SerializeField] bool isInvincible;
    [SerializeField] float invincibleTime;

    [Header("Player Movement")]
    [SerializeField] Vector2 inputVector;
    [SerializeField] FloatVariable moveSpeed;

    [Header("Player Sound")]
    [SerializeField] AudioClip hitSound;

    [Header("Player Event")]
    [SerializeField] GameEvent OnPlayerLevelUp;
    [SerializeField] GameEvent OnPlayerHit;
    [SerializeField] GameEvent OnPlayerDied;
    #endregion

    #region Unity Event
    void OnEnable()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        playerExp.ResetValue();
        playerMaximumExp.ResetValue();
        playerMaximumHp.ResetValue();
        playerHp.runtimeValue = playerMaximumHp.runtimeValue;
        playerPosition.runtimeValue = transform.position;
    }

    void FixedUpdate()
    {
        Move();
        Animation();

        if (isInvincible)
            CheckInvincible();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (isDead)
            return;

        if (collision.gameObject.CompareTag("Monster"))
        {
            Monster monster = collision.gameObject.GetComponent<Monster>();
            
            if (!monster.IsDead)
                GetDamage(monster.AttackPoint.runtimeValue);
        }

        if (collision.gameObject.CompareTag("Item"))
        {
            GetItem(collision.gameObject.gameObject.GetComponent<ICollectable>());
        }
    }
    #endregion

    #region Method
    void Move()
    {
        if (!isDead)
        {
            Vector2 movePosition = moveSpeed.runtimeValue * Time.fixedDeltaTime * inputVector;
            rigidBody.MovePosition(rigidBody.position + movePosition);

            playerPosition.runtimeValue = transform.position;
        }
    }

    void Animation()
    {
        if (isDead)
        {
            animator.SetBool("Dead", true);
            return;
        }

        if (inputVector != Vector2.zero)
        {
            animator.SetBool("Run", true);

            if (inputVector.x < 0)
                spriteRenderer.flipX = true;
            else
                spriteRenderer.flipX = false;
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    void GetDamage(float damage)
    {
        if (!isInvincible && !isDead)
        {
            playerHp.runtimeValue -= damage;
            audioSource.PlayOneShot(hitSound);
            OnPlayerHit.Raise();
            isInvincible = true;

            if (playerHp.runtimeValue <= 0)
            {
                animator.SetBool("Dead", true);
                isDead = true;

                SoundManager.Instance.PauseSound();
                OnPlayerDied.Raise();
                GameManager.Instance.GameFinished(false);
            }
        }
    }

    void GetItem(ICollectable item)
    {
        item.Get();

        if (playerExp.runtimeValue >= playerMaximumExp.runtimeValue)
        {
            PlayerLevelUp();
        }
    }

    void CheckInvincible()
    {
        invincibleTime += Time.deltaTime;
        if (invincibleTime > playerInvincibleTime.runtimeValue)
        {
            isInvincible = false;
            invincibleTime = 0f;
        }
    }

    void PlayerLevelUp()
    {
        playerLevel.runtimeValue += 1;
        playerMaximumExp.runtimeValue = 100 * playerLevel.runtimeValue * 1.2f;
        playerMaximumHp.runtimeValue += 10;

        OnPlayerLevelUp.Raise();
        GameSceneUI.Instance.OpenSkillSelect();

        playerHp.runtimeValue = playerMaximumHp.runtimeValue;
        playerExp.runtimeValue = 0f;
    }
    #endregion

    #region Input System
    void OnMove(InputValue input)
    {
        inputVector = input.Get<Vector2>();
    }
    #endregion
}