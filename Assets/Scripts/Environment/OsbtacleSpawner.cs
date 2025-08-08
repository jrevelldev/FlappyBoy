using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 2f;
    public float spawnYMin = -1f;
    public float spawnYMax = 2f;

    private float timer = 0f;

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing)
            return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        Vector3 spawnPos = transform.position;
        spawnPos.y = Random.Range(spawnYMin, spawnYMax);
        Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
    }
}
