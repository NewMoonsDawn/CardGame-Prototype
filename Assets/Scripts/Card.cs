using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{

    [ExecuteInEditMode]
    public class Card : ScriptableObject
    {
        public string cardName;
        public List<ElementType> cardType;
        public Sprite cardSprite;
        public string description;
        public int cost;
       
    }


    public enum ElementType
    {
        Fire,
        Earth,
        Water,
        Dark,
        Light,
        Air,
    }

    public enum AttackPattern
    {
        Single,
        MultiTarget,
        Cross,
        Column,
        Row,
        TwoByTwo,
        FourByFour
    }

    public enum PriorityTarget
    {
        Close,
        Far,
        LeastCurrentHealth,
        MostCurrentHealth,
        MostMaxHealth,
        MostDamage
    }
    public enum SpellType
    {
        Buff,
        Debuff
    }
    public enum AttributeTarget
    {
        health,
        damage,
        range,
        attackPattern,
        damageType,
        cardType,
        priorityTarget
    }
}
