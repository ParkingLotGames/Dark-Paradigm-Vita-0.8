using DP.Controllers;
using UnityEngine;
using System.Collections.Generic;

namespace DP.AI
{
    /// <summary>
    /// Manages the movement of enemies in the scene.
    /// </summary>
    public class EnemyMovementManager : MonoBehaviour
    {
        public bool usingECS;
        EnemyInstanceComponent[] enemies;
        public Queue<EnemyInstanceComponent> agentsAwaitingForPath = new Queue<EnemyInstanceComponent>();
        public Queue<EnemyInstanceComponent> repathedAgents = new Queue<EnemyInstanceComponent>();
        int enemyCount;
        bool repathAllowed, nextRepathDelayDefined;
        float nextRepath, elapsed;
        GameObject player;

        private void Start()
        {
            // Find the player object and get our enemies via EnemyInstanceComponent
            player = GameObject.FindGameObjectWithTag("Player");
            enemies = GetComponentsInChildren<EnemyInstanceComponent>();

            // Cache array length, useful for vita & low end android
            enemyCount = enemies.Length;

            // Loop through our enemies and enable navigation
            for (int i = 0; i < enemyCount; i++)
            {
                enemies[i].navigationEnabled = true;
                agentsAwaitingForPath.Enqueue(enemies[i]);
            }
        }

        /// <summary>
        /// Gets the next agent in the queue that needs to be repathed.
        /// </summary>
        /// <returns>The next agent that needs to be repathed.</returns>
        EnemyInstanceComponent Repath()
        {
            EnemyInstanceComponent agentRepathQuery = agentsAwaitingForPath.Dequeue();
            return agentRepathQuery;
        }

        private void Update()
        {
            if (queue)
            {
                // Count time
                elapsed += Time.deltaTime;

                // This will be false if we haven't repathed agents or if we just repathed one
                if (!nextRepathDelayDefined)
                {
                    // Define when the next repath will happen
#if UNITY_PS4
                    nextRepath = 0.0003F;
#endif
#if UNITY_PSP2
                nextRepath = Random.Range(0.041f, 0.066f);
#endif
                    // Set defined as true
                    nextRepathDelayDefined = true;
                }

                // If the counted time is equal or bigger than what was defined to wait
                if (elapsed >= nextRepath)
                {
                    // If we have agents waiting
                    if (agentsAwaitingForPath.Count != 0)
                    {
                        // Get the next agent in queue
                        var repathedAgent = Repath();
                        // Set its destination
                        repathedAgent.agent.SetDestination(player.transform.position);
                        // Set the agent as repathed
                        repathedAgent.repathed = true;
                        // Send it to the queue of agents that have been repathed already
                        repathedAgents.Enqueue(repathedAgent);
                    }
                    elapsed = 0;
                    nextRepathDelayDefined = false;
                }

                for (int i = 0; i < enemyCount; i++)
                {
                    if (enemies[i].navigationEnabled)
                    {
                        if (enemies[i].agent.remainingDistance <= 0.5f)
                        {
                            enemies[i].repathed = false;
                        }
                        if (!enemies[i].repathed)
                        {
                            if (repathedAgents.Count != 0)
                                repathedAgents.Dequeue();
                            agentsAwaitingForPath.Enqueue(enemies[i]);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < enemyCount; i++)
                {
                    enemies[i].agent.SetDestination(player.transform.position);
                }
            }
        }

        /// <summary>
        /// Array of Transforms that represent the enemy spawn points.
        /// </summary>
        public Transform[] spawnPoints;

        /// <summary>
        /// The stopping distance for the agent when it reaches its destination.
        /// </summary>
        [SerializeField] float AgentStoppingDistance = 0.5f;

        /// <summary>
        /// The move speed of the agent.
        /// </summary>
        [SerializeField] float AgentMoveSpeed = 3.5f;

        /// <summary>
        /// The acceleration of the agent.
        /// </summary>
        [SerializeField] float AgentAcceleration = 8f;

        /// <summary>
        /// The rotation speed of the agent.
        /// </summary>
        [SerializeField] float AgentRotationSpeed = 10f;

        /// <summary>
        /// The area mask for the agent.
        /// </summary>
        [SerializeField] int AgentAreaMask = -1;

        /// <summary>
        /// A flag indicating if the agent is currently in the queue.
        /// </summary>
        public bool queue;
    }

}