using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HM;

public class New_Character_Controller : MonoBehaviour
{
    
    public float MoveSpeed=5f;
    Vector3 MousePosition;
    public RaycastHit hit;
    public LayerMask Mask;
    public Transform Crosshair3D;

    //Firing¨Param
    #region shooting
    public float FireRate = 5f;
    private float nextTimeToFire = 0f;
    public Transform BoucheCanon;
    public Transform CasingBoucheCanon;
    #endregion

    #region Grenade 
    //Paramaitre de lancement de Grenade
    private float ThrowGrenadeRate = 5;
    private float nextTimeToThrowGrenade = 0;
    public Transform GreandeZone;
    #endregion

    #region movement        
    public float TurnSmoothTIme = .2f;
    float TurnSmoothVelocity;
    public float SpeedSmoothTime = 0.1f;
     float SpeedSmoothVelocity;
     float currentSpeed;
     float TargetRotation;
    #endregion

    #region PLayerParams

    public PlayerRessources MyPR;
    public ParticleSystem Muzzle;
    
    public Transform SoldierHand;
    public AudioSource footSteps;
    PlayerManager playermanager;

    #endregion

    
    private void Awake()
    {
        Init(MyPR);
    }

    private void Start()
    {
        Cursor.visible = false;
    }


    // Update is called once per frame
    void Update()
    {

        FollowCursor();

    }
    private void FixedUpdate()
    {

        

        Grenade();
        Reloading();
        Healing();
        Firing();
        Movement();

        
    }


    public void FollowCursor()
    {
        MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100, Mask))
        {
            
            FaceTarget(hit.point);

        }
    }

    void FaceTarget(Vector3 targetPos)
    {
        Crosshair3D.position = targetPos;

        Vector3 direction = (targetPos - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 8f);
    }

    void Grenade()
    {
        //tester si le lancement de grenade est possible et lancer l'animation si oui
        if (Input_manager.Grenade)
        {
            if (!GreandeZone.gameObject.activeSelf)
            {
                GreandeZone.gameObject.SetActive(true);
            }
            GreandeZone.position = hit.point;

            if (PLayer_Stats.ThrowedGrened == null && !PLayer_Stats.Anim.GetBool("Reloading"))
                {
                
                if (playermanager.GrenadeInv > 0)
                {
                    PLayer_Stats.Anim.SetBool("Grenade", true);//Activer l'animation
                    Input_manager.Grenade = true;
                }
            }
        }
        else
        {
            GreandeZone.gameObject.SetActive(false);
            PLayer_Stats.Anim.SetBool("Grenade", false);
        }

        

    }
    void Reloading()
    {
        //tester si la recharge est possible et lancer l'animation si oui

        if (Input_manager.Reloading && playermanager.invAmmo > 0)
        {
            PLayer_Stats.Anim.SetBool("Reloading", Input_manager.Reloading);//Activer l'animation
            
        }
    }
    void Healing()
    {
        //tester si le soin est possible et lancer le particules si oui
        if (Input_manager.heal)
        {
            if (playermanager.HealPackInv > 0)
            {

                GameObject fx = Instantiate(PLayer_Stats.HealPs, transform.position + new Vector3(0,1,0), Quaternion.identity) as GameObject;
                Destroy(fx, 1.5f);
                PLayer_Stats.AudioS.PlayOneShot(PLayer_Stats.healSound);
                playermanager.heal(50);
            }

           
        }
    }
    public void Movement()
    {
        float Targetspeed = PLayer_Stats.runSpeed * Input_manager.inputDir.magnitude;//Target speed recoit la valeur runspeed 
        currentSpeed = Mathf.SmoothDamp(currentSpeed, Targetspeed, ref SpeedSmoothVelocity, SpeedSmoothTime);//changer de vitesse avec un delai


        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);//avancer vers l'avant 

        float animationSpeedPercent = PLayer_Stats.runSpeed * Input_manager.inputDir.magnitude; //Valeur de l'animation x input (0 ou 1)
        
        
        PLayer_Stats.Anim.SetFloat("speed", animationSpeedPercent, SpeedSmoothTime, Time.deltaTime);
        
    }
    public void Firing()
    {
        if (Input_manager.Firing && playermanager.CurrentAmmo > 0 &&
             (!PLayer_Stats.Anim.GetBool("Reloading") && !PLayer_Stats.Anim.GetBool("Grenade"))
            )
        {
            PLayer_Stats.Anim.SetBool("Shooting", true);//Activer l'animation


            if (Time.time >= nextTimeToFire)//Eviter le raffale infinie 
            {
                Shoot();

                nextTimeToFire = Time.time + 1f / FireRate;
            }

        }
        else
        {
            TargetRotation = Mathf.Atan2(Input_manager.inputDir.x, Input_manager.inputDir.y) * Mathf.Rad2Deg ;
            PLayer_Stats.Anim.SetBool("Shooting", false);
        }
    }
    

    public void Shoot()
    {
        //Balle et effets
        Muzzle.Play();
        BulletInstance();

        //MAJ la munitions 
        playermanager.CurrentAmmo--;
        playermanager.updateAmmo();
        
    }



    public void BulletInstance()
    {
        

        GameObject Bullet = Instantiate(PLayer_Stats.bullet, BoucheCanon.position, BoucheCanon.localRotation) as GameObject;

        Rigidbody rg = Bullet.GetComponent<Rigidbody>();
        Bullet.GetComponent<Bullet>().ShooterTransform = this.transform;

        rg.AddForce(BoucheCanon.forward *3000);

        PLayer_Stats.AudioS.volume = 0.75f;
        PLayer_Stats.AudioS.PlayOneShot(PLayer_Stats.Shooting);

        Destroy(Bullet, 1);
      
    }



    private void Init(PlayerRessources PR)
    {
        PLayer_Stats.AudioS = GetComponent<AudioSource>();
        PLayer_Stats.AudioSFootSteps = footSteps;
        PLayer_Stats.Anim = GetComponent<Animator>();


        //Grenade
        PLayer_Stats.ThrowedGrened = null;
        PLayer_Stats.handAttachGrenade = SoldierHand;
        PLayer_Stats.GrenadePrefab = PR.GrenadePrefab;
        PLayer_Stats.HealPs = PR.HealPs;


        //AudioClips
        PLayer_Stats.pickup = PR.pickup;
        PLayer_Stats.ReloadingSound = PR.ReloadingSound;
        PLayer_Stats.Shooting = PR.ShootingClip;
        PLayer_Stats.healSound = PR.healClip;
        PLayer_Stats.FootStep = PR.FootStepsClip;


        //Firing
        PLayer_Stats.bullet = PR.bullet;
        PLayer_Stats.Casingbullet = PR.Casingbullet;
        
        

        playermanager = GetComponent<PlayerManager>();
        playermanager.updateAmmo();
    }






    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AmmoPack")
        {

            PLayer_Stats.AudioS.PlayOneShot(PLayer_Stats.pickup);

            playermanager.AddAmmo(60);
            playermanager.AddToInventory(2, 0);
            playermanager.updateAmmo();

            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "HealthKit")
        {

            PLayer_Stats.AudioS.PlayOneShot(PLayer_Stats.pickup);

            playermanager.AddToInventory(0, 1);
            playermanager.updateAmmo();

            Destroy(other.gameObject);
        }
    }







}
