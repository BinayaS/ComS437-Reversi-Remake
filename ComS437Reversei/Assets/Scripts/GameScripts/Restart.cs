using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private void OnMouseDown() {
        for (int i = 0; i <= 7; i++) {
            for (int j = 0; j <= 7; j++) {
                GameBoardController.boardData[i, j] = 0;
                GameBoardController.pieceRefrences[i, j] = null;
            }
        }
        GameBoardController.boardData[7, 7] = 0;
        GameBoardController.isPlayerTurn = true;
        GameBoardController.gamePieces = 4;
        GameBoardController.gameOver = false;
        GameBoardController.blackPieces = 2;
        GameBoardController.whitePieces = 2;
        GameBoardController.BoardDataUpdated = true;
        GameBoardController.AIMoveX = -1;
        GameBoardController.AIMoveY = -1;
        GameBoardController.showVM = false;
        GameBoardController.pieceArray.Clear();
        DragObject.canPlace = false;
        DragObject.isPickedUp = false;
        MinMax.AITurn = false;
        SceneManager.LoadScene("Menu");
    }
}
