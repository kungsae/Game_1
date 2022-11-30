using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectionWeapon : WeaponBase
{
    [Range(0, 360)] [SerializeField] protected float fireAngle;
    [SerializeField] protected Transform firePos;
    [SerializeField] protected GameObject projection;
    

//#if UNITY_EDITOR
//    Color _red = new Color(1f, 0f, 0f, 0.1f);
//    private void OnDrawGizmosSelected()
//    {
//        Handles.color = _red;
//        // DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
//        Handles.DrawSolidArc(firePos.position, Vector3.forward, firePos.right, fireAngle / 2, 10);
//        Handles.DrawSolidArc(firePos.position, Vector3.forward, firePos.right, -fireAngle / 2, 10);
//    }
//#endif
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
        GameObject obj = PoolManager<Bullet>.instance.GetPool(projection).gameObject;
        obj.transform.rotation = Quaternion.FromToRotation(Vector2.right * -player.transform.localScale.x, firePos.up);
        obj.transform.position = firePos.position;
        obj.GetComponent<TrailRenderer>().Clear();
        obj.SetActive(true);
    }
    protected override IEnumerator CoolDown()
    {
        yield return base.CoolDown();
    }
}
