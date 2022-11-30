using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public Entity player;
    private void Awake()
    {
        instance = this;
    }
}
