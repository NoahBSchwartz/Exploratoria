using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Plaques : MonoBehaviour
{
    public GameObject plaqueCanvas;
    public GameObject plaque;
    public bool education = false;
    private bool plaqueGo = false; 
    public Text educationText; 
    private static bool turnOff = false;
    static List<float> plaques;
    public bool isInstructions = false;
    private static bool instructionsGo = true;
    public bool moreInstructions = false;
    private static float educationScore = 0f;
    public bool plaqueController = false;
    public bool arcadePlaque = false;
    private bool levelDone = true;
    public bool actualPlaque = true;
    public bool endScreen = false;
    private bool multiplier = true;
    public float educationScoreNum;
    private bool plaqueStart = false;
    private float playerPos;
    private bool inCollider = false;
    //public bool plaqueSound = true;
    public AudioSource plaqueSound;
    // Start is called before the first frame update
    void Start()
    {
        plaqueCanvas.SetActive(false);
        plaque.SetActive(false);
        plaques = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {
        educationScoreNum = educationScore;
        if ((endScreen == true) && (multiplier == true))
        {
            educationScore = educationScore * 100f;
            educationText.text = educationScore.ToString();
            multiplier = false;
        }
        if (plaqueController == true)
        {
            educationText.text = educationScore.ToString();
        }
        if (plaqueGo == true)
        {
            education = true;
        }
        else
        {
            education = false;
        }
        plaqueGo = false;
        if ((isInstructions == true) && (instructionsGo == true)) //&& (SceneManager.GetActiveScene().buildIndex == "Level1"))
        {
            plaqueCanvas.SetActive(true);
            plaque.SetActive(true);
            if((Input.GetAxis("Horizontal") > 0) || (Input.GetAxis("Vertical") > 0))
            {
                plaqueCanvas.SetActive(false);
                plaque.SetActive(false);
                isInstructions = false;
            }
        }
        if ((moreInstructions == true) && ((Input.GetAxis("Horizontal") > 0) || (Input.GetAxis("Vertical") > 0)))
        {
            plaqueCanvas.SetActive(true);
            plaque.SetActive(true);
        }
        turnOff = GameObject.Find("Player").GetComponent<LevelMovement>().plaqueHit;
        if ((turnOff == true) && (moreInstructions == true))
        {
            plaqueCanvas.SetActive(false);
            plaque.SetActive(false);
            instructionsGo = false;
        }
        levelDone = GameObject.Find("Player").GetComponent<LevelMovement>().levelDone;
        plaqueStart = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.name == "Player") && (arcadePlaque == false))
        {
            plaqueCanvas.SetActive(true);
            plaque.SetActive(true);
            plaqueGo = true;
            if (plaques.Contains(transform.position.x))
            {
            }
            else
            {
                if (actualPlaque == true)
                {
                    educationScore += 1f;
                }
                plaques.Add(transform.position.x);
                plaqueGo = true;
                plaqueSound.Play();
            }
        }
        else if ((levelDone == false) && (collision.gameObject.name == "Player"))
        {
            plaqueGo = true;
            plaqueCanvas.SetActive(true);
            plaque.SetActive(true);
            if (plaques.Contains(transform.position.x))
            {
            }
            else
            {
                if (actualPlaque == true)
                {
                    educationScore += 1f;
                }
                plaques.Add(transform.position.x);
                plaqueGo = true;
                plaqueSound.Play();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
        plaqueCanvas.SetActive(false);
        plaque.SetActive(false);
        plaqueStart = false;
        }
    }
}
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Plaques : MonoBehaviour
{
    public GameObject plaqueCanvas;
    public GameObject plaque;
    public bool education = false;
    private bool plaqueGo = false; 
    public Text educationText; 
    private static bool turnOff = false;
    static List<float> plaques;
    public bool isInstructions = false;
    private static bool instructionsGo = true;
    public bool moreInstructions = false;
    private static float educationScore = 0f;
    public bool plaqueController = false;
    public bool arcadePlaque = false;
    private bool levelDone = true;
    public bool actualPlaque = true;
    public bool endScreen = false;
    private bool multiplier = true;
    public float educationScoreNum;
    private bool plaqueStart = false;
    private float playerPos;
    private bool inCollider = false;
    //public bool plaqueSound = true;
    public AudioSource plaqueSound;
    // Start is called before the first frame update
    void Start()
    {
        plaqueCanvas.SetActive(false);
        plaque.SetActive(false);
        plaques = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetAxis("Horizontal") > 0) || (Input.GetAxis("Vertical") > 0))
        {
           plaqueStart = true;
        }
        if ((plaqueStart == false) && (isInstructions == false) && (moreInstructions == false))
        {
            plaqueCanvas.SetActive(false);
            plaque.SetActive(false);
        }
        educationScoreNum = educationScore;
        if ((endScreen == true) && (multiplier == true))
        {
            educationScore = educationScore * 100f;
            educationText.text = educationScore.ToString();
            multiplier = false;
        }
        if (plaqueController == true)
        {
            educationText.text = educationScore.ToString();
        }
        if (plaqueGo == true)
        {
            education = true;
        }
        else
        {
            education = false;
        }
        plaqueGo = false;
        if ((isInstructions == true) && (instructionsGo == true)) //&& (SceneManager.GetActiveScene().buildIndex == "Level1"))
        {
            plaqueCanvas.SetActive(true);
            plaque.SetActive(true);
            if((Input.GetAxis("Horizontal") > 0) || (Input.GetAxis("Vertical") > 0))
            {
                plaqueCanvas.SetActive(false);
                plaque.SetActive(false);
                isInstructions = false;
            }
        }
        if ((moreInstructions == true) && ((Input.GetAxis("Horizontal") > 0) || (Input.GetAxis("Vertical") > 0)))
        {
            plaqueCanvas.SetActive(true);
            plaque.SetActive(true);
        }
        turnOff = GameObject.Find("Player").GetComponent<LevelMovement>().plaqueHit;
        if ((turnOff == true) && (moreInstructions == true))
        {
            plaqueCanvas.SetActive(false);
            plaque.SetActive(false);
            instructionsGo = false;
        }
        levelDone = GameObject.Find("Player").GetComponent<LevelMovement>().levelDone;
        plaqueStart = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        inCollider = true;
        if (((collision.gameObject.name == "Player")) && (arcadePlaque == false) && (plaqueStart == true) || (inCollider == true))
        {
            plaqueGo = true;
            plaqueCanvas.SetActive(true);
            plaque.SetActive(true);
            if (plaques.Contains(transform.position.x))
            {
            }
            else
            {
                if (actualPlaque == true)
                {
                    educationScore += 1f;
                }
                plaques.Add(transform.position.x);
                plaqueGo = true;
                plaqueSound.Play();
            }
        }
        else if (((levelDone == false) && (plaqueStart == true) || (inCollider == true)) && (collision.gameObject.name == "Player"))
        {
            plaqueGo = true;
            plaqueCanvas.SetActive(true);
            plaque.SetActive(true);
            if (plaques.Contains(transform.position.x))
            {
            }
            else
            {
                if (actualPlaque == true)
                {
                    educationScore += 1f;
                }
                plaques.Add(transform.position.x);
                plaqueGo = true;
                plaqueSound.Play();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inCollider = false;
        if (collision.gameObject.name == "Player")
        {
        plaqueCanvas.SetActive(false);
        plaque.SetActive(false);
        plaqueStart = false;
        }
    }
}
*/