using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(fileName = "Mon_Dat_", menuName = "Proto/Monster", order = 1)]
public class MonsterFactory : ScriptableObject
{
    public string MonsterName => _monsterName;
    [SerializeField] private string _monsterName;

    public AnimationClip MonsterAnimations => _monsterAnimations;
    [SerializeField] private AnimationClip _monsterAnimations;

    public BaseStats BaseStats => _baseStats;
    [SerializeField] private BaseStats _baseStats;
}
