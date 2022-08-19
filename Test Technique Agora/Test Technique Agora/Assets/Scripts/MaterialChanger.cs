using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    [SerializeField] Material[] _placedMaterials;
    Renderer _objectRenderer;
    int _currentMaterialIndex;
    void Start()
    {
        _objectRenderer = GetComponent<Renderer>();
        _objectRenderer.material = _placedMaterials[0];
    }

    // Update is called once per frame
    void Update()
    {
        _objectRenderer.material = _placedMaterials[_currentMaterialIndex];
    }
    public void ChangeMaterial()
    {
        _currentMaterialIndex++;
        if(_currentMaterialIndex > 2)
        {
            _currentMaterialIndex = 0;
        }

    }

}
