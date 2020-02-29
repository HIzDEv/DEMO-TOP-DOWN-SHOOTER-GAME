using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HM
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/Death")]
    public class Death : Decision
    {
        public override bool Decide(StateController controller)
        {
            if (controller.Healh <= 0)
            {
                //activier l'animation (death) et arréter le navmesh agent
                controller.anim.SetTrigger("death");

                controller.navMeshAgent.enabled = false;
                controller.GetComponent<CapsuleCollider>().enabled = false;
                if (controller.Pnj.Type == PnjStats.Npc_Type.Melee)
                {
                    controller.GetComponentInChildren<MeleeAxe>().gameObject.SetActive(false);
                }
                //activer le son
                #region ActiveAudio
                controller.Audio.clip = controller.Pnj.DeathSound;
                controller.Audio.Play();
                #endregion
                //ajouter les points de bonus au joueur
                controller.chaseTarget.GetComponent<PlayerManager>().increseScore(20);

                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
