using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CraftTable : MonoBehaviour
{
    ItemBase craftItem = null;
    BuildObj table;
    List<CraftIcon> itemList = new List<CraftIcon>();

    [Header("Total")]
    [SerializeField] private Image totalItemImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Text typeText;
    [SerializeField] private Text infoText;

    [Header("Mat")]
    [SerializeField] private Image[] matItemImages;
    [SerializeField] private Text matItemCountText;

    [Header("Itmes")]
    [SerializeField] private GameObject iconParent;
    [SerializeField] private GameObject itemIconPrfab;
    public void SetTable(ItemBase[] items,BuildObj _table)
    {
        table = _table;
        for (int i = 0; i < items.Length; i++)
        {
            CraftIcon icon = PoolManager<CraftIcon>.instance.GetPool(itemIconPrfab);
            icon.transform.SetParent(iconParent.transform);
            icon.Init(items[i]);
            icon.gameObject.SetActive(true);
            itemList.Add(icon);
        }
    }
    public void InitData()
    {
        table = null;
        craftItem = null;
        totalItemImage.gameObject.SetActive(false);
        for (int i = 0; i < itemList.Count; i++)
        {
            PoolManager<CraftIcon>.instance.SetPool(itemList[i]);
        }
        for (int i = 0; i < matItemImages.Length; i++)
        {
            matItemImages[i].gameObject.SetActive(false);
        }
        StringBuilder builder = new StringBuilder();
        nameText.text = "--";
        builder.Append("Type : --");
        typeText.text = builder.ToString();
        builder.Clear();
        builder.Append("Attack : --");
        builder.AppendLine();
        builder.Append("Speed : --");
        builder.AppendLine();
        builder.Append("KnockBack : --");
        builder.AppendLine();
        infoText.text = builder.ToString();
        matItemCountText.text = "";

    }
    public void Craft()
    {
        if (craftItem != null)
        {
            for (int i = 0; i < craftItem.data.craftItems.Length; i++)
            {
                if (craftItem.data.craftItems[i].needCount > GameManager.instance.inventory.GetItemCount(craftItem.data.craftItems[i].needItem))
                {
                    Debug.Log("재료부족");
                    return;
                }
            }
            Debug.Log("생성가능");
            for (int i = 0; i < craftItem.data.craftItems.Length; i++)
            {
                GameManager.instance.inventory.DeleteItem(craftItem.data.craftItems[i].needItem, craftItem.data.craftItems[i].needCount);
            }
            ItemBase newItem = PoolManager<ItemBase>.instance.GetPool(craftItem.gameObject);
            newItem.gameObject.SetActive(true);
            Select(craftItem);
            newItem.DropItem(table.transform.position);
        }
    }
    public void Select(ItemBase item)
    {
        craftItem = item;
        totalItemImage.sprite = item.spriteRender.sprite;
        totalItemImage.gameObject.SetActive(true);
        nameText.text = item.data.name;
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("Type : {0}", item.data.type);
        typeText.text = builder.ToString();
        builder.Clear();
        builder.AppendFormat("Attack : {0}", item.data.attackP);
        builder.AppendLine();
        builder.AppendFormat("Speed : {0}", item.data.coolDown);      
        builder.AppendLine();
        builder.AppendFormat("KnockBack : {0}", item.data.knockbackP);
        builder.AppendLine();
        infoText.text = builder.ToString();

        builder.Clear();
        for (int i = 0; i < matItemImages.Length; i++)
        {
            matItemImages[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < item.data.craftItems.Length; i++)
        {
            builder.AppendFormat("{0} / {1}", GameManager.instance.inventory.GetItemCount(item.data.craftItems[i].needItem),item.data.craftItems[i].needCount);
            builder.AppendLine();
            matItemImages[i].sprite = item.data.craftItems[i].needItem.icon;
            matItemImages[i].gameObject.SetActive(true);
        }
        matItemCountText.text = builder.ToString();
    }
}
