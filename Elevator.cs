using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    public float velocidad = 1;

    Animator animator;

    Rigidbody2D barr;

    bool band = false, band2 = true;

    Vector2 vvelocidad;


    void Start()
    {
        barr = this.GetComponent<Rigidbody2D>();
        vvelocidad = new Vector2(0f, velocidad);

    }

    void Update()
    {
        if (band) { 

            barr.velocity = vvelocidad;
        }
        
        if (this.transform.position.y >= 10f && band)
        {
            barr.bodyType = RigidbodyType2D.Static;
            band = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && band2)
        {
            collision.GetComponent<Animator>().SetBool("run", false);
            barr.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            band = true;
            band2 = false;
        }

    }

}
