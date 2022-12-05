using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public InvenItem item;
    public RectTransform rect;
    public Text countText;
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
        countText = _item.GetComponentInChildren<Text>();
        item = _item;
        item.transform.parent = transform;
        item.data.Add(_data);
        item.Init();
        countText.gameObject.SetActive(true);
        countText.text = item.data.Count.ToString();
    }
    public void AddItem(ItemBase _data)
    {
        item.data.Add(_data);
        countText.text = item.data.Count.ToString();
    }
    public void EquipItem()
    {
        if(item!=null)
        GameManager.instance.player.ChangeWeapon(item.data[0]);
    }
    public bool HaveItem()
    {
        return item != null;
    }
    public void SlotChange(InvenSlot newParent, InvenItem newItem)
    {
        RectTransform tempRect;
        Text tempText;
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
        tempRect = newParent.rect;
        tempText = newParent.countText;
        newParent.rect = rect;
        newParent.countText = countText;
        item = newItem;
        item.transform.parent = transform;
        rect = tempRect;
        countText = tempText;
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
                item.transform.parent = transform;
                item.Init();
                GameManager.instance.inventory.OutItem(this);
                if (item != null)
                {
                    countText.text = item.data.Count.ToString();
                }
            }
        }
    }
}
