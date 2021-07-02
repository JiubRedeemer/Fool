using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : Character
{

    private int status;
    private int dangerLvl;

    private float hearArea = 3.0F;
    private float viewArea = 6.0F;
    private Vector3 faceDir;
    private Vector3 distPos;

    private Player player;

    [SerializeField]
    public Transform target;
    private NavMeshAgent agent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
            }

    private void Start()
    {
        target = player.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void Update()
    {
        
        listening();
        if (viewing() != null) { Danger1(); distPos = player.transform.position;
        }

    }
    private void listening()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, hearArea);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Player")
            {
                Debug.Log("Catch"); //TODO Logic danger
                Transform victim = colliders[i].transform;
                Debug.Log(victim.position);

            }

        }
    }

    //Debug.Log(colliders[colliders.Length-1].tag);
    private bool detectWall = false;
    private bool niceDistance = false;
    private Player viewing()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y), viewArea);
        for (int i = 0; i < hits.Length; i++) {
            Debug.Log(hits[i].collider.tag);
            
            if (hits[i].collider.tag == "Wall") detectWall = true;
            if (hits[i].collider.tag == "Player" && viewArea - hits[i].distance >= 0) niceDistance = true;
        }
        Debug.Log(hits.Length);
        if (!detectWall && niceDistance) { return player; }
        detectWall = false;
        niceDistance = false;
        Debug.DrawRay(transform.position, new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y), Color.red);
        return null;
    }

    private void Danger1() {
        agent.SetDestination(target.position);
        Rotate();
    }

    protected override void Rotate()
    {
        faceDir = Input.mousePosition;
        Vector2 lookDir = distPos - transform.position;
        // Debug.Log(lookDir);


        Debug.DrawRay(transform.position, lookDir, Color.yellow);

        rotationAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        rb.rotation = rotationAngle;

    }
}