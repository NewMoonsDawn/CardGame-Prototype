using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellbookManager : MonoBehaviour
{
    public List<Spell> spells;
    private SpellManager spellManager;
    public GameObject spellObject;
    private int spellIndex;
    private PlayerManager playerManager;

    private void Awake()
    {
        playerManager = GameManager.Instance.playerManager;
    }
    public void BattleSetup(SpellManager spellManager)
    {
        spellObject = FindObjectOfType<SpellDisplay>().gameObject;
        foreach (var spell in spellManager.ownedSpells)
        {
            spells.Add(Instantiate(spell));
        }
        this.spellManager = spellManager;
        SetCurrentSpell(0);
    }
    //Buttons Logic
    public void NextSpell()
    {
        spellIndex++;
        if (spellIndex >= spells.Count)
        {
            spellIndex = 0;
        }
        SetCurrentSpell(spellIndex);
    }
    public void PreviousSpell()
    {
        spellIndex--;
        if (spellIndex < 0)
        {
            spellIndex = spells.Count - 1;
        }
        SetCurrentSpell(spellIndex);
    }
    public void CastSpell()
    {
        if (playerManager.playerTurn)
        {
            //TODO: Handle Buffs/Debuffs
            Spell spell = GetCurrentSpell().spellData;
            if (spell.damage > 0)
            {
                GameManager.Instance.enemyManager.enemyController.ReceiveDamage(playerManager.player, spell.damage);
            }
            if (spell.block > 0)
            {
                playerManager.playerController.GainBlock(spell.block);
            }
            if (spell.draw != 0)
            {
                playerManager.player.queuedCardDraw += spell.draw;
            }
            spell = spellManager.ownedSpells.Find(x => x.spellName == spell.spellName);
            ResetSpell(spell);
            playerManager.EndPlayerTurn();
        }
    }
    public void SetCurrentSpell(int spellIndex)
    {
        spellObject.GetComponent<SpellDisplay>().spellData = spells[spellIndex];
        spellObject.GetComponent<SpellDisplay>().UpdateSpellDisplay();
    }
    public SpellDisplay GetCurrentSpell()
    {
        return spellObject.GetComponent<SpellDisplay>();
    }
    public void ResetSpell(Spell spell)
    {
        spells[spellIndex] = Instantiate(spellManager.ownedSpells.Find(x => x.spellName == spell.spellName));
        SetCurrentSpell(spellIndex);
    }
}
