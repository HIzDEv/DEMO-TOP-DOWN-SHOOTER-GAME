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

             int RandomDamage = Random.Range(5, 10);
            collision.gameObject.GetComponent<PlayerManager>().GetHit(RandomDamage);
            collision.gameObject.GetComponent<AudioSource>().volume = 1f;
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(ImpactSound);
            
            

        }
        else if(collision.gameObject.tag == "NPC")
        {

            int RandomDamage = Random.Range(25, 45);

            collision.gameObject.GetComponent<StateController>().GetHit(RandomDamage, ShooterTransform);
            collision.gameObject.GetComponent<AudioSource>().volume = 1f;
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(ImpactSound);
        }

        GameObject imp = Instantiate(Impact, transform.position, transform.rotation) as GameObject;
        Destroy(imp, 1f);
        Destroy(this.gameObject);
    }
}
