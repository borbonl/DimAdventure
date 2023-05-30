using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float jumpForce = 6f;
    public float runningSpeed = 6f;

    public AudioClip laser;
    public AudioClip salto;
    public AudioClip heroeHerido;

    AudioSource audioSource;

    Rigidbody2D rigidBody;
    Animator animator;

    public Vector3 startPosition;

    Vector2 vecaux;

    [SerializeField]
    private int healthPoints, manaPoints;

    public const int INITIAL_HEALTH = 200, INITIAL_MANA = 15,
    MAX_HEALTH = 200, MAX_MANA = 30,
    MIN_HEALTH = 10, MIN_MANA = 0;

    public const int SUPERJUMP_COST = 5;
    public const float SUPERJUMP_FORCE = 1.5f;

    public float jumpRaycastDistance = 1.5f;

    float xeul = 0;

    public LayerMask groundMask;

    public Transform bullet;

    public float dist = 2f, dist2 = 0.2f; 

    Transform bullet2;

    public float tiempos = 0.5f;

    bool bshoot = true;

    bool fright = true;

    bool bandSalto = false;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


    void Start()
    {
        animator.SetBool("isOnTheGround", true);
    }

    
    IEnumerator DoCheck()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(5f);
        }
    }


    public void StartGame()
    {

        animator.SetBool("isAlive", true);
        animator.SetBool("isOnTheGround", true);

        animator.enabled = true;

        healthPoints = INITIAL_HEALTH;
        manaPoints = INITIAL_MANA;

        if (GetComponent<Rigidbody2D>().gravityScale < 0)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1f;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        Invoke("RestartPosition", 0.3f);
    }


    void RestartPosition() {
        this.transform.position = startPosition;
        this.rigidBody.velocity = Vector2.zero;
        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraFollow>().ResetCameraPosition();
    }


    void Update()
    {

        if (Input.GetButtonDown("Jump") && GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            Jump(false);
            animator.SetBool("isOnTheGround", false);
            animator.SetTrigger("salto");
        }

        if (bandSalto && IsTouchingTheGround())
        {
            bandSalto = false;
            animator.SetBool("isOnTheGround", true);
        }

        if (GetComponent<Rigidbody2D>().gravityScale > 0)
        {
            xeul = 0;
        }
        else
        {
            xeul = 180;
        }

        if (Input.GetKey(KeyCode.RightArrow) && GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
            this.transform.eulerAngles = new Vector3(xeul, 0, 0);
            fright = true;
            if (IsTouchingTheGround()) { 
                animator.SetBool("run", true);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            rigidBody.velocity = new Vector2(runningSpeed * -1, rigidBody.velocity.y);
            this.transform.eulerAngles = new Vector3(xeul, 180, 0);
            fright = false;
            if (IsTouchingTheGround()) { 
                animator.SetBool("run", true);
            }
        }
        else if (rigidBody.velocity.x == 0 && GameManager.sharedInstance.currentGameState == GameState.inGame && animator.GetBool("run"))
        {
            animator.SetBool("run", false);
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }


        if (Input.GetKey(KeyCode.D) && GameManager.sharedInstance.currentGameState == GameState.inGame && bshoot)
        {
            bshoot = false;
            StartCoroutine("esperashoot");
            audioSource.clip = laser;
            audioSource.Play();
            shoot();
        }

        UnityEngine.Debug.DrawRay(this.transform.position, Vector2.down * dist, Color.red);
    }

    void switchSalto()
    {
        bandSalto = true;
    }


    IEnumerator esperashoot()
    {
        yield return new WaitForSeconds(tiempos);
        bshoot = true;
    }

    void Jump(bool superjump)
    {

        float jumpForceFactor = jumpForce;

        if (GetComponent<Rigidbody2D>().gravityScale < 0)
        {
            jumpForceFactor = jumpForce * -1;
        } else
        {
            jumpForceFactor = jumpForce ;
        }

        if (IsTouchingTheGround())
        {
            audioSource.clip = salto;
            GetComponent<AudioSource>().Play();
            rigidBody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
        }
    }

    bool IsTouchingTheGround()
    {

        if (GetComponent<Rigidbody2D>().gravityScale > 0)
        {
             vecaux = Vector2.down;
        }
        else {
             vecaux = Vector2.up;
        }
        
        if (Physics2D.Raycast(this.transform.position, vecaux, dist, groundMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Die() {


        float travelledDistance = GetTravelledDistance();
        float previousMaxDistance = PlayerPrefs.GetFloat("maxscore", 0f);
        if (travelledDistance > previousMaxDistance)
        {
            PlayerPrefs.SetFloat("maxscore", travelledDistance);
        }

        GameManager.sharedInstance.collectedObject = 0;
        this.animator.SetBool("isAlive", false);
        GameManager.sharedInstance.GameOver();

        if (GameObject.Find("Enemy") != null)
        {
            Destroy(GameObject.Find("Enemy"));
        }

    }


    public void CollectHealth(int points)
    {
        this.healthPoints += points;
        if (this.healthPoints >= MAX_HEALTH)
        {
            this.healthPoints = MAX_HEALTH;
        }

        if (this.healthPoints <= 0)
        {
            Die();
        }


    }

    public void CollectMana(int points)
    {
        this.manaPoints += points;
        if (this.manaPoints >= MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
    }

    public int GetHealth()
    {
        return healthPoints;
    }

    public int GetMana()
    {
        return manaPoints;
    }

    public float GetTravelledDistance()
    {
        return this.transform.position.x - startPosition.x;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullete")
        {

            audioSource.clip = heroeHerido;
            audioSource.Play();

            animator.SetTrigger("herido");
            CollectHealth(-10);

            if (GetComponent<Rigidbody2D>().gravityScale > 0)
            {
                xeul = 0;
            }
            else {
                xeul = 180;
            }


            if (collision.transform.position.x < transform.position.x) { 
                this.transform.eulerAngles = new Vector3(xeul, 180, 0);
            }
            if (collision.transform.position.x > transform.position.x)
            {
                this.transform.eulerAngles = new Vector3(xeul, 0, 0);
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy2") {


            if (GetComponent<Rigidbody2D>().gravityScale > 0)
            {
                 xeul = 0;
            }
            else
            {
                 xeul = 180;
            }


            if (collision.transform.position.x < transform.position.x)
            {
                this.transform.eulerAngles = new Vector3(xeul, 180, 0);
            }
            if (collision.transform.position.x > transform.position.x)
            {
                this.transform.eulerAngles = new Vector3(xeul, 0, 0);
            }

            audioSource.clip = heroeHerido;
            audioSource.Play();

            animator.SetTrigger("herido");
            CollectHealth(-10);
        }
    }

    public void shoot() {

            animator.SetTrigger("shoot");
            if (fright)
            {
                Vector3 posBullet = new Vector3(transform.position.x + 1f, transform.position.y + dist2, transform.position.z);
                bullet2 = Instantiate(bullet, posBullet, transform.rotation);
                bullet2.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            }else { 
                Vector3 posBullet = new Vector3(transform.position.x - 1f, transform.position.y + dist2, transform.position.z);
                bullet2 = Instantiate(bullet, posBullet, transform.rotation);
                bullet2.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10, ForceMode2D.Impulse);
            }
        }

}
