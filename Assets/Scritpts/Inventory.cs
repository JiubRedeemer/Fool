using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public GameObject itemUI;
    public GameObject content;
    public Items item;
    private int[,] items = new int[64, 2]; //1- water 2- energyDrink
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
        UseItemByKeys();
    }
    public void AddItem(int idItem, Sprite spriteItem, int count)
    {
        bool flagFound = false;
        int whereFound = 0;
        for (int i = 0; i < itemsCount; i++)
        {
            if (items[i, 0] == idItem)
            {
                flagFound = true;
                whereFound = i;
            }
        }
        if (!flagFound)
        {
            whereFound = itemsCount;
            items[itemsCount, 0] = idItem;
            items[itemsCount, 1] += count;
            GameObject newItem = itemUI;
            newItem.transform.GetChild(0).GetComponent<Image>().sprite = spriteItem;
            newItem.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = items[whereFound, 1].ToString();
            Instantiate(newItem, content.transform);
        }
        else
        {
            items[whereFound, 0] = idItem;
            items[whereFound, 1] += count;
            content.transform.GetChild(whereFound).GetChild(0).GetChild(0).GetComponent<Text>().text = items[whereFound, 1].ToString();
        }
        itemsCount++;
        Debug.Log(items[itemsCount - 1, 0] + "----" + items[itemsCount - 1, 1]);
    }

    public void UseItem()
    {
        if (content.transform.childCount > 0)
        {
            for (int i = 0; i < itemsCount; i++)
            {

                if (content.transform.GetChild(i).GetComponentInChildren<Item>().pushed)
                {
                    content.transform.GetChild(i).GetComponentInChildren<Item>().pushed = false;
                    int pushItem = items[i, 0];
                    switch (pushItem)
                    {
                        case 1: item.UseWater(); break;
                        case 2: item.UseEnergyDrink(); break;
                        case 3: item.useSmokeItem(); break;
                        case 4: item.UsePetardItem(); break;
                        case 5: item.UseBattery(); break;
                    }
                    if (items[i, 1] > 1)
                    {
                        items[i, 1]--;
                        content.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>().text = items[i, 1].ToString();
                    }
                    else
                    {
                        if (itemsCount > 0)
                        {
                            itemsCount--;
                            items[i, 0] = items[i + 1, 0];
                            items[i, 1] = items[i + 1, 1];
                        }
                        Destroy(content.transform.GetChild(i).gameObject);
                    }
                }
            }
        }
    }


    public void UseItemByKeys()
    {
        bool[] keyPushed = new bool[10];
        keyPushed[9] = Input.GetKeyUp(KeyCode.Alpha0);
        keyPushed[0] = Input.GetKeyUp(KeyCode.Alpha1);
        keyPushed[1] = Input.GetKeyUp(KeyCode.Alpha2);
        keyPushed[2] = Input.GetKeyUp(KeyCode.Alpha3);
        keyPushed[3] = Input.GetKeyUp(KeyCode.Alpha4);
        keyPushed[4] = Input.GetKeyUp(KeyCode.Alpha5);
        keyPushed[5] = Input.GetKeyUp(KeyCode.Alpha6);
        keyPushed[6] = Input.GetKeyUp(KeyCode.Alpha7);
        keyPushed[7] = Input.GetKeyUp(KeyCode.Alpha8);
        keyPushed[8] = Input.GetKeyUp(KeyCode.Alpha9);

        for (int i = 0; i < keyPushed.Length; i++)
        {
            if (keyPushed[i])
            {
                //    Debug.Log(i);
                if (i < itemsCount)
                {
                    content.transform.GetChild(i).GetChild(0).GetComponent<Item>().pushed = true;
                }
            }
        }



    }

}
