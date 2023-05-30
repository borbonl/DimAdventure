using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public AudioClip destroy;
    public AudioClip hurt;

    AudioSource audioSource;

    bool band = true;

    int contDanio = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "bulletc")
        {
            audioSource.clip = hurt;
            audioSource.Play();
            transform.parent.gameObject.GetComponent<Animator>().SetTrigger("eh2");
            contDanio++;
        }

        if (contDanio == 5)
        {
            audioSource.clip = destroy;
            audioSource.Play();
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bulletc")
        {
            audioSource.clip = hurt;
            audioSource.Play();
            transform.parent.gameObject.GetComponent<Animator>().SetTrigger("eh2");
            contDanio++;
        }

        if (contDanio == 5 && band)
        {
            band = false;
            audioSource.clip = destroy;
            audioSource.Play();
            StartCoroutine("DoCheck");
        }
    }

    IEnumerator DoCheck()
    {
            yield return new WaitForSeconds(2f);
            Destroy(transform.parent.gameObject);

    }
}
