using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvenSlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public InvenItem item;
    public RectTransform rect;
    private Canvas canvas;
    private Transform handler;
    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        handler = transform.parent.Find("Hand");
    }
    public void GetItem(InvenItem _item, ItemBase _data)
    {
        rect = _item.GetComponent<RectTransform>();
        item = _item;
        item.transform.parent = transform;
        item.data = _data;
        item.Init();
    }
    public void EquipItem()
    {
        GameManager.instance.player.ChangeWeapon(item.data);
    }
    public bool HaveItem()
    {
        return item != null;
    }
    public void SlotChange(InvenSlot newParent, InvenItem newItem)
    {
        RectTransform temp;
        if (item == null)
        {
            newParent.item = null;
        }
        else
        {
            newParent.item = item;
            newParent.item.transform.parent = newParent.transform;
            newParent.item.Init();
        }
        temp = newParent.rect;
        newParent.rect = rect;
        item = newItem;
        item.transform.parent = transform;
        rect = temp;
        item.Init();

    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("A");
        if (item != null)
            rect.anchoredPosition -= eventData.delta / canvas.scaleFactor;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
            item.transform.parent = handler.transform;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                InvenSlot slot = eventData.pointerCurrentRaycast.gameObject.GetComponent<InvenSlot>();
                if (slot != null)
                {
                    slot.SlotChange(this, item);
                    return;
                }
                item.transform.parent = transform;
                item.Init();
            }
            else
            {
                GameManager.instance.inventory.OutItem(this);
            }
        }
    }
}
