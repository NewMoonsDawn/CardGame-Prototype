using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Player", menuName = "Player")]
public class Player : Character
{
    public Sprite playerSprite;
    public int queuedCardDraw = 0;
    //TODO: ADD logic
}
