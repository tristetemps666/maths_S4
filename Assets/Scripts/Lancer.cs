using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lancer : MonoBehaviour
{
    public TextMeshPro button_text;

    public SpriteRenderer bgrd_button;

    public bool is_launched = false;

    public bool toggle_mode = false;
    
    public bool is_activated = false;


    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(toggle_mode){
            if(!is_activated){
                button_text.color = Color.white;
                bgrd_button.color = Color.black;
            }else{
                button_text.color = Color.black;
                bgrd_button.color = Color.white;
            }
        }
    }

    private void LateUpdate() {
        if(is_launched == true) is_launched = false;
    }


    void OnMouseOver(){
        if(Input.GetMouseButton(0)){
            button_text.color = Color.black;
            bgrd_button.color = Color.white;
        }
        if(Input.GetMouseButtonDown(0)){
            button_text.color = Color.white;
            bgrd_button.color = Color.black;
        }

        if(Input.GetMouseButtonUp(0)){
            if (toggle_mode){
                is_activated = true;
                is_launched = true;
            }
            button_text.color = Color.white;
            bgrd_button.color = Color.black;
            is_launched = true;
        }
    }


    public void set_button_text(string text){
        button_text.text = text;
        button_text.fontSize = (text.Length >=10) ? 5 : 7;
    }
}
