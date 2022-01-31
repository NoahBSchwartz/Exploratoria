using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidCounter : MonoBehaviour
{
    private float asteroidNum = 0f;
    public Text scoreText;
    public Text yearText;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = asteroidNum.ToString();
        yearText.text = (asteroidNum * 3f).ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        asteroidNum += 1f; 
    }
}
