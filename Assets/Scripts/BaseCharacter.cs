using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] protected float firstJumpForce;
    [SerializeField] protected float jumpSpecialtyForce;

    private Transform xform;
    protected Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private Vector2 initialScale;

    private const float RAY_DISTANCE = 0.05f;
    private Vector2 raySize;
    private LayerMask platformLayerMask;

    private bool canPerformJumpSpecialty = false;
    protected bool isPerformingJumpSpecialty = false;

    protected Direction lastDirection = Direction.right;

    public enum Direction
    {
        right = 1,
        left = -1,
    }

    protected void SetForwardSpeed(Direction direction) 
    {
        rb.velocity = new Vector2((int)direction * forwardSpeed, rb.velocity.y);
        lastDirection = direction;
        FlipSprite();
    }

    private void FlipSprite()
    {
        xform.localScale = new Vector2((int)lastDirection * initialScale.x, initialScale.y);
    }

    private void Jump() 
    {
        if (CheckGroundCollision())
        {
            rb.AddForce(new Vector2(0f, firstJumpForce));
            canPerformJumpSpecialty = true;
        }
        else if (canPerformJumpSpecialty && !CheckGroundCollision() && !CheckSideCollision())
        {
            PerformJumpSpecialty(lastDirection);
            isPerformingJumpSpecialty = true;
            canPerformJumpSpecialty = false;
        }
    }

    protected bool CheckGroundCollision()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, raySize, 0f, Vector2.down, RAY_DISTANCE, platformLayerMask);

        if (hit.collider)
        {
            isPerformingJumpSpecialty = false;
            canPerformJumpSpecialty = false;
        }
        return hit.collider;
    }

    protected abstract void PerformJumpSpecialty(Direction lastDirection);

    protected abstract void StopJumpSpecialty();

    private void OnEnable()
    {
        UIManager.OnLeftOrRightButtonClicked += SetForwardSpeed;
        UIManager.OnJumpButtonClicked += Jump;
    }

    private void OnDisable()
    {
        UIManager.OnLeftOrRightButtonClicked -= SetForwardSpeed;
        UIManager.OnJumpButtonClicked += Jump;
    }

    private void Start()
    {
        xform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        initialScale = xform.localScale;
        raySize = boxCollider.bounds.size;
        platformLayerMask = LayerMask.GetMask("Layout");
    }

    private bool CheckSideCollision()
    {
        RaycastHit2D hitRight = Physics2D.BoxCast(boxCollider.bounds.center, raySize, 0f, Vector2.right, RAY_DISTANCE, platformLayerMask);
        RaycastHit2D hitLeft = Physics2D.BoxCast(boxCollider.bounds.center, raySize, 0f, Vector2.left, RAY_DISTANCE, platformLayerMask);

        return hitRight.collider || hitLeft.collider;
    }

    protected virtual void Update()
    {
        if (!isPerformingJumpSpecialty) { return; }

        CheckGroundCollision();

        if (!CheckSideCollision()) { return; }

        StopJumpSpecialty();
    }
}
