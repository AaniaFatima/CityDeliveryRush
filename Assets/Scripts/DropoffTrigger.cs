using UnityEngine;

public class DropoffTrigger : MonoBehaviour
{
    public DeliveryManager deliveryManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            deliveryManager.CompleteDelivery(gameObject);
        }
    }
}