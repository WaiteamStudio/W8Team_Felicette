using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private LayerMask interactableLayer;

    private Vector2 defaultCursorHotspot;
    private Texture2D currentCursor;

    private void Start()
    {
        defaultCursorHotspot = defaultCursor != null
            ? new Vector2(defaultCursor.width / 2, defaultCursor.height / 2)
            : Vector2.zero;

        SetCursor(defaultCursor, defaultCursorHotspot);
    }

    private void Update()
    {
        if (!PauseMenu.isPaused)
        {
            HandleCursorChange();
            HandleInteraction();
        }
        else if (PauseMenu.isPaused)
        {
            SetCursor(defaultCursor, defaultCursorHotspot);
        }
    }

    private void HandleCursorChange()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 0f, interactableLayer);

        if (hit.collider != null)
        {
            var interactable = hit.collider.GetComponent<ICursor>();
            if (interactable != null)
            {
                SetCursor(interactable.CursorTexture, new Vector2(interactable.CursorTexture.width / 2, interactable.CursorTexture.height / 2));
                return;
            }
        }

        SetCursor(defaultCursor, defaultCursorHotspot);
    }

    private void HandleInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 0f, interactableLayer);

            if (hit.collider != null)
            {
                var interactable = hit.collider.GetComponent<ICursor>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }

    private void SetCursor(Texture2D cursor, Vector2 hotspot)
    {
        if (currentCursor != cursor)
        {
            currentCursor = cursor;
            Cursor.SetCursor(cursor, hotspot, CursorMode.Auto);
        }
    }
}

