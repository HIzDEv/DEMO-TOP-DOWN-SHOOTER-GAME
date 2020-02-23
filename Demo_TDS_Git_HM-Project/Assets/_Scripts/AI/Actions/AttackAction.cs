using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HM
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
    public class AttackAction : Action
    {
        public override void Act(StateController controller)
        {
            Attack(controller);
        }

        private void Attack(StateController controller)
        {
            RaycastHit hit;

            Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.Pnj.attackRange, Color.red);


            //NPC vise le joueur (change sa rotation par rapport au joueur)
            controller.transform.LookAt(controller.chaseTarget);

            
            if (Physics.SphereCast(controller.eyes.position, controller.Pnj.lookSphereCastRadius, controller.eyes.forward, out hit, controller.Pnj.attackRange)
                && hit.collider.CompareTag("Player"))      
            {
                //vérifier si le joueur est dans le champs de vision de l'NPC
                
                if (controller.CheckIfCountDownElapsed(controller.Pnj.attackRate))
                {
                    controller.Shooting();

                    //Activer le son et réinitialiser le temps du prochaine attack
                    controller.stateTimeElapsed = 0;
                }
                
    
        }
        }
    }
}

