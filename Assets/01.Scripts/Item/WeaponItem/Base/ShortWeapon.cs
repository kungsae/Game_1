using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortWeapon : WeaponBase
{
    private List<Entity> attackedEntitys = new List<Entity>();
    protected override void Awake()
    {
        base.Awake();
    }
    public override void Attack()
    {
        base.Attack();
    }
    public override void ChangeWeapon(bool get, Player _player = null)
    {
        base.ChangeWeapon(get, _player);
        if (!get)
        {
            attackedEntitys.Clear();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Entity"))
        {
            Entity entity = collision.GetComponent<Entity>();
            if (entity != null)
            {
                if (!attackedEntitys.Contains(entity))
                {
                    entity.OnDamage(player.data.damage * data.powerP,transform.position,data.pushP);
                    attackedEntitys.Add(entity);
                }
            }
        }
    }
    protected override IEnumerator CoolDown()
    {
        yield return base.CoolDown();
        attackedEntitys.Clear();
    }
}