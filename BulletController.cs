using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    void Start()
    {
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(this.tag == "bulletc" && collision.tag != "Enemy" && collision.tag != "invert" && collision.gameObject.name != "Exit Zone") { 
         Destroy(gameObject);
        }

        if (this.tag == "bullete" && collision.tag == "Player" )
        {
            Destroy(gameObject);
        }
    }

}
