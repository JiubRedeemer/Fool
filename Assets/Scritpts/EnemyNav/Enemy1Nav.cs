using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Nav : Patrool
{

    public float timeInFlag = 5.0f;
    public bool stayInFlag;
    float waiter;
    public GameObject agent;
    private EnemyAI enemyAI;
    private int flags;
    private void Awake()
    {
        enemyAI = agent.GetComponent<EnemyAI>();
    }
    void Start()
    {
        waiter = timeInFlag;
        for (int i = 0; i < transform.childCount; i++)
        {
            path.Add(transform.GetChild(i));
            Debug.Log(path[i].transform.position);
        }
        flags = path.Count;

    }
    [SerializeField]
    int navFlagNumber = 0;
    void Update()
    {

        

    }
    private void FixedUpdate()
    {
        if (enemyAI.dangerLvl == 3)
        {

            if (enemyAI.takeLastVictimPos)
            {
                Debug.Log("Work");

                enemyAI.status = 1;
                if ((waiter -= Time.deltaTime) <= 0)
                {
                    newNavFlag(navFlagNumber); enemyAI.takeLastVictimPos = false;
                    enemyAI.status = 2;
                }
            }
            if (navFlagNumber == flags) navFlagNumber = 0;
        }
    }
    private void newNavFlag(int j)
    {
        waiter = timeInFlag;
        enemyAI.navFlagNumber = j;
        enemyAI.lastVictimPos = path[j].position;
        navFlagNumber++;

    }
}
