using UnityEngine;


public class CharController : MonoBehaviour
{
    public InventorySystem myInventory;
    private int inventorySelected = 0;
    private GameObject _loadout;
    Rigidbody2D rigidbody2d;
    public Field[] myForest = {};

    private void setInventory()
    {
        LoadOut.instance.SetSprite(myInventory.inventory[inventorySelected].data.GetSpriteIcon());
        LoadOut.instance.setCount(myInventory.inventory[inventorySelected].stackSize);
        _loadout = myInventory.inventory[inventorySelected].data.prefab;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        string cwd = System.IO.Directory.GetCurrentDirectory();
        myForest = gsoController.ReadForest(cwd, myForest);
        rigidbody2d = GetComponent<Rigidbody2D>();
        // Sprite toSet = myInventory.inventory[inventorySelected].data.GetSpriteIcon();
        // LoadOut.instance.SetSprite(toSet);
        setInventory();
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

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventorySelected+1 == myInventory.inventory.Count)
            {
                inventorySelected += 1;
            }
            else
            {
                inventorySelected = 0;
            }
            
            setInventory();
        }
    
        if(Input.GetKeyDown(KeyCode.P))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, Vector2.zero, 1.5f, LayerMask.GetMask("plots"));
            if (hit.collider != null)
            {
                Debug.Log("Collision!");
                var objHit = hit.collider.gameObject.GetComponent<Plot>();
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
                            myInventory.inventory[inventorySelected].RemoveFromStack();
                            setInventory();
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
