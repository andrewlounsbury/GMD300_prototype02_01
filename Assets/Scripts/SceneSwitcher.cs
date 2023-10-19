using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    //allows for scene name string to be set in the inspector
    [SerializeField]
    private string sceneName; 

    //swithes to the scene set in the inspectoor under the game manager object. Used mostly with button interactions for this prototype. 
    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}