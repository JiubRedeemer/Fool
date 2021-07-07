using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public GameObject itemUI;
    public GameObject content;
    public Items item;
    private int[,] items = new int[64,2]; //1- water
    int itemsCount = 0;
    private Player player;
    private void Awake()
    {
       // content = GameObject.FindGameObjectWithTag("Inventory");
        player = GetComponent<Player>();

    }
    private void Update()
    {
        UseItem();
    }
    public void AddItem(int idItem, Sprite spriteItem) {
        bool flagFound = false;
        int whereFound = 0;
        for (int i = 0; i < itemsCount; i++) {
            if (items[i, 0] == idItem) { 
                flagFound = true;
                whereFound = i;
            }
        }
        if (!flagFound)
        {
            whereFound = itemsCount;
            items[itemsCount, 0] = idItem;
            items[itemsCount, 1]++;
            GameObject newItem = itemUI;
            newItem.transform.GetChild(0).GetComponent<Image>().sprite = spriteItem;
            newItem.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = items[whereFound, 1].ToString();
            Instantiate(newItem, content.transform);
        }
        else {
            items[whereFound, 0] = idItem;
            items[whereFound, 1]++;
            content.transform.GetChild(whereFound).GetChild(0).GetChild(0).GetComponent<Text>().text = items[whereFound, 1].ToString();
        }
        itemsCount++;
        Debug.Log(items[0, 0] +"----"+ items[0, 1]);
    }

    public void UseItem() {
        for (int i = 0; i < itemsCount-1; i++) {

            if (content.transform.GetChild(i).GetChild(0).GetComponent<Item>().pushed) {
                content.transform.GetChild(i).GetChild(0).GetComponent<Item>().pushed = false;
                int pushItem = items[i, 0];
                switch (pushItem) {
                    case 1: item.UseWater(); break;
                }
                if (items[i, 1] > 1)
                {
                    items[i, 1]--;
                    content.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>().text = items[i, 1].ToString();
                }
                else {
                    itemsCount--;
                    Destroy(content.transform.GetChild(i).gameObject);
                }
            }
        }
    }
}
