using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // array of spawn points to set enemies at once they appear at the start of each wave 
    public Transform[] SpawnPoints;

    //attractsenemy to destination point 
    public Transform Player;

    //public variables/objects 
    public GameObject EnemyPrefab;
    public GameObject Trap1, Trap2, Trap3;
    public GameObject Spike1, Spike2, Spike3;
    public TextMeshProUGUI WaveText;
    public float TimeBetweenWaves = 10f;
    public int TotalWaves = 6;
    public int EnemiesPerWave = 10;

    //private variables, animator & list of spawned enemy objects
    private float countdown = 0;
    private int waveNumber = 1;
    private Animator animator; 
    private List<GameObject> spawnedEnemies = new();

    void Start()
    {
        //gets animator componenet for UI text & calls update wave function on start 
        animator = GetComponent<Animator>();
        UpdateWaveText(); 
    }
    void UpdateWaveText()
    {
        //if the wave text existsm update it with a string reading "Wave" and add the current wave number to the tmpro UI object
        if (WaveText != null)
        {
            WaveText.text = "Wave " + waveNumber;  
        }
    }

    void Update()
    {

        //win condition: if player deeats all waves set, call win scene function 
        if (waveNumber > TotalWaves)
        {
            WinScene();
        }

        //gets number of enemies killed every frame 
        int enemiesKilled = 0;

        //if any of the enemies no longer exist in the spawned enemies list, add it to enemies killed int
        foreach (GameObject enemy in spawnedEnemies)
        {
            if(enemy == null)
            {
                enemiesKilled++;
                Debug.Log(enemiesKilled); 
            }
        }

        //once the countdown hits 0 and the wave number is still lower than the total waves possible, 
        //start spawning enemies, hide the wave UI text, set the countdown timer back to its original state
        if (countdown <= 0f && waveNumber <= TotalWaves)
        {
            StartCoroutine(SpawnWave());
            animator.SetTrigger("WaveUIDisappear");
            countdown = TimeBetweenWaves;
        }

        //if the amount of enemies killed is equivalent to the amount of enemies spawned, call update wave text function,  
        //start the countdown timer between waves, set the wave UI to trigger its appearance to show the next wave number 
        if (enemiesKilled == spawnedEnemies.Count)
        {
            UpdateWaveText();
            countdown -= Time.deltaTime;
            animator.SetTrigger("WaveUIAppear");
        }

        //the following 4 if statements set traps active and inactive depending on the current wave number.
        //Should have used event systems, but due to time issues, this is the route i went down
        if (waveNumber == 3)
        {
            Trap2.SetActive(false);
        }

        if (waveNumber == 4)
        {
            Trap1.SetActive(false);
            Trap3.SetActive(false);
        }

        if (waveNumber == 5)
        {
            Spike2.SetActive(true);
        }

        if (waveNumber == 6)
        {
            Spike1.SetActive(true);
            Spike3.SetActive(true);
        }

    }

    //spawn wave coroutine, increases wave number, clears the spawned emnemies list, spawns each enemy in a wave one second after eachother 
    IEnumerator SpawnWave()
    {
        waveNumber++;
        spawnedEnemies.Clear();

        for (int i = 0; i < EnemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1f);
        }
        //adds an enemy each wave 
        EnemiesPerWave++;
    }

    //spawns enemies, recieves destination point (player) from move to destination script 
    void SpawnEnemy()
    {
        int spawnPointIndex = Random.Range(0, SpawnPoints.Length);
        Transform spawnPoint = SpawnPoints[spawnPointIndex];
        GameObject spawnedEnemy = Instantiate(EnemyPrefab, spawnPoint.position, spawnPoint.rotation);
        spawnedEnemy.GetComponent<MoveToDestination>().Destination = Player;
        spawnedEnemies.Add(spawnedEnemy);
    }

    //loads the win screen scene, sets cursor back to visible so player can interact with UI button objects
    void WinScene()
    {
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
        SceneManager.LoadScene("WinScene");
    }

}