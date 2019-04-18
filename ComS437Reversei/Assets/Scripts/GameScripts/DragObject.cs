using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public bool canPickup;
    public static bool canPlace = false;
    private Vector3 mOffset;
    private float mZCoord;
    public GameObject ghostPiece;
    public Vector3 originalPlace;
    public MeshCollider physicsCollider;
    public Rigidbody rb;
    public static Vector3 placingLocation;
    private float setHeight = -10f;

    void Start()
    {
        //canPickup = true;
        ghostPiece.transform.position = new Vector3(0f, setHeight, 0f);
        physicsCollider = GetComponent<MeshCollider>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(!canPickup)
        {
            rb.useGravity = false;
            physicsCollider.enabled = false;
        } else
        {
            rb.useGravity = false;
            physicsCollider.enabled = true;
        }
    }

    Vector3 GetMouseWorldPos() {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDown() {
        if (canPickup == true)
        {
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseWorldPos();
            PieceValueController.isPickedUp = true;
            originalPlace = gameObject.transform.position;
        }
    }

    private void OnMouseUp()
    {
        if(canPickup == true)
        {
            PieceValueController.isPickedUp = false;
            if (canPlace == false)
            {
                Debug.Log(originalPlace);
                gameObject.transform.position = originalPlace;
            }
            else
            {
                gameObject.transform.position = ghostPiece.transform.position;
                GameBoardController.updateBoardData();
                canPickup = false;
            }
        }
        ghostPiece.transform.position = new Vector3(0f, setHeight, 0f);
    }

    private void OnMouseDrag() {
        if (canPickup == true)
        {
            gameObject.transform.position = GetMouseWorldPos() + mOffset;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Non_Placeable" && other.tag != "Piece" && canPickup == true)
        {
            ghostPiece.transform.position = other.transform.position;
            ghostPiece.transform.position = new Vector3(ghostPiece.transform.position.x, 1.1f, ghostPiece.transform.position.z);
            placingLocation = ghostPiece.transform.position;
        }
    }
}
