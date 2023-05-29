using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyRand;

public enum carroussel_state{ready_to_play,start_to_roll, is_rolling, is_paused, finished_to_roll};

public class Carrousel : MonoBehaviour
{
    // Start is called before the first frame update
    public carroussel_state state = carroussel_state.ready_to_play;

    public Transform active_game_transform;
    public Transform left_out_transform;
    public Transform right_out_transform;

    private List<GameObject> list_games;

    public float speed_to_roll = 1f;


    public int active_game;
    public int next_game;

    public Material conveyor_belt_material;
    public float conveyor_belt_speed = 2.23f;
    public float conveyor_belt_offset = 0f;





    // private float[][] transition_matrix = new float[][]
    // {
    //    new float[] {0f,0.3f,0.7f},
    //    new float[] {0.5f,0f,0.5f},
    //    new float[] {0.2f,0.8f,0f}
    // };



    // 0: DICE / 1 : Fakir / 2 : Piece / 3: Bar
    
    private float[][] transition_matrix = new float[][] // test
    {
        new float[] {0f,1/3f,1/3f,1/3f},       // Dé
        new float[] {0.5f,0f,2/6f,1/6f},       // Fakir
        new float[] {0.5f,2/6f,0f,1/6f},       // Piece
        new float[] {2/3f,1/6f,1/6f,0f}        // Bar
    };




   // 0: DICE / 1 : Fakir / 2 : Piece / 3: Bar
    void Start()
    {
        list_games = GetComponentInParent<GameManager>().list_games; 
        active_game = GetComponentInParent<GameManager>().active_game;

        setup_games_positions();
    }

    // Update is called once per frame
    void Update()
    {
        active_game = GetComponentInParent<GameManager>().active_game;
        // next_game = Mathf.Min(active_game+1,2);




        if(state == carroussel_state.start_to_roll){
            choose_next_game();
            state = carroussel_state.is_rolling;
        }

        GameObject go_next_game = list_games[next_game];
        GameObject go_active_game = list_games[active_game];

        if(state == carroussel_state.is_rolling){


            if(Vector3.Distance(go_next_game.transform.position,active_game_transform.position) >= 0.1f){ // am I close ?
                go_active_game.transform.position = move_toward_a_position(go_active_game.transform,left_out_transform);
                go_next_game.transform.position = move_toward_a_position(go_next_game.transform,active_game_transform);

                conveyor_belt_offset+=conveyor_belt_speed*Time.deltaTime;
                conveyor_belt_material.SetFloat("_offset",conveyor_belt_offset);
            }
            else{ // the roll is over
                state = carroussel_state.finished_to_roll;
                go_active_game.transform.position = right_out_transform.position;
                conveyor_belt_material.SetFloat("_is_moving",0f);   


            }
        }   

        
    }



    Vector3 move_toward_a_position(Transform pos_to_mov, Transform target){
        Vector3 direction = Vector3.Normalize(target.position-pos_to_mov.position);
        return pos_to_mov.position+ direction*Time.deltaTime*speed_to_roll;
    }


    void setup_games_positions(){
        for(int i = 0; i< list_games.Count; i++){
            if(i == active_game){
                list_games[i].transform.position = active_game_transform.position;
            }else{
                list_games[i].transform.position = right_out_transform.position;
            }
        }
    }

    void choose_next_game(){
        float val = myRand.rand_0_1();
        float sum_proba = 0f;

        for(int i=0; i<list_games.Count; i++){
            sum_proba+=transition_matrix[active_game][i];
            if(sum_proba >val){
                next_game = i;
                return;
            }
        }
        Debug.Log("ça a cassé / n'a pas trouvé le suivant");
    }


}
