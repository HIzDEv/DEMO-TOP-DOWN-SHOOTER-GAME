using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HM
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
    public class PatrolAction : Action
    {
        public override void Act(StateController controller)
        {
            Patrol(controller);
        }

        private void Patrol(StateController controller)
        {


            controller.navMeshAgent.destination = controller.wayPointList[controller.nextWayPoint].position;
            controller.navMeshAgent.isStopped=false;



            if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
            {
                

                controller.anim.SetBool("Walking", false);


                //rester dans chaque Waypoints une durée de 5s
                if (controller.CheckIfCountDownElapsed(5))
                {
                    
                    controller.anim.SetBool("Walking", true);
                    controller.stateTimeElapsed = 0;
                    controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
                    //changer vers le prochain Waypoint
                }

            }
            else
            {
                controller.anim.SetBool("Walking", true);
            }
        }
    }


}
