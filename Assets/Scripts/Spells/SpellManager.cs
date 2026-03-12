using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [SerializeField]
    private List<Spell> startingOwnedSpells;
    public List<Spell> ownedSpells;

    private void Awake()
    {
        foreach (Spell spell in startingOwnedSpells)
        {
            ownedSpells.Add(Instantiate(spell));
        }
    }
}
