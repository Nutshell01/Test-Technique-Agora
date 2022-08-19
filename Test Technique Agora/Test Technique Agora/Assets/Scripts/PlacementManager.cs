using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] GameObject _objectLocation;
    [SerializeField] GameObject _currentLocationMesh;
    [SerializeField] GameObject _objectToPlace;
    [SerializeField] GameObject[] _objectsToPlace;
    [SerializeField] GameObject[] _locationMesh;

    [SerializeField] LayerMask _groundLayer;
    [SerializeField] LayerMask _placedLayer;

    float _yOffset;
    [SerializeField] float _scrollSpeed;

    bool _placeMode;
    bool _taken;
    GameObject _takenObject;
    Vector3 _objectOrigin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_placeMode)
        {
            ChangeObjectType();
            PlaceObject();
        }
        if(!_placeMode)
        {
            Destroy(_currentLocationMesh);
            DetectPlacedObject();
        }
        if(!_taken)
        {
            TogglePlaceMode();
        }
        
        
     
    }


    void DestroyOldWaypoint(int meshIndex)
    {
        Destroy(_currentLocationMesh);
        GameObject meshToShow = GameObject.Instantiate(_locationMesh[meshIndex], _objectLocation.transform);
        _currentLocationMesh = meshToShow;

    }
    void DetectPlacedObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        
        

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _placedLayer))
        {
            if(Input.GetButtonDown("Fire2"))
            {
                hit.collider.gameObject.GetComponent<MaterialChanger>().ChangeMaterial();
            }
            if(Input.GetButtonDown("Fire1"))
            {
                
                _takenObject = hit.collider.gameObject;
                _objectOrigin = _takenObject.transform.position;
                _taken = true;
            }
            
           


        }
        if (_taken)
        {
            
            _takenObject.GetComponent<MaterialChanger>().enabled = false;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundLayer))
            {
                _yOffset = _takenObject.GetComponent<ObjectInfo>()._offsetNeeded;
                _takenObject.transform.position = hit.point += new Vector3(0, _yOffset, 0);
                _takenObject.transform.Rotate(0, Input.mouseScrollDelta.y * _scrollSpeed, 0);

                if (Input.GetKeyDown(KeyCode.Delete))
                {
                    Destroy(_takenObject);
                    _taken = false;
                }


            }
            if(Input.GetButtonUp("Fire1"))
            {
                _takenObject.GetComponent<MaterialChanger>().enabled = true;
                _taken = false;

                if (_takenObject.GetComponent<DetectCollision>()._canPlace == false)
                    _takenObject.transform.position = _objectOrigin;
            }
        }
       
    }

    void TogglePlaceMode()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            _placeMode = !_placeMode;

            if (_placeMode)
            {
                GameObject meshToShow = GameObject.Instantiate(_locationMesh[0], _objectLocation.transform);
                _currentLocationMesh = meshToShow;
                _objectToPlace = _objectsToPlace[0];
                _objectLocation.transform.rotation = Quaternion.identity;
            }
           
        }
    }
    void PlaceObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        DetectCollision collisionDetection = _currentLocationMesh.GetComponent<DetectCollision>();

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _groundLayer))
        {
            _objectLocation.SetActive(true);
            _yOffset = _objectToPlace.GetComponent<ObjectInfo>()._offsetNeeded;
            _objectLocation.transform.position = hit.point += new Vector3(0, _yOffset, 0);
            _objectLocation.transform.Rotate(0, Input.mouseScrollDelta.y * _scrollSpeed, 0);


            if (Input.GetButtonDown("Fire1") && collisionDetection._canPlace)
            {
                GameObject.Instantiate(_objectToPlace, _objectLocation.transform.position, 
                    _objectLocation.transform.rotation);
                Debug.Log("object Instance");
            }
        }
        else
        {
            _objectLocation.SetActive(false);
        }
      
    }

    void ChangeObjectType()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _objectToPlace = _objectsToPlace[0];
            DestroyOldWaypoint(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _objectToPlace = _objectsToPlace[1];
            DestroyOldWaypoint(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _objectToPlace = _objectsToPlace[2];
            DestroyOldWaypoint(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _objectToPlace = _objectsToPlace[3];
            DestroyOldWaypoint(3);
        }
    }


    

}
