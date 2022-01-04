using Forest;
using Inventory;
using UnityEngine;

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
        
        Vector2 lookDirection = new Vector2(1,0);
        public float speed = 1.8f;
        float horizontal;
        float vertical;

        private void SetInventory()
        {
            LoadOut.Instance.SetSprite(myInventory.Inventory[_inventorySelected].Data.GetSpriteIcon());
            LoadOut.Instance.SetQuantity(myInventory.Inventory[_inventorySelected].StackSize);
            _loadout = Instantiate(myInventory.Inventory[_inventorySelected].Data.prefab);
            _loadout.name = "loadout";
            _loadout.SetActive(false);
        }
    
        // Start is called before the first frame update
        void Start()
        {
            var cwd = System.IO.Directory.GetCurrentDirectory();
            myForest = GSOController.ReadForest(cwd, myForest);
            _rigidbody2d = GetComponent<Rigidbody2D>();
            m_Animator = GetComponent<Animator>();
            SetInventory();
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
        
            m_Animator.SetFloat("MoveX", lookDirection.x);
            m_Animator.SetFloat("MoveY", lookDirection.y);
            // m_Animator.SetFloat("Speed", move.magnitude);

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (_inventorySelected+1 == myInventory.Inventory.Count)
                {
                    _inventorySelected += 1;
                }
                else
                {
                    _inventorySelected = 0;
                }
            
                SetInventory();
            }
    
            if(Input.GetKeyDown(KeyCode.P))
            {
                var hit = Physics2D.Raycast(_rigidbody2d.position + Vector2.up * 0.2f, Vector2.zero, 1.5f, LayerMask.GetMask("plots"));
                if (hit.collider == null) return;
                var objHit = hit.collider.gameObject.GetComponent<Plot>();
                if (_loadout == null) return;
                var isSeed = _loadout.GetComponent<TreeController>();
                if (isSeed != null)
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
                        SetInventory();
                    }
                }
                else
                {
                    Debug.Log("No Seed Equipped");
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
