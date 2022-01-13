using System;
using System.Collections.Generic;
using UnityEngine;

namespace Forest
{
    public class Field : MonoBehaviour
    {
        public int fieldID;
        public Plot[] plots = { };
        public GameObject inactiveSmoke;

        public void Awake()
        {
            //not working atm!
            if (fieldID > 0)
            {
                inactiveSmoke.SetActive(true);
                inactiveSmoke.GetComponent<ParticleSystem>().Play();
            }
        }
        
    }
    

}
