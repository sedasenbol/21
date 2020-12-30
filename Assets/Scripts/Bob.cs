using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : BaseCharacter
{
    private float lastVerticalSpeed;

    protected override void PerformJumpSpecialty(LastDirection lastDirection)
    {
        lastVerticalSpeed = rb.velocity.y;
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, -base.jumpSpecialtyForce), ForceMode2D.Impulse);
    }

    protected override void StopJumpSpecialty()
    {
        rb.velocity = new Vector2(rb.velocity.x, lastVerticalSpeed);
    }

}
