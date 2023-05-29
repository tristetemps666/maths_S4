using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using BERNOULLI;
using EXPONENTIELLE;
using DISCRETE_UNIFORM;

public class end_game : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject hearts;

    public Sprite full_heart_sprite;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        compute_end_game_datas();
        set_hearts();
    }


    void compute_end_game_datas(){
        player_money_text.text = player_money.ToString() + " $";

        ratio_money = Mathf.Clamp(player_money/2000f,0f,1f);
        start_bar_money.localScale = new Vector3(ratio_money,start_bar_money.localScale.y,1f);


    }

    void set_hearts(){
        int number_of_full_hearts = (int)Mathf.Floor(Mathf.Clamp(player_money/1500f,0f,1f)*3f);
        
        SpriteRenderer[] list_hearts = hearts.GetComponentsInChildren<SpriteRenderer>();
        for(int i = 0; i<number_of_full_hearts; i++){
            list_hearts[i].sprite = full_heart_sprite;
        }
    }
}
