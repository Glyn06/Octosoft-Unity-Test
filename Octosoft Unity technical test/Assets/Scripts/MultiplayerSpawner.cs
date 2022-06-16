using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class MultiplayerSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private static List<Transform> spawnPoints = new List<Transform>();

    private int index = 0;

    public static void AddSpawnPoint(Transform transform)
    {
        spawnPoints.Add(transform);
    }

    public static void RemoveSpawnPoint(Transform transform)
    {
        spawnPoints.Remove(transform);
    }

    public override void OnStartServer()
    {
        NetworkManagerCTW.OnServerReadied += SpawnPlayer;
    }

    [ServerCallback]
    private void OnDestroy()
    {
        NetworkManagerCTW.OnServerReadied -= SpawnPlayer;
    }

    [Server]
    private void SpawnPlayer(NetworkConnection conn)
    {
        Transform spawnPoint = spawnPoints[index];

        GameObject playerInstance = Instantiate(playerPrefab, spawnPoints[index].position, spawnPoints[index].rotation);
        NetworkServer.Spawn(playerInstance, conn);

        index++;
    }
}
