using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] private int gold;

    // Start is called before the first frame update
    void Start()
    {
        gold = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetGold()
    {
        return gold;
    }
    
    public void AddGold(int num)
    {
        gold += num;
    }

    public void SubtractGold(int num)
    {
        gold -= num;
    }
}