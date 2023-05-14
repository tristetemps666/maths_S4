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
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("dé : " + dice.roll()+ "\n\n");
            // Debug.Log("rand 0 1 : " + myRand.rand_0_1());

        }
    }
}
