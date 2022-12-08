using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    public float attackP;
    [Range(0,100)]
    public float knockbackP;
    public float coolDown;
    public Sprite icon;
    public ItemType type;
    public CraftItem[] craftItems;
}
[System.Serializable]
public struct CraftItem
{
    public WeaponData needItem;
    public int needCount;
}
public enum ItemType
{
    Weapon,
    Etc,
    Material
}
