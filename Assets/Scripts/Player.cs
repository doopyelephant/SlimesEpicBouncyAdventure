using System;
using System.Collections;
using DefaultNamespace;
using NUnit.Framework;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    // Components
    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public WallBounce wallBounce;
    public GameObject respawnPoint;

    [Header("Movement Details")]
    public float speed;
    public float dashSpeed;
    public bool isDashing = false;

    public float maxspeedx;
    public float maxspeedy;

    public bool facingLeft;

    public float jump;
    public bool dashAvailable = true;

    [Header("Jump Details")]
    public float isgrounddistance;
    public float secondssinceground;
    private float timeonground = 0f;
    private float sincejump = 0f;

    private bool isGrounded = true;
    private bool isDead = false;
    public bool isGrounded = true;

    public float jumpcooldown;

    private int layermask;
    private int mapamount = 0;

    [Header("Other details")]
    public int coinAmount = 0;
    public int chestAmount = 0;
    public GameObject GainedMapPiece;
    public TMP_Text MapAmount;
    public bool IsMapping = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GlobalLevelManager.InitPlayer = transform.position;
        layermask = ~(1 << LayerMask.NameToLayer("Player"));
    }

    // Update is called once per frame
    void Update()
    {
        MapAmount.gameObject.SetActive(IsMapping);
        spriteRenderer.flipX = facingLeft;

        if (!isDead)
        {
            HandleAnimation();
            HandleMovement();
            HandleDash();
            HandleJump();

            if (isGrounded)
            {
                dashAvailable = true;
                wallBounce.dashAmount = 0;
            }
            wallBounce.bounces = wallBounce.maxbounces;
            dashAvailable = true;
            wallBounce.dashAmount = 0;
        }
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.GetComponent(typeof(MapPiece)) != null && IsMapping)
        {

            GainedMapPiece.SetActive(true);
            StartCoroutine(FalseMapPiece());
            Destroy(other.gameObject);
            mapamount += 1;
            MapAmount.text = mapamount.ToString() + "/4";

            Time.timeScale = 0f;
        }
    }

    public IEnumerator FalseMapPiece()
    {
        yield return new WaitForSecondsRealtime(2f);
        GainedMapPiece.SetActive(false);
        Time.timeScale = 1f;
        if (mapamount >= 4)
        {
            GlobalLevelManager.LoadLevel(1);
        }
    }


    private void HandleMovement()
    {
        float hor = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;

        if (hor != 0) rb.AddForce(new Vector2(hor, 0), ForceMode2D.Impulse);
        if (rb.linearVelocityX > 0) facingLeft = false;
        else if (rb.linearVelocityX < 0) facingLeft = true;
        if (Math.Abs(rb.linearVelocity.x) < 0.1f)
        {
            facingLeft = hor < 0;
        }
    }

    private void HandleAnimation()
    {
        animator.SetFloat("Xvelocity", rb.linearVelocity.x);
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetBool("IsGrounded", isGrounded);
    }

    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && dashAvailable && wallBounce.dashAmount < wallBounce.maxDashAmount)
        {
            if (facingLeft)
            {
                rb.AddForce(new Vector2(-dashSpeed, 0), ForceMode2D.Impulse);
            }
            else if (facingLeft == false)
            {
                rb.AddForce(new Vector2(dashSpeed, 0), ForceMode2D.Impulse);
            }
            wallBounce.dashAmount ++;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded == false && dashAvailable && wallBounce.dashAmount < wallBounce.maxDashAmount)
        {   
            dashAvailable = false;
            if (facingLeft)
            {
                Debug.Log("dash inputted");
                rb.AddForce(new Vector2(-dashSpeed, 0), ForceMode2D.Impulse);
            }
            else if (facingLeft == false)
            {
                Debug.Log("dash inputted");
                rb.AddForce(new Vector2(dashSpeed, 0), ForceMode2D.Impulse);
            }
            wallBounce.dashAmount ++;
        }
    }

    private void HandleJump()
    {
        timeonground += Time.deltaTime;
        sincejump += Time.deltaTime;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, isgrounddistance, layermask);
        isGrounded = hit;

        if (hit.collider != null)
        {
            timeonground = 0f;
            Debug.DrawLine(transform.position, hit.point, Color.red);
            //Debug.Log(Vector3.Distance(transform.position, hit.point));
        }

        if (Input.GetButtonDown("Jump") && timeonground < secondssinceground && sincejump > jumpcooldown)
        {
            sincejump = 0f;
            rb.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);
        }
        rb.linearVelocity = new Vector2(Math.Clamp(rb.linearVelocityX, -maxspeedx, maxspeedx), Math.Clamp(rb.linearVelocityY, -maxspeedy, maxspeedy));
    }
    public IEnumerator respawnPlayer()
    {
        animator.GetComponent<Animator>().SetTrigger("Death");
        isDead = true;
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * isgrounddistance);
    }
}
