using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
public class PlayerStats : MonoBehaviour
{
    public Sprite danger1;
    public Sprite danger2;
    public Sprite danger3;
    public Image staminaBar;
    public Image flashLightBar;
    public GameObject dangerLvlGameObject;
    public GameObject flashLight;
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
    //FlashLight
    public float flashLightLevel = 100f;
    private float flashChargeWasted = 0.0f;
    private float flashLightWasteSpeed = 1F;
    public Light2D flashLightLight;

    public float PeeLvl { get { return flashLightLevel; } set { flashLightLevel = value; } }




    private void Awake()
    {
        player = GetComponent<Player>();
        dangerLvlSprite = dangerLvlGameObject.GetComponentInChildren<SpriteRenderer>();
        flashLightLight = flashLight.GetComponent<Light2D>();

    }
    void Start()
    {
        staminaBar.fillAmount = 1f;
        flashLightBar.fillAmount = 1f;
        StartCoroutine(StaminaWaste());

    }

    void Update()
    {

        staminaState();
        flashLightState();
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
    public void staminaState()
    {
        staminaBar.fillAmount = stamina / 100;
        if (stamina <= 0) { stamina = 0; setLowSpeed(); }
        if (stamina >= 100) stamina = 100;
    }
    IEnumerator StaminaWaste()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0F);
            stamina -= staminaWasteSpeed;
            flashLightLevel -= flashLightWasteSpeed;
        }
    }
    private void flashLightState()
    {
        flashLightBar.fillAmount = flashLightLevel / 100;
        if (flashLightLevel <= 0)
        {
            flashLightLevel = 0; 
            Debug.Log("FlashLightIsEmpty");
            flashLightLight.intensity = 0.1f;
        }
        if (flashLightLevel >= 100) flashLightLevel = 100;
    }
    private void dangerUI()
    {
        switch (dangerLvl)
        {
            case 1: dangerLvlSprite.sprite = danger1; break;
            case 2: dangerLvlSprite.sprite = danger2; break;
            case 3: dangerLvlSprite.sprite = danger3; break;
        }
    }
}


















