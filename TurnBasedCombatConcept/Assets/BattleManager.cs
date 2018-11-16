using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField,ReadOnly] private BattleService _battle;

    private void Start()
    {
        _battle = GameServiceCollection.GetService<BattleService>();

        _battle.StartBattle(this);
    }
}

