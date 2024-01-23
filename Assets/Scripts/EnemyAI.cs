using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;

    private void Update()
    {
        target = GameObject.FindWithTag("Player").transform;
        if(target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
