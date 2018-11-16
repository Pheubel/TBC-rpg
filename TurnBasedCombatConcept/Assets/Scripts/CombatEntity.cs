using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class CombatEntity : MonoBehaviour {

    [SerializeField]private Animator _animator;

    // TODO
    public float CalculateSpeed() { return Random.value; }

    private void Update()
    {
        
    }
}

[System.Serializable]
struct NormalizedCombatEntity
{
    public CombatEntity Fighter;
    public float NormalizedSpeed;
    public float Progress;

    public NormalizedCombatEntity(CombatEntity fighter, float normalizedSpeed = 0)
    {
        Fighter = fighter;
        NormalizedSpeed = normalizedSpeed;
        Progress = 0;
    }
}