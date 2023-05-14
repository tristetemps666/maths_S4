using System;

// basis random function for all other randoms

namespace MyRand{

    class myRand{
        public static float rand_0_1(){
            Random r = new Random();
            return r.Next()/Int32.MaxValue;
        }


        public static float rand_range(float a, float b){ // return a uniform random in [a,b]
            return rand_0_1()*(b-a)+a;
        }
    }

}