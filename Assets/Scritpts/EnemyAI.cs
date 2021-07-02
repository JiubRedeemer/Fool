using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private int status;
    private int dangerLvl;

    private float speed;
    private float hearArea = 3.0F;
    private float viewArea = 6.0F;

    private Rigidbody2D rb;
    private Player player;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
            }

    private void Update()
    {
       // listening();
        viewing();
    }
    private void listening()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, hearArea);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Player")
            {
                Debug.Log("Catch"); //TODO Logic danger
                Transform victim = colliders[i].transform;
                Debug.Log(victim.position);

            }

        }
    }

    //Debug.Log(colliders[colliders.Length-1].tag);
    private bool detectWall = false;
    private bool niceDistance = false;
    private void viewing()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y), viewArea);
        for (int i = 0; i < hits.Length; i++) {
            Debug.Log(hits[i].collider.tag);
            
            if (hits[i].collider.tag == "Wall") detectWall = true;
            if (hits[i].collider.tag == "Player" && viewArea - hits[i].distance >= 0) niceDistance = true;
        }
        Debug.Log(hits.Length);
        if (!detectWall && niceDistance) Debug.Log("See this asshole!");
        detectWall = false;
        niceDistance = false;
        Debug.DrawRay(transform.position, new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y), Color.red);

    }
}