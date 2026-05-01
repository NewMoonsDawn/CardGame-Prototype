using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Passive : ScriptableObject
{
    public int amount;
    public bool countdown;
    public bool temporary;
    public bool ammountCorrelates; //if the amount of passive applied correlated to the strength of the passive (ex. -1 damage vs -3 damage)
    public PassiveType type;
    public Sprite sprite;
    public Character owner;
    public TriggerType trigger;


    public virtual void OnCast()
    {

    }
    public virtual void OnEndTurn()
    {
        if (countdown)
        {
            amount--;
        }
        if (temporary)
        {
            amount = 0;
        }
        if (amount == 0)
        {
            if (owner is Player)
            {
                // GameManager.Instance.playerManager.playerController.RemovePassive(this);
            }
            else if (owner is Enemy)
            {
                //GameManager.Instance.enemyManager.enemyController.RemovePassive(this);
            }
        }
    }
    public virtual int DamageMultiplier(int damage)
    {
        return 1;
    }
    public virtual int DamageTakenMultiplier(int damage)
    {
        return 1;
    }
}
public enum PassiveType
{
    None,
    Buff,
    Debuff
}

public enum TriggerType
{
    EndTurn,
    OnCast,
    DamageDealtMultiplier,
    DamageTakenMultiplier,
}
