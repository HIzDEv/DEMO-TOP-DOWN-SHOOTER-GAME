using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HM
{
    public class Exploid : MonoBehaviour
    {


        public float timer; //nbr de seconde pour exploser la bombe



        public bool Lunched;//valeur a activer aprés le declanchelent du bombe
        public GameObject ExplodingPs;//Particle system

        public float Radius = 5f;//Rayon d'explosion
        public float explosiveForce;//force d'explosion
                                   


        // Update is called once per frame
        void Update()
        {

            if (!Lunched)
            {
                return; //Rien faire si la bombe n'est pa lancer
            }

            //Commener le Timer et exploser la bombe 
            if (timer > 0)
            {
                timer -= Time.deltaTime;

            }
            else
            {
                exploid();

            }


        }

        void makeDamage()
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, Radius);//Recupérer les colliders qui entour la bombe dans un tableau
            foreach (Collider nearbyObject in colliders)
            {
                //parcourir le tableau 
                if (nearbyObject.gameObject.tag == "NPC")
                {
                    PlayerManager Player = FindObjectOfType<PlayerManager>();
                    nearbyObject.GetComponent<StateController>().GetHit(200, Player.transform);

                }
            }
        }

        void exploid()
        {

            GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            ExplodingPs.SetActive(true); //Activer Le PS
                                         
            makeDamage(); //Creer l'impact

            Destroy(gameObject, 2f);//Détruire l'objet aprés 3sc
        }
    }
}


