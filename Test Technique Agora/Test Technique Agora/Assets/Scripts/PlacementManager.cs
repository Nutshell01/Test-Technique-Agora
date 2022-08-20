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
    [SerializeField] UIShapes[] _shapeImages;
    [SerializeField] UIShapes[] _placeModeImage;
    [SerializeField] Canvas _shapeCanvas;

    [SerializeField] LayerMask _groundLayer;
    [SerializeField] LayerMask _placedLayer;

    float _yOffset;
    [SerializeField] float _scrollSpeed;

    bool _placeMode;
    bool _taken;
    bool _snap;

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
        ToggleRotationSnap();
        Debug.Log(_snap);
     
    }

    void ToggleRotationSnap()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            _snap = !_snap;
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
                hit.collider.gameObject.GetComponent<DetectCollision>().ChangeDefaultMaterialOnClick();
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
                _shapeCanvas.enabled = true;
                ChangeSpriteShapes(0, _placeModeImage);
            }
            else
            {
                ChangeSpriteShapes(0, _shapeImages);
                ChangeSpriteShapes(1, _placeModeImage);
                _shapeCanvas.enabled = false;
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
           

            if(!_snap)
            {
                _objectLocation.transform.Rotate(0, Input.mouseScrollDelta.y * _scrollSpeed , 0); 
                _objectLocation.transform.position = hit.point += new Vector3(0, _yOffset, 0);
            }
            else
            {
                int YRotation = 0;

                if(Input.mouseScrollDelta.y > 0)
                {
                    YRotation = YRotation + 45;
                    _objectLocation.transform.Rotate(0, YRotation, 0);
                }
                else if(Input.mouseScrollDelta.y < 0)
                {
                    YRotation = YRotation - 45;
                    _objectLocation.transform.Rotate(0, YRotation, 0);
                }
                _objectLocation.transform.position =Vector3Int.RoundToInt(hit.point += new Vector3(0, _yOffset, 0));

            }
            


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
            ChangeSpriteShapes(0, _shapeImages);
            DestroyOldWaypoint(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _objectToPlace = _objectsToPlace[1];
            ChangeSpriteShapes(1, _shapeImages);
            DestroyOldWaypoint(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _objectToPlace = _objectsToPlace[2];
            ChangeSpriteShapes(2, _shapeImages);
            DestroyOldWaypoint(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _objectToPlace = _objectsToPlace[3];
            ChangeSpriteShapes(3, _shapeImages);
            DestroyOldWaypoint(3);
        }
    }

    void ChangeSpriteShapes(int spriteNumber, UIShapes[] spriteArrayToChange)
    {
        for (int i = 0; i < spriteArrayToChange.Length ; i++)
        {
            spriteArrayToChange[i].UnlightSprite();
        }
        spriteArrayToChange[spriteNumber].LightSprite();

    }


    

}
