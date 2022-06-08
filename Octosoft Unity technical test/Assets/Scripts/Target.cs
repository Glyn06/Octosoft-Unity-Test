using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : ClickableObject
{
    public override void OnClick()
    {
        base.OnClick();

        Debug.Log("3 coins bro trust me");
        //Next three items are coins
    }
}
