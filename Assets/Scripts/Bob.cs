using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : BaseCharacter
{
    protected override void PerformJumpSpecialty(Direction lastDirection)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(jumpSpecialtyForce * Vector2.down, ForceMode2D.Impulse);
    }

    protected override void StopJumpSpecialty()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(rb.velocity.x, -firstJumpForce/4), ForceMode2D.Force);
        isPerformingJumpSpecialty = false;
    }
}
