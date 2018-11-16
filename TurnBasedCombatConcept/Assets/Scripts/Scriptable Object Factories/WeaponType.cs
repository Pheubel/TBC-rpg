using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponTypeData", menuName = "Proto/Weapon type", order = 2)]
public class WeaponType : ScriptableObject
{
    public string Name { get { return _name; } }
    [SerializeField] string _name;
}
