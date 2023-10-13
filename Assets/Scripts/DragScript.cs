using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScript : MonoBehaviour
{

    public bool IsDragging;

    private Collider2D collider;

    private DragController dragController;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        dragController = FindObjectOfType<DragController>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        DragScript collidedDraggable = other.GetComponent<DragScript>();

        if(collidedDraggable != null && dragController.LastDragged.gameObject == gameObject)
        {
            ColliderDistance2D colliderDistance2D = other.Distance(collider);
            Vector3 diff = new Vector3(colliderDistance2D.normal.x, colliderDistance2D.normal.y) * colliderDistance2D.distance;
            transform.position = diff;
        }
    }
}
