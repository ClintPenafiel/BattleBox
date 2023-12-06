using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private List<int> enemyCosts;
    [SerializeField] private Transform spawnPoint;

    private GoldManager goldManager;
    private List<GameObject> currentEnemies;
    // Start is called before the first frame update
    void Start()
    {
        goldManager = FindObjectOfType<GoldManager>();
        currentEnemies = new List<GameObject>();
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // return true if enemy has been spawned, false otherwise
    private bool SpawnEnemy(int index)
    {
        if (enemyCosts[index] <= goldManager.currentEnemyGold)
        {
            goldManager.DepositEnemyGold(enemyCosts[index]);
            GameObject newEnemy = Instantiate(enemies[index], spawnPoint.position, Quaternion.identity);
            currentEnemies.Add(newEnemy);
            return true;
        }

        return false;
    }

    private IEnumerator SpawnEnemies()
    {
        while (GameObject.FindGameObjectWithTag("EnemyBase") != null)
        {
            int randIndex = Random.Range(0, enemies.Count - 1);
            SpawnEnemy(randIndex);
            yield return new WaitForSeconds(1);
        }
    }
}
