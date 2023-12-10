using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private List<int> enemyCosts;
    [SerializeField] private Transform spawnPoint;

    private Queue<int> spawnQueue;
    private BaseController baseController;
    private bool gameOver;
    private int numGatherers;
    // Start is called before the first frame update
    void Start()
    {
        spawnQueue = new Queue<int>();
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

    // add index of next enemy to spawn to the queue
    public void AddToQueue(int index)
    {
        if (spawnQueue.Count <= 5) // maximum of 5 in the spawn queue
        {
            spawnQueue.Enqueue(index);
        }
    }

    //spawn the enemy that is first in the queue, unless the spawn queue is being skipped to spawn a gatherer
    public void SpawnEnemy(bool skipQueue)
    {
        if (skipQueue)
        {
            // gatherer should be the first enemy in the list
            if (enemyCosts[0] <= baseController.GetEnemyGold())
            {
                baseController.AddEnemyGold(-enemyCosts[0]);
                Instantiate(enemies[0], spawnPoint.position, Quaternion.identity);
            }
            return;
        }
        int index = Random.Range(0, enemies.Count - 1); 
        if (spawnQueue.Count > 0)
        {
            index = spawnQueue.Peek(); // check index that is first in the spawn queue
        }
        if (enemyCosts[index] <= baseController.GetEnemyGold())
        {
            baseController.AddEnemyGold(-enemyCosts[index]);
            Instantiate(enemies[index], spawnPoint.position, Quaternion.identity);
            spawnQueue.Dequeue(); // remove index from spawn queue
        }
    }

    // attempt to spawn an enemy every second to help reduce amount of computation
    private IEnumerator SpawnEnemies()
    {
        while (!gameOver)
        {
            // int randIndex = Random.Range(0, enemies.Count - 1);
            // don't spawn too many gatherers
            // while (enemies[randIndex].CompareTag("EnemyGatherer") && numGatherers > 0)
            // {
            //     randIndex = Random.Range(0, enemies.Count - 1);
            // }
            // SpawnEnemy(randIndex);
            if (numGatherers <= 0)
            {
                SpawnEnemy(true);
            }
            SpawnEnemy(false);
            yield return new WaitForSeconds(1); // wait 1 second
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
