using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HM
{
    [CreateAssetMenu(menuName = "PluggableAI/State")]
    public class State : ScriptableObject
    {
        //Tableau d'actions
        public Action[] actions;
        //tableau de transitions
        public Transition[] transitions;
        public Color sceneGizmoColor = Color.grey;

        public void UpdateState(StateController controller)
        {
            CheckTransitions(controller);
            DoActions(controller);

        }


        //activer les différents actions de l'etat en cours
        private void DoActions(StateController controller)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Act(controller);
            }
        }

        //vérifier les transitions possibles
        private void CheckTransitions(StateController controller)
        {
            for (int i = 0; i < transitions.Length; i++)
            {
                bool decisionSucceeded = transitions[i].decision.Decide(controller);

                if (decisionSucceeded)
                {
                    controller.TransitionToState(transitions[i].trueState);
                }
                else
                {
                    controller.TransitionToState(transitions[i].falseState);
                }
            }
        }


    }
}

