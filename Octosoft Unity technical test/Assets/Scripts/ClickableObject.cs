using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ClickableObject : NetworkBehaviour
{
    public float despawnTime = 5f;
    private float timer;

    public int reduceScoreBy = 1;
    public int increaseScoreBy = 5;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= despawnTime && hasAuthority)
        {
            ScoreManager.instance.DecreasePlayerScore(reduceScoreBy);
            Destroy(gameObject); //Pool system
        }
    }

    public virtual void OnClick()
    {
        if (hasAuthority)
        {
            Debug.Log("Grande Mostro");
            ScoreManager.instance.IncreasePlayerScore(increaseScoreBy);
            Destroy(gameObject); //Pool system
        }
        else
        {
            Debug.Log("Chhh que toca?");
        }
    }

    public void OnDestroy()
    {
        //ObjectsSpawner.instance.DecreaseObjectsOnScreenCount();
    }
}
