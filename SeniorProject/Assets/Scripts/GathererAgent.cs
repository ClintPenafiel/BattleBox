using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Random = UnityEngine.Random;

public class GathererAgent : Agent
{
    [SerializeField] private GathererController gathererController;
    [SerializeField] private Transform baseTransform;
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask goldResourceLayer;
    [SerializeField] private LayerMask enemyBaseLayer;
    [SerializeField] private float stopDistance = 0.5f;
    [SerializeField] private bool training;
    private GameObject[] resourceNodes;
    private int reward;
    
    public override void OnEpisodeBegin()
    {
        reward = 0;
        if (training)
        {
            ResetResourceNodePositions();
        }
    }

    private void ResetResourceNodePositions()
    {
        // reset positions of resources
        resourceNodes = GameObject.FindGameObjectsWithTag("GoldResource");
        foreach (var resource in resourceNodes)
        {
            Vector3 spawnPosition;
            Vector3 playerBasePos = new Vector3(-14, 0, 0);
            Vector3 enemyBasePos = new Vector3(14, 0, 0);
            int minDistToBase = 5;
            do
            {
                spawnPosition = new Vector3(Random.Range(-7f, 7f), Random.Range(-9f, 9f), 0f);
            } while (Vector3.Distance(spawnPosition, playerBasePos) < minDistToBase &&
                     Vector3.Distance(spawnPosition, enemyBasePos) < minDistToBase);

            resource.transform.localPosition = spawnPosition;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        var basePosition = baseTransform.position;
        sensor.AddObservation(basePosition.x);
        sensor.AddObservation(basePosition.y);
        float minDistance = float.MaxValue;
        Vector2 closestPosition = Vector2.zero;
        // find resource node closest to base
        resourceNodes = GameObject.FindGameObjectsWithTag("GoldResource");
        foreach (var resource in resourceNodes)
        {
            float distance = Vector2.Distance(resource.transform.position, basePosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPosition = resource.transform.position;
            }
        }
        // Debug.DrawLine(closestPosition, basePosition);
        sensor.AddObservation(closestPosition);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        GetComponent<Rigidbody2D>().velocity = moveDirection * speed;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Boundary"))
        {
            SetReward(-100);
            Debug.Log($"boundary exited");
            if (training)
            {
                // reset position of gatherer agent
                transform.localPosition = new Vector3(Random.Range(0, 4f), Random.Range(-9f, 7f), 0);
            }
            EndEpisode();
        }
    }
    
    private void Update() {
        Collider2D[] goldResources = Physics2D.OverlapCircleAll(transform.localPosition, stopDistance, goldResourceLayer);
        if (goldResources.Length > 0) {
            StartCoroutine(gathererController.GatherGold(goldResources[0].transform));
            reward = gathererController.GetGoldCarried();
            SetReward(reward);
        }
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(transform.localPosition, stopDistance, enemyBaseLayer);
        foreach (var obj in enemyColliders)
        {
            if (obj.gameObject.CompareTag("Base"))
            {
                if (reward > 0)
                {
                    StartCoroutine(gathererController.DepositGold());
                    SetReward(reward*reward);
                    EndEpisode();
                }
            }
        }
    }
    
}
