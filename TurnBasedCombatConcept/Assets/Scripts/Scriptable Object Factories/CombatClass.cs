using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "ClassData", menuName = "Proto/classes", order = 3)]
public class CombatClass : ScriptableObject
{
    public string ClassName { get { return _className; } }
    [SerializeField] private string _className;

    public ReadOnlyCollection<WeaponTypeGradePair> WeaponTypeGradePairs { get { return _weaponTypeGradePairs.AsReadOnly(); } }
    [SerializeField]private List<WeaponTypeGradePair> _weaponTypeGradePairs;

    public int SpeedModifier { get {return _speedModifier; } }
    [SerializeField] private int _speedModifier;

    

}

[System.Serializable]
public sealed class WeaponTypeGradePair
{
    public WeaponType WeaponType;
    public GradeModifier Modifier;
}


public static class WeaponTypeGradePairExtension
{
    public static GradeModifier GetModifier(this ICollection<WeaponTypeGradePair> pairs, WeaponType weaponType)
    {
        foreach (var pair in pairs)
        {
            if (pair.WeaponType.GetInstanceID() == weaponType.GetInstanceID())
                return pair.Modifier;
        }

        throw new System.Exception("no matching weapon type found");
    }
}


