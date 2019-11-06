using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{

    [SerializeField] private float change = 10f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 iceVector = collision.gameObject.transform.right * change;
            //collision.gameObject.GetComponent<Movement>().SetControl(false);
            //collision.gameObject.GetComponent<Rigidbody2D>().velocity = iceVector;
            //Debug.Log((iceVector).ToString());
            collision.gameObject.GetComponent<Movement>().ChangeMultiplier(change);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //collision.gameObject.GetComponent<Movement>().SetControl(true);
            collision.gameObject.GetComponent<Movement>().ChangeMultiplier(-change);
        }
    }
}
