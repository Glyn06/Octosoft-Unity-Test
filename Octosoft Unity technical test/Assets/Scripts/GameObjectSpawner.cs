using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameObjectSpawner : MonoBehaviour
{
    [SerializeField] protected List<GameObject> objectsToSpawn;
    [SerializeField] protected Vector3 spawnArea;

    public void SpawnRandomObject()
    {
        int randomIndex = Random.Range(0, objectsToSpawn.Count);

        Vector3 randomPosition = GenerateRandomPositionInArea();

        Instantiate(objectsToSpawn[randomIndex], randomPosition, objectsToSpawn[randomIndex].transform.rotation);
    }
    private Vector3 GenerateRandomPositionInArea()
    {
        return transform.position + new Vector3(Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                                                Random.Range(-spawnArea.y / 2, spawnArea.y / 2),
                                                Random.Range(-spawnArea.z / 2, spawnArea.z / 2));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gameObject.transform.position, spawnArea);
    }
}
