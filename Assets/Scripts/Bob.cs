using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : BaseCharacter
{
    protected override void PerformJumpSpecialty(LastDirection lastDirection)
    {
        base.rb.velocity = Vector2.zero;
        base.rb.AddForce(new Vector2(0, -base.jumpSpecialtyForce), ForceMode2D.Impulse);
    }

    protected override void StopJumpSpecialty()
    {
        rb.velocity = Vector2.zero;
    }

}
