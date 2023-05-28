using System.Net.Mime;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



using GameStrat = System.Func<UnityEngine.GameObject,bool>; // functor for strat

public class GameManager : MonoBehaviour
{

    private Ball ball_fakir;
    private Money money_display;

    // public List<GameObject> list_of_games; TODO COmment le gérer ?

    public int active_game = 0;

    public bool has_choose_strat_1 = false;
    public bool has_choose_strat_2 = false;

    public bool strat_1_used = false;
    public bool strat_2_used = false;

    public GameObject startMenu;

    public AudioSource win_sound;
    public AudioSource loose_sound;


    public GameObject Strat_1;
    public GameObject Strat_2;
    private Lancer strat_1_script;
    private Lancer strat_2_script;

    private List<(string,string)> list_strats_names = new List<(string, string)>();
    private List<(int,int)> list_strat_cost = new List<(int, int)>();

    private List<GameStrat> list_strat_one = new List<GameStrat>();
    private List<GameStrat> list_strat_two = new List<GameStrat>();
    public List<GameObject> list_games;


    public List<bool> list_is_starting_games = new List<bool>();

    public List<bool> list_is_finished_games = new List<bool>(); // if the game is finished but not fully over (can be remade sometimes)
   
    public int player_money = 100;

    public List<GameObject> list_buttons = new List<GameObject>();


    private List<(float,float)> list_proga_games = new List<(float, float)>();


    private Carrousel carrousel;

    public GameObject GameInfos;    
    // Start is called before the first frame update

    public bool on_main_cam = true;

    public Camera main_cam;
    public Camera map_cam;
    void Start()
    {
        strat_1_script = Strat_1.GetComponent<Lancer>();
        strat_2_script = Strat_2.GetComponent<Lancer>();
        money_display = GetComponentInChildren<Money>();
        ball_fakir = GetComponentInChildren<Ball>();
        carrousel = GetComponentInChildren<Carrousel>();

        Application.targetFrameRate = 16;
        // Time.fixedDeltaTime = 0.1f; // low frequency for physics CASSE LA PHYSIQUE
        // IDEAL => Object qui se simule parfaitement => la balle affichée prend une pose toute les tant

        setup_all_strats(); // 0: DICE / 1 : Fakir / 2 : Piece
        update_buttons_names();
        setup_activations();
        update_game_info_name();
        update_game_info_proba();
        set_camera(on_main_cam);





    }

    // Update is called once per frame
    void Update()
    {
        update_camera();

        if (Input.anyKeyDown && !Input.GetMouseButtonDown(0))
        {
            startMenu.SetActive(false);
        }

        switch(carrousel.state){
            case carroussel_state.ready_to_play:
                update_proba_games();
                update_game_info_proba();
                enable_buttons();

                update_list_is_starting();
                update_list_is_finished();

                if(list_is_finished_games[active_game]){
                    strat_1_script.is_activated = false;
                    strat_2_script.is_activated = false;
                }

                handle_win();

                money_display.set_money(player_money);

                handle_strat_one();
                handle_strat_two();

                has_choose_strat_1 = strat_1_script.is_launched;
                has_choose_strat_2 = strat_2_script.is_launched;

                if(list_is_finished_games[active_game]){
                    carrousel.state = carroussel_state.start_to_roll;
                    list_is_finished_games[active_game] = false;
                } 

                break;


            case carroussel_state.is_rolling:
                disable_buttons();
                list_games[carrousel.next_game].SetActive(true);

                break;

            case carroussel_state.is_paused:
                disable_buttons();
                break;

            case carroussel_state.finished_to_roll:
                    carrousel.state = carroussel_state.ready_to_play;
                    has_choose_strat_1 = false;
                    has_choose_strat_2 = false;
                    strat_1_used = false;
                    strat_2_used = false;

                    reset_active_game();
                    list_games[active_game].SetActive(false);

                    active_game = carrousel.next_game;
                    update_buttons_names();
                    update_game_info_name();
                    update_proba_games();
                    update_game_info_proba();
                break;
        }





    }





private void handle_win(){

    int begin_money = player_money;
    
    // DICE
    Dice dice = list_games[0].GetComponent<Dice>();
    if(dice.has_just_win){
        player_money += dice.res*dice.win_multiplier;
    }


    //FAKIR
    Fakir fakir = list_games[1].GetComponent<Fakir>();
    if(fakir.has_just_win){
        player_money+=ball_fakir.win_value;
        player_money = Mathf.Max(0, player_money);
    }

    // PIECE
    Piece piece = list_games[2].GetComponent<Piece>();
    if(piece.has_just_win){
        player_money+=piece.res;
        player_money = Mathf.Max(0, player_money);
    }


    FileAttente attente = list_games[3].GetComponent<FileAttente>();
    if(attente.has_just_win){
        Debug.Log("ouine");
        player_money+= attente.money_earned;
        player_money = Mathf.Max(0, player_money);
    }


    if (player_money>begin_money) {
        win_sound.Play();
    }
    
    if (player_money<begin_money) {
        loose_sound.Play();
    }



}

private void disable_buttons(){
    foreach(GameObject button in list_buttons){
        button.SetActive(false);
    }
}

private void enable_buttons(){
    foreach(GameObject button in list_buttons){
        button.SetActive(true);
    }
}
private void handle_strat_one(){
    bool is_strat_1_activated = has_choose_strat_1 == true && player_money >= list_strat_cost[active_game].Item1 && list_is_starting_games[active_game] && !strat_1_used;
    if (is_strat_1_activated){
        strat_1_script.is_activated = true;

        player_money-= list_strat_cost[active_game].Item1;
        has_choose_strat_1 = false;
        var strat = list_strat_one[active_game];
        if (strat == null) Debug.Log("cassé");
        strat_1_used = true;

        strat(list_games[active_game]);
    } else if(!strat_1_used){
        strat_1_script.is_activated = false;
    }

    if(!has_choose_strat_2 && strat_2_used && active_game ==2) strat_2_used = false; // piece, on peut miser plusieurs fois
}



private void handle_strat_two(){
    bool is_strat_2_activated = has_choose_strat_2 == true && player_money >= list_strat_cost[active_game].Item2 && list_is_starting_games[active_game] && !strat_2_used;
    if (is_strat_2_activated){
        strat_2_script.is_activated = true;
        player_money-=list_strat_cost[active_game].Item2;
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

        for(int i = 0; i<list_games.Count; i++){
            list_is_starting_games.Add(false);
            list_is_finished_games.Add(false);

            list_proga_games.Add((0,0));
            list_strat_cost.Add((0,0));
        }

        setup_strat_cost();

        setup_list_names();

    }

    void setup_strat_cost(){
        list_strat_cost[0] = (50,50);
        list_strat_cost[1] = (50,80);
        list_strat_cost[2] = (100,20);
        list_strat_cost[3] = (30,30);
    }
    private void setup_activations(){
        for(int i=0; i<list_games.Count; i++){
            if (i==active_game) list_games[i].SetActive(true);
            else list_games[i].SetActive(false);
        }
    }



    private void setup_all_strats_one(){
        GameStrat strat_one_dice = (game) =>{
            Dice dice = game.GetComponent<Dice>();
            if(dice==null) return false;
            dice.strat_one();
            return true;
        };
        list_strat_one.Add(strat_one_dice);
        
        GameStrat strat_one_fakir = (game) => {
            Fakir fakir = game.GetComponent<Fakir>();
            if(fakir==null) return false;
            fakir.strat_one();
            return true;
        };
        list_strat_one.Add(strat_one_fakir);


        GameStrat strat_one_piece = (game) => {
            Piece piece = game.GetComponent<Piece>();
            if(piece==null) return false;
            piece.strat_one();
            return true;
        };
        list_strat_one.Add(strat_one_piece);


        GameStrat strat_one_attente = (game) => {
            FileAttente attente = game.GetComponent<FileAttente>();
            if(attente==null) return false;
            attente.strat_one();
            return true;
        };
        list_strat_one.Add(strat_one_attente);

    }


    private void setup_all_strats_two(){
        GameStrat strat_two_dice = (game) =>{
            Dice dice = game.GetComponent<Dice>();
            if(dice==null) return false;
            dice.strat_two();
            return true;
        };
        list_strat_two.Add(strat_two_dice);


        GameStrat strat_two_fakir = (game) => {
            Fakir fakir = game.GetComponent<Fakir>();
            if(fakir==null) return false;
            fakir.strat_two();
            return true;
        };
        list_strat_two.Add(strat_two_fakir);


        GameStrat strat_two_piece = (game) => {
            Piece piece = game.GetComponent<Piece>();
            if(piece==null) return false;
            piece.strat_two();
            return true;
        };
        list_strat_two.Add(strat_two_piece);

        
        GameStrat strat_two_attente = (game) => {
            FileAttente attente = game.GetComponent<FileAttente>();
            if(attente==null) return false;
            attente.strat_two();
            return true;
        };
        list_strat_two.Add(strat_two_attente);


    }

    private void setup_list_names(){
        list_strats_names.Add(("x2 (50$)","Pair = choix jeu (50$)")); // DICE
        list_strats_names.Add(("x2 (50$)","+1 (80$)")); // FAKIR
        list_strats_names.Add(("Win : 0.75% (100$)","Miser 20$")); // PIECE
        list_strats_names.Add(("*2 lambda client (50$)","*2 lambda payment (50$)")); // FILE ATTENTE
    }

    private void update_buttons_names(){
        strat_1_script.set_button_text(list_strats_names[active_game].Item1);
        strat_2_script.set_button_text(list_strats_names[active_game].Item2);  
    }

    private void update_proba_games(){
        Dice dice = list_games[0].GetComponent<Dice>();
        list_proga_games[0] = (dice.dice_proba.esperance()*dice.win_multiplier,Mathf.Pow(dice.win_multiplier,2)*dice.dice_proba.variance());

        Piece piece = list_games[2].GetComponent<Piece>();
        list_proga_games[2] = (piece.piece_proba.esperance()*piece.mise*2,Mathf.Pow(piece.mise*2,2)*piece.piece_proba.variance());
    }

    public int get_money(){
        return player_money;
    }

    // public void run_active_game(){ TODO possible ?
    //     list_of_games[active_game].GetComponent

    // }

    void update_list_is_starting(){
        list_is_starting_games[0] = !list_games[0].GetComponent<Dice>().is_running && !list_games[0].GetComponent<Dice>().has_win;
        list_is_starting_games[1] = !list_games[1].GetComponent<Fakir>().is_running && !list_games[1].GetComponent<Fakir>().has_fallen;
        list_is_starting_games[2] = !list_games[2].GetComponent<Piece>().is_running && !list_games[2].GetComponent<Piece>().has_win;
        list_is_starting_games[3] = !list_games[3].GetComponent<FileAttente>().is_running && !list_games[3].GetComponent<FileAttente>().has_win;
    }


    void update_list_is_finished(){
        list_is_finished_games[0] = list_games[0].GetComponent<Dice>().is_over;
        list_is_finished_games[1] = list_games[1].GetComponent<Fakir>().is_over;
        list_is_finished_games[2] = list_games[2].GetComponent<Piece>().is_over;
        list_is_finished_games[3] = list_games[3].GetComponent<FileAttente>().is_over;
    }



    void update_game_info_name(){
        TextMeshPro game_info_name = GameInfos.GetComponentsInChildren<TextMeshPro>()[0];
        game_info_name.text = list_games[active_game].name;
    }


    void update_game_info_proba(){
        TextMeshPro game_info_proba = GameInfos.GetComponentsInChildren<TextMeshPro>()[1];
        game_info_proba.text = "E(x) = " + list_proga_games[active_game].Item1.ToString()+ "\nV(X) = "+ list_proga_games[active_game].Item2.ToString();

    }


    void set_camera(bool on_main_cam){
        main_cam.enabled = on_main_cam;
        map_cam.enabled = !on_main_cam;
    }

    void update_camera(){
        if(Input.GetKeyDown(KeyCode.C)){
            on_main_cam = !on_main_cam;
        }
        set_camera(on_main_cam);

    }

    void reset_active_game(){
        switch (active_game){

            case 0:
                Debug.Log("reset dice");
                Dice dice = list_games[active_game].GetComponent<Dice>();
                dice.reset();
                break;

            case 1:
                Debug.Log("reset fakir");
                Fakir fakir = list_games[active_game].GetComponent<Fakir>();
                fakir.reset();
                break;
            
            case 2:
                Debug.Log("reset piece");
                Piece piece = list_games[active_game].GetComponent<Piece>();
                piece.reset();
                break;

            case 3:
                Debug.Log("reset file Attente");
                FileAttente file = list_games[active_game].GetComponent<FileAttente>();
                file.reset();
                break;
        }
    }
}
