
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 0.125f;
    public Vector3 offset;

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, speed);
        transform.position = smoothPosition;
    }
}
