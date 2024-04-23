using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectRepeater : MonoBehaviour
{
    public enum Plane
    {
        XY, XZ, YZ
    }

    public Plane plane = Plane.XZ;
    public int rows;
    public int cols;
    public GameObject repeatedObject;
    public float spacing = 1.0f;


    void Start()
    {   
        Debug.Log("Started");
        Vector3 place = new Vector3(repeatedObject.transform.position.x, repeatedObject.transform.position.y, repeatedObject.transform.position.z);
        Vector3 startingPoint = new Vector3(repeatedObject.transform.position.x, repeatedObject.transform.position.y, repeatedObject.transform.position.z);
        Quaternion orientation = repeatedObject.transform.rotation;
        for (int row = 0; row < rows; row++)
        {
            if (plane != Plane.XY)
            {
                place.z = startingPoint.z + spacing * (row - (rows - 1) / 2f);
            } 
            else
            {
                place.x = startingPoint.x + spacing * (row - (rows - 1) / 2f);
            }
            //Debug.Log(row);
            for (int col = 0; col < cols; col++)
            {
                if (plane != Plane.XZ)
                {
                    place.y = startingPoint.y + spacing * (col - (cols - 1) / 2f);
                }
                else
                {
                    place.x = startingPoint.x + spacing * (col - (cols - 1) / 2f);
                }
                
                Instantiate<GameObject>(repeatedObject, place, orientation);
            }
        }

        Destroy(repeatedObject);


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
