using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovement : MonoBehaviour
{
    private float top = 0f;
    private float right = 0f;
    private float left = 0f;
    private float bottom = 0f;

    [SerializeField]
    private float updateInterval = 0.5f;

    [SerializeField]
    private float movingDistance = 100f;

    private Vector3 destination;

    [Range(0.3f, 1.5f)]
    [SerializeField]
    private float moveDistanceRadius = 1f;
    void Start()
    {
        FindFlyBounds();
        StartCoroutine(MoveFly());
    }

    void Update()
    {
        CheckFlyBounds();
    }

    void FindFlyBounds()
    {
        top = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane)).y;
        right = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane)).x;
        left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x;
        bottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y;
    }

    void CheckFlyBounds()
    {
        // Top
        if (transform.position.y > top - 0.5f)
        {
            transform.position = new Vector3(transform.position.x, top - 0.5f, transform.position.z);
        }

        // Right
        if (transform.position.x > right - 0.5f)
        {
            transform.position = new Vector3(right - 0.5f, transform.position.y, transform.position.z);
        }

        // Left
        if (transform.position.x < left + 0.5f)
        {
            transform.position = new Vector3(left + 0.5f, transform.position.y, transform.position.z);
        }

        // Bottom
        if (transform.position.y < bottom + 0.5f)
        {
            transform.position = new Vector3(transform.position.x, bottom + 0.5f, transform.position.z);
        }
    }

    private IEnumerator MoveFly()
    {
        while (true)
        {
            Vector2 randomPos = Random.insideUnitCircle;
            destination = transform.position + new Vector3(randomPos.x, randomPos.y, transform.position.z);
            if(destination.x < left + 0.5f)
            {
                destination.x = left + Random.Range(0.5f, 3f);
            }

            if (destination.x > right - 0.5f)
            {
                destination.x = right - Random.Range(0.5f, 3f);
            }

            if (destination.y < bottom + 0.5f)
            {
                destination.y = bottom + Random.Range(0.5f, 3f);
            }

            if(destination.y > top - 0.5f)
            {
                destination.y = top - Random.Range(0.5f, 3f);
            }

            Debug.DrawLine(transform.position, destination, Color.red, 10f);

            yield return new WaitForSeconds(updateInterval);

            transform.position = destination;
        }
    }
}
