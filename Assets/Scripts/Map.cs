using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameManager game_manager;

    public List<int> list_games_visited = new List<int>();

    public int active_game = 0;

    public GameObject diamond;
    public float diamond_height;

    public List<GameObject> list_game_points;
    // Start is called before the first frame update
    void Start()
    {
        set_diamond_position();
        list_games_visited = new List<int>(game_manager.list_games.Count);

    }

    // Update is called once per frame
    void Update()
    {
        active_game = game_manager.active_game;

        set_diamond_position();
    }

    void set_diamond_position(){
        diamond.transform.position = list_game_points[active_game].transform.position + Vector3.up*diamond_height;
    }
}
