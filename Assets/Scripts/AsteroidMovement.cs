using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidMovement : MonoBehaviour
{
  // public float speed = 4f;
    private Vector2 target;
    public Rigidbody2D rb;
    private float number = 0f; 
    private float x = 0f;
    private float speed;
    public float score = 0f;
    public Text scoreText;
    public Text scoreText2;
    public Transform asteroid;
    public bool clone = false; 
    public GameObject player;
    public GameObject enemy;
    private bool gameStop = false;
    public Rigidbody2D portalrb;
    public Rigidbody2D playerrb;
    public int level = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb.transform.position = new Vector3(Random.Range(-6.8f, 6.5f), 5.4f, 0.0f);
        target = new Vector3(Random.Range(-6.8f, 7.5f), -6.0f, 0.0f);
        speed = Random.Range(1.5f, 2f);
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        scoreText2.text = score.ToString();
        gameStop = GameObject.Find("MiniPlayer").GetComponent<LevelMovement>().gameStop;
       // rb.transform.position = new Vector3(Random.Range(-5.8f, 6.0f), 3.0f, 0.0f);
        if (rb.transform.position.y < -2f)
        {
            rb.transform.position = new Vector3(Random.Range(-6.8f, 6.5f), 5.4f, 0.0f);
            target = new Vector3(Random.Range(-6.8f, 6.5f), -6.0f, 0.0f);
            //speed += 1f;
            if (x == 0)
            {
                number += 1f; 
                x = 1;
            }
            else
            {
                x = 0;
            }
            score += 1000f;
        }
        else if(player.transform.position.x != 2f)
        {
            //Debug.Log(player.transform.position.x);
            if ((x < number) && (clone == false))
            {
                Instantiate(asteroid, new Vector3(x * 2.0F, 0, 0), Quaternion.identity);
                x += 1f;
            }
            //Debug.Log(player.transform.position.x);
           // Debug.Log(Vector3.Distance(transform.position, target));
            float step =  speed * Time.deltaTime; // calculate distance to move
            rb.transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
        if (gameStop == true)
        {
            enemy.SetActive(false);
        }
    }
}