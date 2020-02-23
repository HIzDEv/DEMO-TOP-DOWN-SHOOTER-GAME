using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HM;

public class Bullet : MonoBehaviour
{

    public Transform ShooterTransform;

    public GameObject Impact;
    public AudioClip ImpactSound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerManager>().GetHit(5);
            collision.gameObject.GetComponent<AudioSource>().volume = 1f;
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(ImpactSound);
            
            

        }
        else if(collision.gameObject.tag == "NPC")
        {
            collision.gameObject.GetComponent<StateController>().GetHit(25, ShooterTransform);
            collision.gameObject.GetComponent<AudioSource>().volume = 1f;
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(ImpactSound);
        }

        GameObject imp = Instantiate(Impact, transform.position, transform.rotation) as GameObject;
        Destroy(imp, 1f);
        Destroy(this.gameObject);
    }
}
