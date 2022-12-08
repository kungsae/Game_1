using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : ItemBase
{
    public override void Use()
    {
        if (!canAttack) return;
        base.Use();
        GameManager.instance.player.HpAdd(data.attackP);
        GameManager.instance.inventory.DeleteItem(data,1);
        UIManager.instance.HpUpdate();
    }
}
