using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petard : MonoBehaviour
{
    private string enemyTag = "Enemy";
    PlayerStats playerStats;
    private float flyTime = 1.5f;
    private float soundArea = 5.0f;
    public GameObject bangGameObject;
    EnemyAI enemyAI;
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
    }

    void Update()
    {
        StartCoroutine(FlyTime());
    }

    private void Boom() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, soundArea);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == enemyTag) {
                enemyAI = colliders[i].GetComponent<EnemyAI>();
                enemyAI.petardName = name;
            }
        }

        Destroy(gameObject);
    }



    IEnumerator FlyTime() {
        yield return new WaitForSecondsRealtime(flyTime);
        Boom();
    }
}
