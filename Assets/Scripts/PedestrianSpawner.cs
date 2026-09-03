using UnityEngine;
using System.Collections;

public class PedestrianSpawner : MonoBehaviour
{
    public GameObject pedestrianPrefab;
    public Transform[] waypoints;

    public int maxPedestrians = 5;
    public float spawnDelay = 3f;

    private int currentPedestrians = 0;

    void Start()
    {
        StartCoroutine(SpawnPedestrians());
    }

    IEnumerator SpawnPedestrians()
    {
        while (currentPedestrians < maxPedestrians)
        {
            GameObject newPedestrian = Instantiate(
                pedestrianPrefab,
                waypoints[0].position,
                waypoints[0].rotation
            );

            PedestrianMover mover = newPedestrian.GetComponent<PedestrianMover>();

            if (mover == null)
            {
                mover = newPedestrian.AddComponent<PedestrianMover>();
            }

            mover.waypoints = waypoints;

            currentPedestrians++;

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
