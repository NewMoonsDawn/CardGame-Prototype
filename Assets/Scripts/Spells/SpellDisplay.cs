using CardGame;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

public class SpellDisplay : MonoBehaviour
{
    //All spell elements
    public Spell spellData;
    public SpriteRenderer spellBackground;
    public TMP_Text spellName;
    public TMP_Text spellDescription;
    public List<Image> spellAttachmentIcons = new List<Image>();
    public int maxAttachments = 4;
    public Image spellArt;

    private Dictionary<string, Color> typeColors = new Dictionary<string, Color>
    {
        {"Fire",Color.red},
        {"Physical",Color.grey},
        {"Water",Color.blue},
    };

    public void UpdateSpellDisplay()
    {
        Color typeColor = new Color();
        spellName.text = spellData.spellName;
        string descriptionText = spellData.description.Replace("*X*", spellData.damage.ToString());
        descriptionText = descriptionText.Replace("*Y*", spellData.block.ToString());
        descriptionText = descriptionText.Replace("*Z*", spellData.draw.ToString());
        spellDescription.text = descriptionText;
        spellData.description = descriptionText;
        typeColors.TryGetValue(spellData.Type.ToString(), out typeColor);
        //spellBackground.color = typeColor;
        spellArt.sprite = spellData.spellArt;
        foreach (Image image in spellAttachmentIcons)
        {
            image.sprite = null;
            image.color = new Color(1f, 1f, 1f, 0f);
        }
        for (int i = 0; i < spellData.spellAttachements.Count; i++)
        {
            spellAttachmentIcons[i].sprite = spellData.spellAttachements[i].cardSprite;
            if (spellData.spellAttachements[i] !=null)
            {
                spellAttachmentIcons[i].color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                spellAttachmentIcons[i].color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }

    public void ApplyRitual(Ritual ritual)
    {
        if(ritual.damage != 0)
        {
            UpdateDamage(ritual.damage);
        }
        if(ritual.block != 0)
        {
            UpdateBlock(ritual.block);
        }
        if (ritual.drawAmount != 0)
        {
           UpdateDraw(ritual.drawAmount);
        }
        spellData.spellAttachements.Add(ritual);
        UpdateSpellDisplay();
    }

    public void UpdateDamage(int damage)
    {
        string newDescription;
        damage += spellData.damage;
        if (spellData.damage != 0)
        {
            string color = "";
            if (damage < spellData.damage)
            {
                color = "#FF0000"; // Set Color to Red
            }
            else if (damage > spellData.damage)
            {
                color = "#00FF00"; // Set Color to Green
            }
            newDescription = Regex.Replace(spellData.description, "(<color=#[0-z]+)?[0-9]+(<\\/color>)? (Damage)", $"<color={color}>{damage}</color> Damage");
        }
        else
        {
            newDescription = spellData.description + $"\nInflict {damage} Damage";
        }
        spellData.damage = damage;
        spellData.description = newDescription;
        spellDescription.text = newDescription;
    }
    public void UpdateBlock(int block)
    {
        string newDescription;
        block += spellData.block;
        if (spellData.block != 0)
        {
            string color = "";
            if (block < spellData.block)
            {
                color = "#FF0000"; // Set Color to Red
            }
            else if (block > spellData.block)
            {
                color = "#00FF00"; // Set Color to Green
            }

            newDescription = Regex.Replace(spellData.description, "(<color=#[0-z]+)?[0-9]+ (<\\/color>)?(Block)", $"<color={color}>{block} </color>Block");
        }
        else
        {
            newDescription = spellData.description + $"\nGain {block} Block";
        }
        spellData.block = block;
        spellData.description = newDescription;
        spellDescription.text = newDescription;
    }
    public void UpdateDraw(int draw)
    {
        string newDescription;
        int newDraw = spellData.draw + draw;
        if (spellData.draw != 0)
        {
            string drawColor = "";
            if (newDraw < spellData.draw)
            {
                drawColor = "#FF0000"; // Set Color to Red
            }
            else if (newDraw > spellData.draw)
            {
                drawColor = "#00FF00"; // Set Color to Green
            }

            newDescription = Regex.Replace(spellData.description, "(<color=#[0-z]+)?(Draw) (<\\/color>)?[0-9]+", $"Draw <color={drawColor}>{newDraw}</color>");
        }
        else
        {
            newDescription = spellData.description + $"\nDraw {newDraw} cards";
        }
        spellData.draw = newDraw;
        spellData.description = newDescription;
        spellDescription.text = newDescription;
    }

    public bool CanApplyRitual(Ritual ritual)
    {
        if(ritual.damage < 0 )
        {
            if (spellData.damage + ritual.damage < 0)
            {
                return false;
            }
            else
                return true;
        }
        if (ritual.block < 0)
        {
            if (spellData.block + ritual.block < 0)
            {
                return false;
            }
            else
                return true;
        }
        return true;
    }
}
