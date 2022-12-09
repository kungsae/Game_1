using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    TextMeshPro text;
    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }
    private void OnEnable()
    {
        StartCoroutine(UpPos());
    }
    IEnumerator UpPos()
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            transform.position += new Vector3(0, 1f) * Time.deltaTime;
            if (time > 1f)
            {
                PoolManager<DamageText>.instance.SetPool(this);
                yield break;
            }
            yield return null;

        }
    }
    public void TextSet(float dmg, Vector2 pos)
    {
        text.text = dmg.ToString();
        transform.position = pos;
        gameObject.SetActive(true);
    }
}
