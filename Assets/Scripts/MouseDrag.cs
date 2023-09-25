using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    public Vector3 mOffset;
    public Vector3 startPosition;
    public float mZCoord;
    private bool isDragging = false; // Added variable to track dragging state

    void OnMouseDown()
    {
        // Check if Camera.main is not null before using it
        if (Camera.main != null)
        {
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            // Store offset = gameobject world pos - mouse world pos
            mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
            isDragging = true; // Start dragging
        }
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;
        // z coordinate of game object on screen
        mousePoint.z = mZCoord;
        // Convert it to world points
        return Camera.main != null ? Camera.main.ScreenToWorldPoint(mousePoint) : Vector3.zero;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPos = GetMouseAsWorldPoint() + mOffset;
            transform.position = newPos;
        }
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false; // Stop dragging
            Vector3 finalPosition = transform.position;
            finalPosition.y += 0.5f; // Move the object up by units
            transform.position = finalPosition;
        }
    }
}