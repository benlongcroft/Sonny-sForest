using UnityEngine;

namespace controllers
{
    public class CharController : MonoBehaviour
    {
        private GameObject _loadout;
        Rigidbody2D rigidbody2d;
        public Field[] myForest = {};

        // Start is called before the first frame update
        void Start()
        {
            string cwd = System.IO.Directory.GetCurrentDirectory();
            gsoController.getForestFromJson(cwd, myForest);
            rigidbody2d = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        { 
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 position = transform.position;
            position.x = position.x + 0.4f * horizontal * Time.deltaTime;
            position.y = position.y + 0.4f * vertical * Time.deltaTime;
            transform.position = position;

            if (Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, Vector2.zero, 1.5f,
                    LayerMask.GetMask("seeds"));
                if (hit.collider != null)
                {
                    GameObject objHit = hit.collider.gameObject;
                    _loadout = objHit;
                    LoadOut.instance.SetSprite(_loadout.GetComponent<SpriteRenderer>().sprite);
                    objHit.SetActive(false); // Destroy doesnt work so have to use this but need to garbage collect
                }
                
            }
        
            if(Input.GetKeyDown(KeyCode.P))
            {
                RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, Vector2.zero, 1.5f, LayerMask.GetMask("plots"));
                if (hit.collider != null)
                {
                    Debug.Log("Collision!");
                    Plot objHit = hit.collider.gameObject.GetComponent<Plot>();
                    Debug.Log(_loadout);
                    if (_loadout != null)
                    {
                        treeController isSeed = _loadout.GetComponent<treeController>();
                        if (isSeed != null)
                        {
                            int success = objHit.choosePlot(isSeed);
                            if (success == -1)
                            {
                                Debug.Log("Plot is FULL!");
                            }
                            else
                            {
                                _loadout = null;
                                LoadOut.instance.SetSprite(null);
                            }
                        }
                        else
                        {
                            Debug.Log("No Seed Equipped");
                        }
                    }
                }
            }
        }
    }
}
