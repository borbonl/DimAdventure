﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float runningSpeed = 1.5f;

    public int enemyDamage = 10;

    Rigidbody2D rigidBody;

    public bool facingRight = false;

    private Vector3 startPosition;

    private void Awake()
    {

        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = this.transform.position;
    }

    void Start () {
        this.transform.position = startPosition;
	}

    private void FixedUpdate()
    {
        float currentRunningSpeed = runningSpeed;

        if(facingRight){
            currentRunningSpeed = runningSpeed;
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        } else{
            currentRunningSpeed = -runningSpeed;
            this.transform.eulerAngles = Vector3.zero;
        }

            rigidBody.velocity = new Vector2(currentRunningSpeed, 
                                             rigidBody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Coin"){
            return;
        }

        if(collision.tag == "Player"){
            collision.gameObject.GetComponent<PlayerController>().
                     CollectHealth(-enemyDamage);
            return;
        }

        facingRight = !facingRight;
    }


}
