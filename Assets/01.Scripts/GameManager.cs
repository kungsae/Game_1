using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public Player player;
    public Inventory inventory;
    private void Awake()
    {
        instance = this;
        inventory = GetComponent<Inventory>();
    }
}
