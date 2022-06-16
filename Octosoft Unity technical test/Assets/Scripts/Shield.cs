using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : ClickableObject
{
    public int timesToClick = 3;
    private int timesClicked = 0;

    public override void OnClick()
    {
        timesClicked++;

        if (timesClicked >= timesToClick)
        {
            ScoreManager.instance.IncreasePlayerScore(increaseScoreBy);
            Destroy(gameObject); //pool system
        }
    }
}
