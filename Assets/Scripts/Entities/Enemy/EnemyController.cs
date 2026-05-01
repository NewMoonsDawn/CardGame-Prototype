using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyController : Controller
{
    public Enemy currentEnemy;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private Image intentSprite;
    [SerializeField]
    private TMP_Text intentText;

    public Dictionary<Intention, Sprite> intentionSprites = new Dictionary<Intention, Sprite>();

    public void BattleSetup(Enemy enemy)
    {
        currentEnemy = enemy;
        sprite.sprite = enemy.sprite;
        nameText.text = enemy.characterName;
        currentEnemy.currentHealth = currentEnemy.maxHealth;
        currentEnemy.SetAttacks(currentEnemy.baseAttacks);
        UpdateHealthBar(currentEnemy);
        InitializeSprites();
        UpdateIntentionUI(currentEnemy.DecideNextAttack(0));
    }
    public void EnemyDeath()
    {
        //TODO: End combat
    }
    public override void ReceiveDamage(Character attacker, int damage)
    {
        currentEnemy.TakeDamage(damage);
        UpdateHealthBar(currentEnemy);
        UpdateBlockBar(currentEnemy);
        if(currentEnemy.currentHealth<=0)
        {
            HandleDeath();
        }
    }
    public void UpdateIntentionUI(EnemyAttack attack)
    {
        //intentSprite.sprite TODO: Implement intent sprite dictionary
        Intention intent = attack.intention;
        Debug.Log($"intent: { intent}");
        switch (intent)
        {
            case Intention.Attack:
                intentSprite.sprite = intentionSprites.GetValueOrDefault(intent);
                intentSprite.color = Color.red;
                intentText.text = currentEnemy.CalculateDamage().ToString();
                return;
            case Intention.MultiAttack:
                intentSprite.sprite = intentionSprites.GetValueOrDefault(intent);
                intentSprite.color = Color.red;
                intentText.text = $"{currentEnemy.CalculateDamage().ToString()} x {currentEnemy.currentAttack.multiAttackNumber}";
                return;
            case Intention.AttackBlock:
                intentSprite.sprite = intentionSprites.GetValueOrDefault(intent);
                intentSprite.color = new Color(128f,0f,128f);
                intentText.text = $"{currentEnemy.CalculateDamage().ToString()} / {currentEnemy.currentAttack.block}";
                return;
            case Intention.Block:
                intentSprite.sprite = intentionSprites.GetValueOrDefault(intent);
                intentSprite.color = Color.blue;
                intentText.text = $"{attack.block}";
                return;
        }

    }
    public override void GainBlock(int block)
    {
        currentEnemy.block += block;
        UpdateBlockBar(currentEnemy);
    }

    public override void HandleDeath()
    {
        base.HandleDeath();
        GameManager.Instance.enemyManager.combatIndex++;
        if (GameManager.Instance.enemyManager.combatIndex > 2)
        {
            SceneManager.LoadScene("MainMenu");
            GameManager.Instance.won = true;
        }
        else
        {
            SceneManager.LoadScene("RewardScene");
        }
        SceneManager.UnloadSceneAsync("CombatScene");
    }

    public void InitializeSprites()
    {
        intentionSprites.Add(Intention.Attack,(Sprite)Resources.Load("IntentSprites/Attack"));
        intentionSprites.Add(Intention.MultiAttack, (Sprite)Resources.Load("IntentSprites/MultiAttack"));
        intentionSprites.Add(Intention.Block, (Sprite)Resources.Load("IntentSprites/Block"));
        intentionSprites.Add(Intention.AttackBlock, (Sprite)Resources.Load("IntentSprites/AttackBlock"));
    }

}
