using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int Lives
    {
        get
        {
            return _lives;
        }

        set
        {
            _lives = value;
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
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWaves());
        StartCoroutine(SpawnDiamonds());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while(true)
        {
            for(int i = 0; i < enemyCount; i++)
            {
                Vector2 spawnPosition = new Vector2(spawn.y, Random.Range(-spawn.x, spawn.x));

                Instantiate(Enemy, spawnPosition,  Quaternion.identity);
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
