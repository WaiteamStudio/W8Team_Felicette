using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovement
{
    [SerializeField] private float speed;
    private bool isMovementEnabled = true;
    private Vector2 followSpot;
    private UnityEngine.AI.NavMeshAgent agent;

    private void Start()
    {
        followSpot = transform.position;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void UpdateFollowSpot(Vector2 newSpot)
    {
        if (!isMovementEnabled) return;

        followSpot = newSpot;
        agent.SetDestination(new Vector3(followSpot.x, followSpot.y, transform.position.z));
    }

    public void Teleport(Vector3 position)
    {
        agent.Warp(position);
    }
}
