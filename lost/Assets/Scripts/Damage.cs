using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public Transform player;
    public Rigidbody rb;
    void OnCollisionEnter()
    {
        FindObjectOfType<GameManager>().Exit();
    }
}
