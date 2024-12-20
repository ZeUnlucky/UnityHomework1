using System;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] Transform PostionTarget;
    private NavMeshAgent agent;

    public float BoostMultiplier = 40f;
    private float originalSpeed;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(PostionTarget.position);
        SetSecretAgentAreaCost();
        originalSpeed = agent.speed;
    }

    private void Update()
    {
        if (agent.remainingDistance < 0.1f && !agent.isStopped)
        {
            Debug.Log("Destination reached.");
            agent.isStopped = true;
        }
        CheckIfIce();
    }

    private void CheckIfIce()
    {
        NavMeshHit hit;

        if (NavMesh.SamplePosition(agent.transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            int areaMask = hit.mask;
            Debug.Log(areaMask);
            if ((NavMesh.GetAreaFromName("Ice") & areaMask) != 0)
            {
                if (agent.agentTypeID == NavMesh.GetSettingsByIndex(0).agentTypeID)
                {
                    Debug.Log("Agent Stepped On Ice");
                    agent.speed *= BoostMultiplier;
                }
                else if (agent.agentTypeID == NavMesh.GetSettingsByIndex(1).agentTypeID)
                {
                    Debug.Log("Secret Agent stepped on ice");
                    agent.speed *= 1 / BoostMultiplier;
                }
            }

            else
            {
                agent.speed = originalSpeed;
            }
        }

    }
    private void SetSecretAgentAreaCost()
    {
        if (agent.agentTypeID == 1)
        {
            agent.SetAreaCost(3, 5);
            agent.SetAreaCost(4, 1);
        }
    }
}
