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
    public soundManager soundManager;

    [Header("Movement Details")]
    public float speed;
    public float dashSpeed;

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

    public bool isGrounded = true;
    private bool isDead = false;

    public float jumpcooldown;

    private int layermask;
    private int mapamount = 0;

    [Header("Other details")]
    public GameObject GainedMapPiece;
    public TMP_Text MapAmount;
    public bool IsMapping = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GlobalLevelManager.InitPlayer = transform.position;
        layermask = ~(1 << LayerMask.NameToLayer("Player"));
        GlobalLevelManager.collectedchests = 0;
        GlobalLevelManager.collectedcoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.H) && Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.T))
        {
            GlobalLevelManager.LoadLevel(GlobalLevelManager.currentlevel + 1);
        }
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
                wallBounce.bounces = wallBounce.maxbounces;
                dashAvailable = true;
                wallBounce.dashAmount = 0;
            }
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
        yield return new WaitForSecondsRealtime(1f);
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

        if (hor != 0) { rb.AddForce(new Vector2(hor, 0), ForceMode2D.Impulse); soundManager.playWalk(); }
        if (rb.linearVelocityX > 0) facingLeft = false;
        else if (rb.linearVelocityX < 0) facingLeft = true;
        if (Math.Abs(rb.linearVelocity.x) < 0.1f)
        {
            facingLeft = hor < 0;
        }

        if (Math.Abs(hor) > 0.1f)
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
        soundManager.playDeath();

        isDead = true;
        yield return new WaitForSecondsRealtime(1.5f);
        GlobalLevelManager.ReloadLevel();
    }

    // Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * isgrounddistance);
    }
}
