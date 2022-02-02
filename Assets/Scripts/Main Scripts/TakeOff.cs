using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeOff : MonoBehaviour
{
    public float speed = 1.0f; //how fast it shakes
    //public float amount = 0f; //how much it shakes
    public Rigidbody2D rb;
    private int x = 1;
    private Vector2 startingPos;
    private Vector2 target;
    public float rocketSpeed = 2f;
    public float xCoord = 0f;
    public float yCoord = 20f;
    public bool isFire = false;
    public bool fireActive = false;
    private float scale = .1f;
    private bool smokeOn = true;
    public GameObject fire;
    public GameObject fire2;
    public AudioSource smoke;
    // Start is called before the first frame update
    void Start()
    {
        target = new Vector3(xCoord, yCoord, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        fireActive = GameObject.Find("Player").GetComponent<LevelMovement>().fireActive;
        if (fireActive == true)
        {
        if (x == 1)
        {
            startingPos.x = transform.position.x;
            startingPos.y = transform.position.y;
            x = 0;
        }
        else 
        {
            x = 1;
        }
        rb.transform.position = new Vector3(startingPos.x + Mathf.Sin(Time.time * speed) * Random.Range(.002f, .007f), startingPos.y + Mathf.Sin(Time.time * speed) * Random.Range(.002f, .007f), 0f);
        if (Vector3.Distance(transform.position, target) > 0.001f)
        {
            float step =  rocketSpeed * Time.deltaTime; // calculate distance to move
            rb.transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
        fire.SetActive(true);
        fire2.SetActive(true);
        if (smokeOn == true)
        {
            smoke.Play();
            smokeOn = false;
        }
        //fire.transform.localScale = fire.transform.localScale * scale;
        //scale += .1f;
        }
    }
}
