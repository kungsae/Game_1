using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildTileItem : ItemBase
{
    [SerializeField] private GameObject build;
    [SerializeField] private GameObject ghost;
    [SerializeField] private TileBase buildTile;
    private bool buildMode = false;
    public override void Use()
    {
        if (!canAttack || buildMode) return;
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
                if (GameManager.instance.CanSetTile())
                {
                    GameManager.instance.SetTile(buildTile);
                    buildMode = false;
                    PoolManager<Transform>.instance.SetPool(_ghost.transform);
                    GameManager.instance.inventory.DeleteItem(data, 1);
                    yield break;
                }
            }
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0;
            _ghost.transform.position = Vector3Int.FloorToInt(mouse) + new Vector3(0.5f,0.5f);
            if (GameManager.instance.CanSetTile())
            {
                render.color = Color.green;
            }
            else
            {
                render.color = Color.red;
            }
        }
    }
}
