using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellbookManager : MonoBehaviour
{
    public List<GameObject> spellObjects;
    public List<Spell> spells;
    private SpellManager spellManager;

    private void Start()
    {
        foreach (GameObject spellObject in spellObjects)
        {
            spellObject.SetActive(false);
        }
    }
    public void BattleSetup(SpellManager spellManager)
    {
        spells = spellManager.ownedSpells;
        for (int i = 0; i < spells.Count; i++)
        {
            {
                spellObjects[i].SetActive(true);
                spellObjects[i].GetComponent<SpellDisplay>().spellData = spells[i];
                spellObjects[i].GetComponent<SpellDisplay>().updateSpellDisplay();
            }
        }
    }
}
