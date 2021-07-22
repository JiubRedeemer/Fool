using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petard : MonoBehaviour
{
    private string enemyTag = "Enemy";
    PlayerStats playerStats;
    private float flyTime = 1.5f;
    private float animTime = 1.0f;
    private float soundArea = 15.0f;
    private float petardFlyForce = 6.0f;
    public GameObject bangGameObject;
    private Animator anim;
    EnemyAI enemyAI;
    Rigidbody2D rb;
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(playerStats.player.transform.right * petardFlyForce, ForceMode2D.Impulse);
    }

    void Update()
    {
        StartCoroutine(FlyTime());
    }

    private void Boom()
    {
        Debug.Log("Boom!!!");
        Destroy(gameObject);
    }

    IEnumerator AnimTime()
    {
        anim.SetBool("BangActive", true);
        yield return new WaitForSecondsRealtime(animTime);
        Boom();
            }

    IEnumerator FlyTime()
    {
        yield return new WaitForSecondsRealtime(flyTime);
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        transform.rotation = Quaternion.identity;
        StartCoroutine(AnimTime());
    }
}
