using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum carroussel_state{ready_to_play, is_rolling, is_paused};

public class Carrousel : MonoBehaviour
{
    // Start is called before the first frame update
    public carroussel_state state = carroussel_state.ready_to_play;

    public Transform active_game_transform;
    public Transform left_out_transform;
    public Transform right_out_transform;

    private List<GameObject> list_games;



    void Start()
    {
        list_games = GetComponentInParent<GameManager>().list_games;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
