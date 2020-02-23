using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HM
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
    public class LookDecision : Decision
    {

        public override bool Decide(StateController controller)
        {

            //Si le joueur est repérer ou la valeur ChaseTarget est chargé (NPC est touché par le joueur)
            bool targetVisible = Look(controller);
            return targetVisible ||controller.chaseTarget!=null;
        }


        //Récupérer le Transform du joueur s'il est repéré par NPC
        private bool Look(StateController controller)
        {
            RaycastHit hit;

            Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.Pnj.lookRange, Color.green);

            if (Physics.SphereCast(controller.eyes.position, controller.Pnj.lookSphereCastRadius, controller.eyes.forward, out hit, controller.Pnj.lookRange)
                && hit.collider.CompareTag("Player"))
            {
                controller.chaseTarget = hit.transform;
                controller.navMeshAgent.isStopped = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

