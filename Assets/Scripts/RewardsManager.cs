using CardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewardsManager : MonoBehaviour
{
    [SerializeField]
    private List<CardDisplay> cardRewardObjects = new List<CardDisplay>();
    [SerializeField]
    private List<Ritual> possibleCards;
    private DeckManager deckManager;
    public int cardRewardAmount = 3;

    private void Awake()
    {
        if (possibleCards == null)
        {
            possibleCards = Resources.LoadAll("CardRewards", typeof(Ritual)).Cast<Ritual>().ToList(); //TODO: Fix this
        }
        cardRewardObjects = FindObjectsByType<CardDisplay>(FindObjectsSortMode.None).ToList();

        List<Ritual> chosenCards = ChooseCardInX(cardRewardAmount,reward: true);
        for(int i=0;i<chosenCards.Count;i++)
        {
            cardRewardObjects[i].cardData = chosenCards[i];
            cardRewardObjects[i].UpdateCardDisplay();
        } // TODO: Instantiate the cards instead to accomodate for more than 3 choices
    }

    public List<Ritual> ChooseCardInX(int amount = 3, bool reward = false, ElementType elementType = ElementType.None) //TODO: Rarity
    {
        List<Ritual> cardsChoices = new List<Ritual>();
        List<Ritual> cardsHolder = possibleCards;
        int indexer = 0;
        if(reward)
        {
            for (int i=0;i<amount;i++)
            {
                indexer = Random.Range(0, cardsHolder.Count);
                cardsChoices.Add(possibleCards[indexer]);
                cardsHolder.RemoveAt(indexer);
            }
            //TODO: Rarity
        }
        return cardsChoices;
    }

    public void AddCard(Card card)
    {
        if (GameManager.Instance != null)
        {
            deckManager = GameManager.Instance.deckManager;
        }
        if (deckManager != null)
        {
            deckManager.allcards.Add(card);
        }
        StartCoroutine(LoadCombatScene());
    }
    public void LeaveRewardsSreen()
    {
        StartCoroutine(LoadCombatScene());
    }
    public IEnumerator LoadCombatScene()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("CombatScene");
    }
}
