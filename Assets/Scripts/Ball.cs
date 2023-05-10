using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    public int win_value = 0;
    public bool has_win= false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(has_win) Debug.Log(win_value);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "ScoreFakir" && !has_win){
            win_value = int.Parse(other.gameObject.GetComponent<TextMeshPro>().text);
            has_win = true;
        }
    }
}
