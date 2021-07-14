using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // camera will follow this object
    public Transform Target;
    // offset between camera and target
    private Vector3 Offset;
    // change this value to get desired smoothness
    public float SmoothTimeX = 0.3f;
    public float SmoothTimeZ = 0.3f;

    // This value will change at the runtime depending on target movement. Initialize with zero vector.
    private float velocityX = 0;
    private float velocityZ = 0;

    private void Start()
    {
        Offset = transform.position - Target.position;
    }

    /// <summary>
    /// Im not using Vec3.SmootDamp function since, I want to have a control of X and Z damping seperately.
    /// </summary>
    private void LateUpdate()
    {
        // update position
        Vector3 targetPosition = Target.position + Offset;

        Vector3 newPos = Vector3.zero;
        newPos.x = Mathf.SmoothDamp(transform.position.x, targetPosition.x, ref velocityX, SmoothTimeX);
        newPos.z = Mathf.SmoothDamp(transform.position.z, targetPosition.z, ref velocityZ, SmoothTimeZ);

        newPos.y = Offset.y + Mathf.Abs(velocityZ * 0.2f);

        transform.position = newPos;

    }
}