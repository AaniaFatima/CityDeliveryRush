using UnityEngine;

public class AutoAddRoadColliders : MonoBehaviour
{
    void Start()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name.ToLower().Contains("road"))
            {
                if (obj.GetComponent<Collider>() == null)
                {
                    BoxCollider box = obj.AddComponent<BoxCollider>();
                    box.isTrigger = false;
                }
            }
        }

        Debug.Log("Road colliders added.");
    }
}