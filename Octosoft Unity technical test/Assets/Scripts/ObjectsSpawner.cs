using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectsSpawner : MonoBehaviour
{
    public GameObjectSpawner spawner;

    private float maximunSpawnTime;
    private float minimunSpawnTime;

    private int minObjectsOnScreen;
    private int maxObjectsOnScreen;

    private float timer;
    private float timeNextSpawn;
    private int objectsOnScreen = 0;

    private int[] nextObjects;
    private int currentSpawnIndex;

    // Start is called before the first frame update
    void Start()
    {
        minimunSpawnTime = PersistanceData.instance.GetDifficulty().minimunSpawnTime;
        maximunSpawnTime = PersistanceData.instance.GetDifficulty().maximunSpawnTime;

        minObjectsOnScreen = PersistanceData.instance.GetDifficulty().minObjectsOnScreen;
        maxObjectsOnScreen = PersistanceData.instance.GetDifficulty().maxObjectsOnScreen;

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
            {
                spawner.CmdSpawnObjectByIndex(nextObjects[currentSpawnIndex]);
                IncreaseObjectsOnScreenCount();
            }

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

            spawner.CmdSpawnObjectByIndex(nextObjects[currentSpawnIndex]);
            currentSpawnIndex++;

            IncreaseObjectsOnScreenCount();
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
