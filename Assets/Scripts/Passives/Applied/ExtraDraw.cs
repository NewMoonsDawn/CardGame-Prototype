using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buff", menuName = "Passive/Buff/Extra Draw")]
public class ExtraDraw : Buff
{
    public override void OnCast()
    {
        if (owner is Player player)
        {
            player.queuedCardDraw += amount;
            Debug.Log("extra draw");
        }
    }
}
