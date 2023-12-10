using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GathererController : MonoBehaviour
{
    [SerializeField] private int carryCapacity = 10;    // maximum amount of gold the gatherer can carry
    [SerializeField] private int goldCarried;           // current amount of gold the gatherer can carry
    [SerializeField] private int gatherSpeed = 1;       // time to wait before the gatherer can collect gold
    [SerializeField] private int depositSpeed = 1;      // time to wait before the gatherer can deposit gold
    [SerializeField] private Transform mainBase;        // transform of the base so gatherer knows which base to go to
    [SerializeField] private bool player;               // whether the gatherer is for the player or enemy
    
    private bool isGathering = false;
    private bool isDepositing = false;

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
    
    // this is where the gold is added to the gatherer when it reaches a gold resource node and takes gold from the resource node until the gold carry capacity has been reached
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
        // String message = "player";
        // if (!player)
        // {
        //     message = "enemy";
        // }
        // Debug.Log($"{message} gatherer has {goldCarried} gold");
        yield return null;
    }
    
    //this is where the gold is added to the base from the gatherer when it reaches the base with gold it should update the gold text, and add the gold to the base
    public IEnumerator DepositGold() 
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
        // Debug.Log($"Player has {BaseController.Instance.GetGold()} gold. Enemy has {BaseController.Instance.GetEnemyGold()}.");
        yield return null;
    }
    
    public Transform baseTransform()
    {
        return mainBase;
    }
    
}