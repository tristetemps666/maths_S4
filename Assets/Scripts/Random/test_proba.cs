using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DISCRETE_UNIFORM;
using MyRand;

public class test_proba : MonoBehaviour
{
    // Start is called before the first frame update
    public DiscreteUniform dice;


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
    }

    // Update is called once per frame
    void Update()
    {

        DiscreteUniformStats stats = dice.get_stats();
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("dé : " + dice.rand()+ "\n\n");
            // Debug.Log("rand 0 1 : " + myRand.rand_0_1());

        }

        if (Input.GetKeyDown(KeyCode.A)){
            Debug.Log("stats : \n nombre de lancers : "+stats.roll_number+"\n nombre pour chq faces : "+ to_string(stats.amount_foreach_number) + "\n face(s) la plus fréquente : " + to_string(stats.most_frequent_number) + "\n nombre moyen : "+ stats.average_number);
        }
    }
}
