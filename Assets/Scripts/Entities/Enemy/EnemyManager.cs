using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [HideInInspector]
    public EnemyController enemyController;

    public Enemy[] enemies;
    public int combatIndex;
    public Enemy currentEnemy;

    public void BattleSetup()
    {
        enemyController = FindObjectOfType<EnemyController>();
        currentEnemy = Instantiate(enemies[combatIndex]);
        enemyController.BattleSetup(currentEnemy);
    }

    public void StartEnemyTurn()
    {
        currentEnemy.block = 0;
        enemyController.UpdateBlockBar(currentEnemy);
        //TODO: DOT Damage
        PerformEnemyTurn();
    }
    public void PerformEnemyTurn()
    {
        currentEnemy.ExecuteAttack();
        EndEnemyTurn();
    }
    public void EndEnemyTurn()
    {
        //TODO: Tick down debuffs
        enemyController.UpdateIntentionUI(currentEnemy.DecideNextAttack());
        StartCoroutine(StartPlayerTurn());
    }

    IEnumerator StartPlayerTurn()
    {
        yield return new WaitForSeconds(1);
        GameManager.Instance.playerManager.StartPlayerTurn();
    }
}
