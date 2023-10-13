using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScript : MonoBehaviour
{

    public bool IsDragging;

    public Vector3 LastPosition;

    private Collider2D collider;

    private DragController dragController;

    private float moveTime = 15f;

    private System.Nullable<Vector3> movementDestination;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        dragController = FindObjectOfType<DragController>();
    }

    void FixedUpdate()
    {
        if (movementDestination.HasValue)
        {
            if (IsDragging)
            {
                movementDestination = null;
                return;
            }
            if(transform.position == movementDestination)
            {
                gameObject.layer = Layer.Default;
                movementDestination = null;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, movementDestination.Value, moveTime * Time.fixedDeltaTime);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        DragScript collidedDraggable = other.GetComponent<DragScript>();

        if(collidedDraggable != null && dragController.LastDragged.gameObject == gameObject)
        {
            ColliderDistance2D colliderDistance2D = other.Distance(collider);
            Vector3 diff = new Vector3(colliderDistance2D.normal.x, colliderDistance2D.normal.y) * colliderDistance2D.distance;
            transform.position -= diff;
        }

        if (other.CompareTag("DropValid"))
        {
            movementDestination = other.transform.position;
        }
        else if (other.CompareTag("DropInvalid"))
        {
            movementDestination = LastPosition;
        }
    }
}
