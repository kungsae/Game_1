using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpRecoverItem : UseItem
{
    public override void Use()
    {
        if (!canAttack) return;
        GameManager.instance.player.HpAdd(data.attackP);
        UIManager.instance.HpUpdate();
        base.Use();

    }
}
