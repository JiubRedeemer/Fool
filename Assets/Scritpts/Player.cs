using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public Camera cam;
    private Vector3 faceDir;
    Vector3 mousePos;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }
    void Start()
    {
        speed = 2.0F;
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
        TouchEnemy();
    }
    private void TouchEnemy() {
        Collider2D[] cols;
        cols = Physics2D.OverlapCircleAll(transform.position, 0.5F);
        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag == "Enemy")
            {
                Destroy(this.gameObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    protected override void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        if(Input.GetAxisRaw("Horizontal")!=0 || Input.GetAxisRaw("Vertical") != 0) 
        anim.SetBool("isRun", true);
        else anim.SetBool("isRun", false);


    }

    protected override void Rotate()
    {
        faceDir = Input.mousePosition;
        mousePos = cam.ScreenToWorldPoint(faceDir);
        Vector2 lookDir = mousePos - transform.position;


        Debug.DrawRay(transform.position, lookDir, Color.yellow);

        rotationAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        rb.rotation = rotationAngle;

    }

    
}
