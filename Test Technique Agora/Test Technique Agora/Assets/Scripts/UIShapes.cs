using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShapes : MonoBehaviour
{
    [SerializeField] Sprite _greySprite;
    [SerializeField] Sprite _yellowSprite;

   
    Image _image;

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
