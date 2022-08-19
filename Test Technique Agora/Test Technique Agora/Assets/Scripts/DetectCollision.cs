using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    [SerializeField] Material _canPlaceMat;
    [SerializeField] Material _cannotPlaceMat;
    [SerializeField] LayerMask _objectMask;
    public bool _canPlace = true;


    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Placed")
        {
            
            GetComponent<Renderer>().material = _cannotPlaceMat;
            _canPlace = false;
            
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Placed")
        {
            
            GetComponent<Renderer>().material = _canPlaceMat;
            _canPlace = true;

        }
    }

    

}
