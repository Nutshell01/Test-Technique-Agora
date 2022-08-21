using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour //Permet de changer le material de l'objet
{
    #region SerializeField 

    [SerializeField] Material[] _placedMaterials;
    [SerializeField] Material[] _glowMaterials;

    #endregion

    #region SerializeField

    Renderer _objectRenderer;
    int _currentMaterialIndex;
    public bool _isGlowing;

    #endregion
    void Start()
    {
        _objectRenderer = GetComponent<Renderer>();
        _objectRenderer.material = _placedMaterials[0];
    }
    private void Update()
    {
        if (_isGlowing)
            GlowMaterial();
        else
            UnglowMaterial();
    }

    public void GlowMaterial()
    {
        _objectRenderer.material = _glowMaterials[_currentMaterialIndex];
    }
    public void UnglowMaterial()
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
