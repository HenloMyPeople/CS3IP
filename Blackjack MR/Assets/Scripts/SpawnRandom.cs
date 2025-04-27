using System.Collections;
using UnityEngine;

public class SpawnRandom : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject[] spawnPoints;
    public float minSpawnTime = 20f;
    public float maxSpawnTime = 40f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
            if (prefabs.Length == 0 || spawnPoints.Length == 0)
            {
                Debug.LogWarning("Prefabs or Spawn Points arrays are empty");
                continue;
            }

            int prefabIndex = Random.Range(0, prefabs.Length);
            GameObject prefabToSpawn = prefabs[prefabIndex];
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            GameObject spawnPoint = spawnPoints[spawnIndex];
            Instantiate(prefabToSpawn, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }
}
