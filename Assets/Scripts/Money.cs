using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    // Start is called before the first frame update

    private int player_money;
    private TextMeshPro text_money;
    void Start()
    {
       player_money =  GetComponentInParent<GameManager>().get_money();
       text_money = GetComponentInChildren<TextMeshPro>();

    }

    // Update is called once per frame
    void Update()
    {
        text_money.text = player_money.ToString();
        
    }
    public void set_money(int value){
        player_money = value;
    }
}
