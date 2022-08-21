using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShapes : MonoBehaviour //g�re le changement de sprite des �l�ments de l'UI
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
