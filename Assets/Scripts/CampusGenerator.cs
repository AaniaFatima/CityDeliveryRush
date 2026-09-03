using UnityEngine;

public class CampusGenerator : MonoBehaviour
{
    [Header("Road Prefabs")]
    public GameObject roadStraight;
    public GameObject roadIntersection;
    public GameObject roadCorner;
    public GameObject roadTile;
    public GameObject sidewalk;

    [Header("Campus Buildings")]
    public GameObject[] academicBuildings;
    public GameObject stadium;
    public GameObject coffeeShop;
    public GameObject bookShop;
    public GameObject fastFood;
    public GameObject residentialBuilding;

    [Header("Nature")]
    public GameObject[] trees;
    public GameObject[] bushes;
    public GameObject grassTile;

    [Header("Props")]
    public GameObject bench;
    public GameObject streetLight;
    public GameObject trafficCone;
    public GameObject trafficSignal;
    public GameObject busStop;

    [Header("Vehicles")]
    public GameObject playerScooter;
    public GameObject[] trafficCars;

    [Header("Characters")]
    public GameObject playerCharacter; // Aj
    public GameObject[] pedestrians;   // Bryce, James, Megan, Remy

    [Header("Generated Parent")]
    public Transform generatedParent;

    [Header("Settings")]
    public float tileSize = 10f;

    private void Start()
    {
        GenerateCampus();
    }

    [ContextMenu("Generate Campus")]
    public void GenerateCampus()
    {
        if (generatedParent == null)
        {
            GameObject parent = new GameObject("Generated Campus");
            generatedParent = parent.transform;
        }

        GenerateGround();
        GenerateRoads();
        GenerateBuildings();
        GenerateNature();
        GenerateProps();
        GenerateVehicles();
        GeneratePlayer();
        GeneratePedestrians();
        GenerateMissionPoints();
        SetupLighting();
        SetupCamera();
    }

    private void GenerateGround()
    {
        if (grassTile == null) return;

        for (int x = -8; x <= 8; x++)
        {
            for (int z = -8; z <= 8; z++)
            {
                Vector3 pos = new Vector3(x * tileSize, 0, z * tileSize);
                Spawn(grassTile, pos, Quaternion.identity, "Grass");
            }
        }
    }

    private void GenerateRoads()
    {
        // Main horizontal road
        for (int x = -8; x <= 8; x++)
        {
            Spawn(roadStraight, new Vector3(x * tileSize, 0.05f, 0), Quaternion.identity, "Main Road");
        }

        // Vertical road
        for (int z = -8; z <= 8; z++)
        {
            Spawn(roadStraight, new Vector3(0, 0.05f, z * tileSize), Quaternion.Euler(0, 90, 0), "Vertical Road");
        }

        // Outer loop roads
        for (int x = -7; x <= 7; x++)
        {
            Spawn(roadStraight, new Vector3(x * tileSize, 0.05f, 6 * tileSize), Quaternion.identity, "Top Road");
            Spawn(roadStraight, new Vector3(x * tileSize, 0.05f, -6 * tileSize), Quaternion.identity, "Bottom Road");
        }

        for (int z = -6; z <= 6; z++)
        {
            Spawn(roadStraight, new Vector3(7 * tileSize, 0.05f, z * tileSize), Quaternion.Euler(0, 90, 0), "Right Road");
            Spawn(roadStraight, new Vector3(-7 * tileSize, 0.05f, z * tileSize), Quaternion.Euler(0, 90, 0), "Left Road");
        }

        // Intersections
        Spawn(roadIntersection, Vector3.zero + Vector3.up * 0.08f, Quaternion.identity, "Center Intersection");
        Spawn(roadIntersection, new Vector3(0, 0.08f, 6 * tileSize), Quaternion.identity, "Top Intersection");
        Spawn(roadIntersection, new Vector3(0, 0.08f, -6 * tileSize), Quaternion.identity, "Bottom Intersection");
    }

    private void GenerateBuildings()
    {
        Vector3[] positions =
        {
            new Vector3(-50, 0, 45),
            new Vector3(50, 0, 45),
            new Vector3(-55, 0, -35),
            new Vector3(55, 0, -35),
            new Vector3(-30, 0, 75),
            new Vector3(30, 0, 75),
        };

        for (int i = 0; i < positions.Length; i++)
        {
            if (academicBuildings.Length == 0) break;

            GameObject building = academicBuildings[i % academicBuildings.Length];
            Spawn(building, positions[i], Quaternion.Euler(0, Random.Range(0, 4) * 90, 0), "Campus Building");
        }

        Spawn(stadium, new Vector3(80, 0, 70), Quaternion.identity, "Stadium");
        Spawn(coffeeShop, new Vector3(-70, 0, -70), Quaternion.identity, "Coffee Shop");
        Spawn(bookShop, new Vector3(70, 0, -70), Quaternion.identity, "Book Shop");
        Spawn(fastFood, new Vector3(-35, 0, -75), Quaternion.identity, "Fast Food");
        Spawn(residentialBuilding, new Vector3(35, 0, -75), Quaternion.identity, "Dormitory");
    }

    private void GenerateNature()
    {
        if (trees.Length > 0)
        {
            for (int i = 0; i < 50; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-85, 85), 0, Random.Range(-85, 85));

                if (Mathf.Abs(pos.x) < 12 || Mathf.Abs(pos.z) < 12) continue;

                GameObject tree = trees[Random.Range(0, trees.Length)];
                Spawn(tree, pos, Quaternion.Euler(0, Random.Range(0, 360), 0), "Tree");
            }
        }

        if (bushes.Length > 0)
        {
            for (int i = 0; i < 25; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-75, 75), 0, Random.Range(-75, 75));
                GameObject bush = bushes[Random.Range(0, bushes.Length)];
                Spawn(bush, pos, Quaternion.identity, "Bush");
            }
        }
    }

    private void GenerateProps()
    {
        Vector3[] benchPositions =
        {
            new Vector3(-20, 0, 20),
            new Vector3(20, 0, 20),
            new Vector3(-20, 0, -20),
            new Vector3(20, 0, -20),
            new Vector3(-40, 0, 10),
            new Vector3(40, 0, -10)
        };

        foreach (Vector3 pos in benchPositions)
        {
            Spawn(bench, pos, Quaternion.Euler(0, 90, 0), "Bench");
        }

        for (int i = -7; i <= 7; i += 2)
        {
            Spawn(streetLight, new Vector3(i * tileSize, 0, 7), Quaternion.identity, "Street Light");
            Spawn(streetLight, new Vector3(i * tileSize, 0, -7), Quaternion.Euler(0, 180, 0), "Street Light");
        }

        Spawn(busStop, new Vector3(-15, 0, 10), Quaternion.identity, "Bus Stop");
        Spawn(trafficSignal, new Vector3(8, 0, 8), Quaternion.identity, "Traffic Signal");
    }

    private void GenerateVehicles()
    {
        if (trafficCars.Length == 0) return;

        Vector3[] carPositions =
        {
            new Vector3(-40, 0.2f, 0),
            new Vector3(40, 0.2f, 0),
            new Vector3(0, 0.2f, 40),
            new Vector3(0, 0.2f, -40),
            new Vector3(-65, 0.2f, -55),
            new Vector3(65, 0.2f, -55)
        };

        for (int i = 0; i < carPositions.Length; i++)
        {
            GameObject car = trafficCars[i % trafficCars.Length];
            Spawn(car, carPositions[i], Quaternion.Euler(0, i % 2 == 0 ? 90 : -90, 0), "Traffic Car");
        }
    }

    private void GeneratePlayer()
    {
        Vector3 spawnPos = new Vector3(0, 0.5f, -85);

        GameObject scooter = Spawn(playerScooter, spawnPos, Quaternion.identity, "Player Scooter");

        if (playerCharacter != null)
        {
            GameObject player = Spawn(playerCharacter, spawnPos + new Vector3(0, 1f, -0.2f), Quaternion.identity, "Aj Delivery Guy");

            if (scooter != null)
            {
                player.transform.SetParent(scooter.transform);
            }
        }

        GameObject spawnMarker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        spawnMarker.name = "Player Spawn Point";
        spawnMarker.transform.position = spawnPos;
        spawnMarker.transform.localScale = new Vector3(2, 0.1f, 2);
        spawnMarker.transform.SetParent(generatedParent);
    }

    private void GeneratePedestrians()
    {
        if (pedestrians.Length == 0) return;

        for (int i = 0; i < 10; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-60, 60), 0.2f, Random.Range(-60, 60));

            if (Mathf.Abs(pos.x) < 8 || Mathf.Abs(pos.z) < 8) continue;

            GameObject pedestrian = pedestrians[Random.Range(0, pedestrians.Length)];
            GameObject obj = Spawn(pedestrian, pos, Quaternion.Euler(0, Random.Range(0, 360), 0), "Pedestrian");

            obj.transform.localScale = Vector3.one * 0.8f;
        }
    }

    private void GenerateMissionPoints()
    {
        Vector3[] pickupPositions =
        {
            new Vector3(-70, 0.5f, -65),
            new Vector3(70, 0.5f, -65),
            new Vector3(-45, 0.5f, 40)
        };

        Vector3[] deliveryPositions =
        {
            new Vector3(-50, 0.5f, 35),
            new Vector3(50, 0.5f, 35),
            new Vector3(80, 0.5f, 60),
            new Vector3(35, 0.5f, -65)
        };

        foreach (Vector3 pos in pickupPositions)
        {
            CreateMarker(pos, Color.yellow, "Package Pickup Point");
        }

        foreach (Vector3 pos in deliveryPositions)
        {
            CreateMarker(pos, Color.green, "Delivery Zone");
        }
    }

    private void CreateMarker(Vector3 pos, Color color, string name)
    {
        GameObject marker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        marker.name = name;
        marker.transform.position = pos;
        marker.transform.localScale = new Vector3(2.5f, 0.15f, 2.5f);

        Renderer rend = marker.GetComponent<Renderer>();
        rend.material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        rend.material.color = color;

        marker.transform.SetParent(generatedParent);
    }

    private void SetupLighting()
    {
        RenderSettings.ambientIntensity = 1.2f;

        Light sun = FindObjectOfType<Light>();

        if (sun == null)
        {
            GameObject sunObj = new GameObject("Directional Light");
            sun = sunObj.AddComponent<Light>();
            sun.type = LightType.Directional;
        }

        sun.name = "Campus Sun";
        sun.transform.rotation = Quaternion.Euler(45, -30, 0);
        sun.intensity = 1.3f;
    }

    private void SetupCamera()
    {
        Camera cam = Camera.main;

        if (cam == null)
        {
            GameObject camObj = new GameObject("Main Camera");
            cam = camObj.AddComponent<Camera>();
            cam.tag = "MainCamera";
        }

        cam.transform.position = new Vector3(0, 85, -110);
        cam.transform.rotation = Quaternion.Euler(55, 0, 0);
    }

    private GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, string objectName)
    {
        if (prefab == null) return null;

        GameObject obj = Instantiate(prefab, position, rotation);
        obj.name = objectName;
        obj.transform.SetParent(generatedParent);

        return obj;
    }
}