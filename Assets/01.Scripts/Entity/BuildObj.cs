using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildObj : Entity
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
    public override IEnumerator DisableEntity()
    {
        yield return base.DisableEntity();
        PoolManager<BuildObj>.instance.SetPool(this);
    }
}
