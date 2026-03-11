using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellDisplay : MonoBehaviour
{
    //All spell elements
    public Spell spellData;
    public Image spellBackground;
    public TMP_Text spellName;
    public TMP_Text spellDescription;
    public List<Image> spellAttachmentIcons = new List<Image>();
    public Image spellArt;

    private Dictionary<string, Color> typeColors = new Dictionary<string, Color>
    {
        {"Fire",Color.red},
        {"Physical",Color.grey},
        {"Water",Color.blue},
    };

    private void Start()
    {
        updateSpellDisplay();
    }
    public void updateSpellDisplay()
    {
        Color typeColor = new Color();
        spellName.text = spellData.name;
        string descriptionText = spellData.description.Replace("*X*", spellData.damage.ToString());
        descriptionText = descriptionText.Replace("*Y*", spellData.block.ToString());
        spellDescription.text = descriptionText;
        typeColors.TryGetValue(spellData.Type.ToString(), out typeColor);
        spellBackground.color = typeColor;
    }
}
