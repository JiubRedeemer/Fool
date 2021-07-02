using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private int status;
    private int dangerLvl;

    private float speed;
    private float hearArea = 3.0F;

    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        listening();
    }
    private void listening() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, hearArea);
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].tag == "Player") {
                Debug.Log("Catch"); //TODO Logic danger
                Transform victim = colliders[i].transform;
                Debug.Log(victim.position);

            } 

        }


        //Debug.Log(colliders[colliders.Length-1].tag);

        private void viewing()
        {
        }
    }
