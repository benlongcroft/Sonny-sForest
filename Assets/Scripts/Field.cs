using System.Collections.Generic;
using UnityEngine;

class Field : MonoBehaviour
{
    public List<Plot> Plots = new List<Plot>
    {
        new Plot(), new Plot(),
        new Plot(), new Plot(),
        new Plot(), new Plot()
    };

    private bool hasDefence;
    private bool empty = true;
    public Field(bool empty)
    {
        this.empty = empty;
    }
    
}
