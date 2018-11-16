using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Proto/Weapon", order = 3)]
public class Weapon : ScriptableObject {

    public WeaponType WeaponType { get { return _weaponType; } }
    [SerializeField]private WeaponType _weaponType;

    public int WeaponSpeed { get { return _weaponSpeed; } }
    [SerializeField] private int _weaponSpeed;
}
