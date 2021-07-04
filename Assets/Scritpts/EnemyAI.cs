using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : Character
{
    public int status; // 1-Stay in Flag, 2-Go to Flag, 3 - Go to last Player pos, 4 - Attack
    public int dangerLvl = 3;

    private float hearArea = 3.0F;
    private float viewArea = 6.0F;
    public int fov = 180;
    private float rotateSpeed = 0.1f;
    private float seeInOnePosTime = 1.0f;
    private float seeInOnePosTimeWaiter;

    public Vector3 distPos;
    public Vector3 lastVictimPos;


    private PlayerStats playerStats;
    private NavMeshAgent agent;

    private bool detectWall = false;
    private bool niceDistance = false;
    public int navFlagNumber = 0;
    public bool takeLastVictimPos = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = FindObjectOfType<PlayerStats>();
    }
    private void Start()
    {
        seeInOnePosTimeWaiter = seeInOnePosTime;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        dangerLvl = 3;
        playerStats.dangerLvl = dangerLvl;


    }
    private void Update()
    {
        viewing();
        listening();
        switch (dangerLvl)
        {
            case 1: Danger1(); break;
            case 2: Danger2(); break;
            case 3: Danger3(); break;
            default: Danger3(); break;

        }

    }
    private void listening()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, hearArea);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Player" && Input.GetButtonDown("Fire1"))
            {
                dangerLvl = 2;
                playerStats.dangerLvl = dangerLvl;
                //HERE TAKE DANGER LVL
                Transform victim = colliders[i].transform;
                lastVictimPos = victim.position;
                Debug.Log("HEAR");

            }

        }
    }

    private void viewing()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, new Vector2(playerStats.player.transform.position.x - transform.position.x, playerStats.player.transform.position.y - transform.position.y), viewArea);
        for (int i = 0; i < hits.Length; i++)
        {


            if (hits[i].collider.tag == "Wall") detectWall = true;
            if (hits[i].collider.tag == "Player" && viewArea - hits[i].distance >= 0) niceDistance = true;
        }

        if (!detectWall && niceDistance)
        {
            Debug.Log("SEE");
            dangerLvl = 1;
            playerStats.dangerLvl = dangerLvl;

        } //HERE TAKE DANGER LVL
        detectWall = false;
        niceDistance = false;
        Debug.DrawRay(transform.position, new Vector2(playerStats.player.transform.position.x - transform.position.x, playerStats.player.transform.position.y - transform.position.y), Color.red);
    }

    private void Patrool()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 1.0F);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].name == "NavFlag " + "(" + navFlagNumber + ")")
            {
                takeLastVictimPos = true;

            }
        }

        if (!takeLastVictimPos) distPos = lastVictimPos;
        agent.SetDestination(distPos);
        if (status == 1) RandomRotate();
        else
            Rotate();
    }
    private void Danger1()
    {
        agent.speed = 2.5F;
        distPos = playerStats.player.transform.position;
        agent.SetDestination(distPos);
        if (status == 1) RandomRotate();
        else
            Rotate();
        status = 4;
    }

    private void Danger2()
    {
        agent.speed = 2.0F;
        distPos = lastVictimPos;
        agent.SetDestination(distPos);

        if (status == 1) RandomRotate();
        else
            Rotate();
        status = 3;

    }
    private void Danger3()
    {
        agent.speed = 1.5F;
        Patrool();
    }
    protected override void Rotate()
    {
        Vector2 lookDir = distPos - transform.position;

        Debug.DrawRay(transform.position, lookDir, Color.yellow);
        rotationAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = rotationAngle;

    }
    float randAngle = 0;

    private void RandomRotate()
    {
        Debug.Log(seeInOnePosTime);
        if ((seeInOnePosTimeWaiter -= Time.deltaTime) <= 0)
        {
            randAngle = Random.Range(0, 360);

            seeInOnePosTimeWaiter = seeInOnePosTime;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(randAngle, Vector3.forward), rotateSpeed);


    }


}