using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceValueController : MonoBehaviour
{
    public static Animator anim;
    public static bool isPickedUp = false;
    public static bool isBlack;
    public bool setWhite = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (setWhite) {
            isBlack = false;
        } else {
            isBlack = true;
        }
        anim.SetBool("isBlack", isBlack);
    }

    void FlipToWhite() {
        isBlack = false;
        anim.SetBool("isBlack", isBlack);
    }

    void FlipToBlack() {
        isBlack = true;
        anim.SetBool("isBlack", isBlack);
    }
}
