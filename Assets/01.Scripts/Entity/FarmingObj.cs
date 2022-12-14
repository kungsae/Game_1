using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingObj : Entity
{
    public RecoverObj obj;

    public void RecoverObjSet(RecoverObj _obj)
    {
        obj = _obj;
    }
    public override void Attack()
    {
    }
    public override void Init()
    {
        base.Init();
    }

    public override void Die()
    {
        base.Die();
        obj.Recover();
    }
}
