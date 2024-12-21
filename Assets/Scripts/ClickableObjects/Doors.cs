using UnityEngine;

public class Doors : MonoBehaviour, IInteractable
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform teleportPoint;
    [SerializeField] private LayerMask playerLayer;
    public Texture2D CursorTexture => cursorTexture;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckClick();
        }
    }

    private void CheckClick()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 0f, interactableLayer);
        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            OnClick();
        }
    }

    public void OnClick()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 2f, playerLayer);
        if (playerCollider != null)
        {
            var player = playerCollider.GetComponent<TargetController>();
            if (player != null)
            {
                player.Teleport(teleportPoint.position);
                Debug.Log("Player teleported to: " + teleportPoint.position);
            }
        }
        else
        {
            Debug.Log("Player is too far to interact.");
        }
    }
}
