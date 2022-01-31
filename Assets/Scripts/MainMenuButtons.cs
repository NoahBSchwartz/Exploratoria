using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject MainMenu;
    public int level = 0;
    public Rigidbody2D portalrb;
    public Rigidbody2D playerrb;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton();
    }

    public void PlayNowButton()
    {
        // Play Now Button has been pressed, here you can initialize your game (For example Load a Scene called GameLevel etc.)
        if (level == 0)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
        if (level == 1)
            {
            portalrb.transform.position = playerrb.transform.position;
            }
    }
    public void MainMenuButton()
    {
        // Show Main Menu
        MainMenu.SetActive(true);
    }
    //public void endButton()
    //{
        // Show Main Menu
      //  UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    //}
    public void QuitButton()
    {
        // Quit Game
        Application.OpenURL("https://noahschwartz.itch.io/exploratoria");
        //Application.Quit();
    }
    void FixedUpdate()
    {
        if ((level == 1) && time >= 80f)
        {
            if (Input.anyKey)
            {
                portalrb.transform.position = playerrb.transform.position;
            }
        }
        if ((level == 5) && time >= 80f)
        {
            if (Input.anyKey)
            {
                Application.Quit();
            }
        }
        time += 1f; 
    }
}