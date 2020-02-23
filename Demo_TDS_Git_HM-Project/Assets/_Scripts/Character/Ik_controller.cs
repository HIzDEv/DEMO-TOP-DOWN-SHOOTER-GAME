using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HM
{
    public class Ik_controller : MonoBehaviour
    {



        public Transform lefthand;//Position de la main Gauche 
        public Transform righthand;//Position de la main droite

        public Transform ShootOrientaiton;//Position Tire

        Transform Chest;



        void Start()
        {
       

        }

        //a callback for calculating IK
        void OnAnimatorIK()
        {
            if (PLayer_Stats.Anim)
            {

                //Si etat du personnage Recharge son arme ou Lance une grenade 

                if (Input_manager.Reloading || Input_manager.Grenade)
                {
                    PLayer_Stats.Anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);//Relacher la main Gauche Position
                    PLayer_Stats.Anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);//Relacher la main Gauche Rotation
                    return;
                }





                
                    PLayer_Stats.Anim.SetLookAtWeight(1);
                    PLayer_Stats.Anim.SetLookAtPosition(ShootOrientaiton.position);
                


                //Si le personnage Tire avec larme i oriente son corps et fixer le regard sur la cible 
                if (Input_manager.Firing)
                {

                    
                   

                    //Forcer la main Gauche a rester attacher a l'arme (la position Lefthand.position , la rotation Lefthand.rotation)
                    if (Input_manager.inputDir.magnitude != 0)
                    {
                        PLayer_Stats.Anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                        PLayer_Stats.Anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);

                        PLayer_Stats.Anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);

                        PLayer_Stats.Anim.SetIKRotation(AvatarIKGoal.RightHand, righthand.rotation);
                        PLayer_Stats.Anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

                        PLayer_Stats.Anim.SetIKPosition(AvatarIKGoal.LeftHand, lefthand.position);
                        PLayer_Stats.Anim.SetIKRotation(AvatarIKGoal.LeftHand, lefthand.rotation);

                        PLayer_Stats.Anim.SetIKPosition(AvatarIKGoal.RightHand, righthand.position);
                        
                    }
                    


                }

            }


        }
    }
}




