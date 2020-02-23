using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HM
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
    public class ChaseAction : Action
    {
        public override void Act(StateController controller)
        {
            Chase(controller);
        }

        private void Chase(StateController controller)
        {
            //récupérer la positiion de jouer avec un decalage pour rester dans le champ de vision du NPC
            Vector3 offset = Vector3.forward  + (Vector3.right * 4);
            controller.navMeshAgent.destination = controller.chaseTarget.position + offset;    

        }
    }
}
