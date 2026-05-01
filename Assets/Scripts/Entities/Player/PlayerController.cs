using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : Controller
{
    [SerializeField]
    private Player player;

    private PlayerManager playerManager;

    public void BattleSetup()
    {
        playerManager = GameManager.Instance.playerManager;
        player = playerManager.player;
        sprite.sprite = player.playerSprite;
        healthbar.fillAmount = player.maxHealth / player.currentHealth;
        healthbarText.text = $"{player.currentHealth}/{player.maxHealth}";
        UpdateHealthBar(player);
    }
    public override void ReceiveDamage(Character attacker, int damage)
    {
        player.TakeDamage(damage);
        UpdateHealthBar(player);
        UpdateBlockBar(player);
        if(player.currentHealth<=0)
        {
            HandleDeath();
        }
    }
    public override void GainBlock(int block)
    {
        player.block += block;
        UpdateBlockBar(player);
    }

    public override void HandleDeath()
    {
        base.HandleDeath();
        SceneManager.LoadScene("GameOver");
        SceneManager.UnloadSceneAsync("CombatScene");
    }
}
