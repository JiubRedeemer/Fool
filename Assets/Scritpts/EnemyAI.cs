using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : Character
{
    public int status; // 1-Stay in Flag, 2-Go to Flag, 3 - Go to last Player pos, 4 - Attack, 5 - StayInSmoke
    public int dangerLvl = 3; //1-Attack player, 2-go to last player pos, 3 - Patrool

    private float hearArea = 15.0F;
    private float viewArea = 6.0F;
    private float rotateSpeed = 0.1f;
    private float seeInOnePosTime = 1.0f;
    private float seeInOnePosTimeWaiter;
    private float stayInDanger2ZoneTime = 5.0F;
    private float stayInDanger2ZoneTimeWaiter;

    public string petardTag = "Petard";
    public Vector3 distPos;
    public Vector3 lastVictimPos;


    private PlayerStats playerStats;
    private NavMeshAgent agent;
    private SpriteRenderer statusUI;
    private Transform statusUITransform;
    public Sprite danger1;
    public Sprite danger2;
    public Sprite danger3;

    private bool detectWall = false;
    private bool niceDistance = false;
    public int navFlagNumber = 0;
    public bool takeLastVictimPos = true;


    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        playerStats = FindObjectOfType<PlayerStats>();
        statusUITransform = transform.GetChild(2);
        statusUI = statusUITransform.GetComponentInChildren<SpriteRenderer>();

    }
    private void Start()
    {
        stayInDanger2ZoneTimeWaiter = stayInDanger2ZoneTime;
        seeInOnePosTimeWaiter = seeInOnePosTime;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        dangerLvl = 3;
        playerStats.dangerLvl = dangerLvl;
        Debug.Log("EnemyAiWork");

    }
    private void Update()
    {
        if (status != 5)
        {
            viewing();
            listening();
        }
        SetDanger(dangerLvl);


    }
    private void listening()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, hearArea);

        for (int i = 0; i < colliders.Length; i++)
        {
            Debug.Log(colliders[i].name);

            if (colliders[i].tag == petardTag)
            {
                Debug.Log(petardTag);
                dangerLvl = 2;
                playerStats.dangerLvl = dangerLvl;
                //HERE TAKE DANGER LVL
                Transform victim = colliders[i].transform;
                lastVictimPos = victim.position;
                Debug.Log("HEAR");

            }

        }
    }
    private float rotationToVictimAngle;
    public float fildOfView = 45.0f;

    private void viewing()
    {

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, new Vector2(playerStats.player.transform.position.x - transform.position.x, playerStats.player.transform.position.y - transform.position.y), viewArea);

        rotationToVictimAngle = Vector2.Angle(playerStats.player.transform.position - transform.position, transform.right);

        // Debug.Log(rotationToVictimAngle + "----" + (rotationToVictimAngle<(90.0f- fildOfView) /2)) ;

        for (int i = 0; i < hits.Length; i++)
        {


            if (hits[i].collider.tag == "Wall") detectWall = true;
            if (hits[i].collider.tag == "Player" && viewArea - hits[i].distance >= 0) niceDistance = true;
        }
        if (!detectWall && niceDistance)
        {
            if (rotationToVictimAngle < (90.0f - fildOfView) / 2)
            {
                dangerLvl = 1;
                playerStats.dangerLvl = dangerLvl;
            }

        } //HERE TAKE DANGER LVL
        detectWall = false;
        niceDistance = false;
        Debug.DrawRay(transform.position, new Vector2(playerStats.player.transform.position.x - transform.position.x, playerStats.player.transform.position.y - transform.position.y), Color.red);
    }

    private void Patrool()
    {
        playerStats.dangerLvl = dangerLvl;

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 1.0F);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].name == "NavFlag " + "(" + navFlagNumber + ")")
            {
                takeLastVictimPos = true;

            }
        }

        if (!takeLastVictimPos) distPos = lastVictimPos;
        if (status == 2) agent.SetDestination(distPos);
        if (status == 1) { RandomRotate(); agent.speed = 0; }
        else
            Rotate();
    }
    private void Danger1()
    {
        if (status != 5)
        {

            agent.speed = 2.5F;
            distPos = playerStats.player.transform.position;
            agent.SetDestination(distPos);
            if (status == 1) RandomRotate();
            else
                Rotate();

            status = 4;
        }
    }

    private void Danger2()
    {
        agent.speed = 2.0F;
        distPos = lastVictimPos;
        agent.SetDestination(distPos);

        if ((lastVictimPos - transform.position).magnitude <= 1.0f)
        {
            Debug.Log("Take pos");
            status = 1;
            if ((stayInDanger2ZoneTimeWaiter -= Time.deltaTime) <= 0)
            {
                dangerLvl = 3;
                playerStats.dangerLvl = dangerLvl;

                stayInDanger2ZoneTimeWaiter = stayInDanger2ZoneTime;
            }
        }
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
        //  Debug.Log(seeInOnePosTime);
        if ((seeInOnePosTimeWaiter -= Time.deltaTime) <= 0)
        {
            randAngle = Random.Range(0, 360);

            seeInOnePosTimeWaiter = seeInOnePosTime;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(randAngle, Vector3.forward), rotateSpeed);


    }

    public void StayInSmoke()
    {
        agent.speed = 0.0f;
        fildOfView = 0.0f;
        status = 5;
        Debug.Log("Now it work");
    }

    private void SetDanger(int status)
    {
        switch (status)
        {
            case 1: Danger1(); statusUI.sprite = danger1; break;
            case 2: Danger2(); statusUI.sprite = danger2; break;
            case 3: Danger3(); statusUI.sprite = danger3; break;
            default: Danger3(); break;
        }
    }


}