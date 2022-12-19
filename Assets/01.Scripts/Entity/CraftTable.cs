using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftTable : Entity
{
    public ItemBase[] itemData;

    public override void Attack()
    {
    }

    public override void Awake()
    {
        base.Awake();
    }
    public void Interact()
    {
        UIManager.instance.craftTable.gameObject.SetActive(true);
        UIManager.instance.craftTable.SetTable(itemData,this);
    }
}
