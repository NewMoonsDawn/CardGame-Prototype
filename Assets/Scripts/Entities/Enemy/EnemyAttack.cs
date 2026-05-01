using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Enemies/Attack")]
public class EnemyAttack : ScriptableObject
{
    public string attackName;
    public Intention intention;
    public int damage;
    public int multiAttackNumber;
    public int block;
}
