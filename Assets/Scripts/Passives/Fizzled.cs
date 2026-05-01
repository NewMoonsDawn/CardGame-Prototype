using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Fizzled",menuName ="Passive/Debuff/Fizzled")]
public class Fizzled : Debuff
{
    public override int DamageMultiplier(int damage)
    {
        Mathf.RoundToInt(damage * 0.75f);
        return damage;
    }
}
