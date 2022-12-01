using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject slot;
    public GameObject slotPrefab;
    List<InvenSlot> inventorySlots;
    int itemCount = 0;
    public int nowInvenNum;
    private void Awake()
    {
        inventorySlots = slot.GetComponentsInChildren<InvenSlot>().ToList();
        //inventorySlots = slot.GetComponentsInChildren<Image>();
    }
    private void Start()
    {
    }
    public bool MaxInventory()
    {
        return itemCount >= 10;
    }
    public void PushItem(ItemBase item)
    {
        itemCount++;
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (!inventorySlots[i].HaveItem())
            {
                InvenItem invenItem = PoolManager<InvenItem>.instance.GetPool(slotPrefab);
                invenItem.gameObject.SetActive(true);
                invenItem.ImageSet(item.GetComponent<SpriteRenderer>().sprite,i);
                //inventory[i] = item;
                inventorySlots[i].GetItem(invenItem,item);
                break;
            }
        }
        //inventory.Add(item);
        //inventorySlots[inventory.Count - 1].sprite = item.GetComponent<SpriteRenderer>().sprite;
    }
    public void OutItem(InvenSlot item)
    {
        itemCount--;

        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i] == item)
            {
                inventorySlots[i].item.data.transform.parent = null;
                if (inventorySlots[i].item.data == GameManager.instance.player.weapon)
                {
                    GameManager.instance.player.weapon = null;
                }
                inventorySlots[i].item.data.ChangeWeapon(false);
                inventorySlots[i].item.data.gameObject.SetActive(true);
                PoolManager<InvenItem>.instance.SetPool(inventorySlots[i].item);
                inventorySlots[i].item = null;

                break;
            }
        }
    }
}
