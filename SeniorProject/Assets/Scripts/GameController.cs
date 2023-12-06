using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject resourceNode;
    [SerializeField] private int resourceNodeAmount;
    // Start is called before the first frame update
    [SerializeField] private GameObject startingPlayerUnit; // Reference to the starting player unit in the Unity editor.
    [SerializeField] private GameObject startingEnemyUnit; // Reference to the starting player unit in the Unity editor.
    [SerializeField] private GameObject playerBasePrefab; // Reference to the player base prefab in the Unity editor.
    [SerializeField] private GameObject enemyBasePrefab; // Reference to the enemy base prefab in the Unity editor.
    //GoldManager reference
    private GoldManager goldManager;
    [SerializeField] private float minDistanceToBase = 10f; // Minimum distance between a resource node and the base. the smaller the number the closer the resource node will be to the base
    
    void Start()
    {
        var height = mainCamera.orthographicSize;   // set height using the camera orthographic size
        var width = mainCamera.aspect * height;     // set width using camera aspect ratio and height
        GameObject playerBase = SpawnBase(playerBasePrefab, new Vector3(-14, 0, 0)); // Spawn the base at a specific position when the game starts.
        GameObject enemyBase = SpawnBase(enemyBasePrefab, new Vector3(14, 0, 0)); // Spawn the base at a specific position when the game starts.

        InitializeResourceNodes(7f, 9f, playerBase.transform.localPosition, enemyBase.transform.localPosition);
                // Example: Spawn a character at a specific position when the game starts.
        SpawnCharacter(startingPlayerUnit, new Vector3(-14, 0, 0));
        SpawnCharacter(startingEnemyUnit, new Vector3(14, 0, 0));

    }
    private GameObject SpawnBase(GameObject basePrefab, Vector3 spawnPosition)
{
    Debug.Log("Base spawned at position: " + spawnPosition);
    return Instantiate(basePrefab, spawnPosition, Quaternion.identity);
}
    //function to initialize gold manager

private void InitializeResourceNodes(float width, float height, Vector3 playerBasePosition, Vector3 enemyBasePosition)
{
    // spawn nodes above a certain y position
    float minimumY = -5f;
    // Initialize Resource Nodes at random locations in a rectangular area
    for (int i = 0; i < resourceNodeAmount; i++)
    {
        Vector3 spawnPosition;
        do
        {
            spawnPosition = new Vector3(Random.Range(-width, width), Random.Range(-height, height), 0f);
        } while (spawnPosition.y < minimumY || Vector3.Distance(spawnPosition, playerBasePosition) < minDistanceToBase && Vector3.Distance(spawnPosition, enemyBasePosition) < minDistanceToBase);

        Instantiate(resourceNode, spawnPosition, Quaternion.identity);
    }
}
    // Function to spawn a character at a given position.
    private void SpawnCharacter(GameObject unit, Vector3 spawnPosition)
    {
        Debug.Log("Character spawned at position: " + spawnPosition);
    Instantiate(unit, spawnPosition, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
