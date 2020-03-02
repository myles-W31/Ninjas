﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseScript : MonoBehaviour
{
    public GameObject theCannon;
    public Rigidbody2D theCannonRB;
    public float cannonTimer;
    public LevelManager myLevelManager;
    public bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        cannonTimer = 0;
        myLevelManager = FindObjectOfType<LevelManager>();
        canShoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(cannonTimer < 2)
        {
            canShoot = true;
        }
        else if(cannonTimer > 2 && canShoot)//myLevelManager.ninjaStarCount > 0)
        {
            GameObject projectile = Instantiate(myLevelManager.ninjaStarToUse, new Vector3(this.transform.position.x-2, this.transform.position.y, this.transform.position.z), this.transform.rotation);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-10f, 0);
            //Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            //Vector2 myPos = new Vector2(theCannon.transform.position.x, theCannon.transform.position.y);
            //Vector2 direction = target - myPos;
            //direction.Normalize();
            //Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            //GameObject projectile = (GameObject)Instantiate(myLevelManager.ninjaStarToUse, myPos, this.transform.rotation);
            //projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 0);
            //myLevelManager.ninjaStarCount -= 1;
            Debug.Log("fire");
            canShoot = false;
        }
        else
        {
            cannonTimer = 0;
        }

        cannonTimer += Time.deltaTime;

        
    }
}