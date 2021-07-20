using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    PlayerStats playerStats;
    public Inventory inventory;
    Collider2D[] cols;
    //Water
    public Sprite waterSprite;
    string waterTag = "Water";
    private int waterId = 1;
    private float waterStamina = 20.0F;
    private float waterPeeLvl = 10.0F;
    //EnergyDrink
    public Sprite energyDrinkSprite;
    string energyDrinkTag = "EnergyDrink";
    private int energyDrinkId = 2;
    private float energyDrinkStamina = 100.0F;
    private float energyDrinkSpeedTime = 1.0f;
    private float energyDrinkTime = 15.0f;
    private float energyDrinkPeeLvl = 20.0F;
    private bool energyDrinkActive = false;

    public Sprite smokeItemSprite;
    private string smokeItemtag = "smokeItem";
    private int smokeItemId = 3;




    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        CastCollider();
        TakeWater();
        TakeEnergyDrink();
        takeSmokeItem();
    }
    public void CastCollider() {
        cols = Physics2D.OverlapCircleAll(playerStats.player.transform.position, 0.5F);
    }
    public void TakeWater() {
        
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

    public void TakeEnergyDrink() {
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == energyDrinkTag)
            {
                inventory.AddItem(energyDrinkId, energyDrinkSprite);

                Destroy(cols[i].gameObject);

            }
        }
    }
    public void UseEnergyDrink() {
        energyDrinkActive = true;
        playerStats.Stamina += energyDrinkStamina;
        playerStats.PeeLvl += energyDrinkPeeLvl;
        playerStats.setHighSpeed();
        StartCoroutine(EnergyDrink());
    }

    IEnumerator EnergyDrinkEndEffect() {
        int i = 0;
        while (i<5)
        {
            playerStats.setLowSpeed();
            yield return new WaitForSeconds(energyDrinkSpeedTime);
            playerStats.setHighSpeed();
            yield return new WaitForSeconds(energyDrinkSpeedTime);
            i++;
        }
        playerStats.setNormalSpeed();
    }
    IEnumerator EnergyDrink() {
        yield return new WaitForSeconds(energyDrinkTime);
        StartCoroutine(EnergyDrinkEndEffect());
    }

    public void takeSmokeItem() {
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == smokeItemtag)
            {
                inventory.AddItem(smokeItemId, smokeItemSprite);

                Destroy(cols[i].gameObject);

            }
        }
    }

}
