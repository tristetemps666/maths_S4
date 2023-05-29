using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selection_next_map : MonoBehaviour
{
    // Start is called before the first frame update

    public int number_to_return;
    public int number_selected = -1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseOver() {
        Debug.Log("hover :" + number_to_return);
        if(Input.GetMouseButtonUp(0)) number_selected = number_to_return;
    }

}
