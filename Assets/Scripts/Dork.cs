using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dork : AIActor
{
    protected override void Start()
    {
        GameManager.instance.RegisterDork();
        base.Start();
    }

    protected override void OnDisable()
    {
        GameManager.instance.RemoveDork();
        base.OnDisable();
    }
}
