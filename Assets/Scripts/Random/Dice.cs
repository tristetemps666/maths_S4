using System.Collections;
using System.Collections.Generic;

using MyRand;





namespace DICE
{

    public struct DiceStats{
        public DiceStats(int a = 0){
            roll_number = a;
            amount_foreach_face = new List<int>(){0,0,0,0,0,0};
            most_frequent_face = new List<int>(){0};
            average_number = 0f;
        }


        public int roll_number;
        
        public List<int> most_frequent_face;

        public float average_number;
        public List<int> amount_foreach_face; // number of roll that give the face
    }
    
    public class Dice {

        
        private List<int> faces_number = new List<int>(){1,2,3,4,5,6}; // {1,2,3,4,5,6}
        private List<float> faces_weight =new List<float>(){1f/6,1f/6,1f/6,1f/6,1f/6,1f/6}; // {1.f/6,1.f/6,1.f/6,1.f/6,1.f/6,1.f/6}

        public int actual_number = 1;
        private int actual_id = 0;

        private DiceStats stats = new DiceStats();

        public Dice() {
            stats = new DiceStats(0); // sombre un peu obligé de mettre un param dans le constructeur wsh
        }
        


        public int roll(){
            float rand = myRand.rand_0_1();
            float a = 0f;

            for(int i=0; i<faces_count(); i++){
                a+=faces_weight[1];
                if(rand <a) {
                    actual_id = i;
                    actual_number = faces_number[i];

                    update_dice_stats();
                    return actual_number;
                }
            }
            update_dice_stats();
            return faces_number[faces_count()-1];
        }

        public int faces_count(){
            return faces_number.Count;
        }

        public void modify_face_number(int id_face, int new_number){
            faces_number[id_face] = new_number;
        }
        public void set_faces_number(List<int> list){
            faces_number = list;
            stats.amount_foreach_face = new List<int>(list.Count); // remise à 0 pour le moment;
        }

        public bool set_faces_weight(List<float> list){
            float a = 0f;
            list.ForEach(x=> a=a+x);
            
            if (System.MathF.Abs(a - 1f) <= 0.01f) {
                faces_weight = list;
                stats.amount_foreach_face= new List<int>(list.Count); // RAZ pour le moment;
                return true;
            }
            else return false;
        }


        private void update_dice_stats(){
            stats.average_number = (stats.average_number*stats.roll_number+actual_number)/(stats.roll_number+1); // moyenne entre lancienne moyenne et le nouveau lancé
            stats.roll_number++;
            stats.amount_foreach_face[actual_id]+=1;
            stats.most_frequent_face = get_most_frequent_face();
        }


        private List<int> get_most_frequent_id(){
            List<int> list_max = new List<int>(){0};

            for(int i = 0; i<faces_count(); i++){
                int actual_max = stats.amount_foreach_face[list_max[0]];
                int candidate_max_i = stats.amount_foreach_face[i];
                if(candidate_max_i>actual_max){
                    list_max = new List<int>(){i};
                }else if (candidate_max_i==actual_max){
                    list_max.Add(i);
                }
            }
            return list_max;
        }


        private List<int> get_most_frequent_face(){
            List<int> res = new List<int>();
            get_most_frequent_id().ForEach(a=>res.Add(faces_number[a]));
            return res;
        }

        public DiceStats get_stats(){
            return stats;
        }

    }
}