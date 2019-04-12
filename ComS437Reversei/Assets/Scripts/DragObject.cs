using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;

    Vector3 GetMouseWorldPos() {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDown() {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        PieceValueController.isPickedUp = true;
    }

    private void OnMouseUp()
    {
        PieceValueController.isPickedUp = false;
    }

    private void OnMouseDrag() {
        gameObject.transform.position = GetMouseWorldPos() + mOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        ShowGhostPiece.spawnedPiece.transform.position = gameObject.transform.position;
    }
}
