using UnityEngine;

public class MarkerRotator : MonoBehaviour
{
    public float rotateSpeed = 80f;
    public float floatSpeed = 2f;
    public float floatHeight = 0.25f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);

        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.localPosition = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
