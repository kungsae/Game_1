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
            Debug.LogError("뭔 (검열)을 하면 ui매니저가 두개임");
        }
        instance = this;
    }
    public void HpUpdate()
    {
        hpText.text = GameManager.instance.player.hp.ToString();
    }
}
