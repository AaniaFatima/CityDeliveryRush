using UnityEngine;

public class AutoAddVehicleColliders : MonoBehaviour
{
    void Start()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            string n = obj.name.ToLower();

            if (n.Contains("vehicle") || n.Contains("car") || n.Contains("taxi") || n.Contains("bus") || n.Contains("truck"))
            {
                if (obj.GetComponent<Collider>() == null)
                {
                    BoxCollider box = obj.AddComponent<BoxCollider>();
                    box.isTrigger = false;
                }
            }
        }

        Debug.Log("Vehicle colliders added.");
    }
}
