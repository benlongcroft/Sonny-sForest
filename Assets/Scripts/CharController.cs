using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public GameObject treePrefab;
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
            Plant();
        }
    }

    void Plant()
    {
        GameObject treeObj = Instantiate(treePrefab, rigidbody2d.position+Vector2.up*0.5f, Quaternion.identity);
        PollutionBar.instance.PlantTree();
        // TreeController tree = treeObj.GetComponent<TreeController>();
        // tree.Plant();
    }
}
