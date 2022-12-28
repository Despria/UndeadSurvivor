using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;
using Unity.Burst;
using System.Collections;

public class Monster : MonoBehaviour, IPooledObject
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

    [Header("Monster Setting")]
    [SerializeField] Vector3Variable playerPosition;
    [SerializeField] IntVariable currentMonsterNumber;
    [SerializeField] StringVariable monsterTag;
    [SerializeField] StringVariable dropExpTag;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip deadSound;

    [Header("Monster Movement")]
    [SerializeField] FloatVariable speed;
    [SerializeField] Vector3 direction;

    [Header("Monster Status")]
    [SerializeField] FloatVariable maximumHP;
    [SerializeField] float currentHP;
    [SerializeField] FloatVariable attackPoint;
    [SerializeField] FloatVariable monsterInvincibleTime;
    [SerializeField] bool isDead;
    [SerializeField] bool isInvincible;
    [SerializeField] float invincibleTime;
    public FloatVariable AttackPoint { get { return attackPoint; } }
    public bool IsDead { get { return isDead; } }

    #region Job
    PositionUpdateJob positionUpdateJob;
    JobHandle positionUpdateJobHandle;
    TransformAccessArray transformAccessArray;

    [BurstCompile]
    struct PositionUpdateJob : IJobParallelForTransform
    {
        public Vector3 direction;
        public float speed;
        public bool isDead;
        public float jobDeltaTime;

        public void Execute(int index, TransformAccess transform)
        {
            if (!isDead)
                transform.position += jobDeltaTime * speed * direction;
        }
    }
    #endregion

    #region Unity Event
    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource= GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        transformAccessArray = new TransformAccessArray(1);
        transformAccessArray.Add(transform);
    }

    void FixedUpdate()
    {
        direction = (playerPosition.runtimeValue - transform.position).normalized;
        if (direction.x < 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;

        positionUpdateJob = new PositionUpdateJob()
        {
            direction = direction,
            speed = speed.runtimeValue,
            isDead = isDead,
            jobDeltaTime = Time.fixedDeltaTime
        };

        positionUpdateJobHandle = positionUpdateJob.Schedule(transformAccessArray);
    }

    void LateUpdate()
    {
        positionUpdateJobHandle.Complete();

        if (isInvincible) 
            CheckInvincible();
    }

    void OnDisable()
    {
        positionUpdateJobHandle.Complete();
        transformAccessArray.Dispose();
    }
    #endregion

    #region Method
    public void GetDamage(float damage)
    {
        if (!isInvincible && !isDead)
        {
            isInvincible = true;
            animator.SetBool("Hit", true);
            audioSource.PlayOneShot(hitSound);
            StartCoroutine(ReturnToRunAnimation());
            currentHP -= damage;

            if (currentHP <= 0)
            {
                isDead = true;
                animator.SetBool("Dead", true);
                audioSource.PlayOneShot(deadSound);
                StartCoroutine(ReturnDeadMonsterToPool());
                
                currentMonsterNumber.runtimeValue--;

                ObjectPooler.Instance.SpawnFromPool(dropExpTag.runtimeValue, transform.position, Quaternion.identity);
            }
        }
    }

    void CheckInvincible()
    {
        invincibleTime += Time.deltaTime;
        if (invincibleTime > monsterInvincibleTime.runtimeValue)
        {
            isInvincible = false;
            invincibleTime = 0f;
        }
    }

    IEnumerator ReturnToRunAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        animator.SetBool("Hit", false);
    }

    IEnumerator ReturnDeadMonsterToPool()
    {
        yield return new WaitForSeconds(3f);
        ObjectPooler.Instance.InsertToPool(monsterTag.runtimeValue, gameObject);
    }

    public void OnObjectSpawn()
    {
        currentHP = maximumHP.runtimeValue;
        isDead = false;
    }

    public void OnObjectInsert() { }
    #endregion
}