using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed;
    protected float rotationAngle;

    protected Rigidbody2D rb;
    protected SpriteRenderer sprite;

    protected Vector2 movement;
    protected Vector2 lookDir;

    protected virtual void Move() { }


    protected virtual void Rotate() { }



}
