using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class OptionsManager : MonoBehaviour
{
    private AudioManager audioManager;
    public bool muteAudio = true;

    public List<TMP_FontAsset> fontList;
    public static event Action FontUpdated;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameManager.Instance.audioManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public TMP_FontAsset GetFontClass(string classID)
    {
        switch (classID)
        {
            case "MenuText":
                return fontList[0];
            case "CardTitle":
                return fontList[1];
            case "CardBody":
                return fontList[2];
            case "CardBodyBold":
                return fontList[3];
            case "MenuTextBold":
                return fontList[4];
            default:
                return fontList[0];
        }
    }

    public void UpdateFont()
    {
        FontUpdated?.Invoke();
    }
}
