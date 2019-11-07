using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOnTouch : MonoBehaviour
{
    protected void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Player")
        {
            SceneController.GetController().WinLevel();
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        } 
    }
}
