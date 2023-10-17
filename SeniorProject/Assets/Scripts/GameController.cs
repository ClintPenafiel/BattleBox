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
    
    void Start()
    {
        var height = mainCamera.orthographicSize;   // set height using the camera orthographic size
        var width = mainCamera.aspect * height;     // set width using camera aspect ratio and height
        InitializeResourceNodes(width, height);
                // Example: Spawn a character at a specific position when the game starts.
        SpawnCharacter(new Vector3(-8, 0, 0));
         SpawnBase(new Vector3(-14, 0, 0)); // Spawn the base
    }
    private void SpawnBase(Vector3 spawnPosition)
{
    Debug.Log("Base spawned at position: " + spawnPosition);
    Instantiate(basePrefab, spawnPosition, Quaternion.identity);
}
    private void InitializeResourceNodes(float width, float height)
    {
        // Initialize Resource Nodes at random locations on the map
        for (int i = 0; i < resourceNodeAmount; i++)
        {
            Instantiate(resourceNode, new Vector3(Random.Range(-width, width), Random.Range(-height, height)),
                Quaternion.identity);
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
