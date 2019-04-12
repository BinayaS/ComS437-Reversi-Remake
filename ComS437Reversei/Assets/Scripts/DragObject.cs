using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public bool canPickup;
    private Vector3 mOffset;
    private float mZCoord;
    public GameObject ghostPieceBlack;
    public GameObject ghostPieceWhite;
    public Vector3 originalPlace;
    public MeshCollider physicsCollider;
    public Rigidbody rb;

    void Start()
    {
        //canPickup = true;
        ghostPieceBlack.transform.position = new Vector3(0f, -100f, 0f);
        ghostPieceWhite.transform.position = new Vector3(0f, -100f, 0f);
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
            rb.useGravity = true;
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
            if (ghostPieceBlack.transform.position == new Vector3(0f, -100f, 0f))
            {
                gameObject.transform.position = originalPlace;
            }
            else
            {
                gameObject.transform.position = ghostPieceBlack.transform.position;
            }

            ghostPieceBlack.transform.position = new Vector3(0f, -100f, 0f);
            ghostPieceWhite.transform.position = new Vector3(0f, -100f, 0f);
        }
    }

    private void OnMouseDrag() {
        if (canPickup == true)
        {
            gameObject.transform.position = GetMouseWorldPos() + mOffset;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name != "Model_Board" && other.name != "Plane" && canPickup == true)
        {
            ghostPieceBlack.transform.position = other.transform.position;
        }
    }
}
