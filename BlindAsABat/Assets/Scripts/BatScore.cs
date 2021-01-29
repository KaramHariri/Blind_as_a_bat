using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BatScore : MonoBehaviour
{
    public TMP_Text textScoreAmount;
    private int score = 0;
    public AnimalSpawner spawner;
    public TMP_Text textHealthAmount;


    private int health = 100;
    private int healthRegainedByFly = 20;

    private float timePlayed = 0.0f;
    private float timeBeforeHunger = 15.0f;
    private float timeSinceLastFed = 0.0f;

    private float damageTickFrequency = 3.0f;
    private float timeSinceLastDamage = 0.0f;
    private int hungerDamage = 1;
    private int owlDamage = 50;

    SoundManager soundManager = null;

    private void Start()
    {
        textScoreAmount.text = ":  " + score.ToString();
        textHealthAmount.text = ":  " + health.ToString();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Update()
    {
        timePlayed += Time.deltaTime;
        timeSinceLastFed += Time.deltaTime;

        //Difficulty setting
        if (timePlayed > 200f)
        {
            timeBeforeHunger = 5.0f;
            damageTickFrequency = 3.0f;
        }
        else if (timePlayed > 100.0f)
        {
            timeBeforeHunger = 10.0f;
            damageTickFrequency = 3.0f;
        }
        else if (timePlayed > 50.0f)
        {
            timeBeforeHunger = 13.5f;
            damageTickFrequency = 3.0f;
        }
        else
        {
            timeBeforeHunger = 15.0f;
            damageTickFrequency = 3.0f;
        }

        if (timeSinceLastFed > timeBeforeHunger)
        {
            timeSinceLastDamage += Time.deltaTime;

            if (timeSinceLastDamage > damageTickFrequency)
            {
                timeSinceLastDamage = 0.0f;

                health -= hungerDamage;
            }
        }

        if (health < 0)
        {
            health = 0;
            Debug.Log("Death :(");
        }
        textHealthAmount.text = ":  " + health.ToString();
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if(collision.gameObject.CompareTag("Insect"))
        {
            score += 10;
            textScoreAmount.text = ":  " + score.ToString();

            health += healthRegainedByFly;
            if (health > 100)
                health = 100;

            textHealthAmount.text = ":  " + health.ToString();
            timeSinceLastFed = 0f;

            spawner.FlyEaten();
            soundManager.PlaySound("Eat");
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Owl"))
        {
            health -= owlDamage;
        }
    }

}
