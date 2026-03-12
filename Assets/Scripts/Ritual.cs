using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame;

[CreateAssetMenu(fileName = "New Ritual Card", menuName = "Card/Ritual")]
public class Ritual : Card
{
    public int damage;
    public int drawAmount;
    public int block;
    public bool typeLocked;
    public List<ElementType> applicableTypes;
}
