using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMotion : MonoBehaviour
{
    public Rigidbody enemyrb;
    public Transform enemy;

    public bool m_FacingRight = true;
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(2, 0, 0);
    }
    void Update()
    {
        if (enemy.position.x < 0.4)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().velocity = new Vector3(2, 0, 0);
            if (!m_FacingRight)
            {
                Flip();
            }
        }
        if (enemy.position.x > 4.1)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().velocity = new Vector3(-2, 0, 0);
            if (m_FacingRight)
            {
                Flip();
            }
        }
    }

    public Transform player;
    public Rigidbody rb;
    void OnCollisionEnter()
    {
        FindObjectOfType<GameManager>().Exit();
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
