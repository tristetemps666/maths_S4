using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using DISCRETE_UNIFORM;

public class Dice : MonoBehaviour
{

    public List<Sprite> sprites_dice;
    public SpriteRenderer dice_sprite_displayed;
    public bool is_running = false;

    private GameObject Start_button;

    public Lancer launch_button;

    public int win_multiplier = 10;


    public bool has_win = false;
    // public bool has_fallen = false; = is_over

    public bool is_over = false;

    bool invoked = false;
    private bool strat_2_choose = false;

    public bool can_choose_next_game = false;

    public TextMeshPro win_factor_text;
    public TextMeshPro dice_test_text;

    public DiscreteUniform dice_proba = new DiscreteUniform();
    public float time_to_roll = 2f;
    public float speed_to_roll = 3f;
    private int amount_roll = 0;

    public bool has_just_win = false;
    public int res = 0;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        win_factor_text.text = "gain : x " + win_multiplier.ToString();

        is_running = launch_button.is_launched && !has_win ? true : is_running;
        if (is_running) time_to_roll = Mathf.Max(0f,time_to_roll-Time.deltaTime);
        
        if(is_running) {
            // dice_test_text.text =  "Roll";
            amount_roll = (int)Mathf.Floor((Time.time*speed_to_roll)%6);
            dice_sprite_displayed.sprite = sprites_dice[amount_roll];
        }


        has_just_win = time_to_roll == 0f && !has_win;

        if(has_just_win){
            res = dice_proba.rand();
            if(strat_2_choose && res%2 ==0) can_choose_next_game = true;
            dice_test_text.text = (res*win_multiplier).ToString()+"$";
            dice_sprite_displayed.sprite = sprites_dice[res-1];
            amount_roll = 0;

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
            strat_2_choose = true;
    }


    private void over(){
        is_over = true;
    }

    public void reset(){
        has_just_win = false;
        has_win = false;
        is_over = false;
        is_running = false;
        can_choose_next_game = false;
        strat_2_choose = false;
        res = 0;
        win_multiplier = 10;
        invoked = false;
        time_to_roll = 2f;

        dice_test_text.text = "Le De";
    }

}
