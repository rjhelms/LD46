using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punk : AIActor
{

    protected override void Start()
    {
        GameManager.instance.RegisterPunk();
        base.Start();
    }

    protected override void OnDisable()
    {
        GameManager.instance.RemovePunk();
        base.OnDisable();
    }
}
