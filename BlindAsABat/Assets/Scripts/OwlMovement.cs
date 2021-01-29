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

        GameObject parent;

        [SerializeField]
        private float maxVisibleDuration = 1.5f;
        private float visibleDuration = 0.0f;
        private SpriteRenderer spriteRenderer = null;

        private AnimalSpawner animalSpawner = null;

        private SoundManager soundManager = null;

        private void Start()
        {
            soundManager = FindObjectOfType<SoundManager>();
            animalSpawner = GameObject.Find("AnimalSpawner").GetComponent<AnimalSpawner>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            Path_Point axis = path_comp.GetPathPoint(0);
            transform.position = axis.point;
            parent = transform.parent.gameObject;
        }

        void LateUpdate()
        {
            if(distanceTravelled > 1f)
            {
                Destroy(parent);
                animalSpawner.OwlDied();
                soundManager.StopSound("Owl");
            }

            moveTimer += Time.deltaTime;
            if(moveTimer > moveIntervall)
            {
                moveTimer = 0;

                distanceTravelled = distanceTravelled + distancePerMoveIntervall;
                Mathf.Clamp(distanceTravelled, 0f, 1f);
                // This made it based on the paritcle speed
                float dist = distanceTravelled;

                dist = dist * path_comp.TotalDistance;

                Path_Point axis = path_comp.GetPathPoint(dist);
                transform.position = axis.point;
            }

            if (visibleDuration > 0.0f)
            {
                Color spriteColor = spriteRenderer.color;
                spriteColor.a = 1f;
                spriteRenderer.color = spriteColor;
                visibleDuration -= Time.deltaTime;
            }
            else
            {
                Color spriteColor = spriteRenderer.color;
                spriteColor.a = 0f;
                spriteRenderer.color = spriteColor;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Echo"))
            {
                visibleDuration = maxVisibleDuration;
            }

            if(collision.CompareTag("Bat"))
            {
                Destroy(parent);
                animalSpawner.OwlDied();
                soundManager.StopSound("Owl");
            }
        }
    }
}