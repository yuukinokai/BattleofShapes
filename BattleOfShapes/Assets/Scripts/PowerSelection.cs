using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSelection : MonoBehaviour
{
    private int currSelection = 0;

    private Color[] colours = {Color.red, Color.green, Color.white, Color.yellow};
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
     {
         float horizontal = Input.GetAxisRaw("Horizontal" + name);
        if(horizontal > 0){
            ChangeLeft();
        }
        if(horizontal < 0){
            ChangeRight();
        }
     }  
    }

    void ChangeLeft(){
        currSelection++;
        currSelection = currSelection % colours.Length;
        this.GetComponent<Image>().color = colours[currSelection];
    }
    void ChangeRight(){
        currSelection--;
        if(currSelection < 0){
            currSelection += colours.Length;
        }
        this.GetComponent<Image>().color = colours[currSelection];
    }
}
