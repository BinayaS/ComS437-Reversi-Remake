using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScore : MonoBehaviour
{
    public TextMeshPro textmeshPro;

    private void Start() {
        textmeshPro = GetComponent<TextMeshPro>();
    }

    void Update() {
        textmeshPro.SetText("Black:" + GameBoardController.blackPieces + "  White:" + GameBoardController.whitePieces);
        if (GameBoardController.gameOver == true) {
            if (GameBoardController.whitePieces > GameBoardController.blackPieces) {
                textmeshPro.SetText("Black:" + GameBoardController.blackPieces + "  White:" + GameBoardController.whitePieces + " WHITE WINS");
            }
            else if (GameBoardController.whitePieces < GameBoardController.blackPieces) {
                textmeshPro.SetText("Black:" + GameBoardController.blackPieces + "  White:" + GameBoardController.whitePieces + " BLACK WINS");
            }
            else {
                textmeshPro.SetText("Black:" + GameBoardController.blackPieces + "  White:" + GameBoardController.whitePieces + " TIE");
            }
        }
    }
}
