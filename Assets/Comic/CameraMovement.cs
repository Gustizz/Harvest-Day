using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Vector2 scene1Pos, textPos ,scene2Pos;


    public Vector2[] differentPositions;

    public Animator animator;

    public DialougeBox dialougeBox;

    public MenuScript menuScript;



    public void PauseAnimation()
    {
        //animator.StopPlayback();
        animator.speed = 0.001f;
        dialougeBox.PlayText();
        dialougeBox.clickable = true;
    }

    public void loadNextLevel()
    {
        menuScript.LoadNextLevel();
    }


}
