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
    //EnergyDrink
    public Sprite energyDrinkSprite;
    string energyDrinkTag = "EnergyDrink";
    private int energyDrinkId = 2;
    private float energyDrinkStamina = 100.0F;
    private float energyDrinkSpeedTime = 1.0f;
    private float energyDrinkTime = 15.0f;
    private bool energyDrinkActive = false;
    //SmokeItem
    public Sprite smokeItemSprite;
    public GameObject usedSmokePrefab;
    private string smokeItemTag = "smokeItem";
    private int smokeItemId = 3;
    //Petard
    public Sprite petardItemSprite;
    public GameObject usedPetardPrefab;
    private string petardItemTag = "petardItem";
    private int petardItemId = 4;
    //Battery
    private string batteryTag = "Battery";
    private int batteryId = 5;
    public Sprite batterySprite;
    //GiftBox
    private string giftBoxTag = "GiftBox";



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
        TakeSmokeItem();
        TakePetardItem();
        TakeBattery();
        TakeGiftBox();
    }
    public void CastCollider()
    {
        cols = Physics2D.OverlapCircleAll(playerStats.player.transform.position, 0.5F);
    }
    public void TakeWater()
    {

        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == waterTag)
            {
                inventory.AddItem(waterId, waterSprite, 1);

                Destroy(cols[i].gameObject);

            }
        }
    }

    public void UseWater()
    {
        playerStats.Stamina += waterStamina;
        playerStats.setNormalSpeed();
    }

    public void TakeEnergyDrink()
    {
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == energyDrinkTag)
            {
                inventory.AddItem(energyDrinkId, energyDrinkSprite, 1);

                Destroy(cols[i].gameObject);

            }
        }
    }
    public void UseEnergyDrink()
    {
        energyDrinkActive = true;
        playerStats.Stamina += energyDrinkStamina;
        playerStats.setHighSpeed();
        StartCoroutine(EnergyDrink());
    }

    IEnumerator EnergyDrinkEndEffect()
    {
        int i = 0;
        while (i < 5)
        {
            playerStats.setLowSpeed();
            yield return new WaitForSeconds(energyDrinkSpeedTime);
            playerStats.setHighSpeed();
            yield return new WaitForSeconds(energyDrinkSpeedTime);
            i++;
        }
        playerStats.setNormalSpeed();
    }
    IEnumerator EnergyDrink()
    {
        yield return new WaitForSeconds(energyDrinkTime);
        StartCoroutine(EnergyDrinkEndEffect());
    }

    public void TakeSmokeItem()
    {
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == smokeItemTag)
            {
                inventory.AddItem(smokeItemId, smokeItemSprite, 1);

                Destroy(cols[i].gameObject);

            }
        }
    }


    public void useSmokeItem()
    {
        GameObject usedSmoke = Instantiate(usedSmokePrefab, playerStats.player.transform.position, Quaternion.identity);
    }

    public void TakePetardItem()
    {
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == petardItemTag)
            {
                inventory.AddItem(petardItemId, petardItemSprite, 1);
                Destroy(cols[i].gameObject);
            }
        }
    }

    public void UsePetardItem()
    {
        GameObject usedPetard = Instantiate(usedPetardPrefab, playerStats.player.transform.position, playerStats.player.transform.rotation);
    }
    public void TakeBattery() 
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].tag == batteryTag)
                {
                inventory.AddItem(batteryId, batterySprite, 1);
                Destroy(cols[i].gameObject);
                }
            }
    }

    public void UseBattery() {
        playerStats.flashLightLevel += 100;
        playerStats.flashLightLight.intensity = 1.0f;
    }

    public void TakeGiftBox()
    {


        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == giftBoxTag)
            {
                int randItem = Random.Range(1, 5);
                Sprite[] sprites = new Sprite[5];
                sprites[0] = waterSprite;
                sprites[1] = energyDrinkSprite;
                sprites[2] = smokeItemSprite;
                sprites[3] = petardItemSprite;
                sprites[4] = batterySprite;
                inventory.AddItem(randItem, sprites[randItem-1], Random.Range(1, 6));
                Destroy(cols[i].gameObject);

            }
        }
    }
}
