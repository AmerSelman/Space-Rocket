using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Asteroid : MonoBehaviour
{
    [HideInInspector]
    public float speed;
    private float pos;
    private Rigidbody2D myBody;
    private SpriteRenderer sr;
    private CircleCollider2D cr;
    private ParticleSystem explosion;

    private int time = 3;

    private string BOMB_TAG = "Bomb";

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cr = GetComponent<CircleCollider2D>();
        explosion = GetComponent<ParticleSystem>();
    }

    void Start()
    {
        pos = Random.Range(-4.0f, 4.0f);
        transform.position = new Vector2(pos, transform.position.y);
    }


    void FixedUpdate()
    {
        myBody.velocity = new Vector2(myBody.velocity.x, -speed);
        StartCoroutine(DestroyAsteroids());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(BOMB_TAG))
        {
            sr.enabled = false;
            cr.enabled = false;
            explosion.Play();

            Destroy(collision.gameObject);
        }
    }

    IEnumerator DestroyAsteroids()
    {
        while (!sr.enabled)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
    }
}
