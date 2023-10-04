using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject resourceNode;
    [SerializeField] private int resourceNodeAmount;
    // Start is called before the first frame update
    void Start()
    {
        var height = mainCamera.orthographicSize;   // set height using the camera orthographic size
        var width = mainCamera.aspect * height;     // set width using camera aspect ratio and height
        InitializeResourceNodes(width, height);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
