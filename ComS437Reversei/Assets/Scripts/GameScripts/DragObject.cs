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
    public static GameObject self;

    public Animator anim;
    public static bool isPickedUp = false;
    public bool isBlack;
    public bool setWhite = false;
    public bool addToArray = false;

    void Start()
    {
        self = gameObject;
        ghostPiece.transform.position = new Vector3(0f, setHeight, 0f);
        physicsCollider = GetComponent<MeshCollider>();
        rb = GetComponent<Rigidbody>();

        anim = GetComponent<Animator>();
        if (setWhite)
        {
            isBlack = false;
        }
        else
        {
            isBlack = true;
        }
        anim.SetBool("isBlack", isBlack);

        if(addToArray)
        {
            Vector2 A = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
            if (GameBoardController.pieceArray.Contains(A)) {
                GameBoardController.pieceArray.Remove(A);
            }
            GameBoardController.pieceArray.Add(A, gameObject);
        }
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

    public void FlipToWhite()
    {
        isBlack = false;
        UpdateAnim();
    }

    public void FlipToBlack()
    {
        isBlack = true;
        UpdateAnim();
    }

    public void UpdateAnim()
    {
        anim.SetBool("isBlack", isBlack);
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
            isPickedUp = true;
            originalPlace = gameObject.transform.position;
        }
    }

    private void OnMouseUp()
    {
        if(canPickup == true)
        {
            isPickedUp = false;
            if (canPlace == false)
            {
                //Debug.Log(originalPlace);
                gameObject.transform.position = originalPlace;
            }
            else
            {
                GameBoardController.gamePieces++;
                gameObject.transform.position = ghostPiece.transform.position;
                Vector2 A = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
                if(GameBoardController.pieceArray.Contains(A)) {
                    GameBoardController.pieceArray.Remove(A);
                }
                GameBoardController.pieceArray.Add(A, gameObject);
                GameBoardController.BoardDataUpdated = false;
                GameBoardController.isPlayerTurn = false;
                GameBoardController.updateBoardData(isBlack);
                canPickup = false;
                GameObject a = Instantiate(gameObject, new Vector3(-3f, 1.1f, -6f), Quaternion.identity);
                a.GetComponent<DragObject>().canPickup = true;
                //Debug.Log("MOUSE UP");
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
