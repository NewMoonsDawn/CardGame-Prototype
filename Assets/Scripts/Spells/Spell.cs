using CardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class Spell : ScriptableObject
{
    public SpellType Type;
    public string spellName;
    public string description;
    public Sprite spellArt;
    public List<Card> spellAttachements = new List<Card>();
    public int damage;
    public int block;
}
