using System.Net.Mime;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int player_money = 100;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 16;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int get_money(){
        return player_money;
    }
}
