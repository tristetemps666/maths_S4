using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DISCRETE_UNIFORM;
using BERNOULLI;
using EXPONENTIELLE;
using MyRand;

public class test_proba : MonoBehaviour
{
    // Start is called before the first frame update
    public DiscreteUniform dice;
    public Bernoulli piece;

    public Exponentielle expo;


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
    void Start()
    {
        if (dice == null) dice = new DiscreteUniform(); 
        if (piece == null) piece = new Bernoulli();
        if (expo == null) expo = new Exponentielle();
    }

    // Update is called once per frame
    void Update()
    {

        // DICE TEST
        DiscreteUniformStats stats = dice.get_stats();
        BernoulliStats stats_ber = piece.get_stats();
        ExponentielleStats stats_exp = expo.get_stats();


        if(Input.GetKeyDown(KeyCode.Keypad0)){
            Debug.Log("dé : " + dice.rand()+ "\n\n");

        }
        if (Input.GetKeyDown(KeyCode.D)){
            Debug.Log("stats : \n nombre de lancers : "+stats.roll_number+"\n nombre pour chq faces : "+ to_string(stats.amount_foreach_number) + "\n face(s) la plus fréquente : " + to_string(stats.most_frequent_number) + "\n nombre moyen : "+ stats.average_number);
        }



        // COIN TEST
        if(Input.GetKeyDown(KeyCode.Keypad1)){
            Debug.Log("piece : " + piece.rand()+ "\n\n");
        }
        if(Input.GetKeyDown(KeyCode.C)){
            Debug.Log("stats : \n nombre de lancers : "+stats_ber.roll_number+"\n nombre de win : "+ stats_ber.number_of_win + "\n nombre de looses : "  + stats_ber.number_of_loose + "\n nombre moyen : "+ stats_ber.average_win);
        }


        // EXPO TEST

        if(Input.GetKeyDown(KeyCode.Keypad2)){
            Debug.Log("Expo : " + expo.rand()+ "\n\n");
        }

        if(Input.GetKeyDown(KeyCode.E)){
            Debug.Log("stats : \n nombre de lancers : "+stats_exp.roll_number+"\n nombre moyen: "+ stats_exp.average_value + "\nEsperance théorique : " + expo.esperance());
        }
    }
}
