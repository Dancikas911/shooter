using UnityEngine;

public class Movement : MonoBehaviour
{
    public LayerMask groundLayer;
    public float groundDistance;
    public float moveSpeed;
    public float jumpSpeed;
    public bool grounded;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Ground check
        RaycastHit2D hit = Physics2D.Raycast( transform.position, Vector2.down,groundDistance,groundLayer);
        grounded = hit.collider != null;

        // Jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }

        // Movement
        float h = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(h * moveSpeed, rb.velocity.y);

        //Flip sprite
        if (h != 0)
        {
            float angle = h > 0 ? 0 : 180;
            transform.eulerAngles = new Vector3(0, angle, 0);
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpSpeed;
    }
}