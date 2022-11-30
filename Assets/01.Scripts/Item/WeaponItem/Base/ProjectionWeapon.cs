using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectionWeapon : ItemBase
{
    [Range(0, 360)] [SerializeField] protected float fireAngle;
    [SerializeField] protected Transform firePos;
    [SerializeField] protected GameObject projection;


#if UNITY_EDITOR
    Color _red = new Color(1f, 0f, 0f, 0.1f);
    private void OnDrawGizmosSelected()
    {
        Handles.color = _red;
        // DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
        Handles.DrawSolidArc(firePos.position, Vector3.forward, firePos.right, fireAngle / 2, 10);
        Handles.DrawSolidArc(firePos.position, Vector3.forward, firePos.right, -fireAngle / 2, 10);
    }
#endif
    protected override void Awake()
    {
        base.Awake();
    }
    public override void Attack()
    {
        if (canAttack)
        {
            Fire();
        }
        base.Attack();
    }
    protected virtual void Fire()
    {
        Bullet bullet = PoolManager<Bullet>.instance.GetPool(projection);
        bullet.transform.rotation = Quaternion.FromToRotation(Vector2.right * -player.transform.localScale.x, player.hand.transform.up);
        bullet.transform.rotation *= Quaternion.Euler(0,0,Random.Range(-fireAngle * 0.5f, fireAngle * 0.5f));
        bullet.transform.position = firePos.position;
        bullet.Fire(player.data.damage * data.powerP);
        bullet.gameObject.SetActive(true);
    }
    protected override IEnumerator CoolDown()
    {
        yield return base.CoolDown();
    }
}
