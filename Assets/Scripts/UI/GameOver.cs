using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        SceneManager.UnloadSceneAsync("GameOver");
        Destroy(GameManager.Instance.gameObject);
       // FindObjectOfType<MusicManager>().audioSource.Stop();
        Destroy(FindObjectOfType<MusicManager>().gameObject);
    }
    public void RestartFight()
    {
        GameManager.Instance.playerManager.restartFight = true;
        SceneManager.LoadScene("CombatScene");
        SceneManager.UnloadSceneAsync("GameOver");
    }
}
