using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invert : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<Rigidbody2D>().gravityScale > 0) { 
                collision.GetComponent<Rigidbody2D>().gravityScale = -1f;
                collision.transform.eulerAngles = new Vector3(180, 0, 0);
            }
            else { 
                collision.GetComponent<Rigidbody2D>().gravityScale =  1f;
                collision.transform.eulerAngles = new Vector3(0, 0, 0);

            }
        }
    }
}
