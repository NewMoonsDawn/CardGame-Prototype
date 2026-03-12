using CardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Card", menuName = "Card/Character")]
public class Character : Card
{ 

    public int range;
    public List<ElementType> damageType;
    public GameObject prefab;
    public int health;
    public int damageMin;
    public int damageMax;
}
