using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerStats playerStats;
    EnemyAI enemyAI;
    private string enemyTag = "Enemy";
    private float smokeDur = 5.0f;

    void Start()
    {
       // playerStats = FindObjectOfType<PlayerStats>();


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CheckEnemy() {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == enemyTag) {
            enemyAI = collision.GetComponent<EnemyAI>();
            Effect(); }
    }
    private void Effect() {
        Debug.Log("Enemy in smoke");
        enemyAI.StayInSmoke();
        StartCoroutine(smokeDuration());
    }

    IEnumerator smokeDuration() {

        yield return new WaitForSecondsRealtime(smokeDur);
        Destroy(gameObject);
        enemyAI.takeLastVictimPos = true;
        enemyAI.dangerLvl = 3;
    }
}
