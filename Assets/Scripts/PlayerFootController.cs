using UnityEngine;

public class PlayerFootController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float turnSpeed = 12f;

    public MobileInputManager mobileInput;

    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float horizontal = mobileInput.Horizontal;
        float vertical = mobileInput.Vertical;

        Vector3 inputDirection =
            new Vector3(horizontal, 0f, vertical).normalized;

        bool isMoving = inputDirection.magnitude >= 0.1f;

        bool isRunning =
            isMoving && Input.GetKey(KeyCode.LeftShift);

        float currentSpeed =
            isRunning ? runSpeed : walkSpeed;

        if (isMoving)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(inputDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                turnSpeed * Time.fixedDeltaTime
            );

            Vector3 movePosition =
                rb.position +
                inputDirection * currentSpeed * Time.fixedDeltaTime;

            rb.MovePosition(movePosition);
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", isMoving ? 1f : 0f);

            animator.SetBool("IsRunning", isRunning);
        }
    }
}