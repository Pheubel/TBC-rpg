using System;
using System.Collections.Generic;
using UnityEngine;


/*
 *  TODO:
 *  -----
 *  
 */

/// <summary> The service which hill hold on to data regarding the battle and the order of turns.</summary>
[Serializable]
public class BattleService : IService
{
    /// <summary> A list containing all active party members in the player's party.</summary>
    public IReadOnlyList<CombatEntity> PlayerParty => _playerParty;
    [SerializeField] private List<CombatEntity> _playerParty;

    /// <summary> a list containing all active party members in the enemy party</summary>
    public IReadOnlyList<CombatEntity> EnemyParty => _enemyParty;
    [SerializeField] private List<CombatEntity> _enemyParty;

    /// <summary> A helping instance to keep track of the flow of the battle.</summary>
    [SerializeField, ReadOnly] private BattleTracker _tracker;

    /// <summary> The entity which is currently able to act.</summary>
    public CombatEntity ActiveEntity => _activeEntity;
    [SerializeField, ReadOnly] private CombatEntity _activeEntity;


    /// <summary> Signals the start of the battle by creating the combat entities based on the passed data.</summary>
    /// <param name="battleManagerGameObject"> The object which will be responsible of managing the battle and it's systems.</param>
    public void StartBattle(MonoBehaviour battleManagerGameObject)
    {
        _tracker = new BattleTracker(_playerParty, _enemyParty);
        _activeEntity = _tracker.GetNextEntity();
    }

    /// <summary> Advances the turn to the entity next in the queue.</summary>
    public CombatEntity AdvanceToNextEnity() => _activeEntity = _tracker.GetNextEntity();

    /// <summary> Updates the tracker's speedmap across the active battle entities</summary>
    public void UpdateSpeedMap() => _tracker.RecreatePlan();
}


[Serializable]
public class BattleTracker
{
    /// <summary> A collection of a combat entity who's speed is made relative to all other entities in the battle.</summary>
    [SerializeField] NormalizedCombatEntity[] AllFighters;

    /// <summary> The collection that will store all the entities that are available for a turn based on their order of meeting the quota.</summary>
    [SerializeField] Queue<CombatEntity> _entitiesReady = new Queue<CombatEntity>();

    /// <summary> Constructs the list of normalized combat entities who's speed will be made relative to eachother.</summary>
    /// <param name="playerEntities"> All combat entities on the player's party</param>
    /// <param name="enemyEntities"> All combat entities on the enemy's party</param>
    public BattleTracker(IList<CombatEntity> playerEntities, IList<CombatEntity> enemyEntities)
    {
        int entityCount = playerEntities.Count + enemyEntities.Count;

        // initialize the tracker's fields
        AllFighters = new NormalizedCombatEntity[entityCount];

        // fill in the entities and decide the highest speed meassured
        float highestSpeed = float.MinValue;

        for (int i = 0; i < playerEntities.Count; i++)
        {
            float speed = playerEntities[i].CalculateSpeed();

            if (speed > highestSpeed)
                highestSpeed = speed;

            AllFighters[i] = new NormalizedCombatEntity(playerEntities[i], speed);
        }

        for (int i = 0; i < enemyEntities.Count; i++)
        {
            float speed = enemyEntities[i].CalculateSpeed();

            if (speed > highestSpeed)
                highestSpeed = speed;

            AllFighters[i + playerEntities.Count] = new NormalizedCombatEntity(enemyEntities[i], speed);
        }

        // normalize the speed of all fighters (between 1 and 0)
        for (int i = 0; i < entityCount; i++)
        {
            AllFighters[i].NormalizedSpeed /= highestSpeed;
        }
    }

    /// <summary> Gets the combat entity that is next in line.</summary>
    /// <returns> the combat entity that is next in line</returns>
    public CombatEntity GetNextEntity()
    {
        if (_entitiesReady.Count != 0)
            return _entitiesReady.Dequeue();

        List<NormalizedCombatEntity> entitiesToQueue = new List<NormalizedCombatEntity>();

        for (int i = 0; i < AllFighters.Length; i++)
        {
            AllFighters[i].Progress += AllFighters[i].NormalizedSpeed;

            if (AllFighters[i].Progress >= 1)
            {
                AllFighters[i].Progress -= 1;
                entitiesToQueue.Add(AllFighters[i]);
            }
        }

        entitiesToQueue.Sort((a, b) => b.Progress.CompareTo(a.Progress));

        foreach (var entity in entitiesToQueue)
        {
            _entitiesReady.Enqueue(entity.Fighter);
        }

        return _entitiesReady.Dequeue();
    }

    /// <summary> Recalculates the normalized speed map based on the entity with the highest speed result.</summary>
    public void RecreatePlan()
    {
        float highestSpeed = float.MinValue;

        for (int i = 0; i < AllFighters.Length; i++)
        {
            AllFighters[i].NormalizedSpeed = AllFighters[i].Fighter.CalculateSpeed();

            if (AllFighters[i].NormalizedSpeed > highestSpeed)
                highestSpeed = AllFighters[i].NormalizedSpeed;
        }

        for (int i = 0; i < AllFighters.Length; i++)
        {
            AllFighters[i].NormalizedSpeed /= highestSpeed;
        }
    }
}