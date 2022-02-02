using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMovement : MonoBehaviour
{
    //public means you can change the move speed in the editor
    Renderer pend;
    public float moveSpeed = 10f;
    private float deaths = 0f;
    public bool endScreen = false;
    public GameObject screenEnd;
    public bool gameStop = false;
    private bool education = false;
    private float score = 0f;
    private bool inMiniGame = false;
    public bool inLevel = false;
    public bool fireActive = false;
    private bool loadLevel = false;
    private int stopper = 2; 
    public bool inMini3 = false;
    private int levelLoaded = 0;
    private static float education2 = 0f;
    public float education2Num = 0f;
    private static float finalScore = 0f;
    public float finalScoreNum = 0f;
    private float stopperPosition = 0f;
    public GameObject enemy; 
    public GameObject directions;
    public GameObject background;
    public bool plaqueHit = false;
    private float time = 0f;
    //Creating class for animator (used for different conditions to trigger certain animations)
    public Animator animator;
    //this accesses the ball's physics system
    public Rigidbody2D rb;
    //this is the variable that we use to modify the ball movement system
    Vector2 movement;
    static Vector3 spawnPos;
    public bool level1Dooone = false;
    //make variables to check if minigame is complete to unlock the door (static means that even if the scene changes, the variables won't revert back to their starting value)
    static bool level1Done = false;
    static bool level2Done = false;
    static bool level3Done = false;
    //make variables for the players starting position (because they aren't static, they'll reset with every scene change)
    public float xPos = -20.41f;
    public float yPos = 3.85f;
    //make temporary variables for the player's starting position and wether they died or not(they won't reset so we can change them and set them equal to xPos or yPos when we want to)
    static float xTemp;
    static float yTemp;
    static bool dead = false;
    public Text deathsText;
    public Text endText;
    private bool directionOn = true;
    public Text scoreText;
    private bool display = true;
    private static bool inMini = false; 
    public GameObject timer; 
    public GameObject deathCanvas; 
    //make sound effects
    public AudioSource machine;
    public AudioSource portal;
    public AudioSource door;
    public AudioSource death;
    public AudioSource win;
    public AudioSource plaque;
    static bool levelDoneCom = false;
    public bool levelDone = false;
    static bool mini3 = false;
    public float playerPos;
    // Start is called before the first frame update
    void Start()
    {
        if(level1Done == true) 
        {
            xPos = xTemp;
            yPos = yTemp;
        }
        else if(level2Done == true)
        {
            xPos = xTemp;
            yPos = yTemp;
        }
        else if(level3Done == true)
        {
            xPos = xTemp;
            yPos = yTemp;
        }
        //set where the player will respawn
        spawnPos = new Vector3(xPos, yPos, 7);
        //respawn the player in that location (add 2 to the x value so that the player doesn't spawn right on top of the machine)
        rb.transform.position = spawnPos + new Vector3(2f, 0f, 0f);
        timer.SetActive(false);
        deathCanvas.SetActive(false);
        if (level1Done == true)
        {
            portal.Play();
        }
    }
    // Update is called once per frame
    void Update()
    {
        playerPos = transform.position.x;
        finalScoreNum = finalScore;
        education2Num = education2;
        if (mini3 == false)
            levelDone = level1Done;
        else
            levelDone = level3Done;
        deathsText.text = deaths.ToString();
        endText.text = score.ToString();
        score = 3756f;
        if (inMini3 == true)
        {
            score = GameObject.Find("Miner").GetComponent<CaveMaker>().score;
        }
        if ((endScreen == true) && (display == true))
        {
            finalScore = finalScore;
            scoreText.text = finalScore.ToString();
            education2 = education2 * 100f;
            deathsText.text = education2.ToString();
            display = false;
        }
        else
        {
            scoreText.text = score.ToString();
            deathsText.text = deaths.ToString();
        }
        if (inLevel == true)
        {
        }
        if (education == true)
        {
            education2 += 500f;
            education = false;
        }
        if ((transform.position.x >= 40f) && (transform.position.x <= 41f))
        {
            finalScore += score;
            transform.position = new Vector3(39f, -2f, 0f);
        }
        //modify the movement variable with the arrow keys (that's what horizontal and vertical stand for)
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //Open menu if escape is pressed
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene("Menu");
        }
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        inMini = true;
        if (inMini == true)  
        { 
        if (((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0) || (Input.GetKeyDown("space"))) && ((directionOn == true) && (inMini3 == false)) && (time >= 80f))
        {
            directions.SetActive(false);
            background.SetActive(false);
            deathCanvas.SetActive(true);
            directionOn = false;
            timer.SetActive(true);   
        }
        else if ((inMini3 == true) && (Input.GetKeyDown("space")) && (time >= 80f))
        {
            directions.SetActive(false);
            background.SetActive(false);
            deathCanvas.SetActive(true);
            directionOn = false;
            timer.SetActive(true); 
        }
        if (Input.GetKeyDown("space") && (loadLevel == true))
        {
            if (levelLoaded == 1)
            {
                  UnityEngine.SceneManagement.SceneManager.LoadScene("MiniGame1");
                  loadLevel = false;
            }
            if (levelLoaded == 2)
            {
                  UnityEngine.SceneManagement.SceneManager.LoadScene("MiniGame2");
                  loadLevel = false;
            }
            if (levelLoaded == 3)
            {
                  UnityEngine.SceneManagement.SceneManager.LoadScene("MiniGame3");
                  loadLevel = false;
            }
            if (levelLoaded == 4)
            {
                    fireActive = true;
                    loadLevel = false;
                    level2Done = true;
            }
            inMiniGame = true;
        }
        }
    }
    //FixedUpdate is called 50 times per second regardless of frame rate (better for things like physics)
    void FixedUpdate()
    {
        //change the player position using it's physics system * arrow keys * speed * fixed time (time makes player move at same speed no matter what framerate)
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        time += 1f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Statue"))
        {
            pend = collision.GetComponent<Renderer>();
            if((collision.transform.position.y - rb.transform.position.y) < 0)
            {
                stopperPosition = pend.bounds.min.y + 1f;
                stopper = 0;
                rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, 0f);
            }
            if((collision.transform.position.y - rb.transform.position.y) > 0)
            {
                stopperPosition = pend.bounds.max.y - .7f;
                stopper = 1;
                rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, 0f);
            }
        }
        if ((collision.gameObject.tag == "Statue1"))
        {
            pend = collision.GetComponent<Renderer>();
            if((pend.bounds.min.y - rb.transform.position.y) < 0)
            {
            rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, 0f);
            }
            if((pend.bounds.min.y - rb.transform.position.y) > 0)
            {
            rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, -2f);
            }
        }
        //If player is touching arcade cabinet and it hasn't already finished the miniGame, load in the scene
        if ((collision.gameObject.name == "Machine1") && (level1Done == false))
        {
            levelLoaded = 1;
            loadLevel = true;
            //save the players x and y coordinates so we can spawn them back into the level at this position later
            xTemp = transform.position.x;
            yTemp = transform.position.y;
            inMini = true;
            machine.Play();
        }
        //if player touches portal, bring them back to the level and set that they've completed the minigame
        if (collision.gameObject.name == "Portal1")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
            level1Done = true;
            portal.Play();
            levelDoneCom = true;
        }
        //if player touches the door and they've completed the minigame, load in the next level
        if ((collision.gameObject.name == "Door1") && (level1Done == true))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level2");
            //set level 1 to false so that it doesn't interfere with the player respawning logic 
            level1Done = false;
            door.Play();
        }
        //do this a bunch more times
        if ((collision.gameObject.name == "Machine2"))
        {
            levelLoaded = 2;
            loadLevel = true;
            xTemp = transform.position.x;
            yTemp = transform.position.y;
            inMini = true;
            machine.Play();
        }
        if (collision.gameObject.name == "Portal2")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level2");
            level2Done = true;
            portal.Play();
        }
        if ((collision.gameObject.name == "Door2") && (level2Done == true))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level3");
            level2Done = false;
            door.Play();
        }
        if ((collision.gameObject.name == "Machine3"))
        {
            mini3 = true;
            levelLoaded = 3;
            loadLevel = true;
            xTemp = transform.position.x;
            yTemp = transform.position.y;
            inMini = true;
            machine.Play();
        }
        if (collision.gameObject.name == "Portal3")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level3");
            level3Done = true;
            portal.Play();
        }
        if ((collision.gameObject.name == "Door3") && (level3Done == true))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
            win.Play();
        }
        //if player hits an enemy, reload the scene that it's in 
        if ((collision.gameObject.tag == "Enemy"))
        {
            death.Play();
            enemy.SetActive(false);
            gameStop = true;
            screenEnd.SetActive(true);
            rb.transform.position = new Vector3(40f, -2f, 0f);
        }
         if ((collision.gameObject.tag == "Plaque"))
        {
            plaqueHit = true;
        }
         if ((collision.gameObject.name == "Control"))
        {
            levelLoaded = 4;
            loadLevel = true;
            inMini = true;
            level2Done = true;
        }
    }
    //the level variables will only be true when the player is touching the object so set it to true even after the player has left 
     private void OnTriggerExit2D(Collider2D collision)
     {
        stopper = 2;
        if ((collision.gameObject.name == "Machine1") && (level1Done == false))
        {
            xTemp = transform.position.x;
            yTemp = transform.position.y;
        }
        if ((collision.gameObject.name == "Machine2") && (level2Done == false))
        {
            xTemp = transform.position.x;
            yTemp = transform.position.y;
        }
        if ((collision.gameObject.name == "Machine3") && (level3Done == false))
        {
            xTemp = transform.position.x;
            yTemp = transform.position.y;
        }
        if ((collision.gameObject.name == "Enemy"))
        {
            death.Play();
            deaths += 1;
            dead = true;
        }
        if (collision.gameObject.name == "Portal1")
        {
            level1Done = true;
            levelDoneCom = true;
        }
         if (collision.gameObject.name == "Portal2")
        {
            level2Done = true;
        }
         if (collision.gameObject.name == "Portal3")
        {
            level3Done = true;
        }
     }
}

