using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Camera cam;
    private Vector3 faceDir;
    Vector3 mousePos;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        speed = 2.0F;
    }

    void Update()
    {
        
        
        
    }


    private void FixedUpdate()
    {
        Move();
        Rotate();
    }
    protected override void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    protected override void Rotate()
    {
        faceDir = Input.mousePosition;
        mousePos = cam.ScreenToWorldPoint(faceDir);
        Vector2 lookDir = mousePos - transform.position;
        // Debug.Log(lookDir);


        Debug.DrawRay(transform.position, lookDir, Color.yellow);

        rotationAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        rb.rotation = rotationAngle;

    }

    
}
