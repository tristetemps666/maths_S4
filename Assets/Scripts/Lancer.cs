using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lancer : MonoBehaviour
{
    public TextMeshPro button_text;

    public SpriteRenderer bgrd_button;

    public bool is_launched = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver(){
        if(Input.GetMouseButtonDown(0)){
            button_text.color = Color.black;
            bgrd_button.color = Color.white;
        }
        if(Input.GetMouseButtonUp(0)){
            button_text.color = Color.white;
            bgrd_button.color = Color.black;
            is_launched = true;
        }
    }
}
