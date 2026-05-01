using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Controller : MonoBehaviour
{
    [SerializeField]
    protected Image sprite;
    [SerializeField]
    protected Image healthbar;
    [SerializeField]
    protected TMP_Text healthbarText;
    [SerializeField]
    protected TMP_Text blockText;
    [SerializeField]
    protected Image blockBar;
    [SerializeField]
    protected GameObject blockContainer;
    [SerializeField]
    private float blockBarThreshold = 50f; //How much block does it take to fully fill out the bar?
    public void UpdateHealthBar(Character character)
    {
        healthbar.fillAmount = (float)character.currentHealth / (float)character.maxHealth;
        healthbarText.text = $"{character.currentHealth}/{character.maxHealth}";
    }
    public void UpdateBlockBar(Character character)
    {
        if (character.block > 0)
        {
            blockContainer.SetActive(true);
            blockBar.fillAmount = (float)character.block / blockBarThreshold;
            blockText.text = character.block.ToString();
        }
        else
        {
            blockContainer.SetActive(false);
        }
    }
    public abstract void ReceiveDamage(Character attacker, int damage);

    public abstract void GainBlock(int block);

    public virtual void HandleDeath()
    {

    }
}
