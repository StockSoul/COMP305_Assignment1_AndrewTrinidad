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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWaves());
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
}
