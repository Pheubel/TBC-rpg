using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GradeModifierData", menuName = "Proto/Grades", order = 1)]
public class GradeModifier : ScriptableObject
{
    public ModificationModes ModificationMode { get { return _modificationMode; } }
    [SerializeField] private ModificationModes _modificationMode;

    public float ModifierValue { get { return _modifierValue; } }
    [SerializeField] float _modifierValue;



    public float ApplyModifier(int baseValue)
    {
        switch (_modificationMode)
        {
            case ModificationModes.Additive:
                return baseValue + _modifierValue;

            case ModificationModes.Multiplitive:
                return baseValue * _modifierValue;

            default:
                throw new System.Exception("Unhandled modification mode in GradeModifier.ApplyModifier");
        }
    }

    public float ApplyModifier(float baseValue)
    {
        switch (_modificationMode)
        {
            case ModificationModes.Additive:
                return baseValue + _modifierValue;

            case ModificationModes.Multiplitive:
                return baseValue * _modifierValue;

            default:
                throw new System.Exception("Unhandled modification mode in GradeModifier.ApplyModifier");
        }
    }
}

public enum ModificationModes
{
    Additive,
    Multiplitive
}
