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

    private float win_multiplier = 10f;


    public bool has_win = false;
    // public bool has_fallen = false; = is_over

    public bool is_over = false;

    bool invoked = false;

    public bool can_get_coin = false;

    public TextMeshPro win_factor_text;
    public TextMeshPro dice_test_text;

    private DiscreteUniform dice_proba = new DiscreteUniform();
    public float time_to_roll = 2f;



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


        if(time_to_roll == 0f && !has_win){
            float result = dice_proba.rand();
            dice_test_text.text = result.ToString();
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
        if(!is_running && !has_win) win_multiplier*=2f;

    }

    public void strat_two(){
        if(!is_running && !has_win)
            can_get_coin = true;
    }


    private void over(){
        is_over = true;
    }

}
