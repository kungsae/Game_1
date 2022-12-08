using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Entity
{
    public ItemBase weapon;
    public override void Awake()
    {
        base.Awake();
        //weapon = hand.GetComponentInChildren<ItemBase>();
        //inventory.PushItem(weapon);
        //weapon.player = this;
    }
    private void Start()
    {
        UIManager.instance.HpUpdate();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Look();
        Move();
        if(!EventSystem.current.IsPointerOverGameObject())
        if (Input.GetMouseButton(0)||Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractObj();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetItem();
        }
    }
    public void Look()
    {
        dir = -(transform.position + new Vector3(0,0.5f,0) - Camera.main.ScreenToWorldPoint(Input.mousePosition));
        hand.transform.rotation = Quaternion.FromToRotation(Vector2.right * transform.localScale.x, dir);
    }
    public void Move()
    {
        if (onDamage) return;
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
        if (weapon != null)
        {
            weapon.Use();
        }
    }
    public override void Die()
    {
        base.Die();
        this.enabled = false;
    }
    public void ChangeWeapon(ItemBase newWeapon)
    {
        if (weapon != null)
        {
            weapon.ChangeWeapon(false);
        }
        weapon = newWeapon;
        weapon.transform.parent = hand.transform;
        weapon.ChangeWeapon(true, this);
    }
    public override void OnDamage(float _damage, Vector2 attackPos, float pushPower = 0)
    {
        base.OnDamage(_damage, attackPos, pushPower);
        UIManager.instance.HpUpdate();
    }
    public void InteractObj()
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, 1, transform.right, 0, LayerMask.GetMask("Interact"));
        BuildObj item = null;
        foreach (var idx in hit)
        {
            if (idx.collider.GetComponent<BuildObj>() != null)
            {
                item = idx.collider.GetComponent<BuildObj>();
                break;
            }
        }
        if(item!=null)
        item.Interact();
    }
    public void GetItem()
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position,1,transform.right,0, LayerMask.GetMask("Item"));
        ItemBase item = null;
        foreach (var idx in hit)
        {
            if (idx.collider.GetComponent<ItemBase>() != null)
            {
                item = idx.collider.GetComponent<ItemBase>();
                break;
            }
        }
        if (item != null)
        {
            if (!GameManager.instance.inventory.MaxInventory())
            {
                GameManager.instance.inventory.PushItem(item);
            }
        }
    }

}
