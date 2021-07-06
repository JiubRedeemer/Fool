using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    PlayerStats playerStats;
    public Inventory inventory;

    //Water
    public Sprite waterSprite;
    string waterTag = "Water";
    private int waterId = 1;
    private float waterStamina = 20.0F;
    private float waterPeeLvl = 10.0F;
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        inventory = FindObjectOfType<Inventory>();   
    }

    void Update()
    {
        TakeWater();
    }
    public void TakeWater() {
        Collider2D[] cols = Physics2D.OverlapCircleAll(playerStats.player.transform.position, 0.5F);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == waterTag) {
                inventory.AddItem(waterId, waterSprite);
                
                Destroy(cols[i].gameObject);
                
            }
        }
    }

    public void UseWater() {
        playerStats.Stamina += waterStamina;
        playerStats.PeeLvl += waterPeeLvl;
        playerStats.setNormalSpeed();
    }
}
