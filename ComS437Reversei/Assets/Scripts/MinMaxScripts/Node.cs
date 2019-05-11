using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Node parent;
    public List<Node> children = new List<Node>();
    public int value = 0;
    public int[,] myBoardData = new int[8,8];
    public bool isAI;
    public bool isMax;
    public int depth;
    public int moveX = -1;
    public int moveY = -1;

    public void Simulate(bool isAI, int depth, bool isMax, int number, Node startNode)
    {
        this.isAI = isAI;
        this.isMax = isMax;
        this.depth = depth;

        ClearBoardData();

        if (isAI)
        {
            myBoardData = MinMax.FindValidMoves(MinMax.AIColor, myBoardData);
            CreateChildren(MinMax.AIColor, isAI);
        } else {
            myBoardData = MinMax.FindValidMoves(MinMax.PlayerColor, myBoardData);
            CreateChildren(MinMax.PlayerColor, isAI);
        }

        //Debug.Log("CreateChildren: " + depth + " Count: " + children.Count + " Num: " + number + " Depth: " + depth);
        //Debug.Log("MoveX: " + moveX + " MoveY: " + moveY);
        number++;

        if(depth > 0)
        {
            for (int i = 0; i < children.Count; i++)
            {
                //Debug.Log("I: " + i);
                children[i].Simulate(!isAI, depth - 1, !isMax, number, startNode);
            }
        }

        //Debug.Log("Ended Creation");

        if(this.depth >= 1)
        {
            //Do min max
            //Debug.Log("DOING MIN MAX");
            Debug.Log("Do Min Max");
            int searchVal = ADoMinMax(this, MinMax.AIDifficulty, true);
            if (searchVal == int.MaxValue || searchVal == int.MinValue) {
                Debug.Log("No Valid Moves Found");
                MinMax.AITurn = true;
                GameBoardController.gameOver = true;
            }
            Debug.Log("SearchVal" + searchVal);
            bool foundMove = false;
            int searchValStart = searchVal;
            while(foundMove == false) {
                for (int i = 0; i < children.Count; i++) {
                    Debug.Log("X: " + children[i].moveX + " Y: " + children[i].moveY + " = " + children[i].value);
                    if (children[i].value == searchVal) {
                        //Debug.Log("AI played: " + children[i].moveX + "," + children[i].moveY);
                        GameBoardController.AIMoveX = children[i].moveX;
                        GameBoardController.AIMoveY = children[i].moveY;
                        Debug.Log("X: " + children[i].moveX + " Y: " + children[i].moveY);
                        foundMove = true;
                        break;
                    }
                }
                if(searchVal > 0) {
                    searchVal--;
                } else {
                    searchVal = 5;
                }
                if(searchVal == searchValStart) {
                    GameBoardController.gameOver = true;
                    break;
                }
            }
            
            return;
        } else {
            return;
        }
        
    }

    /*
    public int DoMinMax(Node n, int depth, bool maximaizingPlayer)
    {

        if (depth == 0 || n.children.Count < 0)
        {
            //Debug.Log(n.moveX + "," + n.moveY + ": " + n.value);
            return n.value;
        }
        if(maximaizingPlayer)
        {
            int myValue = int.MinValue;
            for(int i = 0; i < n.children.Count; i++)
            {
                int minMaxVal = DoMinMax(n.children[i], depth - 1, false);
                myValue = Mathf.Max(myValue, minMaxVal);
                //Debug.Log("MAX - X: " + n.children[i].moveX + " Y: " + n.children[i].moveY + " = " + n.children[i].value);
                /*
                if (depth == MinMax.AIDifficulty && myValue == minMaxVal) {
                    Debug.Log("AI played: " + n.children[i].moveX + "," + n.children[i].moveY);
                    GameBoardController.AIMoveX = n.children[i].moveX;
                    GameBoardController.AIMoveY = n.children[i].moveY;
                }
                */
                /*
                }
                return myValue;
                } else
                {
                int myValue = int.MaxValue;
                for (int i = 0; i < n.children.Count; i++)
                {
                    int minMaxVal = DoMinMax(n.children[i], depth - 1, true);
                    myValue = Mathf.Min(myValue, minMaxVal);
                    //Debug.Log("MIN - X: " + n.children[i].moveX + " Y: " + n.children[i].moveY + " = " + n.children[i].value);
                    /*
                    if (depth == MinMax.AIDifficulty && myValue == minMaxVal) {
                        Debug.Log("AI played: " + n.children[i].moveX + "," + n.children[i].moveY);
                        GameBoardController.AIMoveX = n.children[i].moveX;
                        GameBoardController.AIMoveY = n.children[i].moveY;
                    }
                    */
                    /*
                }
            return myValue;
        }
    }
    */

    public int ADoMinMax(Node n, int depth, bool maximizingPlayer) {
        if (depth == 0 || n.children.Count < 0) {
            return n.value;
        }

        if (maximizingPlayer) {
            value = int.MinValue;
            for (int i = 0; i < n.children.Count; i++) {
                value = Mathf.Max(value, ADoMinMax(n.children[i], depth - 1, false));
            }
            return value;
        } else {
            value = int.MaxValue;
            for (int i = 0; i < n.children.Count; i++) {
                value = Mathf.Min(value, ADoMinMax(n.children[i], depth - 1, true));
            }
            return value;
        }
    }

    public int Getvalue(int x, int y, bool isAITurn, int difficulty)
    {
        if(isAITurn)
        {
            //Edges
            if (x >= 2 && x <= 5 && y == 0 ||
                x >= 2 && x <= 5 && y == 7 ||
                y >= 2 && y <= 5 && x == 0 ||
                y >= 2 && y <= 5 && x == 7)
            {
                switch(difficulty) {
                    case 1:
                        return 2;
                    case 2:
                        return 4;
                    case 3:
                        return 6;
                    case 4:
                        return 8;
                    case 5:
                        return 10;
                }
                
            }

            //Corners
            if(x == 0 && y == 0 ||
               x == 7 && y == 0 ||
               x == 0 && y == 7 ||
               x == 7 && y == 7)
            {
                switch (difficulty) {
                    case 1:
                        return 2;
                    case 2:
                        return 4;
                    case 3:
                        return 6;
                    case 4:
                        return 8;
                    case 5:
                        return 10;
                }
            }

            //One out from corner
            if(x >= 0 && x <= 1 && y >= 0 && y <= 1 ||
               x >= 0 && x <= 1 && y >= 6 && y <= 7 ||
               x >= 6 && x <= 7 && y >= 0 && y <= 1 ||
               x >= 6 && x <= 7 && y >= 6 && y <= 7)
            {
                switch (difficulty) {
                    case 1:
                        return 5;
                    case 2:
                        return 4;
                    case 3:
                        return 3;
                    case 4:
                        return 2;
                    case 5:
                        return 1;
                }
            }
            
            //One out from edge
            if(x == 1 && y >= 2 && y <= 5 ||
               x == 6 && y >= 2 && y <= 5 ||
               x >= 2 && x <= 5 && y == 1 ||
               x >= 2 && x <= 5 && y == 6)
            {
                switch (difficulty) {
                    case 1:
                        return 5;
                    case 2:
                        return 4;
                    case 3:
                        return 3;
                    case 4:
                        return 2;
                    case 5:
                        return 1;
                }
            }

            //Middle tiles
            switch (difficulty) {
                case 1:
                    return 3;
                case 2:
                    return 4;
                case 3:
                    return 5;
                case 4:
                    return 6;
                case 5:
                    return 7;
                default:
                    return 3;
            }

        } else
        {
            //Edges
            if (x >= 2 && x <= 5 && y == 0 ||
                x >= 2 && x <= 5 && y == 7 ||
                y >= 2 && y <= 5 && x == 0 ||
                y >= 2 && y <= 5 && x == 7)
            {
                switch (difficulty) {
                    case 1:
                        return 5;
                    case 2:
                        return 4;
                    case 3:
                        return 3;
                    case 4:
                        return 2;
                    case 5:
                        return 1;
                }
            }

            //Corners
            if (x == 0 && y == 0 ||
               x == 7 && y == 0 ||
               x == 0 && y == 7 ||
               x == 7 && y == 7)
            {
                switch (difficulty) {
                    case 1:
                        return 5;
                    case 2:
                        return 4;
                    case 3:
                        return 3;
                    case 4:
                        return 2;
                    case 5:
                        return 1;
                }
            }

            //One out from corner
            if (x >= 0 && x <= 1 && y >= 0 && y <= 1 ||
               x >= 0 && x <= 1 && y >= 6 && y <= 7 ||
               x >= 6 && x <= 7 && y >= 0 && y <= 1 ||
               x >= 6 && x <= 7 && y >= 6 && y <= 7)
            {
                switch (difficulty) {
                    case 1:
                        return 2;
                    case 2:
                        return 4;
                    case 3:
                        return 6;
                    case 4:
                        return 8;
                    case 5:
                        return 10;
                }
            }

            //One out from edge
            if (x == 1 && y >= 2 && y <= 5 ||
               x == 6 && y >= 2 && y <= 5 ||
               x >= 2 && x <= 5 && y == 1 ||
               x >= 2 && x <= 5 && y == 6)
            {
                switch (difficulty) {
                    case 1:
                        return 2;
                    case 2:
                        return 4;
                    case 3:
                        return 6;
                    case 4:
                        return 8;
                    case 5:
                        return 10;
                }
            }

            //Middle tiles
            switch (difficulty) {
                case 1:
                    return 3;
                case 2:
                    return 4;
                case 3:
                    return 5;
                case 4:
                    return 6;
                case 5:
                    return 7;
                default:
                    return 3;
            }
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
                        System.Array.Copy(myBoardData, tempBD, myBoardData.GetLength(0) * myBoardData.GetLength(1));
                        tempBD[row, col] = MinMax.PlayerColor;
                        //tempBD = ClearBoardData(tempBD);
                        temp.myBoardData = tempBD;
                        temp.parent = this;
                        temp.value = temp.Getvalue(row, col, isAITurn, MinMax.AIDifficulty);
                        temp.moveX = row;
                        temp.moveY = col;
                        children.Add(temp);
                        //Debug.Log("ADDED CHILD PLAYER");
                    }
                } else {
                    if (myBoardData[row, col] == 3)
                    {
                        //Debug.Log(row + ":" + col);
                        Node temp = new Node();
                        int[,] tempBD = new int[8,8];
                        System.Array.Copy(myBoardData, tempBD, myBoardData.GetLength(0) * myBoardData.GetLength(1));
                        tempBD[row, col] = MinMax.AIColor;
                        //tempBD = ClearBoardData(tempBD);
                        temp.myBoardData = tempBD;
                        temp.parent = this;
                        temp.value = temp.Getvalue(row, col, isAITurn, MinMax.AIDifficulty);
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
