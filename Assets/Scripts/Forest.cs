
using System.Collections.Generic;

public class Forest 
{
    private List<Field> fields;
    public Forest(string json_path)
    {
        if (json_path != null)
        {
            Forest forest = gsoController.fromJSON(json_path);
            fields = forest.fields;
        }
        else
        {
            fields = new List<Field> {new Field(true), new Field(true)};
        }
    }
}
