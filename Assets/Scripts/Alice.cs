using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alice : BaseCharacter
{
    [SerializeField] private float jumpSpecialtyDuration;

    private float jumpSpecialtyTimer;
    private float lastVerticalSpeed;

    protected override void PerformJumpSpecialty(Direction lastDirection)
    {
        Physics2D.gravity = Vector2.zero;
        lastVerticalSpeed = rb.velocity.y;
        rb.velocity = Vector2.zero;
        jumpSpecialtyTimer += Time.deltaTime;
        rb.AddForce((int)lastDirection * jumpSpecialtyForce * Vector2.right, ForceMode2D.Impulse);
    }

    protected override void StopJumpSpecialty()
    {
        rb.velocity = new Vector2(0f, -Mathf.Abs(lastVerticalSpeed));
        Physics2D.gravity = new Vector2(0f, -21f);
        jumpSpecialtyTimer = 0f;
        isPerformingJumpSpecialty = false;
    }

    private void CheckJumpSpecialtyTime()
    {
        if (jumpSpecialtyTimer > jumpSpecialtyDuration)
        {
            StopJumpSpecialty();
        }
        else
        {
            jumpSpecialtyTimer += Time.deltaTime;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (jumpSpecialtyTimer == 0f) { return; }

        CheckJumpSpecialtyTime();
    }
}
