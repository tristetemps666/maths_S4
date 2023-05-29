using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

using BERNOULLI;
using EXPONENTIELLE;
using DISCRETE_UNIFORM;

public class end_game : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Lancer restart_button;

    public Lancer stat_button;

    public Lancer score_button;

    public GameObject stats_page;

    public GameObject hearts;

    public Sprite full_heart_sprite;

    public TextMeshPro metal_text;
    public TextMeshPro stats_text;

    public string[] names_metal = new string[4] {"","bronze","argent","or"};
    public string[] names_games = new string[4]{"de","fakir","piece","bar"};
    public int player_money = 0;

    public TextMeshPro player_money_text;

    public Transform start_bar_money;

    public List<int> list_count_games_played = new List<int>{0,0,0,0};

    public List<int> list_gain = new List<int> {0,0,0,0};

    public List<float> list_gain_moyen = new List<float> {0,0,0,0};

    public int max_money;
    public int max_money_earned;

    public int max_money_lost;

    public int min_money = 200;

    private float ratio_money = 0f;

    
    public BernoulliStats bs = new BernoulliStats();
    public ExponentielleStats es = new ExponentielleStats();

    public DiscreteUniformStats us = new DiscreteUniformStats();
    void Start()
    {
        stats_page.SetActive(false);
        metal_text.text = "";
        
    }

    // Update is called once per frame
    void Update()
    {
        if(restart_button.is_activated){
            Invoke("restart",1f);
        }

        if(stat_button.is_activated){
            stats_page.SetActive(true);
            set_text_stats();
        }

        if(score_button.is_activated){
            stats_page.SetActive(false);
            stat_button.is_activated = false;
            score_button.is_activated = false;
        }
    }


    public void compute_end_game_datas(){
        player_money_text.text = player_money.ToString() + " $";

        ratio_money = Mathf.Clamp(player_money/2000f,0f,1f);
        start_bar_money.localScale = new Vector3(ratio_money,start_bar_money.localScale.y,1f);
    }

    public void set_hearts_and_metal(){

        int number_of_full_hearts = (int)Mathf.Floor(Mathf.Clamp(player_money/1500f,0f,1f)*3f);
        metal_text.text = names_metal[number_of_full_hearts];
        
        SpriteRenderer[] list_hearts = hearts.GetComponentsInChildren<SpriteRenderer>();
        for(int i = 0; i<number_of_full_hearts; i++){
            list_hearts[i].sprite = full_heart_sprite;
        }
        
    }

    void restart(){
        SceneManager.LoadScene(0);
    }

    void set_text_stats(){
        Debug.Log("set les stats");
        // set_text_stats_dice();
        string text = "argent max : " + max_money.ToString() + "\n";
        text+= "argent min : " + min_money.ToString() + "\n\n";
        text+= "meilleur gain : " + max_money_earned.ToString() +"\n";
        text+= "pire perte : " + max_money_lost.ToString()+ "\n\n";
        text+= "jeu le plus gagnant : " +names_games[get_max(list_gain).Item1]+ "\n";
        text+= "jeu le moins gagnant : " +names_games[get_min(list_gain).Item1]+ "\n";

        stats_text.text = text;
    }


    void set_text_stats_dice(){
        string text = "dice :\n nombre de lancers : "+us.roll_number+"\n nombre pour chq faces : "+ to_string(us.amount_foreach_number) + "\n face(s) la plus fréquente : " + to_string(us.most_frequent_number) + "\n nombre moyen : "+ us.average_number;

        stats_text.text = text;
    }





    private string to_string(List<int> list){
        if(list == null) return "null";
        if(list.Count == 1) return list[0].ToString();
        else{
            string a= "{";
            for(int i=0 ; i<list.Count-1; i++){
                a += list[i].ToString() + ",";
            }
            return a + list[list.Count-1].ToString() +"}";
        }
    }

    private (int,int) get_max(List<int> list){ // (index,value)
        (int,int) res = (0,list[0]);
        for(int i=0; i<list.Count; i++){
            if(list[i] > res.Item2) res = (i,list[i]);
        }
        return res;
    }

    private (int,int) get_min(List<int> list){ // (index,value)
        (int,int) res = (0,list[0]);
        for(int i=0; i<list.Count; i++){
            if(list[i] < res.Item2) res = (i,list[i]);
        }
        return res;
    }
}
