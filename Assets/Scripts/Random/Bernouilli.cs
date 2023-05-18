using System.Collections;
using System.Collections.Generic;

using MyRand;



namespace BERNOULLI
{
    public struct BernoulliStats{

        public BernoulliStats(int val=0){
            roll_number = val;
            number_of_win = 0;
            number_of_loose = 0;
            average_win = 0;  
        }

        public int roll_number;
        
        public int number_of_win;

        public int number_of_loose;

        public float average_win;
    }
    
    public class Bernoulli {

        private float win_proba = 0.5f;
        private int win_number = 1;
        private int loose_number = 0;

        private BernoulliStats  stats = new BernoulliStats(0);


        public Bernoulli() {
            stats = new BernoulliStats(0); // sombre un peu obligé de mettre un param dans le constructeur wsh
        }



        public int rand(){
            int val = myRand.rand_0_1() <=win_proba ? win_number : loose_number;
            update_stats(val);
            return val;
        }


        private void update_stats(int result){
            if(result==1) stats.number_of_win++;
            else stats.number_of_loose++;
            stats.average_win = (stats.average_win*stats.roll_number+result)/(stats.roll_number+1f);
            stats.roll_number++;
        }

        public float esperance(){
            return win_proba;
        }

        public float variance(){
            return win_proba*(1-win_proba);
        }

        public BernoulliStats get_stats(){
            return stats;
        }
    }
}
