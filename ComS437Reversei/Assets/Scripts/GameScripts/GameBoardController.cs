using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardController : MonoBehaviour
{

    // 1 == white
    //-1 == black
    public static int[,] boardData = new int[8,8];
    public static GameObject[,] pieceRefrences = new GameObject[8, 8];
    public GameObject point_00;
    public GameObject validMovePoint;
    public GameObject validAIMovePoint;
    public static Vector3 boardOffset = new Vector3(-4.0f, 1.5f, -4.0f);
    public static Vector3 pieceOffset = new Vector3(0.0f, 0.5f, 0.0f);
    public static bool showVM = false;
    public static bool isPlayerTurn = true;
    public static bool BoardDataUpdated = true;
    public static Hashtable pieceArray = new Hashtable();

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
        pieceRefrences[3, 3] = GameObject.Find("StartPiece(3,3)");
        pieceRefrences[3, 4] = GameObject.Find("StartPiece(3,4)");
        pieceRefrences[4, 3] = GameObject.Find("StartPiece(4,3)");
        pieceRefrences[4, 4] = GameObject.Find("StartPiece(4,4)");

        boardData = MinMax.FindValidMoves(MinMax.PlayerColor, boardData);
        showValidMoves(MinMax.PlayerColor, true);
        //MinMax.FindValidMoves(MinMax.AIColor);
        //showValidMoves(MinMax.AIColor, false);
    }

    public bool isEmpty(int x, int y) {
        //Debug.Log("X: " + x);
       //Debug.Log("Y: " + y);
        //Debug.Log(boardData[x, y]);
        if(boardData[x,y] == 0 || boardData[x,y] == 2 || boardData[x,y] == 3) {
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
        pieceRefrences[a[0, 0], a[0, 1]] = DragObject.self;
        //Debug.Log(isBlack);
        if (isBlack == true) {
            boardData[a[0, 0], a[0, 1]] = -1;
        }
        else {
            boardData[a[0, 0], a[0, 1]] = 1;
        }

        BoardLogic(a[0, 0], a[0, 1], MinMax.PlayerColor);

        //boardData = MinMax.FindValidMoves(MinMax.PlayerColor, boardData);
        showVM = true;
    }

    private static void BoardLogic(int row, int col, int color)
    {
        var currentRow = row;
        var currentCol = col;

        //Check left
        currentRow -= 1;
        while(currentRow >= 0)
        {
            if(boardData[currentRow, currentCol] == color)
            {
                break;
            }
            updatePieceColor(currentRow, currentCol, color);
            currentRow--;
        }
        currentRow = row;
        currentCol = col;


        //Check right
        currentRow += 1;
        while (currentRow <= 7)
        {
            if (boardData[currentRow, currentCol] == color)
            {
                break;
            }
            updatePieceColor(currentRow, currentCol, color);
            currentRow++;
        }
        currentRow = row;
        currentCol = col;

        //Check up
        currentCol -= 1;
        while (currentCol >= 0)
        {
            if (boardData[currentRow, currentCol] == color)
            {
                break;
            }
            updatePieceColor(currentRow, currentCol, color);
            currentCol--;
        }
        currentRow = row;
        currentCol = col;

        //Check down
        currentCol += 1;
        while (currentCol <= 7)
        {
            if (boardData[currentRow, currentCol] == color)
            {
                break;
            }
            updatePieceColor(currentRow, currentCol, color);
            currentCol++;
        }
        currentRow = row;
        currentCol = col;

        //Check down left
        currentCol += 1;
        currentRow -= 1;
        while (currentCol <= 7 && currentRow >= 0)
        {
            if (boardData[currentRow, currentCol] == color)
            {
                break;
            }
            updatePieceColor(currentRow, currentCol, color);
            currentCol++;
            currentRow--;
        }
        currentRow = row;
        currentCol = col;

        //Check down right
        currentCol += 1;
        currentRow += 1;
        while (currentCol <= 7 && currentRow <= 7)
        {
            if (boardData[currentRow, currentCol] == color)
            {
                break;
            }
            updatePieceColor(currentRow, currentCol, color);
            currentCol++;
            currentRow++;
        }
        currentRow = row;
        currentCol = col;

        //Check up left
        currentCol -= 1;
        currentRow -= 1;
        while (currentCol >= 0 && currentRow >= 0)
        {
            if (boardData[currentRow, currentCol] == color)
            {
                break;
            }
            updatePieceColor(currentRow, currentCol, color);
            currentCol--;
            currentRow--;
        }
        currentRow = row;
        currentCol = col;

        //Check up right
        currentCol -= 1;
        currentRow += 1;
        while (currentCol >= 0 && currentRow <= 7)
        {
            if (boardData[currentRow, currentCol] == color)
            {
                break;
            }
            updatePieceColor(currentRow, currentCol, color);
            currentCol--;
            currentRow++;
        }

        BoardDataUpdated = true;
    }

    public static void updatePieceColor(int currentRow, int currentCol, int color)
    {
        if (boardData[currentRow, currentCol] == color * -1)
        {
            boardData[currentRow, currentCol] = color;
            //Debug.Log(currentRow + ":" + currentCol);
            //Debug.Log(TranslateToGameData(currentRow, currentCol)[0, 0] + ":" + TranslateToGameData(currentRow, currentCol)[0, 1]);
            //Debug.Log(pieceArray[new Vector2(TranslateToGameData(currentRow, currentCol)[0, 0], TranslateToGameData(currentRow, currentCol)[0, 1])]);
            var tempPiece = (DragObject)pieceArray[new Vector2(TranslateToGameData(currentRow, currentCol)[0, 0], TranslateToGameData(currentRow, currentCol)[0, 1])];
            if (tempPiece.anim.GetBool("isBlack"))
            {
                tempPiece.anim.SetBool("isBlack", false);
                tempPiece.isBlack = false;
            }
            else
            {
                tempPiece.anim.SetBool("isBlack", true);
                tempPiece.isBlack = true;
            }

        }
    }

    public static void removeValidMoveGhosts()
    {
        GameObject[] validMovesOnBoard = GameObject.FindGameObjectsWithTag("ValidMove");
        for (int i = 0; i < validMovesOnBoard.Length; i++)
        {
            Destroy(validMovesOnBoard[i]);
        }
    }

    public void showValidMoves(int color, bool removePrevious)
    {
        //Debug.Log("SHOWING MOVES");
        if(removePrevious)
        {
            removeValidMoveGhosts();
        }

        for (int i = 0; i <= 7; i++)
        {
            for (int j = 0; j <= 7; j++)
            {
                if(boardData[i,j] == 2)
                {
                    float[,] temp = TranslateToGameData(i, j);
                    Instantiate(validMovePoint, new Vector3(temp[0, 0], 1.1f, temp[0, 1]), Quaternion.identity);
                } else if(boardData[i, j] == 3)
                {
                    float[,] temp = TranslateToGameData(i, j);
                    Instantiate(validAIMovePoint, new Vector3(temp[0, 0], 1.1f, temp[0, 1]), Quaternion.identity);
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

            if (isValidMove(a[0, 0], a[0, 1]) && isPlayerTurn)
            {
                DragObject.canPlace = true;
            }
            else
            {
                DragObject.canPlace = false;
            }

            //Debug.Log(DragObject.canPlace);
        }

        if(showVM && isPlayerTurn)
        {
            Debug.Log("SHOW");
            boardData = MinMax.FindValidMoves(MinMax.PlayerColor, boardData);
            showValidMoves(MinMax.PlayerColor, true);
            showVM = false;
            //MinMax.FindValidMoves(MinMax.AIColor);
            //showValidMoves(MinMax.AIColor, false);
        }
    }
}
