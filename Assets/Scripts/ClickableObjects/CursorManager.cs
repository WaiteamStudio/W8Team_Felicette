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
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 0f, interactableLayer);

        if (hit.collider != null)
        {
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                var cursor = interactable.CursorTexture ?? defaultCursor;
                var hotspot = cursor != defaultCursor
                    ? new Vector2(cursor.width / 2, cursor.height / 2)
                    : defaultCursorHotspot;

                SetCursor(cursor, hotspot);
                return;
            }
        }

        SetCursor(defaultCursor, defaultCursorHotspot);
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

