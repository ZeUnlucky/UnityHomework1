using System;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] Transform PostionTarget;
    private NavMeshAgent agent;

    [SerializeField ]public float BoostMultiplier = 2f;
    private float originalSpeed;
    private int iceArea;
    private bool justEntered = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(PostionTarget.position);
        SetSecretAgentAreaCost();
        originalSpeed = agent.speed;
        iceArea = 1 << NavMesh.GetAreaFromName("Ice");
    }

    private void Update()
    {
        if (agent.remainingDistance < 0.1f && !agent.isStopped)
        {
            Debug.Log($"{agent.gameObject.name} Destination reached.");
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
            if ((iceArea & areaMask) != 0)
            {
                if (justEntered)
                {
                    if (agent.agentTypeID == NavMesh.GetSettingsByIndex(0).agentTypeID)
                    {
                        Debug.Log("Agent Stepped On Ice");
                        agent.speed *= BoostMultiplier;
                        justEntered = false;
                    }
                    else if (agent.agentTypeID == NavMesh.GetSettingsByIndex(1).agentTypeID)
                    {
                        Debug.Log("Secret Agent stepped on ice");
                        agent.speed *= (1 / BoostMultiplier) * 5;
                        justEntered = false;
                    }
                }
            }
            else
            {
                Debug.Log("Agent left ice");
                agent.speed = originalSpeed;
                justEntered = true;
            }
        }

    }
    private void SetSecretAgentAreaCost()
    {
        if (agent.agentTypeID == NavMesh.GetSettingsByIndex(1).agentTypeID)
        {
            agent.SetAreaCost(3, 10);
            agent.SetAreaCost(4, 1);
        }
    }
}
