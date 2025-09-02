using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnlargeButtonOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Vector3 cachedScale;

    public LeanTweenType enlargeEaseType, minimiseEaseType;

    // Start is called before the first frame update

    void Start()
    {
        cachedScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Enter");

        int randInt = Random.Range(-2, 2);

        //transform.localScale = new Vector3(4, 4, 4);
        LeanTween.scale(gameObject, new Vector4(1.1f, 1.1f, 1.1f), 0.5f).setEase(enlargeEaseType);
        LeanTween.rotate(gameObject, new Vector3(0, 0, randInt), 0.5f);
        //Debug.Log("EnTEING");
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        Debug.Log("Exit");
        LeanTween.scale(gameObject, cachedScale, 0.5f).setEase(minimiseEaseType);
        LeanTween.rotate(gameObject, new Vector3(0, 0, 0), 0.5f);
        //transform.localScale = cachedScale;

    }
}
