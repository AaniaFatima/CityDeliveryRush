using UnityEngine;

public class PickupTrigger : MonoBehaviour
{
    public DeliveryManager deliveryManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            deliveryManager.PickUpPackage(gameObject);
        }
    }
}