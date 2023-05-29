using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameManager game_manager;

    public List<int> list_games_visited = new List<int>();

    public List<Sprite> sprites_map;


    public int active_game = 0;

    public SpriteRenderer current_sprite_map;

    public List<GameObject> list_game_points;
    // Start is called before the first frame update
    void Start()
    {
        list_games_visited = new List<int>(game_manager.list_games.Count);

        

    }

    // Update is called once per frame
    void Update()
    {
        active_game = game_manager.active_game;
        current_sprite_map.sprite = sprites_map[active_game];
        
    }


}
