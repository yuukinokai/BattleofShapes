using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{

    private float speed = 10f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 iceVector = collision.gameObject.transform.right * speed;
            collision.gameObject.GetComponent<Movement>().SetControl(false);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = iceVector;
            //Debug.Log((iceVector).ToString());
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Movement>().SetControl(true);
        }
    }
}
