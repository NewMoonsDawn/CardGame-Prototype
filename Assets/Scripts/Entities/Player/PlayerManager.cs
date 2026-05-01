using CardGame;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Player basePlayer;
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public PlayerController playerController;
    private DeckManager deckManager;
    private DrawPileManager drawPileManager;
    private HandManager handManager;
    private DiscardManager discardManager;

    public bool restartFight;
    public int startingHP;
    public bool inCombat;
    public bool playerTurn;
    private void Awake()
    {
        player = Instantiate(basePlayer);
        deckManager = GameManager.Instance.deckManager;
    }

    public void BattleSetup()
        {
        Debug.Log("Player");
        playerController = FindObjectOfType<PlayerController>();
        drawPileManager = FindObjectOfType<DrawPileManager>();
        handManager = FindObjectOfType<HandManager>();
        discardManager = FindObjectOfType<DiscardManager>();
        inCombat = true;

        if(player.currentHealth==0)
        {
            player.currentHealth = player.maxHealth;
        }
        if (restartFight)
        {
            player.currentHealth = startingHP;
        }
        startingHP = player.currentHealth;
        playerController.BattleSetup();
        player.block = 0;
        player.queuedCardDraw = 0;
        player.passives.Clear();
        deckManager.BattleSetup();
        StartPlayerTurn();
    }
    public void StartPlayerTurn()
    {
        player.block = 0;
        playerController.UpdateBlockBar(player);
        StartCoroutine(DrawCards());
    }
    public void EndPlayerTurn()
    {
        playerTurn = false;
        //TODO: END OF TURN LOGIC
        if (inCombat)
        {
            foreach (var card in handManager.cardsInHand)
            {
                discardManager.AddToDiscard(card.GetComponent<CardDisplay>().cardData);
                handManager.UpdateHandVisuals();
                Destroy(card);
            }
            handManager.cardsInHand.Clear();
            StartCoroutine(StartEnemyTurn());
        }
    }

    IEnumerator StartEnemyTurn()
    {
        //TODO: Animations?
        yield return new WaitForSeconds(1);
        GameManager.Instance.enemyManager.StartEnemyTurn();
    }
    IEnumerator DrawCards()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0;i<drawPileManager.startingHand + player.queuedCardDraw;i++)
        {
            yield return new WaitForSeconds(0.05f);
            drawPileManager.DrawCard(handManager);
        }
        player.queuedCardDraw = 0;
        playerTurn = true;
    }
    IEnumerator DiscardCards()
    {
        foreach (var card in handManager.cardsInHand)
        {
            yield return new WaitForSeconds(0.05f);
            discardManager.AddToDiscard(card.GetComponent<CardDisplay>().cardData);
            handManager.UpdateHandVisuals();
            Destroy(card);
        }
        handManager.cardsInHand.Clear();
    }
}
