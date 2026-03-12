using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellbookManager : MonoBehaviour
{
    public List<Spell> spells;
    private SpellManager spellManager;
    public GameObject spellObject;
    private int spellIndex;
    public void BattleSetup(SpellManager spellManager)
    {
        foreach (var spell in spellManager.ownedSpells)
        {
            spells.Add(Instantiate(spell));
        }
        setCurrentSpell(0);
    }
    //Buttons Logic
    public void NextSpell()
    {
        Debug.Log("Next");
        spellIndex++;
        if (spellIndex >= spells.Count)
        {
            spellIndex = 0;
        }
        setCurrentSpell(spellIndex);
    }
    public void PreviousSpell()
    {
        Debug.Log("Previous");
        spellIndex--;
        if (spellIndex < 0)
        {
            spellIndex = spells.Count - 1;
        }
        setCurrentSpell(spellIndex);
    }
    public void CastSpell()
    {
        //TODO: END OF TURN LOGIC
    }
    public void setCurrentSpell(int spellIndex)
    {
        spellObject.GetComponent<SpellDisplay>().spellData = spells[spellIndex];
        spellObject.GetComponent<SpellDisplay>().updateSpellDisplay();
    }
    public SpellDisplay getCurrentSpell()
    {
        return spellObject.GetComponent<SpellDisplay>();
    }
}
