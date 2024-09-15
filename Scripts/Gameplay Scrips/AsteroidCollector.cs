using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            Destroy(collision.gameObject);
        }
    }
}
