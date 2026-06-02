using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothTime = 0.2f;

    public Vector3 offset = new Vector3(0, 0, -10f);

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 targetPosition = target.position + offset;

        // FORCE camera Z so it always renders 2D world correctly
        targetPosition.z = -10f;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );
    }
}