using System.Collections;
using System.Collections.Generic;

using MyRand;





namespace DISCRETE_UNIFORM
{

    public struct DiscreteUniformStats{
        public DiscreteUniformStats(int a = 0){
            roll_number = a;
            amount_foreach_number = new List<int>(){0,0,0,0,0,0};
            most_frequent_number = new List<int>(){0};
            average_number = 0f;
        }


        public int roll_number;
        
        public List<int> most_frequent_number;

        public float average_number;
        public List<int> amount_foreach_number; // number of roll that give the face
    }
    
    public class DiscreteUniform {

        
        private int last_number = 6; // {1,2,3,4,5,6}
        private float weights =1f/6; // {1.f/6,1.f/6,1.f/6,1.f/6,1.f/6,1.f/6}

        public int actual_number = 1;
        private DiscreteUniformStats stats = new DiscreteUniformStats();

        public DiscreteUniform() {
            stats = new DiscreteUniformStats(0); // sombre un peu obligé de mettre un param dans le constructeur wsh
        }

        public DiscreteUniform(int last_number){
            for(int i=1; i<= last_number; i++){
                last_number = i;
                weights = 1f/6;
            }
        }


        public int rand(){
            float rand = myRand.rand_0_1();
            int val = (int)(System.MathF.Floor(last_number*rand)+1);
            actual_number = val;

            // for(int i=0; i<faces_count(); i++){
            //     if(rand <a) {
            //         actual_id = i;
            //         actual_number = list_numbers[i];

            //         update_dice_stats();
            //         return actual_number;
            //     }
            // }
            update_dice_stats();
            return val;
        }

        public float esperance(){
            return (last_number+1f)/2f;
        }

        public float variance(){
            return last_number*last_number;
        }

        public int get_last_number(){
            return last_number;
        }


        private void update_dice_stats(){
            stats.average_number = (stats.average_number*stats.roll_number+actual_number)/(stats.roll_number+1); // moyenne entre lancienne moyenne et le nouveau lancé
            stats.roll_number++;
            stats.amount_foreach_number[actual_number-1]+=1;
            stats.most_frequent_number = get_most_frequent_number();
        }


        private List<int> get_most_frequent_number(){
            List<int> list_max = new List<int>(){0};

            for(int i = 0; i<get_last_number(); i++){
                int actual_max_amount = list_max[0] != 0 ? 
                                        stats.amount_foreach_number[list_max[0]-1]
                                        : 0;

                int candidate_max_i = stats.amount_foreach_number[i];
                if(candidate_max_i>actual_max_amount){
                    list_max = new List<int>(){i+1};
                }else if (candidate_max_i==actual_max_amount){
                    list_max.Add(i+1);
                }
            }
            return list_max;
        }

        public DiscreteUniformStats get_stats(){
            return stats;
        }

    }
}