using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GathererController : MonoBehaviour
{
    [SerializeField] private int carryCapacity = 10;
    [SerializeField] private int goldCarried;
    [SerializeField] private int gatherSpeed = 1;
    [SerializeField] private int depositSpeed = 1;
    [SerializeField] private Transform mainBase;
    [SerializeField] private bool player;
    
    private bool isGathering = false;
    private bool isDepositing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool gatherState()
    {
        return isGathering;
    }
    
    public bool depositState()
    {
        return isDepositing;
    }

    public int GetGoldCarried()
    {
        return goldCarried;
    }
    
    public int GetCarryCapacity()
    {
        return carryCapacity;
    }

    public IEnumerator GatherGold(Transform targetGoldResource)
    {
        if (!isGathering && goldCarried < carryCapacity)
        {
            isGathering = true;
            yield return new WaitForSeconds(gatherSpeed);
            ResourceNodeController resourceScript =
                targetGoldResource.gameObject.GetComponent<ResourceNodeController>();
            int goldGathered = resourceScript.SubtractGold(carryCapacity - goldCarried);
            goldCarried += goldGathered;
            isGathering = false;
        }
        String message = "player";
        if (!player)
        {
            message = "enemy";
        }
        Debug.Log($"{message} gatherer has {goldCarried} gold");
        yield return null;
    }
    
    public IEnumerator DepositGold() //this is where the gold is added to the base from the gatherer when it reaches the base with gold it should update the gold text, and add the gold to the base 
    {
        if (!isDepositing && goldCarried > 0)
        {
            isDepositing = true;
            yield return new WaitForSeconds(depositSpeed);
            if (player) // add gold for player
            {
                BaseController.Instance.AddGold(goldCarried);
            }
            else // add gold for enemy
            {
                BaseController.Instance.AddEnemyGold(goldCarried);
            }
            goldCarried = 0;
            isDepositing = false;
        }
        Debug.Log($"Player has {BaseController.Instance.GetGold()} gold. Enemy has {BaseController.Instance.GetEnemyGold()}.");
        yield return null;
    }
    
    public Transform baseTransform()
    {
        return mainBase;
    }
    
}