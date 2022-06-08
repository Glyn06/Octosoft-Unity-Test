using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public float despawnTime = 5f;
    private float timer;

    public int reduceScoreBy = 1;
    public int increaseScoreBy = 5;

    private void Awake()
    {
        RaycastClick raycastClick = FindObjectOfType<RaycastClick>();
        raycastClick.onHitDelegate += OnClick;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= despawnTime)
        {
            ScoreManager.instance.IncreaseScore(reduceScoreBy);
            Destroy(gameObject); //Pool system
        }
    }

    public virtual void OnClick()
    {
        ScoreManager.instance.IncreaseScore(increaseScoreBy);
        Destroy(gameObject); //Pool system
    }
}
