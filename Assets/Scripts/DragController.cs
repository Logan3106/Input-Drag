using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public DragScript LastDragged => lastDragged;

    private bool isDragActive = false;

    private Vector2 screenPosition;

    private Vector3 worldPosition;

    private DragScript lastDragged;

    void Update()
    {
        if (isDragActive)
        {
            if(Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))
            {
                Drop();
                return;
            }
        }


        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            screenPosition = new Vector2(mousePos.x, mousePos.y);
        }
        else if(Input.touchCount > 0)
        {
            screenPosition = Input.GetTouch(0).position;
        }
        else
        {
            return;
        }


        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (isDragActive)
        {
            Drag();
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (hit.collider != null)
            {
                DragScript dragscript = hit.transform.gameObject.GetComponent<DragScript>();
                if(dragscript != null)
                {
                    lastDragged = dragscript;
                    InitDrag();
                }
            }
        }
    }

    void InitDrag()
    {
        lastDragged.LastPosition = lastDragged.transform.position;
        UpdateDragStatus(true);
    }

    void Drag()
    {
        lastDragged.transform.position = new Vector2(worldPosition.x, worldPosition.y);
    }

    void Drop()
    {
        UpdateDragStatus(false);
    }

    void UpdateDragStatus(bool isDragging)
    {
        isDragActive = lastDragged.IsDragging = isDragging;
        lastDragged.gameObject.layer = isDragging ? Layer.Dragging : Layer.Default;
    }
}
