using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlButton : MonoBehaviour
{
    // Start is called before the first frame update

    private bool Controlshidden = true;
    private bool objectiveHidden = true;

    public LeanTweenType easeType, exitEaseType;

    public GameObject ObjectiveBox;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ControlsLeanTween()
    {
        if (Controlshidden)
        {
            LeanTween.moveX(gameObject, 5.75f, 1f).setDelay(0.25f).setEase(easeType);
            Controlshidden = false;
        }
        else
        {
            LeanTween.moveX(gameObject, 12f, 1f).setDelay(0.25f).setEase(exitEaseType);
            Controlshidden = true;
        }
        
    }

    public void ObjectiveLeanTween()
    {
        if (objectiveHidden)
        {
            LeanTween.moveY(ObjectiveBox, -1f, 1f).setDelay(0.25f).setEase(easeType);
            objectiveHidden = false;
        }
        else
        {
            LeanTween.moveY(ObjectiveBox, -10f, 1f).setDelay(0.25f).setEase(exitEaseType);
            objectiveHidden = true;
        }
    }
}
