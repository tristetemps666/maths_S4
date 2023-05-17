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

    private float strat_one_multiplier = 2f;


    public int number_of_ball = 1;


    public bool has_win = false;
    public bool has_fallen = false;

    public bool is_over = false;

    bool invoked = false;




    // Start is called before the first frame update
    void Start()
    {
        Ball = GetComponentInChildren<Ball>().gameObject;
        Ball.GetComponent<Rigidbody2D>().isKinematic = false;
        ball_start_position = Ball.transform.position;

        list_rewards_text.AddRange(GetComponentsInChildren<TextMeshPro>());
    }

    // Update is called once per frame
    void Update()
    {

        
        is_over = has_fallen && number_of_ball==0;  

        is_running = launch_button.is_launched && number_of_ball>0 ? true : is_running;
        if (launch_button.is_launched) number_of_ball--;
        
        diff = Ball.GetComponent<Ball>().get_diff();
        has_win = Ball.GetComponent<Ball>().has_win;

        if (has_win) has_fallen = true;
        
        
        if(!invoked  && has_fallen && number_of_ball >0){
            Invoke("setup_one_more_lanch",2f);
            invoked = true;
        }


        
        Ball.GetComponent<Rigidbody2D>().bodyType = is_running ? UnityEngine.RigidbodyType2D.Dynamic : UnityEngine.RigidbodyType2D.Static;
    }




    public void strat_one(){
        if(!is_running && !has_win)
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
            Ball.transform.position = ball_start_position;
            has_fallen = false;
    }

}
