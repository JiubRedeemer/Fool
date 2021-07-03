using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Player player;
    private float stamina = 10F;
    [SerializeField]
    private float staminaWasteSpeed = 1.0F;
    public float Stamina { get { return stamina; } set { stamina = value; } }

    private float normalSpeed = 2.0F;
    private float lowSpeed = 1.0F;
    private float highSpeed = 3.0F;
 
    private void Awake()
    {
        player = GetComponent<Player>();
    }
    void Start()
    {
        StartCoroutine(StaminaWaste());

    }

    void Update()
    {
        Debug.Log(stamina);
        if (stamina <= 0) { stamina = 0; setLowSpeed(); }
    }



    public void setHighSpeed()
    {
        player.speed = highSpeed;
    }
    public void setNormalSpeed()
    {
        player.speed = normalSpeed;
    }
    public void setLowSpeed()
    {
        player.speed = lowSpeed;
    }

    IEnumerator StaminaWaste()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0F);
            stamina -= staminaWasteSpeed;
        }
    }
}




