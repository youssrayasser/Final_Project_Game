using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject roadPrefab;       // Road segment prefab
    public GameObject landPrefab;       // Land (grass/terrain) prefab
    public GameObject[] obstaclePrefabs; // Array to hold different obstacle prefabs (other cars, trucks, etc.)
    public float roadLength = 30f;      // Length of each road segment
    public float spawnZPosition = 100f; // Position where the road and land spawn
    public float destroyZPosition = -50f; // Position where the road and land should be destroyed
    public float obstacleSpawnInterval = 2f; // Interval between spawning obstacles
    public float obstacleSpeed = 5f;   // Speed at which the obstacles move

    private Transform playerTransform;  // Reference to the car (player)
    private float lastZPosition;        // Position of the last road segment and land
    private float timeSinceLastObstacle = 0f; // Timer to track the spawning interval

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the car
        lastZPosition = 0f; // Start with the initial position
        SpawnRoadAndLand(); // Spawn the first road and land
    }

    void Update()
    {
        // Continuously check if the player has moved enough to spawn new road and land
        if (playerTransform.position.z > lastZPosition - roadLength)
        {
            SpawnRoadAndLand();
        }

        // Handle obstacle spawning
        timeSinceLastObstacle += Time.deltaTime;
        if (timeSinceLastObstacle >= obstacleSpawnInterval)
        {
            SpawnObstacle();
            timeSinceLastObstacle = 0f; // Reset timer
        }
    }

    void SpawnRoadAndLand()
    {
        // Spawn a new road segment and corresponding land in front of the player
        Instantiate(roadPrefab, new Vector3(0, 0, lastZPosition + roadLength), Quaternion.identity);
        Instantiate(landPrefab, new Vector3(0, -1f, lastZPosition + roadLength), Quaternion.identity); // Slightly below the road

        // Update the lastZPosition to the new road segment's position
        lastZPosition += roadLength;

        // Destroy old road and land segments that are far behind
        DestroyOldRoadAndLand();
    }

    void DestroyOldRoadAndLand()
    {
        GameObject[] roads = GameObject.FindGameObjectsWithTag("Road");
        GameObject[] lands = GameObject.FindGameObjectsWithTag("Land");

        foreach (GameObject road in roads)
        {
            if (road.transform.position.z < playerTransform.position.z + destroyZPosition)
            {
                Destroy(road);
            }
        }

        foreach (GameObject land in lands)
        {
            if (land.transform.position.z < playerTransform.position.z + destroyZPosition)
            {
                Destroy(land);
            }
        }
    }

    void SpawnObstacle()
    {
        // Randomly choose an obstacle prefab from the array
        GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        // Choose a random spawn position along the X-axis within a specific range
        float spawnXPosition = Random.Range(-3f, 3f); // Adjust the range based on your road width

        // Spawn the obstacle on the road in front of the player
        GameObject obstacle = Instantiate(obstaclePrefab, new Vector3(spawnXPosition, 0, playerTransform.position.z + roadLength), Quaternion.identity);

        // Add movement to the obstacle to make it move down the road towards the player
        Rigidbody obstacleRb = obstacle.AddComponent<Rigidbody>();
        obstacleRb.isKinematic = true; // Make it not affected by physics forces but manually move it

        // Make the obstacle move towards the player
        obstacle.transform.Translate(Vector3.back * obstacleSpeed * Time.deltaTime);
    }
}
