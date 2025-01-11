using UnityEngine;

public class MouseClickCollector : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Левая кнопка мыши
        {
            TryCollectItem2D();
        }
    }

    private void TryCollectItem2D()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
        if (hit.collider != null)
        {
            ICollectibleItem item = hit.collider.GetComponent<ICollectibleItem>();
            if (item != null)
            {
                Debug.Log("item hitted");
                item.Collect();
            }
        }
    }

}
