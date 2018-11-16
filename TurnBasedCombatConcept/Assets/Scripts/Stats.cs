using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseStats
{
    #region Base Stat properties
    
    public int MaxHealth => _maxHealth;
    [SerializeField] protected int _maxHealth;

    public int MaxMana => _maxMana;
    [SerializeField] protected int _maxMana;

    public float HitRate => _hitRate;
    [SerializeField] protected float _hitRate;

    public float EvasionRate => _evasionRate;
    [SerializeField] protected float _evasionRate;

    public int Damage => _damage;
    [SerializeField] protected int _damage;

    public int Defense => _defense;
    [SerializeField] protected int _defense;

    public int SpellPower => _spellPower;
    [SerializeField] protected int _spellPower;

    public int Resistance => _resistance;
    [SerializeField] protected int _resistance;

    public int Speed => _speed;
    [SerializeField] protected int _speed;

    public int Luck => _luck;
    [SerializeField] protected int _luck;
    
    #endregion


    public BaseStats(BaseStats baseObj)
    {
        this._maxHealth = baseObj._maxHealth;
        this._maxMana = baseObj._maxMana;
        this._hitRate = baseObj._hitRate;
        this._evasionRate = baseObj._evasionRate;
        this._damage = baseObj._damage;
        this._defense = baseObj._defense;
        this._spellPower = baseObj._spellPower;
        this._resistance = baseObj._resistance;
        this._speed = baseObj._speed;
        this._luck = baseObj._luck;
    }
}


public sealed class Stats : BaseStats
{
    #region Dynamic Stats 

    public int CurrentHealth => _currentHealth;
    [SerializeField] private int _currentHealth;

    public int CurrentMana => _currentMana;
    [SerializeField] private int _currentMana;

    public float CurrentHitRate => _currentHitRate;
    [SerializeField] private float _currentHitRate;

    public float CurrentEvasionRate => _currentEvasionRate;
    [SerializeField] private float _currentEvasionRate;

    public int CurrentDamage => _currentDamage;
    [SerializeField] private int _currentDamage;

    public int CurrentDefense => _currentDefense;
    [SerializeField] private int _currentDefense;

    public int CurrentSpellPower => _currentSpellPower;
    [SerializeField] private int _currentSpellPower;

    public int CurrentResistance => _currentResistance;
    [SerializeField] private int _currentResistance;

    public int CurrentSpeed => _speed;
    [SerializeField] private int _currentSpeed;

    public int CurrentLuck => _luck;
    [SerializeField] private int _currentLuck;

    #endregion

    public Stats(BaseStats baseObj) : base(baseObj)
    {}
}
