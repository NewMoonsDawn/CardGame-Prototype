using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/Enemy")]
public class Enemy : Character
{
    public List<EnemyAttack> baseAttacks;
    private List<EnemyAttack> attacks = new List<EnemyAttack>();
    public int attackIndex = 0;
    public Sprite sprite;
    protected Intention intention;
    [HideInInspector]
    public EnemyAttack currentAttack;

    public void SetAttacks(List<EnemyAttack> attacks)
    {
        foreach (EnemyAttack attack in attacks)
        {
            this.attacks.Add(Instantiate(attack));
        }
    }
    public void ExecuteAttack()
    { 
        if (currentAttack != null)
        {
            PlayerController playerController = GameManager.Instance.playerManager.playerController;
            switch (currentAttack.intention)
            {
                case Intention.Attack:
                    playerController.ReceiveDamage(this, CalculateDamage());
                    return;
                case Intention.MultiAttack:
                    for (int i = 0; i < currentAttack.multiAttackNumber; i++)
                    {
                        playerController.ReceiveDamage(this, CalculateDamage());
                    }
                    return;
                case Intention.Block:
                    GainBlock(currentAttack.block);
                    GameManager.Instance.enemyManager.enemyController.UpdateBlockBar(this);
                    return;
                case Intention.AttackBlock:
                    playerController.ReceiveDamage(this, CalculateDamage());
                    GainBlock(currentAttack.block);
                    GameManager.Instance.enemyManager.enemyController.UpdateBlockBar(this);
                    return;
            }
            DecideNextAttack();
        }
        else
        {
            Debug.LogWarning($"Attack with index {attackIndex} from {base.name} enemy is Null");
            Debug.LogWarning(attacks);
        }
    }
    public EnemyAttack DecideNextAttack(int index =-1)
    {
        EnemyAttack nextAttack = null;
        if (index == -1)
        {
            attackIndex++;
            if(attackIndex >= attacks.Count)
            {
                attackIndex = 0;
                nextAttack = attacks[attackIndex];
            }
            else
            {
                nextAttack = attacks[attackIndex];
            }
            currentAttack = nextAttack;
            return nextAttack;
        }
        else
        {
            currentAttack = attacks[index];
            return attacks[index];
        }
    }
    public int CalculateDamage() //Calculate damage after buffs & debuffs
    {
        int totalDamage = currentAttack.damage;
        return totalDamage;
    }
    public void SetIntention(Intention intention)
    {
        this.intention = intention;
        //TODO: Display this in game
    }
}



public enum Intention
{
    Attack,
    MultiAttack,
    Block,
    Debuff,
    Buff,
    Curse,
    AttackBuff,
    AttackDebuff,
    AttackBlock,
}

