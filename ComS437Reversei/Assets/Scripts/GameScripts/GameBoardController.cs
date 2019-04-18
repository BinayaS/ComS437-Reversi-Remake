using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardController : MonoBehaviour
{

    // 1 == white
    //-1 == black
    public static int[,] boardData = new int[8,8];
    public GameObject point_00;
    public GameObject validMovePoint;
    public static Vector3 boardOffset = new Vector3(-4.0f, 1.5f, -4.0f);
    public static Vector3 pieceOffset = new Vector3(0.0f, 0.5f, 0.0f);
    private static bool showVM = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= 7; i++)
        {
            for(int j = 0; j <= 7; j++)
            {
                boardData[i,j] = 0;
            }
        }

        boardData[3, 3] = -1;
        boardData[4, 4] = -1;
        boardData[3, 4] = 1;
        boardData[4, 3] = 1;
        boardData[7, 7] = 0;

        MinMax.FindValidMoves();
        showValidMoves();
    }

    public bool isEmpty(int x, int y) {
        //Debug.Log("X: " + x);
       //Debug.Log("Y: " + y);
        //Debug.Log(boardData[x, y]);
        if(boardData[x,y] == 0 || boardData[x,y] == 2) {
            return true;
        }
        return false;
    }

    public bool isValidMove(int x, int y)
    {
        if (boardData[x, y] == 2)
        {
            return true;
        }
        return false;
    }

    public static int[,] TranslateToBoardData(Vector3 pieceLocation) {
        int[,] a = new int[2,2];
        a[0, 0] = (int)(Mathf.Abs(pieceLocation.x + 3.5f));
        a[0, 1] = (int)(Mathf.Abs(pieceLocation.z - 3.5f));
        //Debug.Log("X: " + a[0, 0]);
        //Debug.Log("Y: " + a[0, 1]);
        return a;
    }

    public static float[,] TranslateToGameData(int x, int y)
    {
        float[,] a = new float[2, 2];
        a[0, 0] = x - 3.5f;
        a[0, 1] = -y + 3.5f;
        return a;
    }

    public static void updateBoardData(bool isBlack) {
        var a = TranslateToBoardData(DragObject.placingLocation);
        //Debug.Log(isBlack);
        if (isBlack == true) {
            boardData[a[0, 0], a[0, 1]] = -1;
        }
        else {
            boardData[a[0, 0], a[0, 1]] = 1;
        }
        MinMax.FindValidMoves();
        showVM = true;
    }

    private void BoardLogic(int row, int col)
    {

    }

    public void showValidMoves()
    {

        GameObject[] validMovesOnBoard = GameObject.FindGameObjectsWithTag("ValidMove");
        for(int i = 0; i < validMovesOnBoard.Length; i++)
        {
            Destroy(validMovesOnBoard[i]);
        }

        for (int i = 0; i <= 7; i++)
        {
            for (int j = 0; j <= 7; j++)
            {
                if(boardData[i,j] == 2)
                {
                    float[,] temp = TranslateToGameData(i, j);
                    Instantiate(validMovePoint, new Vector3(temp[0,0], 1.1f, temp[0,1]), Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (DragObject.isPickedUp == true && DragObject.placingLocation != null) {
            var a = TranslateToBoardData(DragObject.placingLocation);

            //Debug.Log(isEmpty(a[0, 0], a[0, 1]));
            
            /*
            if (isEmpty(a[0,0], a[0,1])) {
                DragObject.canPlace = true;
            } else {
                DragObject.canPlace = false;
            }
            */

            if (isValidMove(a[0, 0], a[0, 1]))
            {
                DragObject.canPlace = true;
            }
            else
            {
                DragObject.canPlace = false;
            }

            //Debug.Log(DragObject.canPlace);
        }

        if(showVM)
        {
            showVM = false;
            showValidMoves();
        }
    }
}
