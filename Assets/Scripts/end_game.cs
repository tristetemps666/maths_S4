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

    public string[] names_metal = new string[4] {"","bronze","argent","or"};
    public int player_money = 0;

    public TextMeshPro player_money_text;

    public Transform start_bar_money;

    public int[] list_count_games_played = new int[4]{0,0,0,0};

    public int max_money_earned;

    public int int_max_money_lost;

    private float ratio_money = 0f;

    
    public BernoulliStats bs = new BernoulliStats(0);
    public ExponentielleStats es = new ExponentielleStats(0);

    public DiscreteUniformStats us = new DiscreteUniformStats(0);
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
        }

        if(score_button.is_activated){
            stats_page.SetActive(false);
            stat_button.is_activated = false;
            score_button.is_activated = false;
        }

        compute_end_game_datas();
        set_hearts_and_metal();
    }


    void compute_end_game_datas(){
        player_money_text.text = player_money.ToString() + " $";

        ratio_money = Mathf.Clamp(player_money/2000f,0f,1f);
        start_bar_money.localScale = new Vector3(ratio_money,start_bar_money.localScale.y,1f);


    }

    void set_hearts_and_metal(){
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
}
