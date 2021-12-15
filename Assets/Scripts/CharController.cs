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
        public Field[] myForest = {};

        private void SetInventory()
        {
            LoadOut.Instance.SetSprite(myInventory.Inventory[_inventorySelected].Data.GetSpriteIcon());
            LoadOut.Instance.SetQuantity(myInventory.Inventory[_inventorySelected].StackSize);
            _loadout = myInventory.Inventory[_inventorySelected].Data.prefab;
        }
    
        // Start is called before the first frame update
        void Start()
        {
            var cwd = System.IO.Directory.GetCurrentDirectory();
            myForest = GSOController.ReadForest(cwd, myForest);
            _rigidbody2d = GetComponent<Rigidbody2D>();
            SetInventory();
        }

        // Update is called once per frame
        void Update()
        { 
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var transform1 = transform;
            Vector2 position = transform1.position;
            position.x += 0.4f * horizontal * Time.deltaTime;
            position.y += 0.4f * vertical * Time.deltaTime;
            transform1.position = position;

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
    }
}
