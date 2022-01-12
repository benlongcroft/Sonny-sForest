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

        public void Start()
        {
            //not working atm!
            if (fieldID > 0)
            {
                inactiveSmoke.SetActive(true);
                inactiveSmoke.GetComponent<ParticleSystem>().Play();
                Debug.Log("ID:"+fieldID);
                Debug.Log("Emitting: "+inactiveSmoke.GetComponent<ParticleSystem>().isEmitting);
                Debug.Log("Playing: "+inactiveSmoke.GetComponent<ParticleSystem>().isPlaying);
                Debug.Log("Particle Count: "+inactiveSmoke.GetComponent<ParticleSystem>().particleCount);
            }
        }
        
    }
    

}
