using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnlargeOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Vector3 cachedScale;

    public LeanTweenType enlargeEaseType, minimiseEaseType;

    public SoundManager soundManager;

    void Start()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        cachedScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {


        //transform.localScale = new Vector3(4, 4, 4);
        LeanTween.scale(gameObject, new Vector4(3.5f, 3.5f, 3.5f), 0.5f).setEase(enlargeEaseType);
        soundManager.PlaySound(soundManager.cardsEntering, 0.01f, 1f);
        //Debug.Log("EnTEING");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        Debug.Log("Exit");
        LeanTween.scale(gameObject, cachedScale, 0.5f).setEase(minimiseEaseType);
        soundManager.PlaySound(soundManager.cardsEntering, 0.01f, 1f);
        //transform.localScale = cachedScale;

    }
}
