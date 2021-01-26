using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BatScore : MonoBehaviour
{
    public TMP_Text scoreAmount;
    private int score = 0;
    public AnimalSpawner spawner;

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if(collision.gameObject.CompareTag("Insect"))
        {
            score += 10;
            scoreAmount.text = score.ToString();

            spawner.FlyEaten();
            Destroy(collision.gameObject);
        }
    }

}
