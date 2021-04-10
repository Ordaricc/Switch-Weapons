using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        int xDirection = 0;
        if (Input.GetKey(KeyCode.D))
            xDirection = 1;
        else if (Input.GetKey(KeyCode.A))
            xDirection = -1;

        rb.velocity = new Vector2(xDirection * speed, rb.velocity.y);
    }
}