using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject demoOverObject;
    private void Awake()
    {
        Debug.Log("Awake");
        if (GameManager.Instance != null && GameManager.Instance.won)
        {

            demoOverObject.SetActive(true);
        }
    }

    public void Close(GameObject target)
    {
       target.SetActive(false);
    }    
    public void Open(GameObject target)
    {
        target.SetActive(true);
    }    
    public void StartGame()
    {
        StartCoroutine(LoadCombatScene());
    }

    public IEnumerator LoadCombatScene()
    {
        yield return new WaitForSeconds(0.1f);
        var ao = SceneManager.LoadSceneAsync("CombatScene",LoadSceneMode.Single);
        while(!ao.isDone)
        {
            yield return null;
        }
    }
}
