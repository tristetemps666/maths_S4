using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    public int win_value = 0;
    public bool has_win= false;

    public bool mouse_over_ball = false;

    private Vector2 diff = Vector2.zero;

    private bool is_holding_ball = false;

    public float delta_ball_start = 5.4f;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0)) is_holding_ball = false;

        if(is_holding_ball){

            transform.position = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - diff)*Vector2.right + new Vector2(0f, transform.position.y);
            transform.localPosition =  new Vector2(Mathf.Clamp(transform.localPosition.x,-delta_ball_start,delta_ball_start),transform.localPosition.y);
        }
    }

    

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "ScoreFakir" && !has_win){ // && Vector3.Magnitude(GetComponent<Rigidbody2D>().velocity) <= 0.1f
            win_value = int.Parse(other.gameObject.GetComponent<TextMeshPro>().text);
            has_win = true;
        }
    }

    private void OnMouseDown() {
        diff = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }
    private void OnMouseOver() {
        if (Input.GetMouseButton(0)){
            is_holding_ball = true;
        }
    }



    public Vector3 get_diff(){
        return diff;
    }


}
