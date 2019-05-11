using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class MenuButton : MonoBehaviour {
    [SerializeField] MenuButtonController menuButtonController;
    [SerializeField] Animator anim;
    [SerializeField] AnimatorFunctions animatorFunctions;
    [SerializeField] int thisIndex;

    IEnumerator Wait() {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("MainLevel");
        MinMax.AIDifficulty = thisIndex + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(menuButtonController.index == thisIndex) {
            anim.SetBool("selected", true);
            if(Input.GetAxis("Submit") == 1) {
                anim.SetBool("pressed", true);
                StartCoroutine(Wait());
            } else if(anim.GetBool("pressed")) {
                anim.SetBool("pressed", false);
                animatorFunctions.disableOnce = false;
            }
        } else {
            anim.SetBool("selected", false);
        }
    }


}
