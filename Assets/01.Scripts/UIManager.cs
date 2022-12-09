using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Canvas canvas;
    public CraftTable craftTable;
    public Text hpText;
    public GameObject damageTextPrefab;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("�� (�˿�)�� �ϸ� ui�Ŵ����� �ΰ���");
        }
        instance = this;
    }
    public void HpUpdate()
    {
        hpText.text = GameManager.instance.player.hp.ToString();
    }
    public void DamageText(float damage,Vector2 pos)
    {
        DamageText dText = PoolManager<DamageText>.instance.GetPool(damageTextPrefab);
        dText.TextSet(damage, pos);
    }
}
