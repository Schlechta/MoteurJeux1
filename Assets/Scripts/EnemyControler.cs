using UnityEngine;
using System.Collections;

public class EnemyControler : MonoBehaviour {

    public float speed = 2f;
    public float jumpSpeed = 10f;
    public Rigidbody2D rb;
    public GameObject feet;
    public LayerMask isJumpable;
    public Vector2 maxVelocity;
    public GameObject coin;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        var rand = Random.Range(0, 200);

        if (rand < 2)
        {
            rb.velocity += new Vector2(-speed, 0);
        }
        else if (rand < 4)
        {
            rb.velocity += new Vector2(speed, 0);
        }
        else if (rand < 5)
        {
            if (Physics2D.OverlapCircle(feet.transform.position, 0.22f, isJumpable))
            {
                rb.velocity += new Vector2(0, jumpSpeed);
            }
        }
        if (rb.velocity.y > maxVelocity.y)
        {
            rb.velocity = new Vector2(rb.velocity.x, maxVelocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            CollectiveSuicide(coll);
            rb.velocity = new Vector2(0, jumpSpeed);
        }
    }

    void CollectiveSuicide(Collision2D coll)
    {
        coll.gameObject.GetComponent<EnemyControler>().OnSuicide();
        Destroy(coll.gameObject);
    }

    public void OnDeath()
    {
        GameObject coinObject = Instantiate(coin);
        coinObject.transform.position = new Vector3(this.transform.position.x + 3.0f, this.transform.position.y + 2.0f, 0);
    }

    public void OnSuicide()
    {
        GameObject coinObject = Instantiate(coin);
        coinObject.transform.position = this.transform.position;
    }
}
