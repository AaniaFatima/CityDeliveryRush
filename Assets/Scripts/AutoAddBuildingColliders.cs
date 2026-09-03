using UnityEngine;

public class AutoAddBuildingColliders : MonoBehaviour
{
    void Start()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            string n = obj.name.ToLower();

            if (n.Contains("building") || n.Contains("shop") || n.Contains("stadium") || n.Contains("factory"))
            {
                if (obj.GetComponent<Collider>() == null)
                {
                    BoxCollider box = obj.AddComponent<BoxCollider>();
                    box.isTrigger = false;
                }
            }
        }

        Debug.Log("Building colliders added.");
    }
}
