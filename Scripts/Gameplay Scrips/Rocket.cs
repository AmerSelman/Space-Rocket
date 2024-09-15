using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 5f;
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] BombReference;

    private GameObject spawnBomb;

    [SerializeField]
    private float maxX, minX, maxY, minY;
    private float movementX, movementY;
    private Rigidbody2D myBody;
    private SpriteRenderer sr;
    private Animator anim;

    public Joystick joystick;

    private int time = 1;

    private EdgeCollider2D edgeCollider2D;

    private ParticleSystem explosion;

    public int shootPressed, gameOverNumb;

    private bool canShoot = true;
    private string IDLE_ANIMATION = "Idle";
    private string BOOST_ANIMATION = "Boost";
    private string SLOW_ANIMATION = "Slow";
    private string ASTEROID_TAG = "Asteroid";

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        explosion = GetComponent<ParticleSystem>();
        edgeCollider2D = GetComponent<EdgeCollider2D>();

        joystick = FindObjectOfType<Joystick>();
        if (joystick == null)
        {
            Debug.LogError("Joystick not found in scene!");
        }
    }
    void Update()
    {
        PlayerMove();
        Shoot();

        StartCoroutine(DestroyRocket());
    }

    void PlayerMove()
    {
        if (joystick == null)
        {
            return;
        }

        movementX = joystick.Horizontal;
        movementY = joystick.Vertical;

        LimitMovement();
        if (movementX != 0 && movementY != 0)
        {
            transform.position += new Vector3(movementX, movementY, 0f) * Time.deltaTime * moveForce / 1.5f;
            if (movementY > 0)
            {
                AnimateRocketBoost();
            }
            else if (movementY < 0)
            {
                AnimateRocketSlow();
            }
            else
            {
                AnimateRocketIdle();
            }

        }
        else
        {
            transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
            transform.position += new Vector3(0f, movementY, 0f) * Time.deltaTime * moveForce;
            if (movementY > 0)
            {
                AnimateRocketBoost();
            }
            else if (movementY < 0)
            {
                AnimateRocketSlow();
            }
            else
            {
                AnimateRocketIdle();
            }
        }
    }

    void Shoot()
    {
        if ((sr.enabled) && canShoot)
        {
            spawnBomb = Instantiate(BombReference[0]);

            spawnBomb.transform.position = myBody.position + new Vector2(0f, 1.5f);

            canShoot = false;
            ShootingTimer();
        }
    }
    void ShootingTimer()
    {
        StartCoroutine(SpawnAsteroids());
    }

    void LimitMovement()
    {
        if ((transform.position.x >= maxX && movementX == 1) || (transform.position.x <= minX && movementX == -1))
        {
            movementX = 0;
        }
        if ((transform.position.y >= maxY && movementY == 1) || (transform.position.y <= minY && movementY == -1))
        {
            movementY = 0;
        }

    }

    void AnimateRocketIdle()
    {
        anim.SetBool(IDLE_ANIMATION, true);
        anim.SetBool(BOOST_ANIMATION, false);
        anim.SetBool(SLOW_ANIMATION, false);

    }
    void AnimateRocketBoost()
    {
        anim.SetBool(IDLE_ANIMATION, false);
        anim.SetBool(BOOST_ANIMATION, true);
        anim.SetBool(SLOW_ANIMATION, false);
    }
    void AnimateRocketSlow()
    {
        anim.SetBool(IDLE_ANIMATION, false);
        anim.SetBool(BOOST_ANIMATION, false);
        anim.SetBool(SLOW_ANIMATION, true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ASTEROID_TAG))
        {
            sr.enabled = false;
            edgeCollider2D.enabled = false;
            explosion.Play();
        }
    }
    IEnumerator SpawnAsteroids()
    {
        while (!canShoot)
        {
            yield return new WaitForSeconds(time);
            canShoot = true;
        }
    }
    IEnumerator DestroyRocket()
    {
        while (!sr.enabled)
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);

        }
    }
}

