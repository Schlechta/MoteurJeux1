using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Foo : MonoBehaviour {

    public float speed = 10f;
    public float jumpSpeed = 0.1f;
    public Rigidbody2D rb;
    public GameObject feet;
    public LayerMask isJumpable;
    public Vector2 maxVelocity;
    public Animator animator;

    private bool isJumping = false;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity += new Vector2(-speed, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity += new Vector2(speed, 0);
        }
        if (Input.GetKey(KeyCode.Space) && isJumping == false)
        {
            if (Physics2D.OverlapCircle(feet.transform.position, 0.22f, isJumpable))
            {
                rb.velocity += new Vector2(0, jumpSpeed);
            }
            isJumping = true;
        }

        if (rb.velocity.y > maxVelocity.y)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxVelocity.y);
        }
        if (rb.velocity.x > maxVelocity.x)
        {
            rb.velocity = new Vector2(maxVelocity.x, rb.velocity.y);
        }
        else if (rb.velocity.x < - maxVelocity.x)
        {
            rb.velocity = new Vector2(- maxVelocity.x, rb.velocity.y);
        }
        if (rb.velocity.y == 0)
        {
            isJumping = false;
        }
        animator.SetFloat("Jump", Mathf.Abs(rb.velocity.y));
        animator.SetFloat("Walk", Mathf.Abs(rb.velocity.x));
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("pinata");
        if (coll.gameObject.tag == "Enemy")
        {
            Debug.Log(coll.contacts[0].normal.y);
            if (coll.contacts[0].normal.y > 0.75f)
            {
                KillEnemy(coll);
                rb.velocity = new Vector2(0, jumpSpeed);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Coin")
        {
            Destroy(coll.gameObject);
        }
    }

    void KillEnemy(Collision2D coll)
    {
        coll.gameObject.GetComponent<EnemyControler>().OnDeath();
        Destroy(coll.gameObject);
    }
}
