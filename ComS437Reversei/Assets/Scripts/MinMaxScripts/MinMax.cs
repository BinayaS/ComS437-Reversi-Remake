using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMax : MonoBehaviour
{
    public static int AIColor = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static void FindValidMoves()
    {
        for (int row = 0; row <= 7; row++)
        {
            for(int col = 0; col <= 7; col++)
            {
                //Check for a AI Color piece
                if (GameBoardController.boardData[row, col] == AIColor)
                {
                    checkLeft(row, col);
                    checkRight(row, col);
                    checkUp(row, col);
                    checkDown(row, col);
                    checkDiagonalLeftDown(row, col);
                    checkDiagonalLeftUp(row, col);
                    checkDiagonalRightDown(row, col);
                    checkDiagonalRightUp(row, col);
                    //Debug.Log("_____");
                }
            }
        }
    }

    private static void checkLeft(int row, int col)
    {
        var currentRow = row - 1;
        var currentCol = col;
        var foundOtherColor = false;
        while(currentRow >= 0)
        {
            //Check if left side is empty
            if (GameBoardController.boardData[currentRow, currentCol] == 0 || GameBoardController.boardData[currentRow, currentCol] == 2)
            {
                //if there is no piece directly left, stop looking
                if(currentRow == row - 1)
                {
                    break;
                } else
                {
                    //we found a valid move if we hit atleast one other piece with the opposite color of AI Color
                    if(foundOtherColor)
                    {
                        GameBoardController.boardData[currentRow, currentCol] = 2;
                        //Debug.Log("LEFT " + "X: " + currentRow + " _ " + "Y: " + currentCol);
                        //Debug.Log("LEFT " + "row: " + row + " _ " + "col: " + col);
                    }
                    break;
                }
            } else
            {
                if (GameBoardController.boardData[currentRow, currentCol] == AIColor * -1)
                {
                    foundOtherColor = true;
                }
                else if(GameBoardController.boardData[currentRow, currentCol] == AIColor)
                {
                    foundOtherColor = false;
                }
                //Keep looking left
                currentRow--;
            }
        }
    }
    private static void checkRight(int row, int col)
    {
        var currentRow = row + 1;
        var currentCol = col;
        var foundOtherColor = false;
        while (currentRow <= 7)
        {
            //Check if right side is empty
            if (GameBoardController.boardData[currentRow, currentCol] == 0 || GameBoardController.boardData[currentRow, currentCol] == 2)
            {
                //if there is no piece directly right, stop looking
                if (currentRow == row + 1)
                {
                    break;
                }
                else
                {
                    //we found a valid move if we hit atleast one other piece with the opposite color of AI Color
                    if (foundOtherColor)
                    {
                        GameBoardController.boardData[currentRow, currentCol] = 2;
                        //Debug.Log("RIGHT " + "X: " + currentRow + " _ " + "Y: " + currentCol);
                        //Debug.Log("RIGHT " + "row: " + row + " _ " + "col: " + col);
                    }
                    break;
                }
            }
            else
            {
                if (GameBoardController.boardData[currentRow, currentCol] == AIColor * -1)
                {
                    foundOtherColor = true;
                }
                else if (GameBoardController.boardData[currentRow, currentCol] == AIColor)
                {
                    foundOtherColor = false;
                }
                //Keep looking right
                currentRow++;
            }
        }
    }

    private static void checkUp(int row, int col)
    {
        var currentRow = row;
        var currentCol = col - 1;
        var foundOtherColor = false;
        while (currentCol >= 0)
        {
            //Check if up side is empty
            if (GameBoardController.boardData[currentRow, currentCol] == 0 || GameBoardController.boardData[currentRow, currentCol] == 2)
            {
                //if there is no piece directly left, stop looking
                if (currentCol == col - 1)
                {
                    break;
                }
                else
                {
                    //we found a valid move if we hit atleast one other piece with the opposite color of AI Color
                    if (foundOtherColor)
                    {
                        GameBoardController.boardData[currentRow, currentCol] = 2;
                        //Debug.Log("UP " + "X: " + currentRow + " _ " + "Y: " + currentCol);
                        //Debug.Log("UP " + "row: " + row + " _ " + "col: " + col);
                    }
                    break;
                }
            }
            else
            {
                if (GameBoardController.boardData[currentRow, currentCol] == AIColor * -1)
                {
                    foundOtherColor = true;
                }
                else if (GameBoardController.boardData[currentRow, currentCol] == AIColor)
                {
                    foundOtherColor = false;
                }
                //Keep looking left
                currentCol--;
            }
        }
    }
    private static void checkDown(int row, int col)
    {
        var currentRow = row;
        var currentCol = col + 1;
        var foundOtherColor = false;
        while (currentCol <= 7)
        {
            //Check if down side is empty
            if (GameBoardController.boardData[currentRow, currentCol] == 0 || GameBoardController.boardData[currentRow, currentCol] == 2)
            {
                //if there is no piece directly down, stop looking
                if (currentCol == col + 1)
                {
                    break;
                }
                else
                {
                    //we found a valid move if we hit atleast one other piece with the opposite color of AI Color
                    if (foundOtherColor)
                    {
                        GameBoardController.boardData[currentRow, currentCol] = 2;
                        //Debug.Log("DOWN " + "X: " + currentRow + " _ " + "Y: " + currentCol);
                        //Debug.Log("DOWN " + "row: " + row + " _ " + "col: " + col);
                    }
                    break;
                }
            }
            else
            {
                if (GameBoardController.boardData[currentRow, currentCol] == AIColor * -1)
                {
                    foundOtherColor = true;
                }
                else if (GameBoardController.boardData[currentRow, currentCol] == AIColor)
                {
                    foundOtherColor = false;
                }
                //Keep looking down
                currentCol++;
            }
        }
    }

    private static void checkDiagonalLeftDown(int row, int col)
    {
        var currentRow = row - 1;
        var currentCol = col + 1;
        var foundOtherColor = false;
        while (currentCol <= 7 && currentRow >= 0)
        {
            //Check if left down side is empty
            if (GameBoardController.boardData[currentRow, currentCol] == 0 || GameBoardController.boardData[currentRow, currentCol] == 2)
            {
                //if there is no piece directly left down, stop looking
                if (currentCol == col + 1 && currentRow == row - 1)
                {
                    break;
                }
                else
                {
                    //we found a valid move if we hit atleast one other piece with the opposite color of AI Color
                    if (foundOtherColor)
                    {
                        GameBoardController.boardData[currentRow, currentCol] = 2;
                        //Debug.Log("LEFT DOWN " + "X: " + currentRow + " _ " + "Y: " + currentCol);
                        //Debug.Log("LEFT DOWN " + "row: " + row + " _ " + "col: " + col);
                    }
                    break;
                }
            }
            else
            {
                if (GameBoardController.boardData[currentRow, currentCol] == AIColor * -1)
                {
                    foundOtherColor = true;
                }
                else if (GameBoardController.boardData[currentRow, currentCol] == AIColor)
                {
                    foundOtherColor = false;
                }
                //Keep looking left down
                currentCol++;
                currentRow--;
            }
        }
    }
    private static void checkDiagonalRightDown(int row, int col)
    {
        var currentRow = row + 1;
        var currentCol = col + 1;
        var foundOtherColor = false;
        while (currentCol <= 7 && currentRow <= 7)
        {
            //Check if right down side is empty
            if (GameBoardController.boardData[currentRow, currentCol] == 0 || GameBoardController.boardData[currentRow, currentCol] == 2)
            {
                //if there is no piece directly right down, stop looking
                if (currentCol == col + 1 && currentRow == row + 1)
                {
                    break;
                }
                else
                {
                    //we found a valid move if we hit atleast one other piece with the opposite color of AI Color
                    if (foundOtherColor)
                    {
                        GameBoardController.boardData[currentRow, currentCol] = 2;
                        //Debug.Log("RIGHT DOWN " + "X: " + currentRow + " _ " + "Y: " + currentCol);
                    }
                    break;
                }
            }
            else
            {
                if (GameBoardController.boardData[currentRow, currentCol] == AIColor * -1)
                {
                    foundOtherColor = true;
                }
                else if (GameBoardController.boardData[currentRow, currentCol] == AIColor)
                {
                    foundOtherColor = false;
                }
                //Keep looking right down
                currentCol++;
                currentRow++;
            }
        }
    }
    private static void checkDiagonalLeftUp(int row, int col)
    {
        var currentRow = row - 1;
        var currentCol = col - 1;
        var foundOtherColor = false;
        while (currentCol >= 0 && currentRow >= 0)
        {
            //Check if left up side is empty
            if (GameBoardController.boardData[currentRow, currentCol] == 0 || GameBoardController.boardData[currentRow, currentCol] == 2)
            {
                //if there is no piece directly left up, stop looking
                if (currentCol == col - 1 && currentRow == row - 1)
                {
                    break;
                }
                else
                {
                    //we found a valid move if we hit atleast one other piece with the opposite color of AI Color
                    if (foundOtherColor)
                    {
                        GameBoardController.boardData[currentRow, currentCol] = 2;
                        //Debug.Log("LEFT UP " + "X: " + currentRow + " _ " + "Y: " + currentCol);
                        //Debug.Log("LEFT UP " + "row: " + row + " _ " + "col: " + col);
                    }
                    break;
                }
            }
            else
            {
                if (GameBoardController.boardData[currentRow, currentCol] == AIColor * -1)
                {
                    foundOtherColor = true;
                }
                else if (GameBoardController.boardData[currentRow, currentCol] == AIColor)
                {
                    foundOtherColor = false;
                }
                //Keep looking left up
                currentCol--;
                currentRow--;
            }
        }
    }
    private static void checkDiagonalRightUp(int row, int col)
    {
        var currentRow = row + 1;
        var currentCol = col - 1;
        var foundOtherColor = false;
        while (currentCol >= 0 && currentRow <= 7)
        {
            //Check if right up side is empty
            if (GameBoardController.boardData[currentRow, currentCol] == 0 || GameBoardController.boardData[currentRow, currentCol] == 2)
            {
                //if there is no piece directly right up, stop looking
                if (currentCol == col - 1 && currentRow == row + 1)
                {
                    break;
                }
                else
                {
                    //we found a valid move if we hit atleast one other piece with the opposite color of AI Color
                    if (foundOtherColor)
                    {
                        GameBoardController.boardData[currentRow, currentCol] = 2;
                        //Debug.Log("RIGHT UP " + "X: " + currentRow + " _ " + "Y: " + currentCol);
                    }
                    break;
                }
            }
            else
            {
                if (GameBoardController.boardData[currentRow, currentCol] == AIColor * -1)
                {
                    foundOtherColor = true;
                }
                else if (GameBoardController.boardData[currentRow, currentCol] == AIColor)
                {
                    foundOtherColor = false;
                }
                //Keep looking right up
                currentCol--;
                currentRow++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
