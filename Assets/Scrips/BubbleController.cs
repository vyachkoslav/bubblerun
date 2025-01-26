using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RotateSprite))]
public class BubbleController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float maxSpeed = 100;
    [SerializeField] private float acceleration = 10;
    private Vector2 controls = Vector2.zero;
    private RotateSprite spriteScript;

    public UnityEvent OnDestroy = new();

    private void Awake()
    {
        spriteScript = GetComponent<RotateSprite>();
    }

    private void Update()
    {
        var multiplier = Vector2.Dot(controls, body.linearVelocity) < 0 ? 3 : 1;
        body.linearVelocity += controls * (acceleration * multiplier * Time.deltaTime);
        var magnitude = body.linearVelocity.magnitude;
        if (magnitude > maxSpeed)
        {
            body.linearVelocity = body.linearVelocity.normalized * maxSpeed;
        }
    }

    public void DestroyBubble()
    {
        OnDestroy.Invoke();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        controls = ctx.ReadValue<Vector2>();
        spriteScript.UpdateTransformAndSprite(controls);
    }
}
