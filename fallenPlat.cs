using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallenPlat : MonoBehaviour
{

    Rigidbody2D rb1;
    public float tiempo = 0.5f;
    void Start()
    {
        rb1 = GetComponent<Rigidbody2D>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            StartCoroutine("DoCheck");
        }
    }

    IEnumerator DoCheck()
    {
        yield return new WaitForSeconds(tiempo);
        rb1.bodyType = RigidbodyType2D.Dynamic;
    }
}
