using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            Destroy(collision.gameObject);
        }
    }
}
