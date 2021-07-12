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

    //Smoke
    private string smokeTag = "Smoke";
    private int smokeId = 3;
    private float smokeRadius = 0.9f;
    public float stayInSmoke = 3.0f;
    public Sprite smokeSprite;
    public GameObject smokeUse;

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
        TakeSmoke();
        TakeGiftBox();
    }
    public void CastCollider() {
        cols = Physics2D.OverlapCircleAll(playerStats.player.transform.position, 0.5F);
    }
    public void TakeWater() {
        
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == waterTag) {
                inventory.AddItem(waterId, waterSprite, 1);

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
                inventory.AddItem(energyDrinkId, energyDrinkSprite, 1);

                Destroy(cols[i].gameObject);

            }
        }
    }
    public void UseEnergyDrink() {
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

    public void TakeSmoke() {
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == smokeTag)
            {
                inventory.AddItem(smokeId, smokeSprite, 1);

                Destroy(cols[i].gameObject);

            }
        }
    }

    public void UseSmoke() {
        Debug.Log("Used smoke");
        GameObject usedSmoke;
        usedSmoke = Instantiate(smokeUse, playerStats.player.transform.position, Quaternion.identity);
        Collider2D[] colsSmoke = Physics2D.OverlapCircleAll(usedSmoke.transform.position, smokeRadius);
        
        EnemyAI enemy;
        for (int i = 0; i < colsSmoke.Length; i++)
        {
            if (colsSmoke[i].tag == "Enemy")
            {
                enemy = colsSmoke[i].gameObject.GetComponent<EnemyAI>();
                //Debug.Log("Enemy in smoke");
                enemy.inSmoke = true;
                StartCoroutine(SmokeStopTime(enemy));
                enemy.dangerLvl = 2;
            }
        }
     }

    IEnumerator SmokeStopTime(EnemyAI enemy) {
        yield return new WaitForSeconds(stayInSmoke);
        enemy.inSmoke = false;

    }

    IEnumerator EnergyDrink() {
        yield return new WaitForSeconds(energyDrinkTime);
        StartCoroutine(EnergyDrinkEndEffect());

    }
    
    public void TakeGiftBox()
    {
       
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == giftBoxTag)
            {
                Destroy(cols[i].gameObject);
                int randItem = Random.Range(1, 3);
                Sprite randItemSprite;
                switch(randItem){
                    case 1: randItemSprite = waterSprite; break;
                    case 2: randItemSprite = energyDrinkSprite; break;
                    case 3: randItemSprite = smokeSprite; break;
                    default: randItemSprite = waterSprite; break;
                }
                inventory.AddItem(randItem, randItemSprite, Random.Range(1, 5));
            }
        }

    }
}
