using UnityEngine;

public class VehicleEnterExit : MonoBehaviour
{
    public GameObject player;
    public GameObject scooter;

    public PlayerFootController footController;
    public SimpleScooterController scooterController;

    public Transform seatPoint;
    public Transform exitPoint;

    public float enterDistance = 2f;

    private bool isRiding = false;
    private Rigidbody playerRb;
    private Collider playerCollider;
    private Animator animator;

    void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
        playerCollider = player.GetComponent<Collider>();
        animator = player.GetComponent<Animator>();

        footController.enabled = true;
        scooterController.enabled = false;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, scooter.transform.position);

        // Keyboard enter
        if (!isRiding && distance <= enterDistance && Input.GetKeyDown(KeyCode.V))
        {
            EnterScooter();
        }

        // Keyboard exit
        if (isRiding && Input.GetKeyDown(KeyCode.E))
        {
            ExitScooter();
        }

        if (isRiding && seatPoint != null)
        {
            player.transform.position = seatPoint.position;
            player.transform.rotation = seatPoint.rotation;
        }
    }

    // MOBILE V BUTTON
    public void MobileEnterButton()
    {
        float distance = Vector3.Distance(player.transform.position, scooter.transform.position);

        if (!isRiding && distance <= enterDistance)
        {
            EnterScooter();
        }
    }

    // MOBILE E BUTTON
    public void MobileExitButton()
    {
        if (isRiding)
        {
            ExitScooter();
        }
    }

    void EnterScooter()
    {
        isRiding = true;

        footController.enabled = false;
        scooterController.enabled = true;

        playerRb.isKinematic = true;
        playerRb.useGravity = false;
        playerCollider.enabled = false;

        player.transform.SetParent(scooter.transform, true);

        player.transform.position = seatPoint.position;
        player.transform.rotation = seatPoint.rotation;

        if (animator != null)
        {
            animator.Play("Sitting", 0, 0f);
        }
    }

    void ExitScooter()
    {
        isRiding = false;

        player.transform.SetParent(null, true);

        player.transform.position = exitPoint.position;
        player.transform.rotation = exitPoint.rotation;

        playerCollider.enabled = true;
        playerRb.isKinematic = false;
        playerRb.useGravity = true;

        footController.enabled = true;
        scooterController.enabled = false;

        if (animator != null)
        {
            animator.Play("Idle", 0, 0f);
        }
    }

    public bool IsRiding()
    {
        return isRiding;
    }
}