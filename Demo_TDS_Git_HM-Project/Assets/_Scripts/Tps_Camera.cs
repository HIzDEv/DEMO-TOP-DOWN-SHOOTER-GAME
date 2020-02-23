using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace HM
{
    public class Tps_Camera : MonoBehaviour
    {


        public LayerMask Objectmask;
        public float mouseSensitivity = 10;//Sencibilté de la souris mouvement du Camera 


        public Transform target;// Cible de la Camera 
        CameraShake Cshake;

        public float MaxdistFromTaget = 2f;//Distance de la cible 
        public float MindistFromTaget = 1f;//Distance de la cible 
        public float smoothTime= 0.4f;
        Vector3 transationvelocity;
         public float distance=2f;

        public Vector2 pitchMinMax = new Vector2(-10, 50); //Rotation maximal sur les deux Axes (-y,y) du camera 





        public float rotationSmoothTime = 1.2f;//Approximativement le temps qu'il faudra pour atteindre la nouvelle valeur
        Vector3 rotationSmoothVelocity;
        Vector3 CurrentRotation;

        float yaw;
        float pitch;
        private void Start()
        {
            Cshake = GetComponent<CameraShake>();
        }

        // Update is called once per frame

        void LateUpdate()
        {


            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;//Recupaire la rotation du souris (X)
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;//Recupaire la rotation du souris (Y)

            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);//limite la rotation sur l'axe (Y) entre deux valeurs


            CurrentRotation = Vector3.SmoothDamp(CurrentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
            //Valeur actuel ,Valeur a atteindre ,La vélocité actuelle,temps qu'il faudra pour atteindre la nouvelle valeur


            transform.eulerAngles = CurrentRotation;
            //changer la rotation de la camera 

            //tester si le champs de camera entre en collision avec un obstacle
            MaxdistFromTaget = Input_manager.Zoom;


            if(!Cshake.Shaking)
                TestCollider();

        }

        
        void TestCollider()
        {
            RaycastHit hit;

             //lancer un Raycast entre  Position initiale (position voulue hors collision) et le joueur
            if (Physics.Linecast(target.position - target.forward * MaxdistFromTaget, target.position, out hit, Objectmask))
            {
                distance = Mathf.Clamp((hit.distance * 0.9f), MindistFromTaget, MaxdistFromTaget);
                //la distance entre la camera et le joueur change a minDistFromTarget en cas de collision +un décalage a droite
                transform.position = target.position - transform.forward * distance + transform.right * 0.75f;

            }
            else
            {
                //retourner a la potition initiale
                distance = MaxdistFromTaget;
                transform.position = target.position - transform.forward * distance + transform.right * 0.75f;
            }

            //Debug.DrawRay(target.position + target.forward * MaxdistFromTaget, target.position, Color.red);
        }


    }

   
}

