using UnityEngine;
using UnityEngine.AI;

public class TargetMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 followSpot;
    private NavMeshAgent agent;

    public NavMeshAgent Agent => agent;

    private void Start()
    {
        followSpot = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void UpdateFollowSpot(Vector2 newSpot)
    {
        followSpot = newSpot;

        agent.SetDestination(new Vector3(followSpot.x, followSpot.y, transform.position.z));
    }
}
