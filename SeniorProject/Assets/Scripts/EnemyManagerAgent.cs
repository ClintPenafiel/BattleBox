using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class EnemyManagerAgent : Agent
{
    [SerializeField] private bool training; // value is set to true when training the neural network
    private float timePenalty; // agent is penalized the longer it takes to destroy the player base
    private float normalizedBaseHealth; // normalized value to represent player base health
    private GameObject playerBase; // player base for keeping track of player base health
    private EnemyManager enemyManager; // enemy manager script to spawn enemies

    // overall reward is set as 1 - current health / max health of the player's base - time penalty
    private float GetReward()
    {
        return 1 - timePenalty;
    }
    
    private void SetNormalizedBaseHealth()
    {
        if (playerBase != null)
        {
            HealthSystem baseHealth = playerBase.GetComponent<HealthSystem>();
            float currentHealth = baseHealth.GetHealth();
            float maximumHealth = baseHealth.GetMaxHealth();
            normalizedBaseHealth = currentHealth / maximumHealth;
            if (normalizedBaseHealth < 0)
            {
                normalizedBaseHealth = 0;
            }
        }
    }

    public override void OnEpisodeBegin()
    {
        playerBase = GameObject.FindGameObjectWithTag("Base");
        enemyManager = FindObjectOfType<EnemyManager>();
        timePenalty = 0;
        normalizedBaseHealth = 1;
        SetReward(GetReward());
    }

    

    // collect observations for the agent
    public override void CollectObservations(VectorSensor sensor)
    {
        if (GameEnd.IsGameFinished()) return;
        // only record observations when game is playing
        SetNormalizedBaseHealth();
        sensor.AddObservation(normalizedBaseHealth); // record observation
    }

    // discrete actions decide which enemy to spawn (based on an index value corresponding to which enemy to spawn)
    public override void OnActionReceived(ActionBuffers actions)
    {
        int index = actions.DiscreteActions[0] + 1;
        enemyManager.AddToQueue(index);
    }

    private void Update()
    {
        timePenalty += 0.001f;
        if (GameEnd.IsGameFinished())
        {
            RestartTrainingEpisode();
        }
    }

    private void RestartTrainingEpisode()
    {
        SetReward(GetReward());
        StartCoroutine(WaitForLoading());
    }

    private IEnumerator WaitForLoading()
    {
        if (training) // automatically restart the game when training
        {
            FindObjectOfType<MainMenu>().Play();
        }
        yield return new WaitForSeconds(1); // wait before ending the training episode
        EndEpisode();
    }
}
