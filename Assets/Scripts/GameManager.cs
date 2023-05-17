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

    private bool strat_1_used = false;
    private bool strat_2_used = false;


    public GameObject Strat_1;
    public GameObject Strat_2;
    private Lancer strat_1_script;
    private Lancer strat_2_script;
    private List<GameStrat> list_strat_one = new List<GameStrat>();
    private List<GameStrat> list_strat_two = new List<GameStrat>();
    public List<GameObject> list_games;


    public List<bool> list_is_starting_games = new List<bool>();

    public List<bool> list_is_finished_games = new List<bool>();




    public int player_money = 100;
    // Start is called before the first frame update
    void Start()
    {
        strat_1_script = Strat_1.GetComponent<Lancer>();
        strat_2_script = Strat_2.GetComponent<Lancer>();
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

        has_choose_strat_1 = strat_1_script.is_launched;
        has_choose_strat_2 = strat_2_script.is_launched;


        if(ball_fakir.has_win){
            ball_fakir.has_win = false;
            player_money+=ball_fakir.win_value;
        }

        update_list_is_starting();
        update_list_is_finished();

        if(list_is_finished_games[active_game]){
            strat_1_script.is_activated = false;
            strat_2_script.is_activated = false;
        }

        Debug.Log("fakir is starting : " + list_is_starting_games[0]);

        money_display.set_money(player_money);

        


        handle_strat_one();
        handle_strat_two();
    }

private void handle_strat_one(){
    bool is_strat_1_activated = has_choose_strat_1 == true && player_money >= 50 && list_is_starting_games[active_game] && !strat_1_used;
    if (is_strat_1_activated){
        player_money-= 50;
        has_choose_strat_1 = false;
        var strat = list_strat_one[active_game];
        if (strat == null) Debug.Log("cassé");
        strat_1_used = true;

        strat(list_games[active_game]);
    } else if(!strat_1_used){
        strat_1_script.is_activated = false;
    }
}


private void handle_strat_two(){
    bool is_strat_2_activated = has_choose_strat_2 == true && player_money >= 60 && list_is_starting_games[active_game] && !strat_2_used;
    if (is_strat_2_activated){
        strat_2_script.is_activated = true;
        player_money-=60;
        has_choose_strat_2 = false;
        var strat2 = list_strat_two[active_game];
        if (strat2 == null) Debug.Log("cassé");
        strat_2_used = true;

        strat2(list_games[active_game]);
    } 
    else if(!strat_2_used){
        strat_2_script.is_activated = false;
    }
}


    private void setup_all_strats(){
        setup_all_strats_one();
        setup_all_strats_two();
        list_is_starting_games.Add(false);
        list_is_finished_games.Add(false);
    }


    private void setup_all_strats_one(){
        GameStrat strat_one_fakir = (game) => {
            Fakir fakir = game.GetComponent<Fakir>();
            if(fakir==null) return false;
            fakir.strat_one();
            return true;
        };
        list_strat_one.Add(strat_one_fakir);
    }


    private void setup_all_strats_two(){
        GameStrat strat_two_fakir = (game) => {
            Fakir fakir = game.GetComponent<Fakir>();
            if(fakir==null) return false;
            fakir.strat_two();
            return true;
        };
        list_strat_two.Add(strat_two_fakir);


    }

    public int get_money(){
        return player_money;
    }

    // public void run_active_game(){ TODO possible ?
    //     list_of_games[active_game].GetComponent

    // }

    void update_list_is_starting(){
        list_is_starting_games[0] = !list_games[0].GetComponent<Fakir>().is_running && !list_games[0].GetComponent<Fakir>().is_finished;
    }

    void update_list_is_finished(){
        list_is_finished_games[0] = list_games[0].GetComponent<Fakir>().is_finished;
    }


    GameStrat strat_one_fakir = (game) => {
        Fakir fakir = game.GetComponent<Fakir>();
        fakir.strat_one();
        return true;
    };


}
