using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame;


public class DeckManager : MonoBehaviour
{
    public List<Card> allcards = new List<Card>();
    public int startingHand;
    public int maxHandSize;
    public int currentHandSize;
    private HandManager handManager;
    private DrawPileManager drawPileManager;
    private SpellbookManager spellbookManager;
    private SpellManager spellManager;
    private bool startBattleRun = true;
    private void Start()
    {
       // Card[] cards = Resources.LoadAll<Card>("Cards");

       // allcards.AddRange(cards);
    }
    void Awake()
    {
        if (drawPileManager == null)
        {
            drawPileManager = FindObjectOfType<DrawPileManager>();
        }
        if (handManager == null)
        {
            handManager = FindObjectOfType<HandManager>();
        }
        if(spellbookManager == null)
        {
            spellbookManager = FindObjectOfType<SpellbookManager>();
        }
        if(spellManager == null)
        {
            spellManager = FindObjectOfType<SpellManager>();
        }
    }
    private void Update()
    {
        if (startBattleRun)
        {
            BattleSetup();  
        }
    }
    public void BattleSetup()
    {
        handManager.BattleSetup(maxHandSize);
        //drawPileManager.MakeDrawPile(allcards);
        drawPileManager.BattleSetup(startingHand, maxHandSize);
        startBattleRun = false;
        spellbookManager.BattleSetup(spellManager);
    }
}