using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class BattleServiceInjector : BaseServiceInjector<BattleService>
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Service.UpdateSpeedMap();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Service.AdvanceToNextEnity();
        }
    }
}





