using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArrowMover : MonoBehaviour
{
    public float arrowSpeed = 3f;
    private Vector2 target;
    public Rigidbody2D arrowrb;
    private bool hit = false;
    public int arrow = 0;
    public int win = 5;
    // Start is called before the first frame update
    void Start()
    {
        target = new Vector3(transform.position.x, 8f, 0f);
    }
    // Update is called once per frame
    void Update()
    {
          //  Instantiate(asteroid, new Vector3(x * 2.0F, 0, 0), Quaternion.identity);
            //Debug.Log(player.transform.position.x);
           // Debug.Log(Vector3.Distance(transform.position, target));
            float step =  arrowSpeed * Time.deltaTime; // calculate distance to mov
            arrowrb.transform.position = Vector3.MoveTowards(transform.position, target, step);
            if (hit == true)
            {
                if (arrow == 0)
                {
                    if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
                    win = 0;
                }
                if (arrow == 1)
                {
                    if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
                    win = 1;
                }
                if (arrow == 2)
                {
                    if (Input.GetKeyDown("up") || Input.GetKeyDown("w"))
                    win = 2;
                }
                if (arrow == 3)
                {
                    if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
                    win = 3;
                }
            }
            if (win != 5)
            {
                transform.localScale = new Vector3(.8f, .8f, 1f); 
            }
            else
            {
                transform.localScale = new Vector3(.5f, .5f, 1f);
            }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        hit = false;
        Destroy(gameObject);
    }
}
