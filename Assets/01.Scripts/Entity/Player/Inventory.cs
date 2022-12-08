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
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (!inventorySlots[i].HaveItem())
            {
                if (ContainItem(item))
                {
                    Debug.Log("ÀÖÀ½");
                    continue;
                }
                itemCount++;
                InvenItem invenItem = PoolManager<InvenItem>.instance.GetPool(slotPrefab);
                invenItem.gameObject.SetActive(true);
                invenItem.ImageSet(item.GetComponent<SpriteRenderer>().sprite);
                //inventory[i] = item;
                inventorySlots[i].GetItem(invenItem, item);
                item.gameObject.SetActive(false);
                break;
            }
            else
            {
                if (inventorySlots[i].item.data[0].name == item.name)
                {
                    PoolManager<ItemBase>.instance.SetPool(item);
                    inventorySlots[i].AddItem(item);
                    break;
                }
            }
        }
        //inventory.Add(item);
        //inventorySlots[inventory.Count - 1].sprite = item.GetComponent<SpriteRenderer>().sprite;
    }
    public int GetItemCount(WeaponData item)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].item != null && inventorySlots[i].item.data[0].data == item)
            {
                return inventorySlots[i].item.data.Count;
            }
        }
        return 0;
    }
    public void DeleteItem(WeaponData item,int count)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].item !=null&& inventorySlots[i].item.data[0].data.name == item.name)
            {
                ItemBase outItem;
                outItem = inventorySlots[i].item.data.Last();
                inventorySlots[i].item.data.RemoveRange(inventorySlots[i].item.data.Count - count, count);
                inventorySlots[i].CountSet();
                if (inventorySlots[i].item.data.Count < 1)
                {
                    itemCount--;
                    PoolManager<InvenItem>.instance.SetPool(inventorySlots[i].item);
                    if (GameManager.instance.player.weapon !=null&& outItem.data.name == GameManager.instance.player.weapon.data.name)
                    {
                        GameManager.instance.player.weapon.ChangeWeapon(false);
                        PoolManager<ItemBase>.instance.SetPool(GameManager.instance.player.weapon);
                        GameManager.instance.player.weapon = null;
                    }
                    inventorySlots[i].item = null;
                }
                break;
            }
        }
        
    }
    public void OutItem(InvenSlot item)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i] == item)
            {
                ItemBase outItem;
                if (inventorySlots[i].item.data.Count <= 1)
                {
                    itemCount--;
                    PoolManager<InvenItem>.instance.SetPool(inventorySlots[i].item);
                    outItem = inventorySlots[i].item.data.Last();
                    inventorySlots[i].item.data.RemoveAt(inventorySlots[i].item.data.Count - 1);
                    if (outItem == GameManager.instance.player.weapon)
                    {
                        GameManager.instance.player.weapon = null;
                    }
                    inventorySlots[i].item = null;
                }
                else
                {
                    outItem = PoolManager<ItemBase>.instance.GetPool(inventorySlots[i].item.data.Last().gameObject);
                    inventorySlots[i].item.data.RemoveAt(inventorySlots[i].item.data.Count - 1);
                }
                outItem.gameObject.SetActive(true);
                outItem.transform.parent = null;
                outItem.transform.position = GameManager.instance.player.transform.position;
                outItem.ChangeWeapon(false);
                outItem.gameObject.SetActive(true);

                break;
            }
        }
    }
    public bool ContainItem(ItemBase item)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {

            if (inventorySlots[i].item!=null && inventorySlots[i].item.data[0].name == item.name)
            {
                return true;
            }
        }
        return false;
    }
}
