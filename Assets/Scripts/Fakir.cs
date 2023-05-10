using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fakir : MonoBehaviour
{
    public bool is_running = false;
    private Vector3 diff = Vector3.zero;
    private GameObject Ball;

    private GameObject Start_button;

    public Lancer launch_button;

    // Start is called before the first frame update
    void Start()
    {
        Ball = GetComponentInChildren<Ball>().gameObject;
        Ball.GetComponent<Rigidbody2D>().isKinematic = false; 
    }

    // Update is called once per frame
    void Update()
    {

        is_running = launch_button.is_launched ? true : is_running;
        
        diff = Ball.GetComponent<Ball>().get_diff();
        
        Ball.GetComponent<Rigidbody2D>().bodyType = is_running ? UnityEngine.RigidbodyType2D.Dynamic : UnityEngine.RigidbodyType2D.Static;

        Debug.Log(Ball.GetComponent<Rigidbody2D>().isKinematic);
        Debug.Log(is_running);
    }




    // private void OnMouseOver() {
    //     Debug.Log("je touche la balle");
    //     if(Input.GetMouseButton(0)){
    //         diff = Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.localPosition;
            
    //     }
    // }
}
