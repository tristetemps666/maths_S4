using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using EXPONENTIELLE;



public enum shop_state {is_paying, next_client};

public class FileAttente : MonoBehaviour
{
    public GameObject one_client;
    public Queue<GameObject> queue_clients = new Queue<GameObject>();

    public List<GameObject> list_clients_leaving = new List<GameObject>();
    public bool is_running = false;

    public Lancer launch_button;

    public int money_per_clients = 10;
    public int max_clients = 10;

    public bool is_over = false;

    bool invoked = false;

    public Exponentielle new_client_proba = new Exponentielle();
    public Exponentielle manage_client_proba = new Exponentielle();

    public float time_of_game = 10f;
    public float current_time_game = 0f;
    public float speed_client_to_move = 0.5f;
    public float distance_between_clients = 0.1f;

    public Transform position_to_pay;
    private Transform last_client_transform;

    public bool has_just_win = false;

    // client queue
    // public float time_last_client_came = 0f;
    public float time_from_last_client = 0f;
    public float wait_time_for_next_client = 0f;

    // payment
    public float time_from_last_payment = 0f;
    public float delay_for_current_payment = 0f;

    public int money_earned = 0;


    public bool has_win = false;

    private float duree_deplacement = 0f;


    

    public shop_state state = shop_state.next_client;


    public Transform timer_line_scale;

    public TextMeshPro text_money_earn;

    public TextMeshPro client_lambda;

    public TextMeshPro payment_lambda;




    // Start is called before the first frame update
    void Start()
    {
        set_lambdas();
        current_time_game = time_of_game;
        wait_time_for_next_client = new_client_proba.rand();
        delay_for_current_payment = manage_client_proba.rand();
    }

    // Update is called once per frame
    void Update()
    {

        is_running = launch_button.is_launched && !is_over ? true : is_running;

        if(is_running) {

            current_time_game = Mathf.Max(current_time_game-Time.deltaTime,0f);

            timer_line_scale.localScale = new Vector3(current_time_game/time_of_game,1f,1f);

            is_over = current_time_game == 0f;

            move_leaving_clients();

            time_from_last_client+= Time.deltaTime;

            // manage new client comming
            if(time_from_last_client >= wait_time_for_next_client && queue_clients.Count<max_clients){
                time_from_last_client = 0f;
                GameObject new_client = Instantiate(one_client,position_to_pay,false);
                new_client.SetActive(true);
                new_client.transform.Translate(queue_clients.Count*Vector3.up*distance_between_clients +Vector3.up*(distance_between_clients-duree_deplacement*speed_client_to_move));
                // set_position_last_client(new_client);

                queue_clients.Enqueue(new_client);
                last_client_transform = new_client.transform;
                wait_time_for_next_client = new_client_proba.rand();
            }



            if(state == shop_state.is_paying){

                // manage client payment
                time_from_last_payment += Time.deltaTime;
                if(time_from_last_payment>= delay_for_current_payment){
                    process_payement();
                    Debug.Log("leaving after : " +list_clients_leaving.Count);
                    Debug.Log("queue after : " + queue_clients.Count);

                    delay_for_current_payment = manage_client_proba.rand();

                    
                    time_from_last_payment = 0f;
                    state = shop_state.next_client;
                }
            }

            if (state == shop_state.next_client && queue_clients.Count > 0){
                Debug.Log("pos du client : "+ queue_clients.Peek().transform.localPosition);
                if(Vector3.Distance(queue_clients.Peek().transform.localPosition,Vector3.zero) >= 0.03f){ // move while not arrive to the pay pose
                    duree_deplacement+=Time.deltaTime;
                    move_waiting_clients();
                }
                else{
                    duree_deplacement = 0f;
                    state = shop_state.is_paying;
                }
            }

        }

        if(has_just_win){
            has_win = true;
            is_running = false;
            has_just_win = false;
        }

        if(current_time_game == 0f && !has_win){
            has_just_win = true;
        }

        

        if (is_over) is_running =false;


        
    }


    private void set_lambdas(){
        client_lambda.text = "Client E[x] : "+ new_client_proba.esperance().ToString();
        payment_lambda.text = "Paie E[x] : " + manage_client_proba.esperance().ToString();
    }

    private void process_payement(){
        Debug.Log("leaving : " +list_clients_leaving.Count);
        Debug.Log("queue : " +queue_clients.Count);

        if(queue_clients.Count ==0) return;        
        GameObject over_client;
        over_client = queue_clients.Dequeue();
        list_clients_leaving.Add(over_client);
        money_earned+=20;
        text_money_earn.text = money_earned.ToString() +" $"; 
    }

    private void move_leaving_clients(){
        foreach(GameObject leaver in list_clients_leaving){
            Vector3 position = leaver.transform.localPosition;
            if(position.x < -5.6f) {
                list_clients_leaving.Remove(leaver);
                Destroy(leaver);
                break;
            }
            // else leaver.transform.localPosition = position + Vector3.left*Time.deltaTime*speed_client_to_move;
            else leaver.transform.Translate(Vector3.left*Time.deltaTime*speed_client_to_move);

        }
    }

    private void set_position_last_client(GameObject new_client){
        new_client.transform.position = position_to_pay.position + queue_clients.Count*Vector3.up*distance_between_clients;
    }

    private void move_waiting_clients(){
        foreach(GameObject client in queue_clients){
            client.transform.position+= Vector3.down*Time.deltaTime*speed_client_to_move;
        }
    }



    public void strat_one(){
        if(!is_running && !is_over) {
            new_client_proba.lambda*=2f;
            set_lambdas();
        }

    }

    public void strat_two(){
        if(!is_running && !is_over)
            {
                manage_client_proba.lambda*=2f;
                set_lambdas();
            }
    }


    private void over(){
        is_over = true;
    }

    public void reset(){
        is_over = false;

        new_client_proba.lambda = 1f;
        manage_client_proba.lambda = 1f;

        
        foreach(var g in queue_clients){
            Destroy(g);
        }
        queue_clients = new Queue<GameObject>();

        foreach(var g in list_clients_leaving){
            Destroy(g);
        }
        list_clients_leaving = new List<GameObject>();

        current_time_game = 10f;

        time_from_last_client = 0f;
        wait_time_for_next_client = new_client_proba.rand();

        time_from_last_payment = 0f;
        delay_for_current_payment = manage_client_proba.rand();

        money_earned = 0;
        text_money_earn.text = money_earned.ToString() +" $"; 

        has_win = false;
        duree_deplacement = 0f;

        last_client_transform = one_client.transform;



    }

}
