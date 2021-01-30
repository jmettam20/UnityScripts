using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballSound : MonoBehaviour
{
    public AudioSource OOF;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OOF.Play();

        }

    }

}
