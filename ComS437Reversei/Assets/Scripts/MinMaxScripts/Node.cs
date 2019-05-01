using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node parent;
    public List<Node> children = new List<Node>();
    public int value;
    public int[,] myBoardData;
    public int depth;

    public void Simulate(bool simAI, int depth)
    {
        this.depth = depth;
        if(simAI)
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

            value = Getvalue();
            CreateChildren(MinMax.AIColor);

        } else
        {
            myBoardData = MinMax.FindValidMoves(MinMax.PlayerColor, myBoardData);
            value = Getvalue();
            CreateChildren(MinMax.PlayerColor);
        }
        if(depth > 0)
        {
            for (int i = 0; i < children.Count; i++)
            {
                children[i].ClearBoardData();
                children[i].Simulate(!simAI, depth - 1);
            }
        }
        
    }

    public int Getvalue()
    {
        return 0;
    }

    public void CreateChildren(int color)
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
