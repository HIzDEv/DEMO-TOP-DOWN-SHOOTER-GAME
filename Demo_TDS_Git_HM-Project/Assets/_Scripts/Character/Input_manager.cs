using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Class responsale de la récupération des Inputs du joueur
     */
namespace HM
{
    public class Input_manager : MonoBehaviour
    {
        
        public static bool Firing,Reloading, Grenade,heal;//Valeur booleen de l'etat du personnage

        public static Vector2 inputDir;

        public KeyCode Reload_Key;
        public KeyCode Grenade_Key;
        public KeyCode Healing_Key;
        public KeyCode Firing_Key;

        public static float Zoom=10f;
        public float minZoom =5.0f;
        public float MaxZoom =12.0f;

        public static Input_manager IM;
        private void Start()
        {
            IM = this;
        }

        private void Update()
        {
            Reloading = Input.GetKey(Reload_Key);//Boutton Reload activer
            
            Grenade = Input.GetKey(Grenade_Key); //Boutton Grenade activer


            heal = Input.GetKey(Healing_Key); //Boutton Heal activer



            Firing = Input.GetKey(Firing_Key); //Recuperer la valeur activer Click Gauche Souris


            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //Stocker la valeur des deux input dans Vecteur 2
            inputDir = input.normalized; //(0 ou 1 ) pour chaque valeur

            

            Zoom += (Input.GetAxis("Mouse ScrollWheel")*2)*-1; //Zooming
            Zoom = Mathf.Clamp(Zoom, minZoom, MaxZoom);
        }
    }
}

