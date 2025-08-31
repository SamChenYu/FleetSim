using UnityEngine;

public class ShipRotator : MonoBehaviour
{
    public float rotationSpeed = 20f;  // adjust for faster/slower rotation
    private bool isDragging = false;
    private Vector3 lastMousePosition;

    void OnMouseDown()
    {
        isDragging = true;
        lastMousePosition = Input.mousePosition;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {

            Debug.Log("Dragging Ship");
            Vector3 delta = Input.mousePosition - lastMousePosition;
            float rotationX = delta.y * rotationSpeed * Time.deltaTime;
            float rotationY = -delta.x * rotationSpeed * Time.deltaTime;

            // Rotate the ship around its local axes
            transform.Rotate(Camera.main.transform.up, rotationY, Space.World);
            transform.Rotate(Camera.main.transform.right, rotationX, Space.World);

            lastMousePosition = Input.mousePosition;
        }
    }
}
