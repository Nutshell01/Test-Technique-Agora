using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShapes : MonoBehaviour //gère le changement de sprite des éléments de l'UI
{
    #region SerializeField

    [SerializeField] Sprite _greySprite;
    [SerializeField] Sprite _yellowSprite;

    #endregion

    #region Private

    Image _image;

    #endregion

    private void Start()
    {
        _image = GetComponent<Image>();
    }
    
    public void LightSprite()
    {
        _image.sprite = _yellowSprite;
    }

    public void UnlightSprite()
    {
        _image.sprite = _greySprite;
    }
    
    
}
