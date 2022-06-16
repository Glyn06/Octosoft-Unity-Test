using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    private void Awake()
    {
        MultiplayerSpawner.AddSpawnPoint(transform);
    }

    private void OnDestroy()
    {
        MultiplayerSpawner.RemoveSpawnPoint(transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 1f);
    }
}
