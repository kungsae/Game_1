using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public CraftTable craftTable;
    public Text hpText;
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
}
