using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class EnemyMotion : MonoBehaviour
{
    public Rigidbody enemyrb;
    public Transform enemy;
    public Animator animator;

    bool x = true;
    Vector3 temp = new Vector3(1, 0, 0);

    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, -2, 0);
    }
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(enemyrb.velocity.y));        
        if (enemy.position.y > 1.9)
        {
            x = true;            
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().velocity = new Vector3(0, -2, 0);
        }
        if (enemy.position.y < -0.58)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            if (x) { StartCoroutine(resetPos()); }
            
        }
    }

    public Transform player;
    public Rigidbody rb;
    void OnCollisionEnter()
    {
        FindObjectOfType<GameManager>().Exit();
    }

    IEnumerator resetPos()
    {
        x = false;
        yield return new WaitForSeconds(1);
        temp.y = 2f;
        enemy.position = temp;
    }
}
