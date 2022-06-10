using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public void SetDifficulty(Difficulty newDifficulty)
    {
        PersistanceData.instance.SetDifficulty(newDifficulty);
    }
}
