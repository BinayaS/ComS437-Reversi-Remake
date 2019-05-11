using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMax : MonoBehaviour
{
    //Set player to black
    public static int PlayerColor = -1;
    public static int AIColor = 1;
    public static int AIDifficulty = 1;
    public static bool AITurn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static int[,] FindValidMoves(int color, int[,] boardData)
    {
        int[,] tempBD = boardData;
        for (int row = 0; row <= 7; row++)
        {
            for(int col = 0; col <= 7; col++)
            {
                //Check for a player color piece
                if (tempBD[row, col] == color)
                {
                    tempBD = checkLeft(row, col, color, tempBD);
                    tempBD = checkRight(row, col, color, tempBD);
                    tempBD = checkUp(row, col, color, tempBD);
                    tempBD = checkDown(row, col, color, tempBD);
                    tempBD = checkDiagonalLeftDown(row, col, color, tempBD);
                    tempBD = checkDiagonalLeftUp(row, col, color, tempBD);
                    tempBD = checkDiagonalRightDown(row, col, color, tempBD);
                    tempBD = checkDiagonalRightUp(row, col, color, tempBD);
                }
            }
        }
        return tempBD;
    }

    private static int[,] checkLeft(int row, int col, int color, int[,] boardData)
    {
        int[,] tempBD = boardData;
        var currentRow = row - 1;
        var currentCol = col;
        var foundOtherColor = false;
        while(currentRow >= 0)
        {
            //Check if left side is empty
            if (tempBD[currentRow, currentCol] == 0 || tempBD[currentRow, currentCol] == 2 || tempBD[currentRow, currentCol] == 3)
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
                        if(color == PlayerColor)
                        {
                            tempBD[currentRow, currentCol] = 2;
                        } else
                        {
                            tempBD[currentRow, currentCol] = 3;
                        }
                        
                        //Debug.Log("LEFT " + "X: " + currentRow + " _ " + "Y: " + currentCol);
                        //Debug.Log("LEFT " + "row: " + row + " _ " + "col: " + col);
                    }
                    break;
                }
            } else
            {
                if (tempBD[currentRow, currentCol] == color * -1)
                {
                    foundOtherColor = true;
                }
                else if(tempBD[currentRow, currentCol] == color)
                {
                    foundOtherColor = false;
                }
                //Keep looking left
                currentRow--;
            }
        }
        return tempBD;
    }
    private static int[,] checkRight(int row, int col, int color, int[,] boardData)
    {
        int[,] tempBD = boardData;
        var currentRow = row + 1;
        var currentCol = col;
        var foundOtherColor = false;
        while (currentRow <= 7)
        {
            //Check if right side is empty
            if (tempBD[currentRow, currentCol] == 0 || tempBD[currentRow, currentCol] == 2 || tempBD[currentRow, currentCol] == 3)
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
                        if (color == PlayerColor)
                        {
                            tempBD[currentRow, currentCol] = 2;
                        }
                        else
                        {
                            tempBD[currentRow, currentCol] = 3;
                        }
                        //Debug.Log("RIGHT " + "X: " + currentRow + " _ " + "Y: " + currentCol);
                        //Debug.Log("RIGHT " + "row: " + row + " _ " + "col: " + col);
                    }
                    break;
                }
            }
            else
            {
                if (tempBD[currentRow, currentCol] == color * -1)
                {
                    foundOtherColor = true;
                }
                else if (tempBD[currentRow, currentCol] == color)
                {
                    foundOtherColor = false;
                }
                //Keep looking right
                currentRow++;
            }
        }
        return tempBD;
    }

    private static int[,] checkUp(int row, int col, int color, int[,] boardData)
    {
        int[,] tempBD = boardData;
        var currentRow = row;
        var currentCol = col - 1;
        var foundOtherColor = false;
        while (currentCol >= 0)
        {
            //Check if up side is empty
            if (tempBD[currentRow, currentCol] == 0 || tempBD[currentRow, currentCol] == 2 || tempBD[currentRow, currentCol] == 3)
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
                        if (color == PlayerColor)
                        {
                            tempBD[currentRow, currentCol] = 2;
                        }
                        else
                        {
                            tempBD[currentRow, currentCol] = 3;
                        }
                        //Debug.Log("UP " + "X: " + currentRow + " _ " + "Y: " + currentCol);
                        //Debug.Log("UP " + "row: " + row + " _ " + "col: " + col);
                    }
                    break;
                }
            }
            else
            {
                if (tempBD[currentRow, currentCol] == color * -1)
                {
                    foundOtherColor = true;
                }
                else if (tempBD[currentRow, currentCol] == color)
                {
                    foundOtherColor = false;
                }
                //Keep looking left
                currentCol--;
            }
        }
        return tempBD;
    }
    private static int[,] checkDown(int row, int col, int color, int[,] boardData)
    {
        int[,] tempBD = boardData;
        var currentRow = row;
        var currentCol = col + 1;
        var foundOtherColor = false;
        while (currentCol <= 7)
        {
            //Check if down side is empty
            if (tempBD[currentRow, currentCol] == 0 || tempBD[currentRow, currentCol] == 2 || tempBD[currentRow, currentCol] == 3)
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
                        if (color == PlayerColor)
                        {
                            tempBD[currentRow, currentCol] = 2;
                        }
                        else
                        {
                            tempBD[currentRow, currentCol] = 3;
                        }
                        //Debug.Log("DOWN " + "X: " + currentRow + " _ " + "Y: " + currentCol);
                        //Debug.Log("DOWN " + "row: " + row + " _ " + "col: " + col);
                    }
                    break;
                }
            }
            else
            {
                if (tempBD[currentRow, currentCol] == color * -1)
                {
                    foundOtherColor = true;
                }
                else if (tempBD[currentRow, currentCol] == color)
                {
                    foundOtherColor = false;
                }
                //Keep looking down
                currentCol++;
            }
        }
        return tempBD;
    }

    private static int[,] checkDiagonalLeftDown(int row, int col, int color, int[,] boardData)
    {
        int[,] tempBD = boardData;
        var currentRow = row - 1;
        var currentCol = col + 1;
        var foundOtherColor = false;
        while (currentCol <= 7 && currentRow >= 0)
        {
            //Check if left down side is empty
            if (tempBD[currentRow, currentCol] == 0 || tempBD[currentRow, currentCol] == 2 || tempBD[currentRow, currentCol] == 3)
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
                        if (color == PlayerColor)
                        {
                            tempBD[currentRow, currentCol] = 2;
                        }
                        else
                        {
                            tempBD[currentRow, currentCol] = 3;
                        }
                        //Debug.Log("LEFT DOWN " + "X: " + currentRow + " _ " + "Y: " + currentCol);
                        //Debug.Log("LEFT DOWN " + "row: " + row + " _ " + "col: " + col);
                    }
                    break;
                }
            }
            else
            {
                if (tempBD[currentRow, currentCol] == color * -1)
                {
                    foundOtherColor = true;
                }
                else if (tempBD[currentRow, currentCol] == color)
                {
                    foundOtherColor = false;
                }
                //Keep looking left down
                currentCol++;
                currentRow--;
            }
        }
        return tempBD;
    }
    private static int[,] checkDiagonalRightDown(int row, int col, int color, int[,] boardData)
    {
        int[,] tempBD = boardData;
        var currentRow = row + 1;
        var currentCol = col + 1;
        var foundOtherColor = false;
        while (currentCol <= 7 && currentRow <= 7)
        {
            //Check if right down side is empty
            if (tempBD[currentRow, currentCol] == 0 || tempBD[currentRow, currentCol] == 2 || tempBD[currentRow, currentCol] == 3)
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
                        if (color == PlayerColor)
                        {
                            tempBD[currentRow, currentCol] = 2;
                        }
                        else
                        {
                            tempBD[currentRow, currentCol] = 3;
                        }
                        //Debug.Log("RIGHT DOWN " + "X: " + currentRow + " _ " + "Y: " + currentCol);
                    }
                    break;
                }
            }
            else
            {
                if (tempBD[currentRow, currentCol] == color * -1)
                {
                    foundOtherColor = true;
                }
                else if (tempBD[currentRow, currentCol] == color)
                {
                    foundOtherColor = false;
                }
                //Keep looking right down
                currentCol++;
                currentRow++;
            }
        }
        return tempBD;
    }
    private static int[,] checkDiagonalLeftUp(int row, int col, int color, int[,] boardData)
    {
        int[,] tempBD = boardData;
        var currentRow = row - 1;
        var currentCol = col - 1;
        var foundOtherColor = false;
        while (currentCol >= 0 && currentRow >= 0)
        {
            //Check if left up side is empty
            if (tempBD[currentRow, currentCol] == 0 || tempBD[currentRow, currentCol] == 2 || tempBD[currentRow, currentCol] == 3)
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
                        if (color == PlayerColor)
                        {
                            tempBD[currentRow, currentCol] = 2;
                        }
                        else
                        {
                            tempBD[currentRow, currentCol] = 3;
                        }
                        //Debug.Log("LEFT UP " + "X: " + currentRow + " _ " + "Y: " + currentCol);
                        //Debug.Log("LEFT UP " + "row: " + row + " _ " + "col: " + col);
                    }
                    break;
                }
            }
            else
            {
                if (tempBD[currentRow, currentCol] == color * -1)
                {
                    foundOtherColor = true;
                }
                else if (tempBD[currentRow, currentCol] == color)
                {
                    foundOtherColor = false;
                }
                //Keep looking left up
                currentCol--;
                currentRow--;
            }
        }
        return tempBD;
    }
    private static int[,] checkDiagonalRightUp(int row, int col, int color, int[,] boardData)
    {
        int[,] tempBD = boardData;
        var currentRow = row + 1;
        var currentCol = col - 1;
        var foundOtherColor = false;
        while (currentCol >= 0 && currentRow <= 7)
        {
            //Check if right up side is empty
            if (tempBD[currentRow, currentCol] == 0 || tempBD[currentRow, currentCol] == 2 || tempBD[currentRow, currentCol] == 3)
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
                        if (color == PlayerColor)
                        {
                            tempBD[currentRow, currentCol] = 2;
                        }
                        else
                        {
                            tempBD[currentRow, currentCol] = 3;
                        }
                        //Debug.Log("RIGHT UP " + "X: " + currentRow + " _ " + "Y: " + currentCol);
                    }
                    break;
                }
            }
            else
            {
                if (tempBD[currentRow, currentCol] == color * -1)
                {
                    foundOtherColor = true;
                }
                else if (tempBD[currentRow, currentCol] == color)
                {
                    foundOtherColor = false;
                }
                //Keep looking right up
                currentCol--;
                currentRow++;
            }
        }
        return tempBD;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameBoardController.isPlayerTurn == false && GameBoardController.BoardDataUpdated == true)
        {
            Debug.Log("AI TURN");
            AITurn = true;
            //TODO: 
            //Create Tree & do min max & make turn

            Node a = new Node();
            System.Array.Copy(GameBoardController.boardData, a.myBoardData, GameBoardController.boardData.GetLength(0) * GameBoardController.boardData.GetLength(1));
            Debug.Log("Simulate Start");
            a.Simulate(true, AIDifficulty, true, 0);
            Debug.Log("Simulate Done");

            //Set back to player's turn
            GameBoardController.isPlayerTurn = true;
            GameBoardController.showVM = true;
            AITurn = false;
            Debug.Log("End AI Turn");
        }
    }
}
