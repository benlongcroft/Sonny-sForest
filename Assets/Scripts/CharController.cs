using Forest;
using Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class CharController : MonoBehaviour
    {
        public InventorySystem myInventory;
        private int m_InventorySelected = 1;
        private GameObject m_Loadout;
        private Rigidbody2D m_Rigidbody2d;
        public Field[] myForest = { };
        Animator m_Animator;

        private Vector2 m_LookDirection = new Vector2(0,0);
        public float speed = 1.8f;
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void CheckNewSeeds()
        /*
         * Check to see if any new seeds have been dropped 
         */
        {
            if (myInventory.Inventory.Count == 0)
            {
                m_InventorySelected += 1;
                SetLoadOut(myInventory.Inventory[m_InventorySelected]);
            }
            else
            {
                LoadOut.Instance.SetQuantity(myInventory.Inventory[m_InventorySelected].StackSize); 
            }
        }

        public static void SetLoadOut(InventoryItem item)
        {
            /*
             * Set LoadOut to most current inventory
             */
            LoadOut.Instance.SetSprite(item.Data.GetSpriteIcon());
            LoadOut.Instance.SetQuantity(item.StackSize);
            LoadOut.Instance.SetItemName(item.Data.displayName);
        }
        
        private void ChangeLoadOut()
        /*
         * Change the loadout with tab
         */
        {
            if (m_InventorySelected != myInventory.Inventory.Count)
            {
                var item = myInventory.Inventory[m_InventorySelected];
                SetLoadOut(item);
                if (item.Data.displayName != "Money")
                {
                    Destroy(m_Loadout);
                    m_Loadout = Instantiate(myInventory.Inventory[m_InventorySelected].Data.prefab);
                    m_Loadout.name = "loadout";
                    m_Loadout.SetActive(false);   
                }
            }
            else
            {
                MakeLoadoutNull();

            }
        }

        private void MakeLoadoutNull()
        {
            m_Loadout = null;
            LoadOut.Instance.SetSprite(null);
            LoadOut.Instance.SetQuantity(0);
            LoadOut.Instance.SetItemName("Empty");
            m_InventorySelected = -1;
        }
    
        // Start is called before the first frame update
        void Start()
        {
            Application.targetFrameRate = -1;
            var cwd = System.IO.Directory.GetCurrentDirectory();
            // myForest = GSOController.ReadForest(cwd, myForest);
            m_Rigidbody2d = GetComponent<Rigidbody2D>();
            m_Animator = GetComponent<Animator>();
            ChangeLoadOut();
        }

        // Update is called once per frame
        void Update()
        {
            //move Sonny
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            {
                m_LookDirection.Set(move.x, move.y);
                m_LookDirection.Normalize();
            }
            
            
            //animator movement settings
            m_Animator.SetFloat(MoveX, m_LookDirection.x);
            m_Animator.SetFloat(MoveY, m_LookDirection.y);
            m_Animator.SetFloat(Speed, move.magnitude);
            // m_Animator.SetFloat("Speed", move.magnitude);
            
            //Change Inventory
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (m_InventorySelected+1 != myInventory.Inventory.Count)
                {
                    m_InventorySelected += 1;
                }
                else
                {
                    m_InventorySelected = 1;
                }
                ChangeLoadOut();
            }
            
            
            //Plant loadout
            if(Input.GetKeyDown(KeyCode.P))
            {
                var hit = Physics2D.Raycast(m_Rigidbody2d.position + Vector2.up * 0.2f, Vector2.zero, 1.5f, LayerMask.GetMask("plots"));
                if (hit.collider == null) return;
                var objHit = hit.collider.gameObject.GetComponent<Plot>();
                if (m_Loadout == null) return;
                var isSeed = m_Loadout.GetComponent<TreeController>();
                if (isSeed != null && myInventory.Inventory[m_InventorySelected].StackSize != 0)
                {
                    var success = objHit.ChoosePlot(isSeed);
                    Debug.Log("PLOTID: "+objHit.plotID);
                    if (success == -1)
                    {
                        Debug.Log("Plot is FULL!");
                    }
                    else
                    {
                        myInventory.Inventory[m_InventorySelected].RemoveFromStack();
                        ChangeLoadOut();
                    }
                }
                else
                {
                    Debug.Log("No Seed Equipped");
                }
            }
            //must be continually checked to see if seeds have been dropped
            CheckNewSeeds();
            
            //Cheat Code
            if (Input.GetKeyDown(KeyCode.C) && Input.GetKeyDown(KeyCode.M))
            {
                //cheat code for God mode
                foreach (var item in myInventory.Inventory)
                {
                    for (var _ = 0; _ < 999; _++)
                    {
                        item.AddToStack();
                    }
                }
                LoadOut.Instance.SetBalance(myInventory.Inventory[0].StackSize);
            }
            
            //Chop Down Tree
            if (Input.GetKeyDown(KeyCode.C))
            {
                var hit = Physics2D.Raycast(m_Rigidbody2d.position + Vector2.up * 0.2f, Vector2.zero, 1.5f, LayerMask.GetMask("plots"));
                if (hit.collider == null) return;
                var objHit = hit.collider.gameObject.GetComponent<Plot>();
                var success = objHit.ChopDownTree();
                if (success == -1)
                {
                    Debug.Log("No Tree to chop!");
                }
            }
        }
        
        void FixedUpdate()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            Vector2 position = m_Rigidbody2d.position;
            position.x = position.x + speed * horizontal * Time.deltaTime;
            position.y = position.y + speed * vertical * Time.deltaTime;
            m_Rigidbody2d.MovePosition(position);
        }
    }
}
