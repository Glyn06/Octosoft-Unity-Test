using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using kcp2k;

public class NetworkManagerCTW : NetworkManager
{
    [Scene] [SerializeField] private string menuScene;
    [SerializeField] private string gameScene;

    [SerializeField] private RoomPlayer roomPlayerPrefab;
    [SerializeField] private GamePlayer gamePlayerPrefab;

    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;
    public static event Action<NetworkConnection> OnServerReadied;

    public List<RoomPlayer> roomPlayers { get; } = new List<RoomPlayer>();
    public List<GamePlayer> gamePlayers { get; } = new List<GamePlayer>();


    private bool isPlaying = false;
    [SerializeField] private GameObject multiPlayerSpawn;

    [SerializeField] private GameObject timerPrefab;

    [SerializeField] private GameObject spawnerPrefab;

    [SerializeField] private GameObject scoreManagerPrefab;


    public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

    public override void OnStartClient()
    {
        GameObject[] spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

        foreach (var prefab in spawnPrefabs)
        {
            NetworkClient.RegisterPrefab(prefab);
        }
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();

        OnClientConnected?.Invoke();
    }

    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();

        OnClientDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        if (numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }

        if (SceneManager.GetActiveScene().path != menuScene)
        {
            conn.Disconnect();
            return;
        }
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        if (conn.identity != null)
        {
            RoomPlayer player = conn.identity.GetComponent<RoomPlayer>();

            roomPlayers.Remove(player);

            NotifyPlayersOfReadyState();
        }

        base.OnServerDisconnect(conn);
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            bool isLeader = roomPlayers.Count == 0;

            RoomPlayer roomPlayerInstance = Instantiate(roomPlayerPrefab);
            roomPlayerInstance.IsLeader = isLeader;

            NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
        }
    }

    public override void OnStopServer()
    {
        roomPlayers.Clear();
    }

    public void NotifyPlayersOfReadyState()
    {
        foreach (var player in roomPlayers)
        {
            player.HandleReadyToStart(IsReadyToStart());
        }
    }

    private bool IsReadyToStart()
    {
        if (numPlayers < maxConnections) { return false; }

        foreach (var player in roomPlayers)
        {
            if (!player.IsReady) { return false; }
        }

        return true;
    }

    public void StartGame()
    {
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            if (!IsReadyToStart()) { return; }

            isPlaying = true;

            ServerChangeScene(gameScene);
        }
    }

    internal void HandleSpawn(GameObject objectInstance)
    {
        for (int i = gamePlayers.Count - 1; i >= 0; i--)
        {
            NetworkConnectionToClient conn = gamePlayers[i].connectionToClient;

            if (conn != null)
                NetworkServer.Spawn(objectInstance, conn);
            else
                NetworkServer.Spawn(objectInstance);
        }
    }

    internal void HandleIncreaseScore(int score)
    {
        for (int i = gamePlayers.Count - 1; i >= 0; i--)
        {
            if (gamePlayers[i].hasAuthority)
            {
                gamePlayers[i].CmdIncreaseScore(score);
                return;
            }
        }
    }

    internal void HandleDecreaseScore(int score)
    {
        for (int i = gamePlayers.Count - 1; i >= 0; i--)
        {
            if (gamePlayers[i].hasAuthority)
            {
                gamePlayers[i].CmdDecreaseScore(score);
                return;
            }
        }
    }

    public override void ServerChangeScene(string newSceneName)
    {
        if (isPlaying)
        {
            for (int i = roomPlayers.Count - 1; i >= 0; i--)
            {
                NetworkConnectionToClient conn = roomPlayers[i].connectionToClient;

                GamePlayer gamePlayerInstance = Instantiate(gamePlayerPrefab);
                gamePlayerInstance.SetDisplayName(roomPlayers[i].DisplayName);
                gamePlayerInstance.SetIsLeader(roomPlayers[i].IsLeader);

                GameObject gOSpawnerPrefab = Instantiate(spawnerPrefab, gamePlayerInstance.spawnerTransform);
                NetworkServer.Spawn(gOSpawnerPrefab, conn);

                NetworkServer.Destroy(conn.identity.gameObject);

                NetworkServer.ReplacePlayerForConnection(conn, gamePlayerInstance.gameObject, true);
            }
        }

        base.ServerChangeScene(newSceneName);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        if (sceneName == gameScene)
        {
            GameObject multiPlayerSpawnerInstance = Instantiate(multiPlayerSpawn);
            NetworkServer.Spawn(multiPlayerSpawnerInstance);

            GameObject timerInstance = Instantiate(timerPrefab);
            NetworkServer.Spawn(timerInstance);

            GameObject scoreManagerInstance = Instantiate(scoreManagerPrefab);
            NetworkServer.Spawn(scoreManagerInstance);
        }
    }

    public override void OnServerReady(NetworkConnectionToClient conn)
    {
        base.OnServerReady(conn);

        OnServerReadied?.Invoke(conn);
    }
}
