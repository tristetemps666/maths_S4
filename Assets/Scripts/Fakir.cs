using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fakir : MonoBehaviour
{
    public bool is_running = false;
    private Vector3 diff = Vector3.zero;
    private GameObject Ball;

    private GameObject Start_button;

    public Lancer launch_button;

    public List<TextMeshPro> list_rewards_text;

    private float strat_one_multiplier = 2f;


    // Start is called before the first frame update
    void Start()
    {
        Ball = GetComponentInChildren<Ball>().gameObject;
        Ball.GetComponent<Rigidbody2D>().isKinematic = false; 
        list_rewards_text.AddRange(GetComponentsInChildren<TextMeshPro>());
    }

    // Update is called once per frame
    void Update()
    {

        is_running = launch_button.is_launched ? true : is_running;
        
        diff = Ball.GetComponent<Ball>().get_diff();
        
        Ball.GetComponent<Rigidbody2D>().bodyType = is_running ? UnityEngine.RigidbodyType2D.Dynamic : UnityEngine.RigidbodyType2D.Static;
    }




    public void start_one(){
        if(is_running) return;
        list_rewards_text.ForEach(txt => txt.text = (int.Parse(txt.text)*strat_one_multiplier).ToString());
    }
}
