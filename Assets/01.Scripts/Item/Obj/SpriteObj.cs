using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteObj : MonoBehaviour
{
    private SpriteRenderer render;
    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }
    public void RenderChange(Material mat)
    {
        render.material = mat;
    }
}
