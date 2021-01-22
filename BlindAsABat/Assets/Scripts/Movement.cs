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

        transform.Rotate(0f, 0f, rotationAmount * -horizontalInput * Time.deltaTime);
        isRotating = false;
    }
}
