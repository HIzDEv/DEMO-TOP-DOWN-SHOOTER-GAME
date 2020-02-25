using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace HM
{
    public class CameraObstaclesFade : MonoBehaviour
    {
        Camera cam;
        public Transform Target;
        public LayerMask IgnoredMask;

        public List<GameObject> Obstacle;
        

        void Start()
        {
            cam = GetComponent<Camera>();
        }

        void Update()
        {

           Vector3 dir = Target.position - cam.transform.position;
            
            RaycastHit hit;
            if (Physics.Raycast(transform.position,dir, out hit,1000,IgnoredMask))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    FadeObstacles(false);
                    Obstacle.Clear();
                }
                else
                {
                    AddToObstacleslist(hit.transform.gameObject);
                    FadeObstacles(true);
                }
                
            }
            

        }

        public void AddToObstacleslist(GameObject obs)
        {
            if (!Obstacle.Contains(obs))
            {
                Obstacle.Add(obs);
            }
        }

        public void FadeObstacles(bool Fade)
        {
            foreach(GameObject O in Obstacle)
            {
                O.GetComponent<FadeController>().fading = Fade;
            }
        }
    }


}

