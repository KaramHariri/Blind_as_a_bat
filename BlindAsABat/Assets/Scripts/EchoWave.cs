using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoWave : MonoBehaviour
{
    private Vector3 originPos = Vector3.zero;
    private Vector3 targetPos = Vector3.zero;
    private Vector3 dir = Vector3.zero;

    private bool isMoving = false;

    [SerializeField]
    private float updateInterval = 0.5f;

    [SerializeField]
    private float movingDistance = 25f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
    }

    void Update()
    {
        
    }

    private IEnumerator Move()
    {
        while (true)
        {
            isMoving = true;

            originPos = transform.position;
            targetPos = originPos + (transform.up * movingDistance * Time.deltaTime);

            yield return new WaitForSeconds(updateInterval);

            transform.position = targetPos;

            isMoving = false;
        }
    }
}
