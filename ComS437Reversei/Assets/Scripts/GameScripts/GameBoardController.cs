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
    public GameObject showAIMove;
    public static Vector3 boardOffset = new Vector3(-4.0f, 1.5f, -4.0f);
    public static Vector3 pieceOffset = new Vector3(0.0f, 0.5f, 0.0f);
    public static bool showVM = false;
    public static bool isPlayerTurn = true;
    public static int AIMoveX = -1;
    public static int AIMoveY = -1;
    public static bool BoardDataUpdated = true;
    public static Hashtable pieceArray = new Hashtable();
    public GameObject piece;
    public int AITimerDuration = 40;
    public int AITimer = 0;
    public static int gamePieces = 4;
    public static int maxGamePieces = 64;
    public static bool gameOver = false;
    public static int blackPieces = 2;
    public static int whitePieces = 2;
    public static Node MoveNode = new Node();

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
        if (x >= 0 && x <= 8 && y >= 0 && y <= 8 && boardData[x, y] == 2)
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

        removeBoardValidMoves(boardData);
        
        //Sets BoardDataUpdated = true
        BoardLogic(a[0, 0], a[0, 1], MinMax.PlayerColor);

        //boardData = MinMax.FindValidMoves(MinMax.PlayerColor, boardData);
        showVM = true;
    }

    public static void BoardLogic(int row, int col, int color)
    {
        var currentRow = row;
        var currentCol = col;
        bool foundSameColor = false;

        //Check left
        currentRow -= 1;
        while(currentRow >= 0)
        {
            if(boardData[currentRow, currentCol] == color)
            {
                foundSameColor = true;
                break;
            }
            if (boardData[currentRow, currentCol] == 0) {
                break;
            }
            currentRow--;
        }
        if(foundSameColor)
        {
            for (int i = row; i > currentRow; i--)
            {
                updatePieceColor(i, currentCol, color);
            }
        }
        
        currentRow = row;
        currentCol = col;
        foundSameColor = false;


        //Check right
        currentRow += 1;
        while (currentRow <= 7)
        {
            if (boardData[currentRow, currentCol] == color)
            {
                foundSameColor = true;
                break;
            }
            if (boardData[currentRow, currentCol] == 0) {
                break;
            }
            currentRow++;
        }
        if (foundSameColor)
        {
            for (int i = row; i < currentRow; i++)
            {
                updatePieceColor(i, currentCol, color);
            }
        }

        currentRow = row;
        currentCol = col;
        foundSameColor = false;


        //Check up
        currentCol -= 1;
        while (currentCol >= 0)
        {
            if (boardData[currentRow, currentCol] == color)
            {
                foundSameColor = true;
                break;
            }
            if (boardData[currentRow, currentCol] == 0) {
                break;
            }
            currentCol--;
        }
        if (foundSameColor)
        {
            for (int i = col; i > currentCol; i--)
            {
                updatePieceColor(currentRow, i, color);
            }
        }
        currentRow = row;
        currentCol = col;
        foundSameColor = false;

        //Check down
        currentCol += 1;
        while (currentCol <= 7)
        {
            if (boardData[currentRow, currentCol] == color)
            {
                foundSameColor = true;
                break;
            }
            if (boardData[currentRow, currentCol] == 0) {
                break;
            }
            currentCol++;
        }
        if (foundSameColor)
        {
            for (int i = col; i < currentCol; i++)
            {
                updatePieceColor(currentRow, i, color);
            }
        }
        currentRow = row;
        currentCol = col;
        foundSameColor = false;


        //Check down left
        currentCol += 1;
        currentRow -= 1;
        while (currentCol <= 7 && currentRow >= 0)
        {
            if (boardData[currentRow, currentCol] == color)
            {
                foundSameColor = true;
                break;
            }
            if (boardData[currentRow, currentCol] == 0) {
                break;
            }
            currentCol++;
            currentRow--;
        }
        if(foundSameColor)
        {
            while(currentCol != col && currentRow != row)
            {
                currentCol--;
                currentRow++;
                updatePieceColor(currentRow, currentCol, color);
            }
        }
        currentRow = row;
        currentCol = col;
        foundSameColor = false;

        //Check down right
        currentCol += 1;
        currentRow += 1;
        while (currentCol <= 7 && currentRow <= 7)
        {
            if (boardData[currentRow, currentCol] == color)
            {
                foundSameColor = true;
                break;
            }
            if(boardData[currentRow, currentCol] == 0) {
                break;
            }
            currentCol++;
            currentRow++;
        }
        if (foundSameColor)
        {
            while (currentCol != col && currentRow != row)
            {
                currentCol--;
                currentRow--;
                updatePieceColor(currentRow, currentCol, color);
            }
        }
        currentRow = row;
        currentCol = col;
        foundSameColor = false;

        //Check up left
        currentCol -= 1;
        currentRow -= 1;
        while (currentCol >= 0 && currentRow >= 0)
        {
            if (boardData[currentRow, currentCol] == color)
            {
                foundSameColor = true;
                break;
            }
            if (boardData[currentRow, currentCol] == 0) {
                break;
            }
            currentCol--;
            currentRow--;
        }
        if (foundSameColor)
        {
            while (currentCol != col && currentRow != row)
            {
                currentCol++;
                currentRow++;
                updatePieceColor(currentRow, currentCol, color);
            }
        }
        currentRow = row;
        currentCol = col;
        foundSameColor = false;

        //Check up right
        currentCol -= 1;
        currentRow += 1;
        while (currentCol >= 0 && currentRow <= 7)
        {
            if (boardData[currentRow, currentCol] == color)
            {
                foundSameColor = true;
                break;
            }
            if (boardData[currentRow, currentCol] == 0) {
                break;
            }
            currentCol--;
            currentRow++;
        }
        if (foundSameColor)
        {
            while (currentCol != col && currentRow != row)
            {
                currentCol++;
                currentRow--;
                updatePieceColor(currentRow, currentCol, color);
            }
        }
        BoardDataUpdated = true;
    }

    public static void updatePieceColor(int currentRow, int currentCol, int color)
    {
        if (boardData[currentRow, currentCol] == (color * -1))
        {
            
            boardData[currentRow, currentCol] = color;
            //Debug.Log(currentRow + " -|- " + currentCol + " : " + boardData[currentRow, currentCol]);
            //Debug.Log(currentRow + ":" + currentCol);
            //Debug.Log(TranslateToGameData(currentRow, currentCol)[0, 0] + ":" + TranslateToGameData(currentRow, currentCol)[0, 1]);
            //Debug.Log(pieceArray[new Vector2(TranslateToGameData(currentRow, currentCol)[0, 0], TranslateToGameData(currentRow, currentCol)[0, 1])]);
            var tempPiece = (GameObject)pieceArray[new Vector2(TranslateToGameData(currentRow, currentCol)[0, 0], TranslateToGameData(currentRow, currentCol)[0, 1])];
            if(tempPiece != null) {
                if (tempPiece.GetComponent<DragObject>().anim.GetBool("isBlack")) {
                    tempPiece.GetComponent<DragObject>().anim.SetBool("isBlack", false);
                    tempPiece.GetComponent<DragObject>().isBlack = false;
                }
                else {
                    tempPiece.GetComponent<DragObject>().anim.SetBool("isBlack", true);
                    tempPiece.GetComponent<DragObject>().isBlack = true;
                }
            }
        }
    }

    public static void removeBoardValidMoves(int[,] currentBoardData) {
        for (int i = 0; i <= 7; i++) {
            for (int j = 0; j <= 7; j++) {
                if(currentBoardData[i,j] == 2 || currentBoardData[i,j] == 3) {
                    currentBoardData[i, j] = 0;
                }
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
        bool foundValidMove = false;
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
                    foundValidMove = true;
                    //Show possible moves
                    //Debug.Log(i + "," + j);
                } else if(boardData[i, j] == 3)
                {
                    float[,] temp = TranslateToGameData(i, j);
                    Instantiate(validAIMovePoint, new Vector3(temp[0, 0], 1.1f, temp[0, 1]), Quaternion.identity);
                    foundValidMove = true;
                }
            }
        }
        if(foundValidMove == false) {
            isPlayerTurn = false;
            BoardDataUpdated = true;
        }
    }

    public void findNumberOfPieces() {
        int black = 0;
        int white = 0;
        for (int i = 0; i <= 7; i++) {
            for (int j = 0; j <= 7; j++) {
                if (boardData[i, j] == 1) {
                    white++;
                }
                else if(boardData[i,j] == -1) {
                    black++;
                }
            }
        }
        whitePieces = white;
        blackPieces = black;
    }

    // Update is called once per frame
    void Update()
    {
        findNumberOfPieces();

        if (DragObject.isPickedUp == true && DragObject.placingLocation != null) {
            var a = TranslateToBoardData(DragObject.placingLocation);

            if (isValidMove(a[0, 0], a[0, 1]) && isPlayerTurn && !MinMax.AITurn)
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
            boardData = MinMax.FindValidMoves(MinMax.PlayerColor, boardData);
            showValidMoves(MinMax.PlayerColor, true);
            showVM = false;
        }

        if(AIMoveX != -1 && AIMoveY != -1)
        {
            AITimer++;
            if(AITimer > AITimerDuration) {
                float[,] temp = TranslateToGameData(AIMoveX, AIMoveY);
                GameObject tempPiece = Instantiate(piece, new Vector3(temp[0, 0], 1.1f, temp[0, 1]), Quaternion.identity);
                showAIMove.transform.position = new Vector3(temp[0, 0], 2.1f, temp[0, 1]);
                pieceArray.Add(new Vector2(tempPiece.gameObject.transform.position.x, tempPiece.gameObject.transform.position.z), tempPiece.gameObject);
                gamePieces++;
                //Debug.Log(tempPiece.transform.position.z + ":" + tempPiece.transform.position.x);
                boardData[AIMoveX, AIMoveY] = MinMax.AIColor;
                pieceRefrences[AIMoveX, AIMoveY] = tempPiece;
                removeBoardValidMoves(boardData);

                BoardLogic(AIMoveX, AIMoveY, MinMax.AIColor);

                AIMoveX = -1;
                AIMoveY = -1;
                isPlayerTurn = true;
                showVM = true;
                AITimer = 0;
                MinMax.AITurn = false;
                Debug.Log("AI Placed Piece");
                Debug.Log("Game Pieces on Board: " + gamePieces);
            }
        }

        if(gamePieces == maxGamePieces && gameOver == false) {
            gameOver = true;
            
        }
    }
}
