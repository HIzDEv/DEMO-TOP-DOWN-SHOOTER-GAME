using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
la class Event est responsables de evenement d'eclancher par les différents annimations au cours du jeu
*/
namespace HM
{
    public class Events : MonoBehaviour
    {

        PlayerManager PM;

        private void Awake()
        {
            PM = GetComponent<PlayerManager>(); //récupérer le PlayerManager
        }

        //Evénement lancer si le joueur fini le lancement de grenade

        public void ThrowGreanadeEnded()
        {
            PM.GrenadeInv-=1;
            PM.updateAmmo();
            Input_manager.Grenade = false;
           
        }


        //Evénement lancer si le joueur fini le rechargement d'arm
        public void ReloadEnded()
        {


            if (PM.invAmmo > PLayer_Stats.maxRoundAmmo)
            {
                if (PM.CurrentAmmo == 0)
                {
                    PM.CurrentAmmo = PLayer_Stats.maxRoundAmmo;
                    PM.invAmmo -= PLayer_Stats.maxRoundAmmo;
                }
                else
                {
                    PM.invAmmo -= PLayer_Stats.maxRoundAmmo - PM.CurrentAmmo;
                    PM.CurrentAmmo += PLayer_Stats.maxRoundAmmo - PM.CurrentAmmo;
                }
                
            }
            else
            {
                PM.CurrentAmmo = PM.invAmmo;
                PM.invAmmo = 0;
            }

            PM.updateAmmo();
            Input_manager.Reloading = false;
            PLayer_Stats.Anim.SetBool("Reloading", false);
        }


        //Evénement attache une grenade a la main du joeur avant de la lancer
        public void AttachGrenade()
        {
            if (PLayer_Stats.ThrowedGrened == null)
            {
                PLayer_Stats.ThrowedGrened = Instantiate(PLayer_Stats.GrenadePrefab, PLayer_Stats.handAttachGrenade.position, PLayer_Stats.handAttachGrenade.localRotation);
                PLayer_Stats.ThrowedGrened.transform.parent = PLayer_Stats.handAttachGrenade.transform;
                PLayer_Stats.ThrowedGrened.GetComponent<GrenadeLauncher>().NCC = GetComponent<New_Character_Controller>();
            }    
          }

        //Evénement détache la grenade de la main du joeur et la lancer
        public void DettachGrenade()
        {

            PLayer_Stats.ThrowedGrened.transform.parent = null;

            GrenadeLauncher Gl = PLayer_Stats.ThrowedGrened.GetComponent<GrenadeLauncher>();
            Exploid Ep = PLayer_Stats.ThrowedGrened.GetComponent<Exploid>();
            Ep.Lunched = true;

            
                
               // Gl.target = PLayer_Stats.Grenade_Zone;
                Gl.Launch();
                PLayer_Stats.ThrowedGrened.AddComponent<CapsuleCollider>();

        }


        //active le son de Reload
        public void ReloadinSound()
        {

            PLayer_Stats.AudioS.volume = 0.5f;
            PLayer_Stats.AudioS.PlayOneShot(PLayer_Stats.ReloadingSound);
           
        }

        public void FootStepsSFX()
        {
            if (GetComponent<New_Character_Controller>() && Input_manager.inputDir.magnitude > 0)
            {
                PLayer_Stats.AudioSFootSteps.PlayOneShot(PLayer_Stats.FootStep);
            }
                
            
            
        }
        //Evénement lancer aprés l'activation de l'animation (Death)
        public void lose()
        {
               
                Time.timeScale = 0;
                PM.LosePanel.SetActive(true);
        }

    }


    
}

