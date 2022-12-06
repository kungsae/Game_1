using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortWeapon : ItemBase
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
        if ((collision.CompareTag("Entity")&&gameObject.layer == LayerMask.NameToLayer("Hand"))||(collision.CompareTag("Player") && gameObject.layer == LayerMask.NameToLayer("Hand")))
        {
            Entity entity = collision.GetComponent<Entity>();
            if (entity != null)
            {
                if (!attackedEntitys.Contains(entity))
                {
                    entity.OnDamage(player.data.damage * data.attackP,transform.position,data.knockbackP);
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