using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : ClickableObject
{
    public int objectIndex;
    public int quantity;

    public override void OnClick()
    {
        base.OnClick();

        //ObjectsSpawner.instance.SetNextObject(quantity, objectIndex);
    }
}
