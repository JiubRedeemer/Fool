using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : Character
{
    private int status;
    public int dangerLvl = 3;

    private float hearArea = 3.0F;
    private float viewArea = 6.0F;
    public Vector3 distPos;
    public Vector3 lastVictimPos;


    private Player player;
    private NavMeshAgent agent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        dangerLvl = 3;

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
                dangerLvl = 2;  //HERE TAKE DANGER LVL
                Transform victim = colliders[i].transform;
                Debug.Log("HEAR");

            }

        }
    }


    private bool detectWall = false;
    private bool niceDistance = false;
    private void viewing()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y), viewArea);
        for (int i = 0; i < hits.Length; i++)
        {


            if (hits[i].collider.tag == "Wall") detectWall = true;
            if (hits[i].collider.tag == "Player" && viewArea - hits[i].distance >= 0) niceDistance = true;
        }

        if (!detectWall && niceDistance)
        {
            Debug.Log("SEE");
            dangerLvl = 1;
        } //HERE TAKE DANGER LVL
        detectWall = false;
        niceDistance = false;
        Debug.DrawRay(transform.position, new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y), Color.red);
    }
    public int navFlagNumber = 0;
    private void Patrool()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 1.0F);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].name == "NavFlag " + "(" + navFlagNumber + ")") { takeLastVictimPos = true; };
        }

        if (!takeLastVictimPos) distPos = lastVictimPos;
        agent.SetDestination(distPos);

        Rotate();
    }
    private void Danger1()
    {
        agent.speed = 2.5F;
        distPos = player.transform.position;
        agent.SetDestination(distPos);
        Rotate();
    }

    public bool takeLastVictimPos = true;
    private void Danger2()
    {
        agent.speed = 2.0F;
        distPos = player.transform.position;
        agent.SetDestination(distPos);

        Rotate();

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


}