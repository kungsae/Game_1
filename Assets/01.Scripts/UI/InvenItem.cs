using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenItem : MonoBehaviour
{
    private Image image;
    private RectTransform rect;
    public List<ItemBase> data = new List<ItemBase>();

    [SerializeField] private GameObject handler;
    private void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }
    public void ImageSet(Sprite sprite)
    {
        image.sprite = sprite;
    }
    public void Init()
    {
        rect.anchoredPosition = Vector2.zero;
    }
}
