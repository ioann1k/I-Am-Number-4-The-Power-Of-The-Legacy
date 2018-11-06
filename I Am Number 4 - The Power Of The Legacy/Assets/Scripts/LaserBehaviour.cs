using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Destroy(gameObject);

            collision.collider.GetComponent<MainCharacter>().LoseHealth(1);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
