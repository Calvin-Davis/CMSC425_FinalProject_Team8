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
        //Debug.Log("Started");
        //this script allows for quick and easy duplication of an object and was inspired
        //by the inchworm board
        //Takes in how many collumns, rows and the space wanted between the centers of 
        //the objects

        //variable tracks where to place current obj
        Vector3 place = new Vector3(repeatedObject.transform.position.x, repeatedObject.transform.position.y, repeatedObject.transform.position.z);
        //tracks where the first obj should be centered
        Vector3 startingPoint = new Vector3(repeatedObject.transform.position.x, repeatedObject.transform.position.y, repeatedObject.transform.position.z);
        //make sure all copies are rotated the same way (not really needed to be stored in
        //variable unless we want to add some rotations in the repititions later)
        Quaternion orientation = repeatedObject.transform.rotation;
        //places objs
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
