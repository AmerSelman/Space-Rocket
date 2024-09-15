using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    public float speed = 5;
    void Update()
    {
        Vector2 pos = transform.position;

        if (pos.y < -19.2)
        {
            pos.y = 0;
        }

        pos.y -= speed * Time.deltaTime;

        transform.position = pos;
    }
}
