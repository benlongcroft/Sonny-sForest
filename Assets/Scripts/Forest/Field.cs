using System;
using System.Collections.Generic;
using UnityEngine;

namespace Forest
{
    public class Field : MonoBehaviour
    {
        public int fieldID;
        public Plot[] plots = { };
        public GameObject smoke;
        public bool unlocked { private set; get; }
        public void Awake()
        {
            if (fieldID > 0)
            {
                SetNotActive();
            }
            else
            {
                SetActive();
            }
        }

        public void SetActive()
        {
            foreach (var p in plots)
            {
                p.gameObject.SetActive(true);
            }
            Destroy(this.GetComponent<PolygonCollider2D>());
            foreach (var s in smoke.GetComponentsInChildren<ParticleSystem>())
            {
                s.gameObject.SetActive(false);
                s.Stop();
            }

            unlocked = true;
        }
        
        public void SetNotActive()
        {
            foreach (var p in plots)
            {
                p.gameObject.SetActive(false);
                foreach (var s in smoke.GetComponentsInChildren<ParticleSystem>())
                {
                    s.gameObject.SetActive(true);
                    s.Play();
                }
            }

            unlocked = false;
        }
    }


}
