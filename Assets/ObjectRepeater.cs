using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectRepeater : MonoBehaviour
{
    public int rows;
    public int cols;
    public GameObject repeatedObject;
    public float spacing = 1.0f;


    void Start()
    {   
        Debug.Log("Started");
        Vector3 place = new Vector3(repeatedObject.transform.position.x, repeatedObject.transform.position.y, repeatedObject.transform.position.z);
        Vector3 startingPoint = new Vector3(repeatedObject.transform.position.x, repeatedObject.transform.position.y, repeatedObject.transform.position.z);
        for (int row = 0; row < rows; row++)
        {
            place.z = startingPoint.z + spacing * (row - (rows - 1) / 2f);
            //Debug.Log(row);
            for (int col = 0; col < cols; col++)
            {
                
                place.x = startingPoint.x + spacing * (col - (cols - 1) / 2f);
                
                Instantiate<GameObject>(repeatedObject, place, Quaternion.identity);
            }
        }




        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
