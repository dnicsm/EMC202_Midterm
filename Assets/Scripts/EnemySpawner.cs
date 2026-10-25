using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform[] spawnPoints;

    public float spawnInterval = 2f;

    private float spawnTimer;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints == null || spawnPoints.Length == 0 || enemyPrefab == null) return;

        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject newEnemyObj = Instantiate(enemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);

        EnemyBase enemyComponent = newEnemyObj.GetComponent<EnemyBase>();
        if (enemyComponent != null)
        {
            System.Array colorValues = System.Enum.GetValues(typeof(GameColor));
            GameColor randomColor = (GameColor)colorValues.GetValue(Random.Range(0, colorValues.Length));
            
            enemyComponent.enemyColor = randomColor;
        }
    }
}