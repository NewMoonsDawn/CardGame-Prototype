using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public OptionsManager optionsManager { get; private set; }
    public AudioManager audioManager { get; private set; }
    public DeckManager deckManager { get; private set; }
    public SpellManager spellManager { get; private set; }
    public PlayerManager playerManager { get; private set; }
    public EnemyManager enemyManager { get; private set; }
    [HideInInspector]
    public bool PlayingCard = false;
    public bool won = false;

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

    public void BattleSetup()
    {   
        playerManager.BattleSetup();
        enemyManager.BattleSetup();
    }
    private void InitializeManagers()
    {
        optionsManager = GetComponentInChildren<OptionsManager>();
        audioManager = GetComponentInChildren<AudioManager>();
        deckManager = GetComponentInChildren<DeckManager>();
        spellManager = GetComponentInChildren<SpellManager>();
        playerManager = GetComponentInChildren<PlayerManager>();
        enemyManager = GetComponentInChildren<EnemyManager>();
        
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
        if (spellManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/SpellManager");
            if(prefab == null)
            {
                Debug.Log($"Spell Manager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                spellManager = GetComponentInChildren<SpellManager>();
            }
        }
        if (playerManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/PlayerManager");
            if (prefab == null)
            {
                Debug.Log($"Player Manager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                playerManager = GetComponentInChildren<PlayerManager>();
            }
        }
        if (enemyManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/EnemyManager");
            if (prefab == null)
            {
                Debug.Log($"Enemy Manager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                enemyManager= GetComponentInChildren<EnemyManager>();
            }
        }
    }
}
