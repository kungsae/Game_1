using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemBase : MonoBehaviour
{
    [HideInInspector] public Action attackAction;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Player player;
    [SerializeField] protected WeaponData data;
    protected WaitForSeconds coolDown;
    protected bool canAttack = true;
    protected Collider2D col;

    protected virtual void Awake()
    {
        coolDown = new WaitForSeconds(data.coolDown);
        col = gameObject.GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }
    public virtual void Attack()
    {
        if (!canAttack) return;
            animator.Play("Attack");
            StartCoroutine(CoolDown());
    }
    public virtual void ChangeWeapon(bool get,Player _player = null)
    {
        player = _player;
        StopAllCoroutines();
        canAttack = true;
        col.enabled = !get;
        if (get)
        {
            gameObject.transform.localPosition = Vector2.zero;
            gameObject.transform.localRotation = Quaternion.identity;
            //gameObject.transform.parent = _player.hand.transform;
            gameObject.SetActive(true);
            gameObject.layer = LayerMask.NameToLayer("Hand");
            if (gameObject.transform.localScale.x < 0)
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
        }
        else
        {
            //gameObject.transform.parent = null;
            gameObject.SetActive(false);
            gameObject.layer = LayerMask.NameToLayer("Item");
        }
        animator.enabled = get;
        animator.Play("Idle");
    }
    public void DropItem(Vector2 createPos)
    {
        transform.position = createPos;
        float valY = UnityEngine.Random.Range(-1.8f,-0.8f);
        float valX = UnityEngine.Random.Range(-1.3f,0.8f);
        transform.DOLocalMoveY(transform.position.y + 2, 0.5f).SetEase(Ease.OutQuad).OnComplete(()=> 
        {
            transform.DOLocalMoveY(transform.position.y + valY, 0.5f).SetEase(Ease.InQuad);
        });
        transform.DOLocalMoveX(transform.position.x + valX, 1f);
        transform.DOLocalRotate(new Vector3(0, 0,3600), 1f,RotateMode.FastBeyond360);
    }
    protected virtual IEnumerator CoolDown()
    {
        canAttack = false;
        yield return coolDown;
        canAttack = true;
    }
}
