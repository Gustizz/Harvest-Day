using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Card : MonoBehaviour
{
    
    public int id;
    public Text description;
    public Image preview;

    public CardManager cardManager;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCard(Text _description, Image _preview, int _id)
    {
        description.text = _description.text;
        preview.sprite = _preview.sprite;
        id = _id;
    }

    public void WhenClicked()
    {
        cardManager.ApplyUpgrade(id);
    }

}
