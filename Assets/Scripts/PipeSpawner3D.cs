using UnityEngine;

public class PipeSpawner3D : MonoBehaviour
{
    // Prefab of the pipe that will be spawned
    public GameObject pipePrefab;

    // How often a pipe spawns (in seconds)
    public float spawnRate = 1.5f;

    // Range for random Y position (vertical height)
    public float minY = -2f;
    public float maxY = 2f;

    // Keeps track of time between spawns
    private float timer;

    void Update()
    {
        // Increase timer based on frame time
        timer += Time.deltaTime;

        // When enough time has passed, spawn a pipe
        if (timer >= spawnRate)
        {
            // Pick a random height within the range
            float randomY = Random.Range(minY, maxY);

            // Create spawn position using current X and random Y
            Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0f);

            // Spawn the pipe at that position
            Instantiate(pipePrefab, spawnPosition, Quaternion.identity);

            // Reset timer after spawning
            timer = 0f;
        }
    }
}