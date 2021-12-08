using UnityEngine;

namespace controllers
{
    public class subPlotController : MonoBehaviour
    {
        private Tree tree;

        public GameObject child;
    
        private SpriteRenderer spriteRenderer;

        public bool seeded = false;

        private float timer = 0;

        private int currentState = 0;
        // Start is called before the first frame update
        void Start()
        {
            spriteRenderer = child.gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = null;
        }
    

        // Update is called once per frame
        void Update()
        {
            if (seeded) 
            {
                timer = timer + Time.deltaTime;
                if (timer > 5)
                {
                    switch (currentState)
                    {
                        case 0:
                            spriteRenderer.sprite = tree.seedlingSprite;
                            break;
                        case 1:
                            spriteRenderer.sprite = tree.saplingSprite;
                            break;
                        case 2:
                            spriteRenderer.sprite = tree.treeSprite;
                            break;
                        case 3:
                            spriteRenderer.sprite = tree.ancientSprite;
                            break;
                        case 4:
                            spriteRenderer.sprite = tree.deadSprite;
                            break;
                    }

                    currentState = currentState + 1;
                    timer = 0;
                }
            }    
        }

        public int getCurrentState()
        {
            return currentState;
        }

        public void SetTree(Tree t)
        {
            tree = t;
            seeded = true;
            Debug.Log("Tree Planted");

        }
    }
}
