using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int playerHealth;
    private int playerXP;
    private int difficulty = 5;

    public OptionsManager optionsManager { get; private set; }
    public AudioManager audioManager { get; private set; }
    public DeckManager deckManager { get; private set; }

    public bool PlayingCard = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeManagers();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void InitializeManagers()
    {
        optionsManager = GetComponentInChildren<OptionsManager>();
        audioManager = GetComponentInChildren<AudioManager>();
        deckManager = GetComponentInChildren<DeckManager>();

        if(optionsManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/OptionsManager");
            if (prefab == null)
            {
                Debug.Log($"Options Manager prefab not found");
            }
            else
            {
                Instantiate(prefab,transform.position, Quaternion.identity, transform);
                optionsManager = GetComponentInChildren<OptionsManager>();
            }
        }
        if (audioManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/AudioManager");
            if (prefab == null)
            {
                Debug.Log($"Audio Manager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                audioManager = GetComponentInChildren<AudioManager>();
            }
        }
        if (deckManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/DeckManager");
            if (prefab == null)
            {
                Debug.Log($"Deck Manager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                deckManager = GetComponentInChildren<DeckManager>();
            }
        }




    }

    public int PlayerHealth
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }
    public int PlayerXP
    {
        get { return playerXP; }
        set { playerXP = value; }
    }
    public int Difficulty
    {
        get { return difficulty; }
        set { difficulty = value; }
    }

}
