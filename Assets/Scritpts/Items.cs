using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    PlayerStats playerStats;
  

    string waterTag = "Water";
    private float waterStamina = 20.0F;
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        
        
    }

    void Update()
    {
        Water();
    }
    public void Water() {
        Collider2D[] cols = Physics2D.OverlapCircleAll(playerStats.player.transform.position, 0.5F);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == "Water") { 
                playerStats.Stamina += waterStamina; 
                playerStats.setNormalSpeed();
                Destroy(cols[i].gameObject);
                
            }
        }
    }
}
