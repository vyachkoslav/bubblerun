using System;
using UnityEngine;

public class TrapButton : MonoBehaviour
{
    [SerializeField] private float yMovement;
    [SerializeField] private float xMovement;
    private float currentY;
    private float currentX;
    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;
    private Vector2 movementDir = Vector2.one;
    private Vector3 initPos;

    private void Start()
    {
        currentX = xMovement / 2;
        currentY = yMovement / 2;
        initPos = transform.position;
        initPos.x -= currentX;
        initPos.y -= currentY;
    }

    void Update()
    {
        currentX += movementDir.x * xSpeed * Time.deltaTime;
        currentY += movementDir.y * ySpeed * Time.deltaTime;
        if (currentX >= xMovement)
        {
            currentX = xMovement;
            movementDir.x = -1;
        }
        else if (currentX <= 0)
        {
            currentX = 0;
            movementDir.x = 1;
        }

        if (currentY >= yMovement)
        {
            currentY = yMovement;
            movementDir.y = -1;
        }
        else if (currentY <= 0)
        {
            currentY = 0;
            movementDir.y = 1;
        }
        transform.position = new Vector2(initPos.x + currentX, initPos.y + currentY);
    }
}
