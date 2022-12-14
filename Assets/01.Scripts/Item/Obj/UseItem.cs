using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : ItemBase
{
    public override void Use()
    {
        if (!canAttack) return;
        base.Use();
        GameManager.instance.inventory.DeleteItem(data, 1);
    }
}
