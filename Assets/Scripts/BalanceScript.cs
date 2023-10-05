using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceScript : MonoBehaviour
{

    //////// Enemies

    // Small child
    public float smallChildSpeed;
    public int smallChildHealth;
    public int smallChildPointValue;
    public float[] smallChildSpawnRatesByRound = { 2, 1.8f, 1.6f, 1.4f, 1.2f, 1f };//new float[6];
    //

    // Teenager
    public float teenagerSpeed;
    public int teenagerHealth;
    public int teenagerPointValue;
    public float[] teenagerSpawnRatesByRound = { 6, 5.6f, 5.2f, 4.8f, 4.4f, 4f };// new float[6];
    //

    // Pillowsack Kid
    public float pillowsackSpeed;
    public int pillowsackHealth;
    public int pillowsackPointValue;
    public float[] pillowsackSpawnRatesByRound = { 10, 9f, 8f, 7f, 6f, 5f };//new float[6];
    //

    // Game Objects
    public GameObject SmallChild;
    public GameObject Teenager;
    public GameObject PillowsackKid;
    public GameObject CandyStation;
    public GameObject SlowStation;
    public GameObject SlowStationSlowBomb;
    public GameObject SlowStationUpgradedSlowBomb;
    public GameObject ScareStation;
    public GameObject ScareStationScareBomb;
    public GameObject ScareStationUpgradedScareBomb;
    //

    //////// Stations

    // Candy Station
    public float candyStationFireRate = 1;
    public int candyStationAmmo = 30;
    public int candyStationMaxAmmo = 30;
    public int candyStationDamage = 1;
    public int candyStationCost;
    public int candyStationCostToUpgrade;
    //

    // Candy Station Add-on Upgrade values (will add to the original for upgrade)
    public float candyStationAddedFireRate = 1;
    public int candyStationAddedMaxAmmo = 30;
    public int candyStationAddedDamage = 1;
    public Vector3 candyStationAddedArea = Vector3.one;
    //

    // Slow Station
    public float slowStationFireRate = .2f;
    public Vector2 slowStationSizeMax; // determines how large initial station AOE is (flat value)
    public int slowStationCost;
    public int slowStationCostToUpgrade;
    //

    // Slow Station Add-on Upgrade values (will add to the original for upgrade)
    public float slowStationAddedFireRate = .1f;
    public Vector2 slowStationUpgradeSizeMax; // determines how large upgrade AOE will be (not added... flat value)
    //

    // Scare Station
    public float scareStationFireRate = .1f;
    public Vector2 scareStationSizeMax; // determines how large initial station AOE is (flat value)
    public int scareStationCost;
    public int scareStationCostToUpgrade;
    //

    // Scare Station Add-on Upgrade values (will add to the original for upgrade)
    public float scareStationAddedFireRate = .1f;
    public Vector2 scareStationUpgradeSizeMax; // determines how large upgrade AOE will be (not added... flat value)
    //

    //////// Round
    public float totalRoundTime;
    public int startingPoints;


    // Start is called before the first frame update
    void Start()
    {
        // Set Spawn Array Values by kid type and the total round time
        GetComponent<EnemySpawns>().smallChildSpawnRates = smallChildSpawnRatesByRound;
        GetComponent<EnemySpawns>().teenagerSpawnRates = teenagerSpawnRatesByRound;
        GetComponent<EnemySpawns>().pillowsackSpawnRates = pillowsackSpawnRatesByRound;
        GetComponent<EnemySpawns>().totalRoundTime = totalRoundTime;
        //

        // Set Health and Speed Values by Kid Across the level
        SmallChild.GetComponent<EnemyScript>().enemyHealth = smallChildHealth;
        SmallChild.GetComponent<EnemyScript>().enemySpeed = smallChildSpeed;
        SmallChild.GetComponent<EnemyScript>().pointValue = smallChildPointValue;
        Teenager.GetComponent<EnemyScript>().enemyHealth = teenagerHealth;
        Teenager.GetComponent<EnemyScript>().enemySpeed = teenagerSpeed;
        Teenager.GetComponent<EnemyScript>().pointValue = teenagerPointValue;
        PillowsackKid.GetComponent<EnemyScript>().enemyHealth = pillowsackHealth;
        PillowsackKid.GetComponent<EnemyScript>().enemySpeed = pillowsackSpeed;
        PillowsackKid.GetComponent<EnemyScript>().pointValue = pillowsackPointValue;
        //

        // Set Candy Station Stats
        CandyStation.GetComponent<CandyStationScript>().fireRate = candyStationFireRate;
        CandyStation.GetComponent<CandyStationScript>().ammo = candyStationAmmo;
        CandyStation.GetComponent<CandyStationScript>().maxAmmo = candyStationMaxAmmo;
        CandyStation.GetComponent<CandyStationScript>().damage = candyStationDamage;
        CandyStation.GetComponent<CandyStationScript>().upgradeFireRate = candyStationAddedFireRate;
        CandyStation.GetComponent<CandyStationScript>().upgradeMaxAmmo = candyStationAddedMaxAmmo;
        CandyStation.GetComponent<CandyStationScript>().upgradeDamage = candyStationAddedDamage;
        CandyStation.GetComponent<CandyStationScript>().upgradeArea = candyStationAddedArea;
        CandyStation.GetComponent<StationUniversalProperties>().cost = candyStationCost;
        CandyStation.GetComponent<StationUniversalProperties>().costToUpgrade = candyStationCostToUpgrade;
        //

        // Set Slow Station Stats/prefabs for slow bomb visuals
        SlowStation.GetComponent<SlowStationScript>().fireRate = slowStationFireRate;
        SlowStation.GetComponent<SlowStationScript>().upgradeFireRate = slowStationAddedFireRate;
        SlowStation.GetComponent<SlowStationScript>().slowBomb = SlowStationSlowBomb;
        SlowStation.GetComponent<SlowStationScript>().upgradedSlowBomb = SlowStationUpgradedSlowBomb;
        SlowStationSlowBomb.GetComponent<SlowBombScript>().maxScale = slowStationSizeMax;
        SlowStationUpgradedSlowBomb.GetComponent<SlowBombScript>().maxScale = slowStationUpgradeSizeMax;
        SlowStation.GetComponent<StationUniversalProperties>().cost = slowStationCost;
        SlowStation.GetComponent<StationUniversalProperties>().costToUpgrade = slowStationCostToUpgrade;
        SlowStation.GetComponent<SlowStationScript>().setScale();
        //

        // Set Scare Station Stats/prefabs for scare bomb visuals
        ScareStation.GetComponent<ScareStationScript>().fireRate = scareStationFireRate;
        ScareStation.GetComponent<ScareStationScript>().upgradeFireRate = scareStationAddedFireRate;
        ScareStation.GetComponent<ScareStationScript>().scareBomb = ScareStationScareBomb;
        ScareStation.GetComponent<ScareStationScript>().upgradedScareBomb = ScareStationUpgradedScareBomb;
        ScareStationScareBomb.GetComponent<ScareBombScript>().maxScale = scareStationSizeMax;
        ScareStationUpgradedScareBomb.GetComponent<ScareBombScript>().maxScale = scareStationUpgradeSizeMax;
        ScareStation.GetComponent<StationUniversalProperties>().cost = scareStationCost;
        ScareStation.GetComponent<StationUniversalProperties>().costToUpgrade = scareStationCostToUpgrade;
        ScareStation.GetComponent<ScareStationScript>().setScale();
        //
    }
}
