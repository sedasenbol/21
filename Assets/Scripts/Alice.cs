using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alice : BaseCharacter
{
    private float forwardDashTimer = 0f;
    [SerializeField] private float forwardDashDuration;

    private float lastVerticalSpeed;

    protected override void PerformJumpSpecialty(LastDirection lastDirection)
    {
        Physics2D.gravity = Vector2.zero;
        lastVerticalSpeed = rb.velocity.y;
        rb.velocity = Vector2.zero;
        forwardDashTimer += Time.deltaTime;
        base.rb.AddForce((int)lastDirection * new Vector2(base.jumpSpecialtyForce, 0f), ForceMode2D.Impulse);
    }

    private void CheckForwardDashTime()
    {
        if (forwardDashTimer == 0f) { return; }
        if (forwardDashTimer > forwardDashDuration)
        {
            rb.velocity = new Vector2(0f, lastVerticalSpeed);
            Physics2D.gravity = new Vector2(0f, -21f);
            forwardDashTimer = 0f;
        }
        else
        {
            forwardDashTimer += Time.deltaTime;
        }
    }

    protected override void StopJumpSpecialty()
    {
        rb.velocity = new Vector2(0f, lastVerticalSpeed);
        Physics2D.gravity = new Vector2(0f, -21f);
        forwardDashTimer = 0f;
    }

    private void Update()
    {
        CheckForwardDashTime();

        if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Layout")) || canPerformJumpSpecialty) { return; }

        StopJumpSpecialty();
    }
}
