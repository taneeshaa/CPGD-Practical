using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = new Vector2(0f, rb.velocity.y);

    }

    public void OnJump()
    {
        Debug.Log("Jump");
        rb.velocity = new Vector2(rb.velocity.x, 6f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Collision"))
        {
            Destroy(gameObject);
        }
    }
}
