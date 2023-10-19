using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    //quits the game, added the debug to test in editor 
    public void Quit()
    {
        Debug.Log("Successfully Quit");
        Application.Quit(); 
    }
}
