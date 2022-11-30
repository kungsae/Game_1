using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Instantiation(GameObject obj,GameObject parent)
    {
        return Instantiate(obj, parent.transform);
    }
    public void SetActive(GameObject obj,bool active)
    {
        obj.SetActive(active);
    }
}
