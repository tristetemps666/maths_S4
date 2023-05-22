using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum carroussel_state{ready_to_play, is_rolling, is_paused, finished_to_roll};

public class Carrousel : MonoBehaviour
{
    // Start is called before the first frame update
    public carroussel_state state = carroussel_state.ready_to_play;

    public Transform active_game_transform;
    public Transform left_out_transform;
    public Transform right_out_transform;

    private List<GameObject> list_games;

    public float speed_to_roll = 1f;


    private int active_game;
    public int next_game;





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
        next_game = Mathf.Min(active_game+1,2);


        GameObject go_active_game = list_games[active_game];
        GameObject go_next_game = list_games[next_game];

        if(state == carroussel_state.is_rolling){
            if(Vector3.Distance(go_next_game.transform.position,active_game_transform.position) >= 0.1f){ // am I close ?
                go_active_game.transform.position = move_toward_a_position(go_active_game.transform,left_out_transform);
                go_next_game.transform.position = move_toward_a_position(go_next_game.transform,active_game_transform);
            }
            else{ // the roll is over
                state = carroussel_state.finished_to_roll;
                go_active_game.transform.position = right_out_transform.position;

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
}
