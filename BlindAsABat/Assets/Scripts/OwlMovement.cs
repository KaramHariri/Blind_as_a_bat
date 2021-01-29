using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BLINDED_AM_ME
{


    public class OwlMovement : MonoBehaviour
    {
        public Path_Comp path_comp;

        [Range(0, 1)]
        public float distanceTravelled = 0;

        public float moveIntervall = 1.75f;
        public float moveTimer = 0f;
        private float distancePerMoveIntervall = 0.03f;


        private void Start()
        {
            Path_Point axis = path_comp.GetPathPoint(0);
            transform.position = axis.point;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            moveTimer += Time.deltaTime;
            if(moveTimer > moveIntervall)
            {
                moveTimer = 0;

                distanceTravelled = distanceTravelled + distancePerMoveIntervall;
                // This made it based on the paritcle speed
                float dist = distanceTravelled;

                dist = dist * path_comp.TotalDistance;

                Path_Point axis = path_comp.GetPathPoint(dist);
                transform.position = axis.point;
            }

        }
    }

}