using System.Net.Mime;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



using GameStrat = System.Func<UnityEngine.GameObject,bool>; // functor for strat

public class GameManager : MonoBehaviour
{

    private Ball ball_fakir;
    private Money money_display;

    // public List<GameObject> list_of_games; TODO COmment le gérer ?

    private int active_game = 0;

    public bool has_choose_strat_1 = false;
    private bool has_choose_strat_2 = false;

    public List<GameObject> list_games;
    private List<GameStrat> list_strat_one = new List<GameStrat>();


    public int player_money = 100;
    // Start is called before the first frame update
    void Start()
    {
        money_display = GetComponentInChildren<Money>();
        ball_fakir = GetComponentInChildren<Ball>();
        
        Application.targetFrameRate = 16;
        // Time.fixedDeltaTime = 0.1f; // low frequency for physics CASSE LA PHYSIQUE
        // IDEAL => Object qui se simule parfaitement => la balle affichée prend une pose toute les tant

        setup_all_strats();





    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(list_strat_one.Count);

        if(ball_fakir.has_win){
            ball_fakir.has_win = false;
            player_money+=ball_fakir.win_value;
        }

        money_display.set_money(player_money);


        if (has_choose_strat_1 == true){
            has_choose_strat_1 = false;
            var strat = list_strat_one[active_game];
            if (strat == null) Debug.Log("cassé");

            strat(list_games[active_game]);
        }

    }


    private void setup_all_strats(){
        GameStrat start_one_fakir = (game) => {
            Fakir fakir = game.GetComponent<Fakir>();
            if(fakir==null) return false;
            fakir.start_one();
            return true;
        };

        list_strat_one.Add(start_one_fakir);
    }

    public int get_money(){
        return player_money;
    }

    // public void run_active_game(){ TODO possible ?
    //     list_of_games[active_game].GetComponent

    // }


    GameStrat start_one_fakir = (game) => {
        Fakir fakir = game.GetComponent<Fakir>();
        if(fakir=null) return false;
        fakir.start_one();
        return true;
    };


}
