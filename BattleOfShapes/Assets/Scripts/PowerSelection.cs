using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSelection : MonoBehaviour
{
    private int currSelection = 0;

    private Color[] colours = {Color.red, Color.green, Color.white, Color.yellow};

    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Player")){
            if(p.name == name){
                player = p;
            }
        }
        if(player == null){
            Destroy(gameObject);
        }
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
        player.GetComponent<Battle>().SetBullet(currSelection);
    }
    void ChangeRight(){
        currSelection--;
        if(currSelection < 0){
            currSelection += colours.Length;
        }
        this.GetComponent<Image>().color = colours[currSelection];
        player.GetComponent<Battle>().SetBullet(currSelection);
    }

    public int GetSelection(){
        return currSelection;
    }
}
