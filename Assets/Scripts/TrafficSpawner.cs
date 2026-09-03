using UnityEngine;
using System.Collections;

public class TrafficSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public Transform[] waypoints;

    public int maxCars = 4;
    public float spawnDelay = 4f;

    private int currentCars = 0;

    void Start()
    {
        StartCoroutine(SpawnCars());
    }

    IEnumerator SpawnCars()
    {
        while (currentCars < maxCars)
        {
            GameObject newCar = Instantiate(
                carPrefab,
                waypoints[0].position,
                waypoints[0].rotation
            );

            TrafficCarMover mover = newCar.GetComponent<TrafficCarMover>();

            if (mover == null)
            {
                mover = newCar.AddComponent<TrafficCarMover>();
            }

            mover.waypoints = waypoints;

            currentCars++;

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
