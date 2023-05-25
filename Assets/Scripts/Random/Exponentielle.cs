using System.Collections;
using System.Collections.Generic;

using MyRand;



namespace EXPONENTIELLE
{
    public struct ExponentielleStats{

        public ExponentielleStats(int val=0){
            roll_number = val;
            average_value = 0f;
        }
        public int roll_number;
        public float average_value;
    }
    
    public class Exponentielle {

        public float lambda = 0.5f;

        private System.Func<float, float> density_function;
        

        private ExponentielleStats  stats = new ExponentielleStats(0);


        public Exponentielle() {
            stats = new ExponentielleStats(0); 
        }

        public float repartition_function(float t){ // usefull ??
            return 1f-System.MathF.Exp(lambda*t);
        }

        public float invert_repartition_function(float U){
            return -1f/lambda*System.MathF.Log(1-U);
        }
        
        public float rand(){
            float res = invert_repartition_function(myRand.rand_0_1());
            update_stats(res);
            return res;
        }


  
        public float get_proba(float t){
            return repartition_function(t);
        }


        private void update_stats(float result){
            stats.average_value = (stats.average_value * stats.roll_number +result)/(stats.roll_number+1);
            stats.roll_number++;
        }

        public float esperance(){
            return 1/lambda;
        }

        public float variance(){
            return 1/(lambda*lambda);
        }

        public ExponentielleStats get_stats(){
            return stats;
        }
    }
}
