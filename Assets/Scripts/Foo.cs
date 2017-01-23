using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Foo : MonoBehaviour {

    public float speed = 0.6f;
    public float jumpSpeed = 6f;
    public Rigidbody2D rb;
    public GameObject feet;
    public LayerMask isJumpable;
    public Vector2 maxVelocity;
    public Animator animator;
    public Score score;
    public float slidingLength = 5000.0f;
    public int life = 100;
    public Text lifeText;

    private bool isJumping = false;
    private bool isDead = false;
    private bool isSliding = false;
    private float slidingSince = 0.0f;

    // Use this for initialization
    void Start() {
        UpdateLifeText();
    }

    // Update is called once per frame
    void Update() {
        if (isDead)
        {
            SceneManager.LoadScene("Death");
        }
        UpdateSlidingState();
        UpdateKeyEvent();
        UpdateVelocityCapping();
        UpdateAnimation();
    }

    void UpdateSlidingState()
    {
        if (slidingSince < Time.fixedTime + slidingLength)
        {
            isSliding = false;
        }
    }


    void UpdateKeyEvent()
    {
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
            && isJumping == false)
        {
            if (Physics2D.OverlapCircle(feet.transform.position, 0.22f, isJumpable))
            {
                rb.velocity += new Vector2(0, jumpSpeed);
            }
            isJumping = true;
        }
        if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.DownArrow))
            && isSliding == false)
        {
            isSliding = true;
            slidingSince = Time.fixedTime;
        }
    }

    void UpdateVelocityCapping() {
        if (rb.velocity.y > maxVelocity.y) {
            rb.velocity = new Vector2(rb.velocity.x, maxVelocity.y);
        }
        else if (rb.velocity.y < -maxVelocity.y) {
            rb.velocity = new Vector2(rb.velocity.x, -maxVelocity.y);
        }
        else if (rb.velocity.y == 0) {
            isJumping = false;
        }

        if (rb.velocity.x > maxVelocity.x)
        {
            rb.velocity = new Vector2(maxVelocity.x, rb.velocity.y);
        }
    }

    void UpdateAnimation() {
        animator.SetFloat("Jump", Mathf.Abs(isJumping ? 1.0f : 0.0f));
        animator.SetFloat("Walk", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("Slide", Mathf.Abs(isSliding ? 1.0f : 0.0f));
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Enemy") {
            if (coll.contacts[0].normal.y > 0.65f) {
                KillEnemy(coll);
                score.UpdateScore(100);
                rb.velocity = new Vector2(0, jumpSpeed);
            }
            else if (isSliding == false) {
                LooseLife(1.0f - coll.contacts[0].normal.y);
            }
        }
        if (coll.gameObject.tag == "Level")
        {
            if (coll.contacts[0].normal.x < -0.95)
            {
                LooseLife(1f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.tag == "Coin") {
            Destroy(coll.gameObject);
            score.UpdateScore(50);
        }
    }

    void KillEnemy(Collision2D coll) {
        coll.gameObject.GetComponent<EnemyControler>().OnDeath();
        Destroy(coll.gameObject);
    }

    public void Move(float scrollingSpeed)
    {
        rb.velocity += new Vector2(scrollingSpeed * Time.fixedDeltaTime, 0);
    }

    void UpdateLifeText()
    {
        lifeText.text = "HP: " + life;
    }

    void LooseLife(float hit)
    {
        life -= (int)(hit * 10);
        if (life < 0)
        {
            isDead = true;
        }
        else
        {
            UpdateLifeText();
        }
    }
}
