using UnityEngine;

namespace Forest
{
    public class Field : MonoBehaviour
    {
        /*
         * Field is a class that contains all subplots,
         * applies smoke particle system when locked
         * Can be unlocked in shop.
         */
        public int fieldID;
        public Plot[] plots = { };
        public GameObject smoke;
        public bool Unlocked { private set; get; }
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
        /*
         * Sets a field as Active and ready to be planted in
         */
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

            Unlocked = true;
        }
        
        public void SetNotActive()
        /*
         * Sets a fields as Locked and covers it with smoke particle system.
         * It blocks movement to this field from Sonny.
         */
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

            Unlocked = false;
        }
    }


}
