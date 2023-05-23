using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using DISCRETE_UNIFORM;

public class Dice : MonoBehaviour
{
    public bool is_running = false;

    private GameObject Start_button;

    public Lancer launch_button;

    public int win_multiplier = 10;


    public bool has_win = false;
    // public bool has_fallen = false; = is_over

    public bool is_over = false;

    bool invoked = false;

    public bool can_get_coin = false;

    public TextMeshPro win_factor_text;
    public TextMeshPro dice_test_text;

    public DiscreteUniform dice_proba = new DiscreteUniform();
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

        win_factor_text.text = "win : x " + win_multiplier.ToString();

        is_running = launch_button.is_launched && !has_win ? true : is_running;
        if (is_running) time_to_roll = Mathf.Max(0f,time_to_roll-Time.deltaTime);
        
        if(is_running) dice_test_text.text =  "Roll";


        has_just_win = time_to_roll == 0f && !has_win;

        if(has_just_win){
            res = dice_proba.rand();
            dice_test_text.text = res.ToString();
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
        if(!is_running && !has_win) win_multiplier*=2;

    }

    public void strat_two(){
        if(!is_running && !has_win)
            can_get_coin = true;
    }


    private void over(){
        is_over = true;
    }

    public void reset(){
        has_just_win = false;
        has_win = false;
        is_over = false;
        is_running = false;
        can_get_coin = false;
        res = 0;
        win_multiplier = 10;
        invoked = false;
        time_to_roll = 2f;

        dice_test_text.text = "Dice";
    }

}
