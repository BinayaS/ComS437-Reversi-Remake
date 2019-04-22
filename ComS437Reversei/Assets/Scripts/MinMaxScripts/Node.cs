using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node parent;
    public List<Node> children;
    public int value;
    public int[,] myBoardData;
    public int depth;

    public void Simulate(bool simAI, int depth)
    {

        if(simAI)
        {
            myBoardData = MinMax.FindValidMoves(MinMax.PlayerColor, myBoardData);
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
            for (int i = 0; i < children.Capacity; i++)
            {
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
                if(color == MinMax.PlayerColor)
                {
                    if (myBoardData[row, col] == 2)
                    {
                        Node temp = new Node();
                        int[,] tempBD = myBoardData;
                        tempBD[row, col] = MinMax.PlayerColor;
                        tempBD = ClearBoardData(tempBD);
                        temp.myBoardData = tempBD;

                        children.Add(temp);
                    }
                } else
                {
                    if (myBoardData[row, col] == 3)
                    {
                        Node temp = new Node();
                        int[,] tempBD = myBoardData;
                        tempBD[row, col] = MinMax.AIColor;
                        tempBD = ClearBoardData(tempBD);
                        temp.myBoardData = tempBD;

                        children.Add(temp);
                    }
                }
                
            }
        }
    }

    public int[,] ClearBoardData(int[,] boardData)
    {
        int[,] tempBD = boardData;
        for (int row = 0; row <= 7; row++)
        {
            for (int col = 0; col <= 7; col++)
            {
                if(tempBD[row,col] == 2 || tempBD[row, col] == 3)
                {
                    tempBD[row, col] = 0;
                }
            }
        }
        return tempBD;
    }

}
