using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour //permet la détection d'objet dans lors du placement, ainsi que
                                             //le changement de material.
{
    #region SerializeField

    [SerializeField] Material _canPlaceMat;
    [SerializeField] Material _cannotPlaceMat;

    #endregion

    #region Public

    public bool _canPlace = true;

    #endregion

    private void Start()
    {
        if(gameObject.tag == "Placed")
        {
            _canPlaceMat = GetComponent<Renderer>().material;
        }
    }

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

    public void ChangeDefaultMaterialOnClick()
    {
        _canPlaceMat = GetComponent<Renderer>().material;
    }

    

}
