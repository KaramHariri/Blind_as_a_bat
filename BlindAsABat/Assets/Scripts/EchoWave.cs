using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoWave : MonoBehaviour
{
    private Vector3 originPos = Vector3.zero;
    private Vector3 targetPos = Vector3.zero;

    [SerializeField]
    private float updateInterval = 0.5f;

    [SerializeField]
    private float movingDistance = 25f;

    private float scaleModifier = 0.5f;
    private SpriteRenderer spriteRenderer = null;


    private void Awake()
    {
        transform.localScale = Vector3.zero;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
            originPos = transform.position;
            targetPos = originPos + (transform.up * movingDistance * Time.deltaTime);
            AnimateEcho();
            yield return new WaitForSeconds(updateInterval);

            transform.position = targetPos;

        }
    }

    private void AnimateEcho()
    {   
        transform.localScale += new Vector3(scaleModifier * Time.deltaTime, scaleModifier * Time.deltaTime, scaleModifier * Time.deltaTime);
        if (transform.localScale.x > 1)
        {
            Color alpha = spriteRenderer.color;
            alpha.a -= scaleModifier * Time.deltaTime;
            spriteRenderer.color = alpha;
        }
    }
}
