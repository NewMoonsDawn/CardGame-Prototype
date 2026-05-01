using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardRewardSelector : MonoBehaviour
{
    private RewardsManager rewardsManager;
    private void Awake()
    {
        rewardsManager = FindObjectOfType<RewardsManager>();
    }
    public void CardSelected()
    {
        rewardsManager.AddCard(this.GetComponentInParent<CardDisplay>().cardData);
    }
}
