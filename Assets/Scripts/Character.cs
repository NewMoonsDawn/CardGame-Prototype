using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ScriptableObject
{
    public string characterName;
    public int maxHealth;
    [HideInInspector]
    public int currentHealth;
    public int block;
    public List<Passive> passives = new List<Passive>();
    public void TakeDamage(int damage)
    {
        if (block > 0)
        {
            int previousBlock = block;
            block = Mathf.Clamp(block,0,block-damage);
            damage = Mathf.Clamp(damage,0,damage-previousBlock);
            if (damage > 0)
            {
                currentHealth -= damage;
            }
        }
        else
        {
            currentHealth -= damage;
        }
    }
    public void GainBlock(int block)
    {
        this.block += block;

    }
}
