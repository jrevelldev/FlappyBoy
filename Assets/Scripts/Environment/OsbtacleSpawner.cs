using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
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
        if (obstaclePrefabs.Length == 0) return;

        int index = Random.Range(0, obstaclePrefabs.Length);
        GameObject selectedPrefab = obstaclePrefabs[index];

        Vector3 spawnPos = transform.position;
        spawnPos.y = Random.Range(spawnYMin, spawnYMax);
        Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
    }

}
