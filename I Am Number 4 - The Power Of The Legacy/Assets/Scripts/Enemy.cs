using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject laser;

    // Use this for initialization
    void Start()
    {

    }

    private void Shoot()
    {
        Vector3 updatedPosition = new Vector3();
        updatedPosition = Vector3.right * 10.0f * Time.deltaTime;

        laser.transform.Translate(updatedPosition, Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
}
