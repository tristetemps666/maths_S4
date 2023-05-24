using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fakir : MonoBehaviour
{
    public bool is_running = false;
    private Vector3 diff = Vector3.zero;
    private GameObject Ball;

    private Vector3 ball_start_position;

    private GameObject Start_button;

    public Lancer launch_button;

    public List<TextMeshPro> list_rewards_text;

    public float strat_one_multiplier = 1f;


    public int number_of_ball = 1;


    public bool has_win = false;
    public bool has_fallen = false;

    public bool is_over = false;

    bool invoked = false;
    bool invoked2 = false;
    public bool has_just_win = false;



    // Start is called before the first frame update
    void Start()
    {
        Ball = GetComponentInChildren<Ball>().gameObject;
        Ball.GetComponent<Rigidbody2D>().isKinematic = false;
        ball_start_position = Ball.transform.localPosition;

        list_rewards_text.AddRange(GetComponentsInChildren<TextMeshPro>());
    }

    // Update is called once per frame
    void Update()
    {

        
        is_over = has_fallen && number_of_ball==0;
        
        diff = Ball.GetComponent<Ball>().get_diff();
        has_win = Ball.GetComponent<Ball>().has_win;

        if (has_win) has_fallen = true;
        
        has_just_win = false;

        if(!invoked2 && has_fallen && !is_over){
            invoked2 = true;
            has_just_win = true;
        }
        
        if(!invoked  && has_fallen && number_of_ball >0){
            Debug.Log("encore");
            Invoke("setup_one_more_lanch",2f);
            invoked = true;
        }

        is_running = launch_button.is_launched && number_of_ball>0? true : is_running;
        if (launch_button.is_launched) number_of_ball--;

        if (is_over) is_running =false;


        
        Ball.GetComponent<Rigidbody2D>().bodyType = is_running ? UnityEngine.RigidbodyType2D.Dynamic : UnityEngine.RigidbodyType2D.Static;
    }




    public void strat_one(){
        if(!is_running && !has_win)
            strat_one_multiplier*=2f;
            list_rewards_text.ForEach(txt => txt.text = (int.Parse(txt.text)*strat_one_multiplier).ToString());
    }

    public void strat_two(){
        if(!is_running && !has_win){
            number_of_ball ++;
        }

    }


    void setup_one_more_lanch(){
            Debug.Log("encore !!!");
            is_running = false;
            Ball.transform.localPosition = ball_start_position;
            has_fallen = false;
            Ball.GetComponent<Ball>().has_win = false;
            Ball.GetComponent<Ball>().win_value = 0;
            invoked2 = false;
    }


    public void reset(){
        is_over = false;
        has_fallen = false;
        has_win = false;
        invoked = false;
        invoked2 = false;
        number_of_ball = 1;
        is_running = false;

        list_rewards_text.ForEach(txt => txt.text = (int.Parse(txt.text)/strat_one_multiplier).ToString());
        strat_one_multiplier /= strat_one_multiplier;
        
        Ball.transform.localPosition = ball_start_position;
    }


}
