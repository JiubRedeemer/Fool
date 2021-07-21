using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearEnemy : MonoBehaviour
{
    EnemyAI enemyAI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") {
            enemyAI = collision.GetComponent<EnemyAI>();
            enemyAI.dangerLvl = 1;
        };
    }
}
