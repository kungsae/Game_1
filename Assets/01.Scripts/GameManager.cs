using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public Player player;
    public Inventory inventory;
    public ItemInfo info;
    public bool dragable = false;

    public Tilemap seaCol;
    public Tilemap groundCol;
    public Tilemap goundSprite;
    public TileBase goundTileBase;
    int[] dir = { 1, -1 };
    private void Awake()
    {
        instance = this;
        inventory = GetComponent<Inventory>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }
    public void BuildMode()
    {
        
    }
    IEnumerator BuildProccess()
    {
        while (true)
        {
            if (Input.GetMouseButtonUp(1))
            {
                yield break;   
            }
            yield return null;
        }
    }
    public void SetTile()
    {
        Vector3Int mousePos = Vector3Int.FloorToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        mousePos.z = 0;
        if (groundCol.GetTile(mousePos) == null)
        {
            groundCol.SetTile(mousePos, goundTileBase);
            groundCol.SetColliderType(mousePos, Tile.ColliderType.Grid);
            goundSprite.SetTile(mousePos, goundTileBase);
            seaCol.SetTile(mousePos, null);
            for (int i = 0; i < 2; i++)
            {
                if (groundCol.GetTile(mousePos + new Vector3Int(dir[i], 0, 0)) == null)
                {
                    seaCol.SetTile(mousePos + new Vector3Int(dir[i], 0, 0), goundTileBase);
                    seaCol.SetColliderType(mousePos + new Vector3Int(dir[i], 0, 0), Tile.ColliderType.Grid);
                }
                if (groundCol.GetTile(mousePos + new Vector3Int(0, dir[i], 0)) == null)
                {
                    seaCol.SetTile(mousePos + new Vector3Int(0, dir[i], 0), goundTileBase);
                    seaCol.SetColliderType(mousePos + new Vector3Int(0, dir[i], 0), Tile.ColliderType.Grid);
                }
            }

        }
    }
    public void ShakeCam()
    {
        
    }
}
