using System.Net.Mime;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private Ball ball_fakir;
    private Money money_display;


    public int player_money = 100;
    // Start is called before the first frame update
    void Start()
    {
        money_display = GetComponentInChildren<Money>();
        ball_fakir = GetComponentInChildren<Ball>();
        Application.targetFrameRate = 16;
    }

    // Update is called once per frame
    void Update()
    {
        if(ball_fakir.has_win){
            Debug.Log("win "+ ball_fakir.win_value);
            ball_fakir.has_win = false;
            player_money+=ball_fakir.win_value;
        }

        money_display.set_money(player_money);
    }

    public int get_money(){
        return player_money;
    }
}
