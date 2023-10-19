using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; 

public class PauseMenu : MonoBehaviour
{
    //gets panel object from inspector to be set active or disabled 
    public GameObject PausePanel; 

    void Update()
    {
        //if player presses escape, pause menu is brought up. Couldnt figure out input action mapping for this one for some reason
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(); 
        }
    }

    public void Pause()
    {
        //enables cursor to be able to interact with menu buttons, freezes gamespace timing to pause game, sets menu panel to active
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
        Time.timeScale = 0;
        PausePanel.SetActive(true); 
    }

    public void Resume()
    {
        //reverses everything in the pause function, enables time in the gamespace to resume game, hides the cursor (unlike fall guys), and disables menu panel 
        Time.timeScale = 1.0f; 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PausePanel.SetActive(false);
    }

    public void MainMenu()
    {
        // sets game time to normal and brings the player back to the main menu at any point during gameplay 
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        //quits game on the spot
        Debug.Log("Quit Successfully");
        Application.Quit();
    }

}
