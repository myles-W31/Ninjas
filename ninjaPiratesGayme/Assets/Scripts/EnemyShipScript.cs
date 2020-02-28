﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipScript : MonoBehaviour
{
    public float moveSpeed;
    public float speed;

    public float enemyHealth;
    private float maxEnemyHealth;
    public GameObject goldBlock;

    // rigidbody and player objects
    public Rigidbody2D theShipRB;
    public GameObject theShip;
    public LevelManager myLevelManager;
    public GameObject goldCoin;

    // Start is called before the first frame update
    void Start()
    {
        theShipRB = GetComponent<Rigidbody2D>();
        maxEnemyHealth = 2;
        enemyHealth = maxEnemyHealth;
        moveSpeed = 1;
        goldBlock = GameObject.FindGameObjectWithTag("GoldBlock");
        myLevelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (theShip.transform.position.x > goldBlock.transform.position.x)
        {
            speed = -moveSpeed;
            theShipRB.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            speed = moveSpeed;
            theShipRB.transform.localScale = new Vector3(1, 1, 1);
        }

        if (enemyHealth <= 0)
        {
            Destroy(theShip);
            myLevelManager.goldCoinCount += 1;
            Instantiate(goldCoin, theShipRB.transform.position, theShip.transform.rotation);
        }

        theShipRB.velocity = new Vector3(speed, 0f, 0f);
    }

    public void HurtEnemyMethod(EnemyShipScript objectToHurt, float damageToTake)
    {
        objectToHurt.enemyHealth -= damageToTake;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Tower")
        {
            myLevelManager.ninjaStarCount = 0;

            Destroy(gameObject);
        }
    }
}
