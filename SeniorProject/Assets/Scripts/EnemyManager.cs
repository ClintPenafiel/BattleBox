using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private List<int> enemyCosts;
    [SerializeField] private Transform spawnPoint;

    private BaseController baseController;
    private bool gameOver;
    private int numGatherers;
    // Start is called before the first frame update
    void Start()
    {
        numGatherers = 1;
        gameOver = false;
        baseController = FindObjectOfType<BaseController>();
        StartCoroutine(SpawnEnemies());
        StartCoroutine(TrackEnemyGatherers());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // return true if enemy has been spawned, false otherwise
    private bool SpawnEnemy(int index)
    {
        if (enemyCosts[index] <= baseController.GetEnemyGold())
        {
            baseController.AddEnemyGold(-enemyCosts[index]);
            Instantiate(enemies[index], spawnPoint.position, Quaternion.identity);
            return true;
        }

        return false;
    }

    // attempt to spawn an enemy every second to help reduce amount of computation
    private IEnumerator SpawnEnemies()
    {
        while (!gameOver)
        {
            int randIndex = Random.Range(0, enemies.Count - 1);
            // don't spawn too many gatherers
            while (enemies[randIndex].CompareTag("EnemyGatherer") && numGatherers > 0)
            {
                randIndex = Random.Range(0, enemies.Count - 1);
            }
            SpawnEnemy(randIndex);
            yield return new WaitForSeconds(1);
        }
    }
    
    // keep track of enemy gatherers every second to help reduce amount of computation
    private IEnumerator TrackEnemyGatherers()
    {
        while (!gameOver)
        {
            numGatherers = GameObject.FindGameObjectsWithTag("EnemyGatherer").Length;
            yield return new WaitForSeconds(1);
        }
    }
}
