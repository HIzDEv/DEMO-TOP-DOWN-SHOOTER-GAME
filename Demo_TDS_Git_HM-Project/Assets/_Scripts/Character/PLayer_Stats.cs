using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HM
{    
     public  class PLayer_Stats 
    {
        #region playerVar
        public static float runSpeed = 4;
        public static float maxHealth=100;
        public static int maxRankPoits=100;
        #endregion

        #region inventaire
        public static int MaxGrenadeInv=1;
        public static int MaxHealPack=1;

        public static int maxRoundAmmo = 30;
        public static int maxAmmo=180;
        #endregion
        #region Animation 
        public static Animator Anim;

        #endregion

        #region prefabs
        public static GameObject GrenadePrefab;
        public static GameObject ThrowedGrened;//Prefab grenade
        public static GameObject Casingbullet; //Prefab balle vide
        public static GameObject bullet; //Prefab balle
        public static GameObject HealPs; //Prefab balle
        #endregion

        #region PS
        public static GameObject PsShoot;        
        public  GameObject ImpactEffect;
        
        #endregion

        #region TransformNeeds
        public static Transform handAttachGrenade;
        public static Transform Grenade_Zone;
        #endregion

        #region AudioClips
        public static AudioSource AudioS;
        public static AudioSource AudioSFootSteps;
        public static AudioClip ReloadingSound;
        public static AudioClip pickup;
        public static AudioClip Shooting;
        public static AudioClip healSound;
        public static AudioClip FootStep;
        
        #endregion
    }
}

