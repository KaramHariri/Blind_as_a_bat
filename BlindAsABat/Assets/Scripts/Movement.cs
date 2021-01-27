using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    enum MovementType
    {
        CONTINOUS,
        ONPRESS,
        INTERVALED
    }

    [SerializeField]
    private MovementType movementType = MovementType.CONTINOUS;

    private bool isMoving = false;
    private bool isRotating = false;
    private Vector3 originPos, targetPos;
    [SerializeField]
    private float updateInterval = 0.2f;

    [SerializeField]
    private float movingDistance = 10f;

    private float horizontalInput = 0f;
    private float verticalInput = 0f;

    [SerializeField]
    private float rotationAmount = 50f;

    private float top = 0f;
    private float right = 0f;
    private float left = 0f;
    private float bottom = 0f;

    [SerializeField]
    private GameObject echoWave = null;

    private void Start()
    {
        FindPlayerBounds();
        StartCoroutine(Echo());
    }

    private void Update()
    {
        if(movementType != MovementType.ONPRESS)
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");
        }

        switch (movementType)
        {
            case MovementType.CONTINOUS:
                {
                    if (verticalInput != 0 && !isMoving)
                    {
                        StartCoroutine(MovePlayer(transform.up));
                    }
                    if(horizontalInput != 0 && !isRotating)
                    {
                        StartCoroutine(Rotate());
                    }
                }
                break;
        
            case MovementType.ONPRESS:
                {
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        verticalInput = 1f;
                        StartCoroutine(MovePlayer(transform.up));
                    }
                    else if (Input.GetKeyDown(KeyCode.A))
                    {
                        horizontalInput = -1f;
                        StartCoroutine(Rotate());
                    }
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        verticalInput = -1f;
                        StartCoroutine(MovePlayer(transform.up));
                    }
                    else if (Input.GetKeyDown(KeyCode.D))
                    {
                        horizontalInput = 1f;
                        StartCoroutine(Rotate());
                    }
                }
                break;
        
            case MovementType.INTERVALED:
                {
                    if ( verticalInput != 0  && !isMoving)
                    {
                        StartCoroutine(MovePlayer(transform.up));
                    }

                    if ( horizontalInput != 0 && !isRotating)
                    {
                        StartCoroutine(Rotate());
                    }
                }
                break;
        }

        CheckPlayerBounds();
    }

    IEnumerator Echo()
    {
        while (true)
        {
            yield return new WaitForSeconds(4.0f);
            CreateEchoWave();
            yield return new WaitForSeconds(updateInterval);
            CreateEchoWave();
            yield return new WaitForSeconds(updateInterval);
            CreateEchoWave();
            yield return new WaitForSeconds(updateInterval);
            CreateEchoWave();
        }
    }

    void CreateEchoWave()
    {
        GameObject gameObject = GameObject.Instantiate(echoWave, transform.position, transform.rotation);
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        originPos = transform.position;
        targetPos = originPos + (direction * movingDistance * verticalInput * Time.deltaTime);

        if(movementType == MovementType.INTERVALED)
        {
            yield return new WaitForSeconds(updateInterval);
        }

        transform.position = targetPos;

        isMoving = false;
    }

    private IEnumerator Rotate()
    {
        isRotating = true;

        if(movementType == MovementType.INTERVALED)
        {
            yield return new WaitForSeconds(updateInterval);
        }

        transform.Rotate(0f, 0f, rotationAmount * -horizontalInput);
        isRotating = false;
    }

    void FindPlayerBounds()
    {
        top = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane)).y;
        right = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane)).x;
        left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x;
        bottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).y;
    }

    void CheckPlayerBounds()
    {
        // Top
        if(transform.position.y > top - 0.5f)
        {
            transform.position = new Vector3(transform.position.x, top - 0.5f, transform.position.z);
        }

        // Right
        if(transform.position.x > right - 0.5f)
        {
            transform.position = new Vector3(right - 0.5f, transform.position.y, transform.position.z);
        }
        
        // Left
        if(transform.position.x < left + 0.5f)
        {
            transform.position = new Vector3(left + 0.5f, transform.position.y, transform.position.z);
        }

        // Bottom
        if(transform.position.y < bottom + 0.5f)
        {
            transform.position = new Vector3(transform.position.x, bottom + 0.5f, transform.position.z);
        }
    }
}
