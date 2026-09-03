using UnityEngine;

public class SimpleScooterController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float turnSpeed = 120f;
    public float brakeForce = 3f;

    public MobileInputManager mobileInput;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveInput = mobileInput.Vertical;
        float turnInput = mobileInput.Horizontal;

        float currentSpeed = moveSpeed;

        if (mobileInput.Brake)
        {
            currentSpeed = brakeForce;
        }

        Vector3 move =
            transform.forward *
            moveInput *
            currentSpeed *
            Time.fixedDeltaTime;

        rb.MovePosition(rb.position + move);

        if (Mathf.Abs(moveInput) > 0.1f)
        {
            Quaternion turn =
                Quaternion.Euler(
                    0f,
                    turnInput * turnSpeed * Time.fixedDeltaTime,
                    0f
                );

            rb.MoveRotation(rb.rotation * turn);
        }
    }
}