using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject resourceNode;
    [SerializeField] private int resourceNodeAmount;
    // Start is called before the first frame update
    [SerializeField] private GameObject characterPrefab; // Reference to the character prefab in the Unity editor.
    [SerializeField] private GameObject basePrefab; // Reference to the base prefab in the Unity editor.
    //GoldManager reference
    private GoldManager goldManager;
    [SerializeField] private float minDistanceToBase = 10f; // Minimum distance between a resource node and the base. the smaller the number the closer the resource node will be to the base
    
    void Start()
    {
        var height = mainCamera.orthographicSize;   // set height using the camera orthographic size
        var width = mainCamera.aspect * height;     // set width using camera aspect ratio and height
        GameObject baseInstance = SpawnBase(new Vector3(-14, 0, 1)); // Spawn the base at a specific position when the game starts.
        InitializeResourceNodes(width, height, baseInstance);
                // Example: Spawn a character at a specific position when the game starts.
        SpawnCharacter(new Vector3(-8, 0, 0));
    }
    private GameObject SpawnBase(Vector3 spawnPosition)
{
    Debug.Log("Base spawned at position: " + spawnPosition);
    return Instantiate(basePrefab, spawnPosition, Quaternion.identity);
}
    //function to initialize gold manager

private void InitializeResourceNodes(float width, float height, GameObject baseInstance)
{
    // Initialize Resource Nodes at random locations in a rectangular area
    for (int i = 0; i < resourceNodeAmount; i++)
    {
        Vector3 spawnPosition;
        do
        {
            spawnPosition = new Vector3(Random.Range(-7f, -1f), Random.Range(-9f, 9f), 0f);
        } while (Vector3.Distance(spawnPosition, baseInstance.transform.position) < minDistanceToBase);

        Instantiate(resourceNode, spawnPosition, Quaternion.identity);
    }
}
    // Function to spawn a character at a given position.
    private void SpawnCharacter(Vector3 spawnPosition)
    {
        Debug.Log("Character spawned at position: " + spawnPosition);
    Instantiate(characterPrefab, spawnPosition, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
