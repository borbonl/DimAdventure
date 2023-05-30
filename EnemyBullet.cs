using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    GameObject player;

    public Transform bullet;

    public float tiempo = 5f;

    Transform bullet2;

    public float Fuerza = 0.5f;

    bool b1 = true;

    int contDanio = 0;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (player.transform.position.x < this.transform.position.x)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (b1  && collision.tag == "Player") {
            b1 = false;
            StartCoroutine("DoCheck");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            b1 = true;
        }
    }

    IEnumerator DoCheck()
    {
        while (!b1)
        {
            yield return new WaitForSeconds(tiempo);
            shoot();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "bulletc")
        {
            GetComponent<Animator>().SetTrigger("eh2");
            contDanio++;
        }

        if (contDanio == 3)
        {
            Destroy(gameObject);
        }

    }

    public void shoot()
    {
        Vector3 posBullet = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        bullet2 = Instantiate(bullet, posBullet, transform.rotation);
        Vector2 Direction = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        bullet2.GetComponent<Rigidbody2D>().AddForce(Direction*Fuerza, ForceMode2D.Impulse);
    }
}
