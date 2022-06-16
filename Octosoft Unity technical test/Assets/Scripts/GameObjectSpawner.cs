using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Mirror;

public class GameObjectSpawner : NetworkBehaviour
{
    [SerializeField] protected List<GameObject> objectsToSpawn;
    [SerializeField] protected Vector3 spawnArea;

    private NetworkManagerCTW room;
    private NetworkManagerCTW Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerCTW;
        }
    }

    [Server]
    public void CmdSpawnRandomObject()
    {
        int randomIndex = Random.Range(0, objectsToSpawn.Count);

        Vector3 randomPosition = GenerateRandomPositionInArea();

        GameObject randomObjectInstance;
        randomObjectInstance = Instantiate(objectsToSpawn[randomIndex], randomPosition, objectsToSpawn[randomIndex].transform.rotation);

        Room.HandleSpawn(randomObjectInstance);
    }

    [Server]
    public void CmdSpawnObjectByIndex(int index)
    {
        Vector3 randomPosition = GenerateRandomPositionInArea();

        GameObject objectInstance;
        objectInstance = Instantiate(objectsToSpawn[index], randomPosition, objectsToSpawn[index].transform.rotation);

        Room.HandleSpawn(objectInstance);
    }

    private Vector3 GenerateRandomPositionInArea()
    {
        return transform.position + new Vector3(Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                                                Random.Range(-spawnArea.y / 2, spawnArea.y / 2),
                                                Random.Range(-spawnArea.z / 2, spawnArea.z / 2));
    }

    public int[] GenerateRandomIndexes(int count)
    {
        int[] indexes = new int[count];

        for (int i = 0; i < count; i++)
        {
            indexes[i] = Random.Range(0, objectsToSpawn.Count);
        }

        return indexes;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gameObject.transform.position, spawnArea);
    }
}
