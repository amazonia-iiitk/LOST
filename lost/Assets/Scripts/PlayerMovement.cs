using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float xforce = -.05f;
    public float yforce = -.05f;

    public bool m_FacingRight=true;
    void FixedUpdate()
    {
        if (Input.GetKey("w")){
            GetComponent<Rigidbody>().velocity = new Vector3(0, yforce * Time.deltaTime, 0);
        }
        if (Input.GetKey("a")){
            if (m_FacingRight)
            {
                Flip();
            }
            GetComponent<Rigidbody>().velocity = new Vector3(-xforce * Time.deltaTime, 0 , 0);
        }
        if (Input.GetKey("d")){
            if (!m_FacingRight)
            {
                Flip();
            }
            GetComponent<Rigidbody>().velocity = new Vector3(xforce * Time.deltaTime, 0 , 0);
        }
        if (Input.GetKey("s"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, -yforce * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            FindObjectOfType<GameManager>().pause();
        }
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
