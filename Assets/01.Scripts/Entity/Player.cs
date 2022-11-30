using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public GameObject hand;
    private WeaponBase weapon;
    public override void Awake()
    {
        base.Awake();
        weapon = hand.GetComponentInChildren<WeaponBase>();
        weapon.player = this;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Look();
        Move();
        if (Input.GetMouseButton(0))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractObj();
        }
    }
    public void Look()
    {
        dir = -(transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition));
        hand.transform.rotation = Quaternion.FromToRotation(Vector2.right * transform.localScale.x, dir);
    }
    public void Move()
    {
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * data.speed;
        if (move.sqrMagnitude != 0)
        {
            animator.Play("Move");
        }
        else
        {
            animator.Play("Idle");
        }
        rigid.velocity = move;
    }
    public override void Attack()
    {
        weapon.Attack();
    }
    public void ChangeWeapon(WeaponBase newWeapon)
    {
        if (weapon != null)
        {
            weapon.ChangeWeapon(false);
            newWeapon.transform.localScale = weapon.transform.localScale;
        }
        weapon = newWeapon;
        weapon.ChangeWeapon(true, this);
    }
    public void InteractObj()
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position,1,transform.right,0, LayerMask.GetMask("Weapon"));
        WeaponBase newWeapon = null;
        Debug.Log(hit.Length);
        foreach (var item in hit)
        {
            if (item.collider.GetComponent<WeaponBase>() != weapon)
            {
                newWeapon = item.collider.GetComponent<WeaponBase>();
                break;
            }
        }
        if (newWeapon != null)
        {
            ChangeWeapon(newWeapon);
        }
    }
}
