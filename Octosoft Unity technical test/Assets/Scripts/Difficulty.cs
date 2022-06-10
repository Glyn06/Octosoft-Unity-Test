using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDifficulty", menuName = "Difficulty")]
public class Difficulty : ScriptableObject
{
    public float minimunSpawnTime;
    public float maximunSpawnTime;

    public int minObjectsOnScreen;
    public int maxObjectsOnScreen;
}
