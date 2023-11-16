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
    [SerializeField] private Transform baseTransform;
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask goldResourceLayer;
    [SerializeField] private float stopDistance = 0.5f;
    private GameObject[] resourceNodes;
    
    public override void OnEpisodeBegin()
    {
        // reset position of gatherer
        transform.localPosition = new Vector3(Random.Range(-7f, 7f), Random.Range(-9f, 9f), 0);
        // reset positions of resources
        resourceNodes = GameObject.FindGameObjectsWithTag("GoldResource");
        foreach (var resource in resourceNodes)
        {
            Vector3 spawnPosition;
            Vector3 playerBasePos = new Vector3(14, 0, 0);
            Vector3 enemyBasePos = new Vector3(-14, 0, 0);
            int minDistToBase = 5;
            do
            {
                spawnPosition = new Vector3(Random.Range(-7f, 7f), Random.Range(-9f, 9f), 0f);
            } while (Vector3.Distance(spawnPosition, playerBasePos) < minDistToBase && Vector3.Distance(spawnPosition, enemyBasePos) < minDistToBase);
            resource.transform.localPosition = spawnPosition;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(baseTransform.localPosition);
        // 5 resource nodes are assumed
        resourceNodes = GameObject.FindGameObjectsWithTag("GoldResource");
        foreach (var resource in resourceNodes)
        {
            sensor.AddObservation(resource.transform.localPosition);
        }
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
            SetReward(-1f);
            EndEpisode();
        }
    }
    
    private void Update() {
        Collider2D[] goldResources = Physics2D.OverlapCircleAll(transform.position, stopDistance, goldResourceLayer);
        if (goldResources.Length > 0) {
            SetReward(1f);
            EndEpisode();
        }
    }
    
    
}
