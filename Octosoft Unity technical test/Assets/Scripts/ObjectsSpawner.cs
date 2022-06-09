using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectsSpawner : MonoBehaviour
{
    public GameObjectSpawner spawner;

    public float minimunSpawnTime;
    public float maximunSpawnTime;

    public int minObjectsOnScreen;
    public int maxObjectsOnScreen;

    private float timer;
    private float timeNextSpawn;
    private int objectsOnScreen = 0;

    #region Singleton
    public static ObjectsSpawner instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        timeNextSpawn = Random.Range(minimunSpawnTime, maximunSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeNextSpawn)
        {
            if (objectsOnScreen < minObjectsOnScreen)
                SpawnUntilMinObjects();
            else if (objectsOnScreen < maxObjectsOnScreen)
                spawner.SpawnRandomObject();

            timer = 0;
        }
    }

    public void SpawnUntilMinObjects()
    {
        for (int i = objectsOnScreen; i < minObjectsOnScreen; i++)
        {
            spawner.SpawnRandomObject();
        }

    }

    public void DecreaseObjectsOnScreenCount()
    {
        objectsOnScreen--;
    }
    public void IncreaseObjectsOnScreenCount()
    {
        objectsOnScreen++;
    }
}
