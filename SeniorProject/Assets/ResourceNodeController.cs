using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNodeController : MonoBehaviour
{
    [SerializeField] private int gold = 0;
    [SerializeField] private int maxGold = 100;
    [SerializeField] private int goldRegenerationPerSecond = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("GenerateGold");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator GenerateGold()
    {
        while (true)
        {
            gold = Mathf.Clamp(gold + goldRegenerationPerSecond, 0, maxGold);
            yield return new WaitForSeconds(1);
        }
    }

    public int SubtractGold(int num)
    {
        // subtracts gold amount from total gold at the resource node
        int difference = gold - num;
        if (difference < 0) return num + difference;
        return num;
    }
}
