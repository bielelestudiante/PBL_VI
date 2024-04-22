using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Referencia al prefab del enemigo
    public Transform[] spawnPoints; // Puntos de spawn donde aparecerán los enemigos
    public float spawnInterval = 2f; // Intervalo de tiempo entre spawns
    public int maxEnemies = 5; // Número máximo de enemigos activos
    private int currentEnemies = 0; // Contador de enemigos activos

    void Start()
    {
        // Iniciar el método SpawnEnemy repetidamente con un intervalo
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Comprobar si hemos alcanzado el número máximo de enemigos
        if (currentEnemies >= maxEnemies) return;

        // Elegir un punto de spawn al azar
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instanciar el enemigo en el punto de spawn
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Incrementar el contador de enemigos
        currentEnemies++;
    }

    // Método para decrementar el contador de enemigos cuando un enemigo es destruido
    public void EnemyDestroyed()
    {
        currentEnemies--;
    }
}
