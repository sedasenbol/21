using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float firstJumpForce;
    [SerializeField] protected float jumpSpecialtyForce;

    protected Rigidbody2D rb;
    protected BoxCollider2D boxCollider;
    protected CapsuleCollider2D capsuleCollider;

    protected bool canPerformJumpSpecialty = false;

    protected LastDirection lastDirection = LastDirection.right;

    protected enum LastDirection
    {
        right = 1,
        left = -1,
    }

    protected void MoveLeft() 
    {
        rb.velocity = new Vector2(-walkingSpeed, 0f);
        lastDirection = LastDirection.left;
    }

    protected void MoveRight() 
    {
        rb.velocity = new Vector2(walkingSpeed, 0f);
        lastDirection = LastDirection.right;
    }

    private void Jump() 
    {
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Layout")))
        {
            rb.AddForce(new Vector2(0f, firstJumpForce));
            canPerformJumpSpecialty = true;
        }
        else if (canPerformJumpSpecialty && !boxCollider.IsTouchingLayers(LayerMask.GetMask("Layout")))
        {
            PerformJumpSpecialty(lastDirection);
            canPerformJumpSpecialty = false;
        }
    }

    protected abstract void StopJumpSpecialty();

    protected abstract void PerformJumpSpecialty(LastDirection lastDirection);

    private void OnEnable()
    {
        UIManager.OnLeftButtonClicked += MoveLeft;
        UIManager.OnRightButtonClicked += MoveRight;
        UIManager.OnJumpButtonClicked += Jump;
    }

    private void OnDisable()
    {
        UIManager.OnLeftButtonClicked -= MoveLeft;
        UIManager.OnRightButtonClicked -= MoveRight;
        UIManager.OnJumpButtonClicked += Jump;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

}
