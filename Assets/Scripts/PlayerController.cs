using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Jump desde suelo (click al mouse)")]
    public float jumpForce = 10f;

    [Header("Wall jump")]
    public float wallAngleThreshold = 50f;
    public float wallJumpHorizontalSpeed = 15f;
    public float wallJumpVerticalSpeed = 10f;
    public float wallCoyoteTime = 0.2f;
    public float detachImpulse = 1.5f;

    [Header("Fricción dinámica")]
    public Collider2D playerCollider;
    public PhysicsMaterial2D groundMaterial1;
    public PhysicsMaterial2D groundMaterial2;
    public PhysicsMaterial2D airMaterial;

    [Header("Respawn")]
    public Transform respawnPoint; // asigna en el inspector el punto de respawn

    private Rigidbody2D rb;
    private bool canGroundJump;
    private float wallTimer;
    private Vector2 lastWallNormal;
    private bool isTouchingWall;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = respawnPoint.position;
    }

    void Update()
    {
        wallTimer -= Time.deltaTime;

        if (isTouchingWall && Input.GetMouseButton(1))
        {
            SetGroundMaterial();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            if (rb.linearVelocity.y < -2f)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -2f);
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.None;
            if (!canGroundJump) SetAirMaterial();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (canGroundJump) JumpToMouse();
            else if (wallTimer > 0f) WallJump();
        }
    }

    void JumpToMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector2 dir = (mousePos - transform.position).normalized;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(dir * jumpForce, ForceMode2D.Impulse);

        canGroundJump = false;
        SetAirMaterial();
    }

    void WallJump()
    {
        Vector2 jumpVel = lastWallNormal.normalized * wallJumpHorizontalSpeed + Vector2.up * wallJumpVerticalSpeed;
        rb.linearVelocity = jumpVel;

        rb.AddForce(lastWallNormal.normalized * detachImpulse, ForceMode2D.Impulse);

        wallTimer = 0f;
        canGroundJump = false;
        SetAirMaterial();
        rb.constraints = RigidbodyConstraints2D.None;
    }

    void EvaluateCollision(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        float angle = Vector2.Angle(contact.normal, Vector2.up);

        if (angle <= wallAngleThreshold)
        {
            canGroundJump = true;
            wallTimer = 0f;
            isTouchingWall = false;
            SetGroundMaterial();
        }
        else
        {
            canGroundJump = false;
            isTouchingWall = true;
            lastWallNormal = contact.normal;
            wallTimer = wallCoyoteTime;
            SetAirMaterial();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("GroundType1")) EvaluateCollision(col);
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("GroundType1")) EvaluateCollision(col);
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("GroundType1"))
        {
            canGroundJump = false;
            isTouchingWall = false;
            wallTimer = Mathf.Max(wallTimer, wallCoyoteTime * 0.5f);
            SetAirMaterial();
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }

    // 🔹 Detecta entrada en trigger de respawn
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Respawn"))
        {
            Respawn();
        }
    }

    // 🔹 Teletransporta al jugador
    void Respawn()
    {
        if (respawnPoint != null)
        {
            rb.linearVelocity = Vector2.zero; // limpiar velocidad
            transform.position = respawnPoint.position;
        }
        else
        {
            Debug.LogWarning("No hay punto de respawn asignado en el inspector");
        }
    }

    void SetGroundMaterial()
    {
        if (playerCollider && groundMaterial1) playerCollider.sharedMaterial = groundMaterial1;
    }

    void SetAirMaterial()
    {
        if (playerCollider && airMaterial) playerCollider.sharedMaterial = airMaterial;
    }
}
