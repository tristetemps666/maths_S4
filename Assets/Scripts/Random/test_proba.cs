using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DICE;
using MyRand;

public class test_proba : MonoBehaviour
{
    // Start is called before the first frame update
    public Dice dice;
    void Start()
    {
        if (dice == null) dice = new Dice(); 
    }

    // Update is called once per frame
    void Update()
    {

        DiceStats stats = dice.get_stats();
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("dé : " + dice.roll()+ "\n\n");
            // Debug.Log("rand 0 1 : " + myRand.rand_0_1());

        }

        if (Input.GetKeyDown(KeyCode.A)){
            Debug.Log("stats : \n nombre de lancers : "+stats.roll_number+"\n nombre pour chq faces : "+ stats.string_amount_foreach() + "\n face la plus fréquente : " + stats.most_frequent_face + "\n nombre moyen : "+ stats.average_number);
        }
    }
}
