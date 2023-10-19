using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinemticTrigger : MonoBehaviour
{
    //public variable 
    public bool CanPlayCinematic = true;

    //private objects  
    private PlayableDirector cinematicDirector;
    private ThirdPersonController playerObject;
    
    //on object awake get the director component and stope the cinematic from playing 
    private void Awake()
    {
        cinematicDirector = GetComponent<PlayableDirector>();
        cinematicDirector.stopped += CinematicDirectorStopped; 
    }

    void CinematicDirectorStopped(PlayableDirector CanPlayCinematic)
    {
        //if the cinematic can still play, the player can still control the character 
        if (cinematicDirector == CanPlayCinematic)
        {
            playerObject.canControl = true; 
        }
    }

    //once the player collides with this invisisble collider, the cinematic triggers and the player loses control of their character object for its duration
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && CanPlayCinematic)
        {
            playerObject = other.GetComponent<ThirdPersonController>();

            cinematicDirector.Play(); 

            playerObject.canControl = false;

            CanPlayCinematic = false; 
        }
    }
}
