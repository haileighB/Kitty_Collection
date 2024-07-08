using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCarry : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //make player child of platform when we are on it
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            //revert when jump off platform
            collision.gameObject.transform.SetParent(null);
        }
    }
}
