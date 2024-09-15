using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AsteroidSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] asteroidReference;

    private GameObject spawnAsteroid;

    [SerializeField]
    private Transform topPos;
    private int randomIndex;

    private float time = 0.9f;
    private float timeToWait = 10;
    private int randomTopPos;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAsteroids());
        StartCoroutine(AsteroidTimer());
    }
    IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            randomIndex = Random.Range(0, asteroidReference.Length);
            randomTopPos = Random.Range(-4, 4);

            spawnAsteroid = Instantiate(asteroidReference[randomIndex]);

            spawnAsteroid.transform.position = topPos.position;

            spawnAsteroid.GetComponent<Asteroid>().speed = Random.Range(4, 10);
        }
    }
    IEnumerator AsteroidTimer()
    {
        while (time > 0.2)
        {
            yield return new WaitForSeconds(timeToWait);
            time -= 0.1f;
        }
    }
}