using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardController : MonoBehaviour
{

    // 1 == white
    //-1 == black
    public int[,] boardData = new int[7,7];

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(boardData.Length/7);
        for (int i = 0; i < boardData.Length/7; i++)
        {
            for(int j = 0; j < boardData.Length/7; j++)
            {
                boardData[i,j] = 0;
            }
        }

        boardData[3, 3] = 1;
        boardData[4, 4] = 1;
        boardData[3, 4] = -1;
        boardData[4, 3] = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
