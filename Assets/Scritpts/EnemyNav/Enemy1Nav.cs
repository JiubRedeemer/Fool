using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Nav : Patrool
{

    public GameObject agent;
    private EnemyAI enemyAI;
    private int flags;
    private void Awake()
    {

        enemyAI = agent.GetComponent<EnemyAI>();
    }
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        { path.Add(transform.GetChild(i));
            Debug.Log(path[i].transform.position);
        }
        flags = path.Count;

    }
    [SerializeField]
    int navFlagNumber = 0;
    void Update()
    {
        Debug.Log(navFlagNumber);
        Debug.Log(enemyAI.lastVictimPos);

        if (enemyAI.dangerLvl == 3)
        {

            if (enemyAI.takeLastVictimPos) {   newNavFlag(navFlagNumber); navFlagNumber++; enemyAI.takeLastVictimPos = false; }
            if(navFlagNumber == path.Count) navFlagNumber = 0;
        }

    }

    private void newNavFlag(int j) {
        enemyAI.navFlagNumber = j;
        enemyAI.lastVictimPos = path[j].position;
        
    }
}
