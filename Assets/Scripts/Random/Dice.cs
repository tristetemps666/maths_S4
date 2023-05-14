using System.Collections;
using System.Collections.Generic;

using MyRand;



namespace DICE
{
    
    public class Dice {

        private List<int> faces_number = new List<int>(){1,2,3,4,5,6}; // {1,2,3,4,5,6}
        private List<float> faces_weight =new List<float>(){1f/6,1f/6,1f/6,1f/6,1f/6,1f/6}; // {1.f/6,1.f/6,1.f/6,1.f/6,1.f/6,1.f/6}



        public int roll(){
            float rand = myRand.rand_0_1();
            float a = 0f;

            for(int i=0; i<faces_count(); i++){
                a+=faces_weight[1];
                if(rand <a) return faces_number[i];
            }
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
        }

        public bool set_faces_weight(List<float> list){
            float a = 0f;
            list.ForEach(x=> a=a+x);
            
            if (System.MathF.Abs(a - 1f) <= 0.01f) {
                faces_weight = list;
                return true;
            }
            else return false;
        }

    }
}