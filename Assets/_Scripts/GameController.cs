using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//GameController
//Andrew Trinidad
//Last Modified: Oct 5, 2019
//Program Description: This controller controls the main logic of the game.
//From the waves, UI elements, Scene Management, etc

public class GameController : MonoBehaviour
{
    //Enemy wave settings
    [Header("Wave Settings")]
    public GameObject Enemy;
    public Vector2 spawn;
    public int enemyCount;
    public float startWait;
    public float spawnWait;
    public float waveWait;

    //Sound Files
    [Header("Audio Sources")]
    public SoundClip activeSoundClip;
    public AudioSource[] audioSources;

    //Diamond settings 
    //(Diamonds borrow some variables from Enemy save settings)
    [Header("Diamond Settings")]
    public GameObject Diamond;
    public Vector2 diamondSpawn;
    public int diamondCount;

    //Scoreboard variables
    [Header("Scoreboard")]
    [SerializeField]
    private int _lives;

    [SerializeField]
    private int _score;

    public Text livesLabel;
    public Text scoreLabel;
    public Text highScoreLabel;

    public GameObject highScore;

    [Header("UI Control")]
    public GameObject startLabel;
    public GameObject startButton;
    public GameObject endLabel;
    public GameObject restartButton;

    //public properties
    public int Lives
    {
        get
        {
            return _lives;
        }

        set
        {
            _lives = value;
            if (_lives < 1)
            {

                SceneManager.LoadScene("End");
            }
            else
            {
                livesLabel.text = "Lives: " + _lives.ToString();
            }

        }
    }

    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;

            if (highScore.GetComponent<HighScore>().score < _score)
            {
                highScore.GetComponent<HighScore>().score = _score;
            }
            scoreLabel.text = "Score: " + _score.ToString();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObjectInitialization();
        SceneConfiguration();
        //Keeps highScore in the scene to later be found by other scenes.
        DontDestroyOnLoad(highScore);
    }

    //Initialize various variables by finding GameObjects.
    private void GameObjectInitialization()
    {
        highScore = GameObject.Find("HighScore");

        startLabel = GameObject.Find("StartLabel");
        endLabel = GameObject.Find("EndLabel");
        startButton = GameObject.Find("StartButton");
        restartButton = GameObject.Find("RestartButton");
    }

    //This method allows the game to switch scenes from start, main, and end.
    //Also this method starts spawning waves of enemiess and Diamonds.
    //It also sets audio settings.
    private void SceneConfiguration()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Start":
                scoreLabel.enabled = false;
                livesLabel.enabled = false;
                highScoreLabel.enabled = false;
                endLabel.SetActive(false);
                restartButton.SetActive(false);
                activeSoundClip = SoundClip.NONE;
                break;
            case "Main":
                highScoreLabel.enabled = false;
                startLabel.SetActive(false);
                startButton.SetActive(false);
                endLabel.SetActive(false);
                restartButton.SetActive(false);
                activeSoundClip = SoundClip.MUSIC;
                break;
            case "End":
                scoreLabel.enabled = false;
                livesLabel.enabled = false;
                startLabel.SetActive(false);
                startButton.SetActive(false);
                activeSoundClip = SoundClip.NONE;
                highScoreLabel.text = "High Score: " + highScore.GetComponent<HighScore>().score;
                break;
        }

        Lives = 5;
        Score = 0;

        StartCoroutine(SpawnWaves());
        StartCoroutine(SpawnDiamonds());

        if ((activeSoundClip != SoundClip.NONE) && (activeSoundClip != SoundClip.NUM_OF_CLIPS))
        {
            AudioSource activeAudioSource = audioSources[(int)activeSoundClip];
            activeAudioSource.playOnAwake = true;
            activeAudioSource.loop = true;
            activeAudioSource.volume = 0.5f;
            activeAudioSource.Play();
        }
    }


    // Update is called once per frame
    void Update()
    {
    }

    //When the start button is clicked load the main scene.
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }

    //When the restart button is clicked load the main scene.
    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }

    //This method is what spawns the enemy waves at a fixed y position 
    //and a random x position within parameters
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Vector2 spawnPosition = new Vector2(spawn.y, Random.Range(-spawn.x, spawn.x));

                Instantiate(Enemy, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    //This method spawns diamonds. An exact clone of the spawn waves method.
    IEnumerator SpawnDiamonds()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < diamondCount; i++)
            {
                Vector2 spawnPosition = new Vector2(Random.Range(-diamondSpawn.x, diamondSpawn.x), diamondSpawn.y);
                Instantiate(Diamond, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
}
