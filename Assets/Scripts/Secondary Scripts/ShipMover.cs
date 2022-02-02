using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMover : MonoBehaviour
{
    Vector2 movement;
    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    private Vector2 target;
    // Start is called before the first frame update
    void Start()
    {
        target = new Vector3(0f, 0f, 0f);
    }
    // Update is called once per frame
    void Update()
    {
          //movement.x = 0;
          //movement.y = 0;
        if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
        {
            target = new Vector3(-5f, -3f, 0f);
        }
        if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
        {
           target = new Vector3(-1.5f, -7f, 0f);
        }
        if (Input.GetKeyDown("up") || Input.GetKeyDown("w"))
        {
            target = new Vector3(-1.5f, 1f, 0f);
        }
        if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
        {
           target = new Vector3(1.5f, -3f, 0f);
        }
    }
    void FixedUpdate()
    {
       float step =  moveSpeed * Time.deltaTime; 
       rb.transform.position = Vector3.MoveTowards(transform.position, target, step);
    }
}
