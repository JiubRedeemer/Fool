using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToFloor3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") collision.transform.position = new Vector3(2, -103, 21);
    }
}
