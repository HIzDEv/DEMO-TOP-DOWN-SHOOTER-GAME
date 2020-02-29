using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HM
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/TargetRunAway")]
    public class TargetRunAway : Decision
    {
        public override bool Decide(StateController controller)
        {
            //si la distance entre le NPc et le joueur dépace le max du Champs de tire de l'NPC (NPC passe a la chasse du joueur)
            if (Vector3.Distance(controller.transform.position, controller.chaseTarget.position) > controller.Pnj.attackRange)
            {
                controller.navMeshAgent.isStopped = false;
                controller.anim.SetBool("Walking", true);
                controller.anim.SetBool("Attack", false);
                return true;
            }
            else
            {
                
                controller.navMeshAgent.isStopped = true;
                controller.anim.SetBool("Attack", true);
                controller.anim.SetBool("Walking", false);
                return false;
            }
        }
    }
}

    


