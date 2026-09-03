using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTarget;
    public Transform scooterTarget;
    public VehicleEnterExit enterExitScript;

    public float smoothTime = 0.18f;
    public float rotationSmooth = 6f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 offset;
    private Transform currentTarget;

    void Start()
    {
        currentTarget = playerTarget;

        // Jo camera ki current position hai, usi ka offset save karega
        if (currentTarget != null)
        {
            offset = transform.position - currentTarget.position;
        }
    }

    void LateUpdate()
    {
        if (enterExitScript != null && enterExitScript.IsRiding())
        {
            currentTarget = scooterTarget;
        }
        else
        {
            currentTarget = playerTarget;
        }

        if (currentTarget == null) return;

        Vector3 targetPosition = currentTarget.position + offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );

        Quaternion targetRotation = Quaternion.LookRotation(
            currentTarget.position - transform.position
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSmooth * Time.deltaTime
        );
    }
}