using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour
{
    public Sprite danger1;
    public Sprite danger2;
    public Sprite danger3;
    public Image staminaBar;
    public Image peeBar;
    public GameObject dangerLvlGameObject;
    private SpriteRenderer dangerLvlSprite;
    public Player player;
    public float dangerLvl;
    //Speeds
    private float normalSpeed = 2.0F;
    private float lowSpeed = 1.0F;
    private float highSpeed = 3.5F;
    //Stamina
    [SerializeField]
    private float staminaWasteSpeed = 1.0F;
    private float stamina = 100F;
    public float Stamina { get { return stamina; } set { stamina = value; } }
    //Pee
    private float peeLvl;
    private float peeWannaLvl = 100F;
    public float PeeLvl { get { return peeLvl; } set { peeLvl = value; } }


   

    private void Awake()
    {
        player = GetComponent<Player>();
        dangerLvlSprite = dangerLvlGameObject.GetComponentInChildren<SpriteRenderer>();
    }
    void Start()
    {
        staminaBar.fillAmount = 1f;
        peeBar.fillAmount = 0f;
        StartCoroutine(StaminaWaste());

    }

    void Update()
    {
        
        staminaState();
        peeState();
        dangerUI();
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
    public void staminaState() {
        staminaBar.fillAmount = stamina/100;
        if (stamina <= 0) { stamina = 0; setLowSpeed(); }
        if (stamina >= 100) stamina = 100;
    }
    IEnumerator StaminaWaste()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0F);
            stamina -= staminaWasteSpeed;
        }
    }
    private void peeState() {
        peeBar.fillAmount = peeLvl / 100;
        if (peeLvl >= peeWannaLvl) {
            Debug.Log("Wanna pee!!!");
        }
    }
    private void dangerUI() {
        switch (dangerLvl) {
            case 1: dangerLvlSprite.sprite = danger1; break;
            case 2: dangerLvlSprite.sprite = danger2; break;
            case 3: dangerLvlSprite.sprite = danger3; break;
        }
    }
}


















