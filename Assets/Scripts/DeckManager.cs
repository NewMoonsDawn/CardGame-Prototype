using CardGame;
using System.Collections.Generic;
using UnityEngine;


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
    //private bool startBattleRun = true;
    private void Start()
    {
       // Card[] cards = Resources.LoadAll<Card>("Cards");

       // allcards.AddRange(cards);
    }
    /*private void Update()
    //{
      //  if (startBattleRun)
        //{
          //  BattleSetup();  
        //}
    }*/
    public void BattleSetup()
    {
     if (drawPileManager == null)
       {
                drawPileManager = FindObjectOfType<DrawPileManager>();
            }
            if (handManager == null)
            {
                handManager = FindObjectOfType<HandManager>();
            }
            if (spellbookManager == null)
            {
                spellbookManager = FindObjectOfType<SpellbookManager>();
            }
        if (spellManager == null)
    {
      spellManager = FindObjectOfType<SpellManager>();
    }
        handManager.BattleSetup(maxHandSize);
        drawPileManager.MakeDrawPile(allcards);
        //startBattleRun = false;
        spellbookManager.BattleSetup(spellManager);
    }
}