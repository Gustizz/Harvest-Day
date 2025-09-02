using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialougeBox : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    public CameraMovement cameraMovement;

    public bool clickable;

    public SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
       // StartDialouge();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && clickable)
        {
            if (textComponent.text == lines[index - 1])
            {
                //NextLine();
                // gameObject.SetActive(false);
                StartCoroutine(TextFadeAway());
                clickable = false;

            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index - 1];
            }
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;

            

            yield return new WaitForSeconds(textSpeed);

            if((c % 3) == 0)
            {
                soundManager.PlaySound(soundManager.KeyboardType, 0.01f, 2);
            }

           

        }


    }

    public void PlayText()
    {
        gameObject.SetActive(true);
        textComponent.color = new Color(1, 1, 1, 1);

        if (index < lines.Length - 1)
        {
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
            index++;
        }
        else
        {
            gameObject.SetActive(false);
        }


    }

    IEnumerator TextFadeAway()
    {
        for (float i = 0; i < 20; i++)
        {
            textComponent.color = new Color(1, 1, 1, 1 - (i / 20));
            yield return new WaitForSeconds(0.05f);
        }

        textComponent.color = new Color(1, 1, 1, 0);

        cameraMovement.animator.speed = 1;
    }
}
