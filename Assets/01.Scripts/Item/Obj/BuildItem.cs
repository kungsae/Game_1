using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildItem : ItemBase
{
    [SerializeField]private GameObject build;
    [SerializeField] private GameObject ghost;
    private bool buildMode = false;
    public override void Use()
    {
        if (!canAttack|| buildMode) return;
        base.Use();
        StartCoroutine(BuildMode());

    }
    public override void ChangeWeapon(bool get, Player _player = null)
    {
        base.ChangeWeapon(get, _player);
        if (!get)
            buildMode = false;
    }
    public IEnumerator BuildMode()
    {
        if (buildMode) yield break;
        buildMode = true;
        GameObject _ghost = PoolManager<Transform>.instance.GetPool(ghost).gameObject;
        _ghost.SetActive(true);
        Vector2 colSize = _ghost.GetComponent<SpriteRenderer>().size;
        SpriteRenderer render = _ghost.GetComponent<SpriteRenderer>();
        Vector3 mouse;
        while (buildMode)
        {
            yield return null;
            if (Input.GetMouseButtonDown(1))
            {
                PoolManager<Transform>.instance.SetPool(_ghost.transform);
                buildMode = false;
                yield break;
            } 
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics2D.BoxCast(_ghost.transform.position, colSize, 0, Vector2.right, 0, ~LayerMask.GetMask("Map")).collider == null)
                {
                    BuildObj obj = PoolManager<BuildObj>.instance.GetPool(build);
                    obj.transform.position = _ghost.transform.position;
                    obj.gameObject.SetActive(true);
                    PoolManager<Transform>.instance.SetPool(_ghost.transform);
                    buildMode = false;
                    GameManager.instance.inventory.DeleteItem(data, 1);
                    yield break;
                }
            }
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0;
            _ghost.transform.position = mouse;
            if (Physics2D.BoxCast(_ghost.transform.position, colSize, 0, Vector2.right, 0, ~LayerMask.GetMask("Map")).collider != null)
            {
                render.color = Color.red;
            }
            else
            {
                render.color = Color.green;
            }



        }
    }
}
