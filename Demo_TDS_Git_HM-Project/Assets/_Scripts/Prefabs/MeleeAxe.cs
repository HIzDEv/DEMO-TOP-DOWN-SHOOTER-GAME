using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HM;

public class MeleeAxe : MonoBehaviour
{
    public AudioClip ImpactSound;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            int RandomDamage =  Random.Range(5, 12);

            other.gameObject.GetComponent<PlayerManager>().GetHit(RandomDamage);
            other.gameObject.GetComponent<AudioSource>().volume = 1f;
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(ImpactSound);



        }
    }
    
}
