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
    int j = 0;
    void Update()
    {
        Debug.Log(j);
        Debug.Log(enemyAI.lastVictimPos);

        if (enemyAI.dangerLvl == 3)
        {

            if (enemyAI.takeLastVictimPos) {   newNavFlag(j); j++; enemyAI.takeLastVictimPos = false; }
            if(j == path.Count) j = 0;
        }

    }

    private void newNavFlag(int j) {
        enemyAI.j = j;
        enemyAI.lastVictimPos = path[j].position;
        
    }
}
