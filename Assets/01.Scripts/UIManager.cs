using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;


    public Canvas canvas;
    
    public CraftTableUI craftTable;
    public GameObject damageTextPrefab;
    public Text hpText;
    public Image hpBar;
    public Image fakeHpBar;
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
        StringBuilder builder = new StringBuilder();
        builder.Append(GameManager.instance.player.hp);
        builder.Append("/");
        builder.Append(GameManager.instance.player.data.maxHp);
        hpText.text = builder.ToString() ;
        hpBar.fillAmount = (float)(GameManager.instance.player.hp / GameManager.instance.player.data.maxHp);
        StopAllCoroutines();
        StartCoroutine(FakeHpBarProccess());
    }
    IEnumerator FakeHpBarProccess()
    {
        float time = 0;
        float origineValue = fakeHpBar.fillAmount;
        while (true)
        {
            time += Time.deltaTime;
            fakeHpBar.fillAmount = Mathf.Lerp(origineValue, GameManager.instance.player.hp/ GameManager.instance.player.data.maxHp, time*2f);
            yield return null;
        }
    }
    public void DamageText(float damage,Vector2 pos)
    {
        DamageText dText = PoolManager<DamageText>.instance.GetPool(damageTextPrefab);
        dText.TextSet(damage, pos);
    }
}
