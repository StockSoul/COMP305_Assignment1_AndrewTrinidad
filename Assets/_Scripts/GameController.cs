using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Wave Settings")]
    public GameObject Enemy;
    public Vector2 spawn;
    public int enemyCount;
    public float startWait;
    public float spawnWait;
    public float waveWait;

    [Header("Diamond Settings")]
    public GameObject Diamond;
    public Vector2 diamondSpawn;
    public int diamondCount;

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
        DontDestroyOnLoad(highScore);
    }


    private void GameObjectInitialization()
    {
        highScore = GameObject.Find("HighScore");

        startLabel = GameObject.Find("StartLabel");
        endLabel = GameObject.Find("EndLabel");
        startButton = GameObject.Find("StartButton");
        restartButton = GameObject.Find("RestartButton");
    }

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
                break;
            case "Main":
                highScoreLabel.enabled = false;
                startLabel.SetActive(false);
                startButton.SetActive(false);
                endLabel.SetActive(false);
                restartButton.SetActive(false);
                break;
            case "End":
                scoreLabel.enabled = false;
                livesLabel.enabled = false;
                startLabel.SetActive(false);
                startButton.SetActive(false);
                highScoreLabel.text = "High Score: " + highScore.GetComponent<HighScore>().score;
                break;
        }

        Lives = 5;
        Score = 0;

        StartCoroutine(SpawnWaves());
        StartCoroutine(SpawnDiamonds());

    }


    // Update is called once per frame
    void Update()
    {

    }

    public void OnStartButtonClick()
    {
        //DontDestroyOnLoad(highScore);
        SceneManager.LoadScene("Main");
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }

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
