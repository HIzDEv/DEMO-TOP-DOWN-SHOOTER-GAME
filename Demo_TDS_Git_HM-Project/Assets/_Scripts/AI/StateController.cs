using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace HM
{
    public class StateController : MonoBehaviour
    {

        [Header("StateMachine Param")]
        public State currentState;
        public PnjStats Pnj;//paramaitres NPC
        public Transform eyes;//Champs vision NPC (Position initiale)
        public State remainState;
        //Positions Patrols
        public List<Transform> wayPointList;
        [HideInInspector] public int nextWayPoint;

        //Transform Joueur 
        [HideInInspector] public Transform chaseTarget;


        //Delai écoulé
        [HideInInspector] public float stateTimeElapsed;

        [Header("")]
        
        //différent composonts et Préfabs 
        [HideInInspector] public Animator anim;
        [HideInInspector] public AudioSource Audio;
        [HideInInspector]  public float Healh;
         public Image HeathBarre;
         public Transform boucheCanon;
         public ParticleSystem Muzzle;
        

        [HideInInspector] public NavMeshAgent navMeshAgent;

        




        void Awake()
        {
            anim = GetComponent<Animator>();
            Audio = GetComponent<AudioSource>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            Healh = Pnj.MaxHealh;
        }



        void Update()
        {

            currentState.UpdateState(this);  

        }

        //Débuger les Différents Etats NPCs

        void OnDrawGizmos()
        {
            if (currentState != null && eyes != null)
            {
                Gizmos.color = currentState.sceneGizmoColor;
                Gizmos.DrawWireSphere(eyes.position, Pnj.lookSphereCastRadius);
            }
        }

        //Vérifier les transitions entres les etats
        public void TransitionToState(State nextState)
        {
            if (nextState != remainState)
            {
                currentState = nextState;
                OnExitState();
            }
        }


        //Vérifier si le delai est écoulé
        public bool CheckIfCountDownElapsed(float duration)
        {
            stateTimeElapsed += Time.deltaTime;
            return (stateTimeElapsed >= duration);
        }

        private void OnExitState()
        {
            stateTimeElapsed = 0;
        }

        
        public void GetHit(int value,Transform PlayerPosition)
        {
            if (chaseTarget == null)
            {
                chaseTarget = PlayerPosition;
            }
            Healh -= value;

            HeathBarre.fillAmount = Healh / Pnj.MaxHealh;
        }

        public void Shooting()
        {
            

            GameObject Bullet = Instantiate(Pnj.prefabs_Impact, boucheCanon.position, boucheCanon.localRotation) as GameObject;

            Rigidbody rg = Bullet.GetComponent<Rigidbody>();
            rg.AddForce(boucheCanon.forward * 3000);

            Audio.PlayOneShot(Pnj.ShootSound);
            Muzzle.Play();
            Destroy(Bullet, 1);
            
        }

    }
}

