using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverObj : MonoBehaviour
{
    [SerializeField] private float recoverTime;
    [SerializeField] private GameObject prefabObj;
    SpriteRenderer render;

    private WaitForSeconds waitTime;
    private void Awake()
    {
        waitTime = new WaitForSeconds(recoverTime);
        render = GetComponent<SpriteRenderer>();
    }
    public void Recover()
    {
        StartCoroutine(RecoverObjProccess());
        render.enabled = true;
    }
    public IEnumerator RecoverObjProccess()
    {
        yield return waitTime;
        Entity obj = PoolManager<Entity>.instance.GetPool(prefabObj);
        obj.GetComponent<FarmingObj>().RecoverObjSet(this);
        render.enabled = false;
        obj.transform.position = transform.position;
        obj.gameObject.SetActive(true);
    }
}
