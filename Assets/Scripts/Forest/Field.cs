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

        public void Awake()
        {
            if (fieldID > 0)
            {
                setNotActive();
            }
            else
            {
                setActive();
            }
        }

        public void setActive()
        {
            foreach (Plot p in plots)
            {
                p.gameObject.SetActive(true);
            }
            Destroy(this.GetComponent<PolygonCollider2D>());
            foreach (ParticleSystem s in smoke.GetComponentsInChildren<ParticleSystem>())
            {
                s.gameObject.SetActive(false);
                s.Stop();
            }
        }
        
        public void setNotActive()
        {
            foreach (Plot p in plots)
            {
                p.gameObject.SetActive(false);
                foreach (ParticleSystem s in smoke.GetComponentsInChildren<ParticleSystem>())
                {
                    s.gameObject.SetActive(true);
                    s.Play();
                }
            }
        }
    }


}
