using System.Collections;
using System.Collections.Generic;

using MyRand;



namespace BERNOUILLI
{
    
    public class Bernouilli {

        private float win_proba = 0.5f;
        private int win_number = 1;
        private int loose_number = 0; 



        public int rand(){
            return myRand.rand_0_1() <=win_proba ? win_number : loose_number;
        }

        public float esperance(){
            return win_proba;
        }

        public float variance(){
            return win_proba*(1-win_proba);
        }
    }
}