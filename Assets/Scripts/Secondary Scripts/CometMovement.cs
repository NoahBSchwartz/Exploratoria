using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CometMovement : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 target;
    public Rigidbody2D rb;
    private float number = 0f; 
    private float x = 0f;
    public Transform asteroid;
    public bool clone = false; 
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb.transform.position = new Vector3(Random.Range(-5.8f, 6.0f), 5.4f, 0.0f);
        target = new Vector3(Random.Range(-5.8f, 6.0f), -6.0f, 0.0f);
    }
    // Update is called once per frame
    void Update()
    {
       // rb.transform.position = new Vector3(Random.Range(-5.8f, 6.0f), 3.0f, 0.0f);
        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            rb.transform.position = new Vector3(Random.Range(-5.8f, 6.0f), 5.4f, 0.0f);
            target = new Vector3(Random.Range(-5.8f, 6.0f), -6.0f, 0.0f);
            speed += .3f;
            number += 1f; 
        }
        else if(player.transform.position.x != 2)
        {
            if ((x < number) && (clone == false))
            {
                Instantiate(asteroid, new Vector3(0, 0, 0), Quaternion.identity);
                x += 1f;
            }
            //Debug.Log(player.transform.position.x);
           // Debug.Log(Vector3.Distance(transform.position, target));
            float step =  speed * Time.deltaTime; // calculate distance to move
            rb.transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
    }
}