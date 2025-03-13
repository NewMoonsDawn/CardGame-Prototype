using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame;
using TMPro;

public class DrawPileManager : MonoBehaviour
{
    public List<Card> drawPile = new List<Card>();

    private int currentIndex = 0;
    public int startingHand = 6;
    public int maxHandSize;
    public int currentHandSize;
    private HandManager handManager;
    private DiscardManager discardManager;

    public TextMeshProUGUI drawPileCounter;
    void Start()
    {
        handManager = FindObjectOfType<HandManager>();
    }
    void Update()
    {
        if (handManager != null)
        {
            currentHandSize = handManager.cardsInHand.Count;
        }
    }

    public void MakeDrawPile(List<Card> cardsToAdd)
    {
        drawPile.AddRange(cardsToAdd);
        Utility.Shuffle(drawPile);
        UpdateDrawPileCount();
    }
    public void BattleSetup(int numberOfCardsToDraw, int setMaxHandSize)
    {
        maxHandSize = setMaxHandSize;
        for (int i = 0; i < numberOfCardsToDraw; i++)
        {
            DrawCard(handManager);
        }
    }
    public void DrawCard(HandManager handManager)
    {
        if (drawPile.Count == 0)
        {
            RefillDeckFromDiscard();
        }
        Card nextCard = drawPile[currentIndex];
        if (handManager.AddCardToHand(nextCard))
        {
            drawPile.RemoveAt(currentIndex);
            UpdateDrawPileCount();
            if (drawPile.Count > 0)
            {
                currentIndex = (currentIndex + 1) % drawPile.Count;
            }
        }
        else
        {
            return; //Replace with in game max hand size error message
        }
    }
    private void UpdateDrawPileCount()
    {
        drawPileCounter.text = drawPile.Count.ToString();

    }
    private void RefillDeckFromDiscard()
    {
        if(discardManager == null)
        {
            discardManager = FindObjectOfType<DiscardManager>();
        }
        if(discardManager != null && discardManager.discardCardsCount >0)
        {
            drawPile = discardManager.PullAllFromDiscard();
            Utility.Shuffle(drawPile);
            currentIndex = 0;
        }
    }
}
