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

    private int[] nextObjects;
    private int currentSpawnIndex;

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
        nextObjects = spawner.GenerateRandomIndexes(maxObjectsOnScreen);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        CheckIndexOverload();

        if (timer >= timeNextSpawn)
        {
            if (objectsOnScreen < minObjectsOnScreen)
                SpawnUntilMinObjects();
            else if (objectsOnScreen < maxObjectsOnScreen)
                spawner.SpawnObjectByIndex(nextObjects[currentSpawnIndex]);

            timer = 0;
            currentSpawnIndex++;
        }
    }

    public void CheckIndexOverload()
    {
        if (currentSpawnIndex >= nextObjects.Length)
        {
            currentSpawnIndex = 0;
            nextObjects = spawner.GenerateRandomIndexes(maxObjectsOnScreen);
        }
    }

    public void SpawnUntilMinObjects()
    {
        for (int i = objectsOnScreen; i < minObjectsOnScreen; i++)
        {
            CheckIndexOverload();

            spawner.SpawnObjectByIndex(nextObjects[currentSpawnIndex]);
            currentSpawnIndex++;
        }
    }

    public void SetNextObject(int count, int index)
    {
        for (int i = 0; i < count; i++)
        {
            nextObjects[i] = index;
        }
        currentSpawnIndex = 0;
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
