using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, new Vector2(1,0), 1.5f, LayerMask.GetMask("plots"));
            if (hit.collider != null)
            {
                subPlotController objHit = hit.collider.gameObject.GetComponent<subPlotController>();
                objHit.FlipSeeded();
                PollutionBar.instance.PlantTree();
            }
        }
    }

    void Plant(PlotController plot)
    {
        //How do we get the plot that the user hit and make that the plot that is assigned to assign a subplot to grow
        // GameObject plotObj = Instantiate(plotPrefab, rigidbody2d.position+Vector2.up*1.2f, Quaternion.identity);
        // PlotController p = plot.GetComponent<PlotController>();
        plot.growOne();
        PollutionBar.instance.PlantTree();
        // TreeController tree = treeObj.GetComponent<TreeController>();
        // tree.Plant();
    }
}
