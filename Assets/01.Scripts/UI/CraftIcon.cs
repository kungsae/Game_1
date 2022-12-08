using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftIcon : MonoBehaviour
{
    ItemBase item;
    [SerializeField] private Image itemImage;
    public void Init(ItemBase _item)
    {
        item = _item;
        itemImage.sprite = _item.spriteRender.sprite;
    }
    public void Select()
    {
        UIManager.instance.craftTable.Select(item);
    }
}
