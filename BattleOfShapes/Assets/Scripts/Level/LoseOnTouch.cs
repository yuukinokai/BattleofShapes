using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseOnTouch : MonoBehaviour
{
    bool isTouching = false;
    GameObject player;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player != null && !player.GetComponent<Player>().IsOnBridge()){
            if(isTouching){
                SceneController.GetController().FailedLevel();
                player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            isTouching = true;
        } 
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.tag == "Player")
        {
            isTouching = false;
        } 
    }
}
