using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HM
{
    public class Character_Controller : MonoBehaviour
    {
        #region movement        
        public float TurnSmoothTIme = .2f;
        public float TurnSmoothVelocity;
        public float SpeedSmoothTime = 0.1f;
        public float SpeedSmoothVelocity;
        public float currentSpeed;
        public float TargetRotation;
        #endregion

        #region Camera    
        Transform CameraT;
        #endregion       

        #region Firing
             
        //Paramaitre de Tire
        public float FireRate = 5f;
        private float nextTimeToFire = 0f;       
        public Transform handAttachGrenade;//Transform de la main qui lance la grenade
        public GameObject ThrowedGrened;//GameObject qui contient la bombe lancer
        //Paramaitre de lancement de Grenade
        private float ThrowGrenadeRate = 5;
        private float nextTimeToThrowGrenade = 0;
        #endregion


        #region tmp

        [HideInInspector] public GameObject ImpactEffect;//Impcat Particle en cas de colition de tire avec un obstacle
        [HideInInspector] public GameObject BloodEffect;//Impcat Particle en cas de colition de tire avec un NPC
        [HideInInspector] public GameObject HealFX;//lance Particle en cas soin


        [HideInInspector] public GameObject bullet; //Prefab balle vide
        [HideInInspector] public Transform ShootPos;//position de bouche canon
        [HideInInspector] public GameObject GrenadePrefab;//Prefab grenade
        [HideInInspector] public Transform Grenade_Zone;//position static de zone de chute de grenade

        //Différents Audio Clips
        public AudioClip reloadingSound;
        public AudioClip PickUp; 
        //Différents Audio Sources
        public AudioSource footSteps;
        AudioSource Sound;


        
        PlayerManager playermanager;

        #endregion
        
        //Charger Player_Stats par les veleurs Statics
        private void Awake()
        {
            PLayer_Stats.ReloadingSound = reloadingSound;
            PLayer_Stats.Anim = GetComponent<Animator>();
            PLayer_Stats.GrenadePrefab = GrenadePrefab;
            PLayer_Stats.ThrowedGrened = ThrowedGrened;
            PLayer_Stats.handAttachGrenade = handAttachGrenade;
            PLayer_Stats.Grenade_Zone = Grenade_Zone;
            PLayer_Stats.pickup = PickUp;
        }


        //récupérer les composents 
        void Start()
        {
            Sound= GetComponent<AudioSource>();
            playermanager = GetComponent<PlayerManager>();
            CameraT = Camera.main.transform;           
        }

        // Update is called once per frame
        void FixedUpdate()
        {

            if (Input_manager.Reloading && playermanager.invAmmo > 0)
            {
                PLayer_Stats.Anim.SetBool("Reloading", Input_manager.Reloading);//Activer l'animation
            }//tester si la recharge est possible et lancer l'animation si oui

            if (Input_manager.Grenade)
            {
                if (playermanager.GrenadeInv > 0)
                {
                    PLayer_Stats.Anim.SetBool("Grenade", true);//Activer l'animation
                }
            }//tester si le lancement de grenade est possible et lancer l'animation si oui



            if (Input_manager.heal)
            {
                if (playermanager.HealPackInv > 0)
                {

                    GameObject fx = Instantiate(HealFX, transform.position, Quaternion.identity) as GameObject;
                    Destroy(fx, 1.5f);
                    playermanager.heal(50);
                }

                Input_manager.heal = false;
            }//tester si le soin est possible et lancer le particules si oui

           // if (Input_manager.Firing && (!Input_manager.Reloading && !Input_manager.Grenade) && playermanager.CurrentAmmo > 0)
            if (Input_manager.Firing )
            {
                PLayer_Stats.Anim.SetBool("Shooting", true);//Activer l'animation

                //if (Input_manager.inputDir.magnitude != 0)
                //{
                //    TargetRotation = CameraT.eulerAngles.y;//+ 20;// pour fixer la rotation du corps du personnage en movement

                //}
                //else
                //{
                //    TargetRotation = CameraT.eulerAngles.y;//touner par rapport au camera 
                //}

                Vector3 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 Direction = (MousePosition - transform.position).normalized;
                TargetRotation = Direction.y *-1 ;


                if (Time.time >= nextTimeToFire)//Eviter le raffale infinie 
                {
                 //  Shoot();//tirer

                    nextTimeToFire = Time.time + 1f / FireRate;
                }

            }
            else
            {
                 TargetRotation = Mathf.Atan2(Input_manager.inputDir.x, Input_manager.inputDir.y) * Mathf.Rad2Deg + CameraT.eulerAngles.y;
                
                PLayer_Stats.Anim.SetBool("Shooting", false);
            }




            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetRotation, ref TurnSmoothVelocity, TurnSmoothTIme);//chager de rotation avec un delai

            float Targetspeed = PLayer_Stats.runSpeed * Input_manager.inputDir.magnitude;//Target speed recoit la valeur runspeed 
            currentSpeed = Mathf.SmoothDamp(currentSpeed, Targetspeed, ref SpeedSmoothVelocity, SpeedSmoothTime);//chager de vitesse avec un delai


            transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);//avancer vers l'avant 

            float animationSpeedPercent = PLayer_Stats.runSpeed * Input_manager.inputDir.magnitude; //Valeur de l'animation x input (0 ou 1)

            PLayer_Stats.Anim.SetFloat("speed", animationSpeedPercent, SpeedSmoothTime, Time.deltaTime);


            //activer les FootSteps Sound
         //   FootSteps();
        }

        public void Shoot()
        {
            RaycastHit hit;

            //laancer un raycast de la camera pour viser vers l'avant (Distance 100)
            if (Physics.Raycast(CameraT.transform.position, CameraT.transform.forward, out hit, 100))
            {
                if (hit.collider.CompareTag("NPC"))
                {
                    hit.collider.GetComponent<StateController>().GetHit(25,this.transform);//faire damage (25)
                    ImpactFx(BloodEffect, hit,1.5f);//lancer la particule impact dans le point toucher (cible NPC)
                }
                else
                {
                    ImpactFx(ImpactEffect, hit,0.5f);//lancer la particule impact dans le point toucher (cible Obtacle)
                }

                
            }

            //MAJ la munitions 
            playermanager.CurrentAmmo--;
            playermanager.updateAmmo();

            BulletInstance();//Instance d'une balle vide
        }

        //lancer une balle vide de l'arme
        public void BulletInstance()
        {

            GameObject Bullet = Instantiate(bullet, ShootPos.position, ShootPos.localRotation) as GameObject;

            Rigidbody rg = Bullet.GetComponent<Rigidbody>();
            rg.AddForce(ShootPos.right * 25);
            Destroy(Bullet, 1);
        }

        //impact lance le préfabs dans le point toucher par le raycast et le detruit après un delai
        public void ImpactFx(GameObject Prefab,RaycastHit postion,float delai)
        {
            GameObject Impact = Instantiate(Prefab, postion.point, Quaternion.LookRotation(postion.normal)) as GameObject;
            Destroy(Impact, delai);
        }

        
        void FootSteps()
        {
            if (currentSpeed > 4)
            {
                if (!footSteps.isPlaying)
                {
                    footSteps.Play();
                }
            }
            else
            {
                footSteps.Stop();
            }
        }


        //récupérer les différents bonus
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "AmmoPack")
            {
                Debug.Log("TriggerAmmo");

                
                Sound.clip = PLayer_Stats.pickup;
                Sound.Play();

                playermanager.AddAmmo(60);
                playermanager.AddToInventory(2, 0);
                playermanager.updateAmmo();

                Destroy(other.gameObject);
            }
            if (other.gameObject.tag == "HealthKit")
            {
                Debug.Log("TriggerHealthKit");
                Sound.clip = PLayer_Stats.pickup;
                Sound.Play();

                playermanager.AddToInventory(0, 1);
                playermanager.updateAmmo();

                Destroy(other.gameObject);
            }
        }
    } 
}

