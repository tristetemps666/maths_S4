using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using BERNOULLI;

public class Piece : MonoBehaviour
{
    public bool is_running = false;

    private GameObject Start_button;

    public Lancer launch_button;

    public int mise = 10;


    public bool has_win = false;
    // public bool has_fallen = false; = is_over

    public bool is_over = false;

    bool invoked = false;

    public TextMeshPro win_text;
    public TextMeshPro loose_text;
    public TextMeshPro piece_test_text;

    public Bernoulli piece_proba = new Bernoulli();
    public float time_to_roll = 2f;

    public bool has_just_win = false;
    public int res = 0;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        win_text.text = "win : + " + mise*2 + " ("+(piece_proba.win_proba).ToString()+")";
        loose_text.text = "loose : + " + 0 + " ("+(1-piece_proba.win_proba).ToString()+")";

        is_running = launch_button.is_launched && !has_win ? true : is_running;
        if (is_running) time_to_roll = Mathf.Max(0f,time_to_roll-Time.deltaTime);
        
        if(is_running) piece_test_text.text =  "Roll";


        has_just_win = time_to_roll == 0f && !has_win;

        if(has_just_win){
            res = piece_proba.rand()*mise*2;
            piece_test_text.text = (0.5*res/mise).ToString();
            has_win = true;
            is_running = false;

            if(!invoked){
                invoked = true;
                Invoke("over",1f);
            }
            

        }
        
        // has_win = Ball.GetComponent<Ball>().has_win;

        // if (has_win) has_fallen = true;
        
        
        // if(!invoked  && has_fallen && number_of_ball >0){
        //     Invoke("setup_one_more_lanch",2f);
        //     invoked = true;
        // }

        if (is_over) is_running =false;


        
    }




    public void strat_one(){
        if(!is_running && !has_win) piece_proba.win_proba = 0.75f;

    }

    public void strat_two(){
        if(!is_running && !has_win)
            mise+=20;
    }


    private void over(){
        is_over = true;
    }

    public void reset(){
        mise = 10;
        is_running = false;
        has_win = false;
        is_over = false;
        invoked = false;
        piece_proba.win_proba = 0.5f;
        has_just_win = false;
        res = 0;
        time_to_roll = 2f;
        piece_test_text.text = "Piece";
    }

}
