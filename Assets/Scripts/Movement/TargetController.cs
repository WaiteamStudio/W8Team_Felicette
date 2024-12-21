using UnityEngine;

public class TargetController : MonoBehaviour
{
    private TargetMovement movement;
    private Camera mainCamera;

    private void Start()
    {
        movement = GetComponent<TargetMovement>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            movement.UpdateFollowSpot(new Vector2(mousePosition.x, mousePosition.y));
        }
    }

    public void Teleport(Vector3 position)
    {
        //transform.position = position;

        //movement.Agent.isStopped = true;
        movement.Agent.Warp(position);

    }
}
