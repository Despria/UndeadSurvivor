using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3Variable playerPosition;

    void Update()
    {
        transform.position = new Vector3(playerPosition.runtimeValue.x, playerPosition.runtimeValue.y, -10f);
    }
}