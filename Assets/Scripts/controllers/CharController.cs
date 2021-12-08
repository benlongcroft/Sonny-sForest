using UnityEngine;

namespace controllers
{
    public class CharController : MonoBehaviour
    {
        public GameObject plotPrefab;
        Rigidbody2D rigidbody2d;
        // Start is called before the first frame update
        void Start()
        {
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
        
            if(Input.GetKeyDown(KeyCode.P))
            {
                RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, Vector2.zero, 1.5f, LayerMask.GetMask("plots"));
                if (hit.collider != null)
                {
                    Debug.Log("Collision!");
                    //insert select seed routine here
                    Plot objHit = hit.collider.gameObject.GetComponent<Plot>();
                    objHit.choosePlot(new Tree(20, 87, 30, 1, 29, null));
                    // if (objHit.getCurrentState() == 0)
                    // {
                    //     Debug.Log("Tree Planted");
                    //     objHit.FlipSeeded();
                    //     PollutionBar.instance.PlantTree();
                    // }
                }
            }
        }
    }
}
