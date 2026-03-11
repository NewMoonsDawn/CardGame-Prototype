using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CardGame;
using System;
using JetBrains.Annotations;

[ExecuteInEditMode]
public class CardDisplay : MonoBehaviour
{
    //All Card Type Elements
    public Card cardData;
    public Image cardImage;
    public TMP_Text cardName;
    public TMP_Text costText;
    public Image[] typeImages;
    public Image cardBackground;
    
    public static Sprite lightSprite;
    public static Sprite darkSprite;
    public static Sprite airSprite;
    public static Sprite fireSprite;
    public static Sprite waterSprite;
    public static Sprite earthSprite;

    private Dictionary<string, Color> typeColors = new Dictionary<string, Color>
    {
        {"Fire",Color.red },
        {"Earth",new Color (0.8f,0.52f,0.24f) },
        {"Water",Color.blue },
        {"Dark", Color.magenta },
        {"Light", Color.yellow },
        {"Air", Color.cyan },
    };
    private Dictionary<string, Sprite> typeSprites = new Dictionary<string, Sprite>();

    public GameObject characterElements;
    public GameObject spellElements;
    public GameObject characterCardLabel;
    public GameObject spellCardLabel;
    public TMP_Text descriptionText;

    //Character Card Elements
    public TMP_Text healthText;
    public TMP_Text damageText;

    //Spell Card Elements

    public GameObject[] spellTypeLabels;
    public GameObject[] attributeTargetSymbols;
    public float attributeSymbolSpacing = 10f;
    public TMP_Text attributeChangeAmountText;


    void Start()
    {
        for (int i = 0; i < typeImages.Length; i++)
        {
            typeImages[i].gameObject.SetActive(false);
        }
        InitializeSprites();
        UpdateCardDisplay();
    }

    public void UpdateCardDisplay()
    {
        Color typeColor = new Color();
        Sprite typeSprite;
        typeColors.TryGetValue(cardData.cardType[0].ToString(), out typeColor);
        cardBackground.color = new Color(typeColor.r, typeColor.g, typeColor.b, 0.5f);


        cardName.text = cardData.name;
        costText.text = cardData.cost.ToString();
        cardImage.sprite = cardData.cardSprite;
        descriptionText.text = cardData.description;

        for (int i = 0; i < cardData.cardType.Count; i++)
        {
           // typeColors.TryGetValue(cardData.cardType[i].ToString(), out typeColor);
            typeSprites.TryGetValue(cardData.cardType[i].ToString(), out typeSprite);
            typeImages[i].color = Color.white; 
            typeImages[i].sprite = typeSprite;
            typeImages[i].gameObject.SetActive(true);
        }

        //Specific Card Changes
        if (cardData is Character character)
        {
            UpdateDisplayCharacterCard(character);

        }
       // if(cardData is Spell spell)
       // {
       //     UpdateDisplaySpellCard(spell);
       // }
    }
    public void UpdateDisplayCharacterCard(Character character)
    {
        spellElements.SetActive(false);
        characterElements.SetActive(true);
        characterCardLabel.SetActive(true);
        healthText.text = character.health.ToString();
        damageText.text = $"{character.damageMin} - {character.damageMax}";
    }
    //public void UpdateDisplaySpellCard(Spell spell)
   // {
       // spellElements.SetActive(true);
       // characterElements.SetActive(false);
       // spellCardLabel.SetActive(true);

        //Set correct spell type label
       // foreach(GameObject label in spellTypeLabels)
       // {
        //    label.SetActive(false);
        //}
       // spellTypeLabels[(int)spell.spellType].SetActive(true); 
        //foreach(GameObject label in attributeTargetSymbols)
       // {
       //     label.SetActive(false);
       // }
       // for(int i =0;i<spell.attributeTarget.Count;i++)
        //{
          //  GameObject currentSymbol = attributeTargetSymbols[(int)spell.attributeTarget[i]];
           // currentSymbol.SetActive(true);
           // float newYPosition = i * attributeSymbolSpacing;
           // currentSymbol.transform.localPosition = new Vector3(0, newYPosition, 0);
        //}

       // attributeChangeAmountText.text = string.Join(", ", spell.attributeChangeAmount);
   // }
    public void UpdateHealth(int health)
    {
        if (cardData is Character character)
        {
            healthText.text = health.ToString();
            if (health < character.health)
            {
                healthText.color = Color.red;
                return;
            }
            if (health > character.health)
            {
                healthText.color = Color.green;
                return;
            }
        }
    }

    public void UpdateDamage(int damageMin, int damageMax)
    {
        if (cardData is Character character)
        {
            string damageMinColor = "";
            string damageMaxColor = "";
            if (damageMin < character.damageMax)
            {
                damageMinColor = "#FF0000"; // Set Color to Red
            }
            else if (damageMin > character.damageMin)
            {
                damageMinColor = "#00FF00"; // Set Color to Green
            }
            if (damageMax < character.damageMax)
            {
                damageMaxColor = "#FF0000"; // Set Color to Red
            }
            else if (damageMax > character.damageMax)
            {
                damageMaxColor = "#00FF00"; // Set Color to Green
            }

            damageText.text = $"<color={damageMinColor}>{damageMin}</color>-<color={damageMaxColor}>{damageMax}</color>";
        }
    }

    public void UpdateCost(int cost)
    {
        if (cost < cardData.cost)
        {
            costText.color = Color.green;
        }
        if(cost > cardData.cost)
        {
            costText.color = Color.red;
        }
        costText.text = cost.ToString(); 
    }

    public void InitializeSprites()
    {
        lightSprite = (Sprite)Resources.Load("TypeSprites/LightSprite");
        earthSprite = (Sprite)Resources.Load("TypeSprites/EarthSprite");
        fireSprite = (Sprite)Resources.Load("TypeSprites/FireSprite");
        waterSprite = (Sprite)Resources.Load("TypeSprites/WaterSprite");
        airSprite = (Sprite)Resources.Load("TypeSprites/AirSprite");
        darkSprite = (Sprite)Resources.Load("TypeSprites/DarkSprite");
        typeSprites.Add("Fire", fireSprite);
        typeSprites.Add("Earth",earthSprite);
        typeSprites.Add("Water",waterSprite);
        typeSprites.Add("Dark", darkSprite);
        typeSprites.Add("Light", lightSprite);
        typeSprites.Add("Air",airSprite);
    }
}

