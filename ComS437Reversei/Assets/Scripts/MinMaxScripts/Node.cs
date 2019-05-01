using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node parent;
    public List<Node> children = new List<Node>();
    public int value;
    public int[,] myBoardData = new int[8,8];
    public bool isAI;
    public bool isMax;
    public int depth;
    public int moveX = -1;
    public int moveY = -1;

    public void Simulate(bool isAI, int depth, bool isMax)
    {
        this.isAI = isAI;
        this.isMax = isMax;
        this.depth = depth;

        if(isAI)
        {
            myBoardData = MinMax.FindValidMoves(MinMax.AIColor, myBoardData);
            
            /*
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    Debug.Log(i + ":" + j + " = " + myBoardData[i, j]);
                }
            }
            */
            CreateChildren(MinMax.AIColor, isAI);

        } else
        {
            myBoardData = MinMax.FindValidMoves(MinMax.PlayerColor, myBoardData);
            CreateChildren(MinMax.PlayerColor, isAI);
        }

        if(depth > 0)
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].ClearBoardData();
                children[i].Simulate(!isAI, depth - 1, !isMax);
            }
        }

        if(this.depth == MinMax.AIDifficulty)
        {
            //Do min max
            int searchVal = DoMinMax(this, MinMax.AIDifficulty - 1, true);
            //Debug.Log(searchVal);
            for(int i = 0; i < children.Count; i++)
            {
                if(children[i].value == searchVal)
                {
                    Debug.Log(children[i].moveX + ":" + children[i].moveY);
                    GameBoardController.AIMoveX = children[i].moveX;
                    GameBoardController.AIMoveY = children[i].moveY;
                    break;
                }
            }
            return;
        }
        
    }

    public int DoMinMax(Node n, int depth, bool maximaizingPlayer)
    {
        /*
        function minimax(node, depth, maximizingPlayer) is
        if depth = 0 or node is a terminal node then
            return the heuristic value of node
        if maximizingPlayer then
            value := −∞
            for each child of node do
                value:= max(value, minimax(child, depth − 1, FALSE))
            return value
        else (*minimizing player *)
            value:= +∞
            for each child of node do
                value:= min(value, minimax(child, depth − 1, TRUE))
            return value
        */

        if(depth == 0 || n.children.Count < 0)
        {
            //Debug.Log(n.moveX + "," + n.moveY + ": " + n.value);
            return n.value;
        }
        if(maximaizingPlayer)
        {
            int myValue = int.MinValue;
            for(int i = 0; i < n.children.Count; i++)
            {
                myValue = Mathf.Max(myValue, DoMinMax(n.children[i], depth - 1, false));
            }
            return myValue;
        } else
        {
            int myValue = int.MaxValue;
            for (int i = 0; i < n.children.Count; i++)
            {
                myValue = Mathf.Min(myValue, DoMinMax(n.children[i], depth - 1, true));
            }
            return myValue;
        }
    }

    public int Getvalue(int x, int y, bool isAITurn)
    {
        if(isAITurn)
        {
            //Edges
            if (x >= 2 && x <= 5 && y == 0 ||
                x >= 2 && x <= 5 && y == 7 ||
                y >= 2 && y <= 5 && x == 0 ||
                y >= 2 && y <= 5 && x == 7)
            {
                return 5;
            }

            //Corners
            if(x == 0 && y == 0 ||
               x == 7 && y == 0 ||
               x == 0 && y == 7 ||
               x == 7 && y == 7)
            {
                return 5;
            }

            //One out from corner
            if(x >= 0 && x <= 1 && y >= 0 && y <= 1 ||
               x >= 0 && x <= 1 && y >= 6 && y <= 7 ||
               x >= 6 && x <= 7 && y >= 0 && y <= 1 ||
               x >= 6 && x <= 7 && y >= 6 && y <= 7)
            {
                return 1;
            }
            
            //One out from edge
            if(x == 1 && y >= 2 && y <= 5 ||
               x == 6 && y >= 2 && y <= 5 ||
               x >= 2 && x <= 5 && y == 1 ||
               x >= 2 && x <= 5 && y == 6)
            {
                return 1;
            }

            //Middle tiles
            return 3;

        } else
        {
            //Edges
            if (x >= 2 && x <= 5 && y == 0 ||
                x >= 2 && x <= 5 && y == 7 ||
                y >= 2 && y <= 5 && x == 0 ||
                y >= 2 && y <= 5 && x == 7)
            {
                return 1;
            }

            //Corners
            if (x == 0 && y == 0 ||
               x == 7 && y == 0 ||
               x == 0 && y == 7 ||
               x == 7 && y == 7)
            {
                return 1;
            }

            //One out from corner
            if (x >= 0 && x <= 1 && y >= 0 && y <= 1 ||
               x >= 0 && x <= 1 && y >= 6 && y <= 7 ||
               x >= 6 && x <= 7 && y >= 0 && y <= 1 ||
               x >= 6 && x <= 7 && y >= 6 && y <= 7)
            {
                return 5;
            }

            //One out from edge
            if (x == 1 && y >= 2 && y <= 5 ||
               x == 6 && y >= 2 && y <= 5 ||
               x >= 2 && x <= 5 && y == 1 ||
               x >= 2 && x <= 5 && y == 6)
            {
                return 5;
            }

            //Middle tiles
            return 3;
        }
    }

    public void CreateChildren(int color, bool isAITurn)
    {
        for (int row = 0; row <= 7; row++)
        {
            for (int col = 0; col <= 7; col++)
            {
                //Debug.Log(row + ":" + col + " = " + myBoardData[row, col]);
                if (color == MinMax.PlayerColor)
                {
                    if (myBoardData[row, col] == 2)
                    {
                        Node temp = new Node();
                        int[,] tempBD = new int[8, 8];
                        tempBD = myBoardData;
                        tempBD[row, col] = MinMax.PlayerColor;
                        //tempBD = ClearBoardData(tempBD);
                        temp.myBoardData = tempBD;
                        temp.parent = this;
                        temp.value = temp.Getvalue(row, col, isAITurn);
                        temp.moveX = row;
                        temp.moveY = col;
                        children.Add(temp);
                        //Debug.Log("ADDED CHILD PLAYER");
                    }
                } else
                {
                    if (myBoardData[row, col] == 3)
                    {
                        //Debug.Log(row + ":" + col);
                        Node temp = new Node();
                        int[,] tempBD = new int[8,8];
                        tempBD = myBoardData;
                        tempBD[row, col] = MinMax.AIColor;
                        //tempBD = ClearBoardData(tempBD);
                        temp.myBoardData = tempBD;
                        temp.parent = this;
                        temp.value = temp.Getvalue(row, col, isAITurn);
                        temp.moveX = row;
                        temp.moveY = col;
                        children.Add(temp);
                        //Debug.Log("ADDED CHILD AI");
                    }
                }
                
            }
        }
    }

    public void ClearBoardData()
    {
        for (int row = 0; row <= 7; row++)
        {
            for (int col = 0; col <= 7; col++)
            {
                if(myBoardData[row,col] == 2 || myBoardData[row, col] == 3)
                {
                    myBoardData[row, col] = 0;
                }
            }
        }
    }

}
