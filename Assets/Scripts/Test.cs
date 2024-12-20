using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    public NavMeshAgent agent;

    private void Start()
    {
        int agentTypeIndex = agent.agentTypeID;

        if (agentTypeIndex == NavMesh.GetSettingsByIndex(0).agentTypeID)
        {
            Debug.Log("Agent");
        }

        else if (agentTypeIndex == NavMesh.GetSettingsByIndex(1).agentTypeID)
        {
            Debug.Log("secret agent");
        }
    }
}
