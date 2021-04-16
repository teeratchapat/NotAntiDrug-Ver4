using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // serializeField make private variable can edit by unity inspector
    public static PlayerMovement instance;

    private Animator anim;

    [Header("object variable")]
    public Rigidbody2D playerRigidbody2d;
    public SpriteRenderer playerSpriteRenderer;
    public Transform aim;
    public Transform aimUp;
    public Transform groundCheckpoint;
    public LayerMask whatIsGround;

    [Header("move variable")]
    [SerializeField] private bool isTurnRight = false;
    public float speed = 8f;
    public float moveHorizontal;

    [Header("aim variable")]
    public float aimVertical;
    public bool isAimDown;
    public bool isAimUp;

    [Header("jump variable")]
    public bool isGround = false;
    public bool isCanDoubleJump = false;
    public int jumpForce = 15;

    [Header("fire variable")]
    private float fireRate = 0.2f;
    private float nextFire = 0.0f;

    [Header("change variable")]
    public int currentBulletsID = 1;
    public int i;

    [Header("knockback variable")]
    public float knockbackTime = 0.5f;
    public float knockbackTimeCounter;
    [SerializeField] private float knockbackPwr = 7f;

    void Start()
    {
        instance = this;
        playerRigidbody2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        isGround = Physics2D.OverlapCircle(groundCheckpoint.position, 0.2f, whatIsGround);
        
        if (isGround)
        {
            anim.SetBool("isGrounded", true);
            isCanDoubleJump = true;
        }
        else
        {
            anim.SetBool("isGrounded", false);
        }

        if (FindObjectOfType<PauseGameScript>().isPause)
        {
            //do nothing
        }
        else
        {
            //jump
            if (Input.GetButtonDown("Jump") /*&& jumpcount < maxJump && nextJump < Time.time*/)
            {
                jump();
            }

            //shoot
            if (Input.GetButtonDown("Fire1") && nextFire < Time.time)
            {
                anim.SetTrigger("shootTrigger");
                shoot();
                
            }

            //change bullets
            if (Input.GetButtonDown("Fire2"))
            {
                changeBullets();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                knockback();
            }
        }
    }

    void FixedUpdate()
    {
        if (knockbackTimeCounter <= 0)
        {
            anim.SetBool("isHurt", false);
            //horizontal move
            moveHorizontal = Input.GetAxis("Horizontal");
            //vetacal aim
            aimVertical = Input.GetAxis("Vertical");

            playerRigidbody2d.velocity = new Vector2(moveHorizontal* speed, playerRigidbody2d.velocity.y) ;

            //facing handle
            if (moveHorizontal < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                isTurnRight = false;
                anim.SetBool("isRunning", true);
            }
            else if (moveHorizontal > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                isTurnRight = true;
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
        }
        else
        { 
            AudioManager.instance.PlaySfx(14);
            anim.SetBool("isHurt", true);
            knockbackTimeCounter -= Time.deltaTime;
            if (isTurnRight)
            {
                playerRigidbody2d.velocity = new Vector2(-knockbackPwr, playerRigidbody2d.velocity.y);
            }
            else
            {
                playerRigidbody2d.velocity = new Vector2(knockbackPwr, playerRigidbody2d.velocity.y);
            }
        }

        //
        if (aimVertical < 0)
        {
            isAimDown = true;
        }else if (aimVertical > 0)
        {
            isAimUp = true;
        }
        else
        {
            isAimUp = false;
            isAimDown = false;
        }
    }

    private void jump()
    {
        if (isGround)
        {
            AudioManager.instance.PlaySfx(7);
            playerRigidbody2d.velocity = new Vector2(playerRigidbody2d.velocity.x, jumpForce);
        }
        else
        {
            if (isCanDoubleJump)
            {
                AudioManager.instance.PlaySfx(7);
                playerRigidbody2d.velocity = new Vector2(playerRigidbody2d.velocity.x, jumpForce);
                isCanDoubleJump = false;
            }
        }
    }

    private void shoot()
    {
        i = currentBulletsID - 1;

        if (BulletsController.instance.bullets[i].currentBullets > 0)
        {
            BulletsController.instance.bullets[i].currentBullets--;

            GameObject bulletInstance = (GameObject)Instantiate(BulletsController.instance.bullets[i].bulletPrefab, aim.position, aim.rotation);

            if (isTurnRight)
            {
                if (isAimUp)
                {
                    bulletInstance.GetComponent<BulletBehavior>().throwDirection(new Vector2(1,1));
                }
                else if(isAimDown)
                {
                    bulletInstance.GetComponent<BulletBehavior>().throwDirection(new Vector2(1,-1));
                }
                else
                {
                    bulletInstance.GetComponent<BulletBehavior>().throwDirection(Vector2.right);
                }

            }
            else
            {
                if (isAimUp)
                {
                    bulletInstance.GetComponent<BulletBehavior>().throwDirection(new Vector2(-1, 1));
                }
                else if (isAimDown)
                {
                    bulletInstance.GetComponent<BulletBehavior>().throwDirection(new Vector2(-1, -1));
                }
                else
                {
                    bulletInstance.GetComponent<BulletBehavior>().throwDirection(Vector2.left);
                }
            }

            bulletInstance.GetComponent<BulletBehavior>().bulletDamage = BulletsController.instance.bullets[i].damagePower;

            nextFire = Time.time + fireRate;
        }
    }

    private void changeBullets()
    {
        currentBulletsID++;
        
        if (currentBulletsID > BulletsController.instance.bullets.Count)
        {
            currentBulletsID = 1;
        }
        Debug.Log("PlayerMovement change bullets to :" + currentBulletsID + " / " + BulletsController.instance.bullets.Count);
    }

    public void knockback()
    {
        knockbackTimeCounter = knockbackTime;
        playerRigidbody2d.velocity = new Vector2(0f, knockbackPwr);
    }
}

