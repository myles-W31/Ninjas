﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float ninjaStarCount;
    public float ninjaStarValue;
    public Text ninjaStarText;

    public float goldCoinCount;
    public float goldCoinValue;
    public Text goldCoinText;

    public Text towerHeightText;

    public GameObject pirateShip;
    public Transform leftPoint1;
    public Transform rightPoint1;
    private Transform spawnPosition;
    public SpriteRenderer theShipSprite;
    public Button newFloorButton;
    public GameObject towerSegment;
    public GameObject attackTowerSegment;
    public int towerHeight;
    bool attackTower = false;
    public int towerCost;

    public GoldBlockScript theGoldBlockScript;

    // Start is called before the first frame update
    void Start()
    {
        ninjaStarCount = 0;
        ninjaStarValue = 1;

        goldCoinCount = 0;
        goldCoinValue = 1;

        newFloorButton.gameObject.SetActive(false);
        theGoldBlockScript = FindObjectOfType<GoldBlockScript>();
        towerHeight = 0;
        towerCost = 0;

        StartCoroutine(NinjaStarTime());
        StartCoroutine(PirateTime());
    }

    // Update is called once per frame
    void Update()
    {
        ninjaStarText.text = "Ninja Stars: " + ninjaStarCount;
        goldCoinText.text = "Gold Coins: " + goldCoinCount;
        ninjaStarValue = towerHeight * 0.5f;
        Debug.Log(ninjaStarValue);

        if(ninjaStarCount >= 10+towerCost)
        {
            newFloorButton.gameObject.SetActive(true);
        }
        else
        {
            newFloorButton.gameObject.SetActive(false);
        }

        //newFloorButton.onClick.AddListener(TaskOnClick);
        towerCost = 5 * towerHeight;
    }

    IEnumerator NinjaStarTime()
    {
        while (true)
        {
            ninjaStarCount = ninjaStarCount + ninjaStarValue + 1;
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator PirateTime()
    {
        while (true)
        {
            ChooseSpawn();
            GameObject cloneShip = (GameObject)Instantiate(pirateShip, new Vector3(spawnPosition.position.x, spawnPosition.position.y, 0f), Quaternion.Euler(new Vector3(0, 0, 0)));
            yield return new WaitForSeconds(5);
        }
    }

    void ChooseSpawn()
    {
        int spawnNum = Random.Range(1, 3);

        if(spawnNum == 1)
        {
            spawnPosition = leftPoint1;
        }
        else
        {
            spawnPosition = rightPoint1;
        }
    }

    public void FlashRed(SpriteRenderer objectToHurt)
    {
        objectToHurt.color = Color.red;

        StartCoroutine(ExecuteAfterTime(objectToHurt));
    }

    public void ResetColor(SpriteRenderer objectToHurt)
    {
        if (objectToHurt != null)
        {
            objectToHurt.color = theShipSprite.color;
        }
    }

    IEnumerator ExecuteAfterTime(SpriteRenderer objectToAffect)
    {
        yield return new WaitForSeconds(0.25f);
        ResetColor(objectToAffect);
    }

    public void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
        if (attackTower)
        {
            Instantiate(attackTowerSegment, new Vector3(theGoldBlockScript.goldBlockPosition.position.x, towerHeight, 0f), Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        else
        {
            Instantiate(towerSegment, new Vector3(theGoldBlockScript.goldBlockPosition.position.x, towerHeight, 0f), Quaternion.Euler(new Vector3(0, 0, 0)));
        }
        towerHeight += 1;
        ninjaStarCount -= 10+towerCost;
    }

    //sets bool to true/false
    public void towerSwitch()
    {
        attackTower = !attackTower;
    }
}
