using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject item;
    GameObject content;
    private void Awake()
    {
        content = GameObject.FindGameObjectWithTag("Inventory");
        
    }

    public void AddItem() { 
    //content.transform.Chi
    }
}
