using Forest;
using Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class CharController : MonoBehaviour
    {
        public InventorySystem myInventory;
        private int _inventorySelected = 0;
        private GameObject _loadout;
        private Rigidbody2D _rigidbody2d;
        public Field[] myForest = { };
        Animator m_Animator;

        private Vector2 lookDirection = new Vector2(0,0);
        public float speed = 1.8f;
        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        private static readonly int Speed = Animator.StringToHash("Speed");

        private void CheckNewSeeds()
        {
            if (myInventory.Inventory.Count == 0)
            {
                _inventorySelected += 1;
                SetLoadOut(myInventory.Inventory[_inventorySelected]);
            }
            else
            {
                LoadOut.Instance.SetQuantity(myInventory.Inventory[_inventorySelected].StackSize); 
            }
        }

        public static void SetLoadOut(InventoryItem item)
        {
            LoadOut.Instance.SetSprite(item.Data.GetSpriteIcon());
            LoadOut.Instance.SetQuantity(item.StackSize);
            LoadOut.Instance.SetItemName(item.Data.displayName);
        }
        
        private void ChangeLoadOut()
        {
            if (_inventorySelected != myInventory.Inventory.Count)
            {
                var item = myInventory.Inventory[_inventorySelected];
                SetLoadOut(item);
                if (item.Data.displayName != "Money")
                {
                    Destroy(_loadout);
                    _loadout = Instantiate(myInventory.Inventory[_inventorySelected].Data.prefab);
                    _loadout.name = "loadout";
                    _loadout.SetActive(false);   
                }
            }
            else
            {
                MakeLoadoutNull();

            }
        }

        private void MakeLoadoutNull()
        {
            _loadout = null;
            LoadOut.Instance.SetSprite(null);
            LoadOut.Instance.SetQuantity(0);
            LoadOut.Instance.SetItemName("Empty");
            _inventorySelected = -1;
        }
    
        // Start is called before the first frame update
        void Start()
        {
            var cwd = System.IO.Directory.GetCurrentDirectory();
            // myForest = GSOController.ReadForest(cwd, myForest);
            _rigidbody2d = GetComponent<Rigidbody2D>();
            m_Animator = GetComponent<Animator>();
            ChangeLoadOut();
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            {
                lookDirection.Set(move.x, move.y);
                lookDirection.Normalize();
            }
            
            
            m_Animator.SetFloat(MoveX, lookDirection.x);
            m_Animator.SetFloat(MoveY, lookDirection.y);
            m_Animator.SetFloat(Speed, move.magnitude);
            // m_Animator.SetFloat("Speed", move.magnitude);

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (_inventorySelected+1 != myInventory.Inventory.Count)
                {
                    _inventorySelected += 1;
                }
                else
                {
                    _inventorySelected = 0;
                }
                ChangeLoadOut();
            }

            if(Input.GetKeyDown(KeyCode.P))
            {
                var hit = Physics2D.Raycast(_rigidbody2d.position + Vector2.up * 0.2f, Vector2.zero, 1.5f, LayerMask.GetMask("plots"));
                if (hit.collider == null) return;
                var objHit = hit.collider.gameObject.GetComponent<Plot>();
                if (_loadout == null) return;
                var isSeed = _loadout.GetComponent<TreeController>();
                if (isSeed != null && myInventory.Inventory[_inventorySelected].StackSize != 0)
                {
                    var success = objHit.ChoosePlot(isSeed);
                    Debug.Log("PLOTID: "+objHit.plotID);
                    if (success == -1)
                    {
                        Debug.Log("Plot is FULL!");
                    }
                    else
                    {
                        myInventory.Inventory[_inventorySelected].RemoveFromStack();
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
            }
            
            if (Input.GetKeyDown(KeyCode.C))
            {
                var hit = Physics2D.Raycast(_rigidbody2d.position + Vector2.up * 0.2f, Vector2.zero, 1.5f, LayerMask.GetMask("plots"));
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
            Vector2 position = _rigidbody2d.position;
            position.x = position.x + speed * horizontal * Time.deltaTime;
            position.y = position.y + speed * vertical * Time.deltaTime;
            _rigidbody2d.MovePosition(position);
        }
    }
}
