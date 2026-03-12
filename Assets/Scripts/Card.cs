using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardGame
{

    [ExecuteInEditMode]
    public class Card : ScriptableObject
    {
        public string cardName;
        public ElementType cardType;
        public Sprite cardSprite;
        public string description;
        //public int cost;
       
    }


    public enum ElementType
    {
        Fire,
        Earth,
        Water,
        Dark,
        Light,
        Physical,
    }
    public enum SpellType
    {
        Fire,
        Water,
        Physical,
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
