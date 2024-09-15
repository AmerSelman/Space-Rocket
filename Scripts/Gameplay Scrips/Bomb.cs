using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Rigidbody2D myBody;
    private SpriteRenderer sr;

    [SerializeField]
    private int moveForce = 9;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        moveBomb();
    }

    private void moveBomb()
    {
        transform.position += new Vector3(0f, 1f, 0f) * Time.deltaTime * moveForce;
    }
}
