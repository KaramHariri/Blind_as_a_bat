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
    [SerializeField]
    private int hungerDamage = 1;
    [SerializeField]
    private int owlDamage = 50;

    [SerializeField]
    private float difficultyEasy = 30f;
    [SerializeField]
    private float difficultyMedium = 60f;
    [SerializeField]
    private float difficultyHard = 120f;

    SoundManager soundManager = null;

    public GameObject deathScreen = null;
    public TMP_Text highScoreText;

    private void Start()
    {
        deathScreen.SetActive(false);
        textScoreAmount.text = ":  " + score.ToString();
        textHealthAmount.text = ":  " + health.ToString();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void Update()
    {
        timePlayed += Time.deltaTime;
        timeSinceLastFed += Time.deltaTime;

        //Difficulty setting
        if (timePlayed > difficultyHard)
        {
            timeBeforeHunger = 5.0f;
            damageTickFrequency = 3.0f;
        }
        else if (timePlayed > difficultyMedium)
        {
            timeBeforeHunger = 10.0f;
            damageTickFrequency = 3.0f;
        }
        else if (timePlayed > difficultyEasy)
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
            deathScreen.SetActive(true);
            highScoreText.text = "YOU DIED ! \n HighScore : " + score.ToString();
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
