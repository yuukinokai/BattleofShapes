using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{

    [SerializeField] private float change = 2.0f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().SetIce(true);
            Vector3 iceVector = collision.gameObject.transform.right * change;
            collision.gameObject.GetComponent<Movement>().ChangeMultiplier(change);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.GetComponent<Player>().IsOnIce()){
                collision.gameObject.GetComponent<Player>().SetIce(false);
                collision.gameObject.GetComponent<Movement>().ResetMultiplier();
            }
            
        }
    }
}
