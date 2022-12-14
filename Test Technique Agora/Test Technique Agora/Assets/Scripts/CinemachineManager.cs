using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineManager : MonoBehaviour //G?re les mouvements de cam?ra
{
    #region SerializeField

    [SerializeField] GameObject _frontCam;
    [SerializeField] GameObject _topCam;

    [Space]

    [SerializeField] GameObject _frontHolder;
    [SerializeField] GameObject _topHolder;

    [Space]

    [SerializeField] UIShapes _povButton;

    #endregion

    #region Private

    bool _topView;

    #endregion
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            _topView = !_topView;

            if(_topView)
                _povButton.LightSprite();
            else
                _povButton.UnlightSprite();
        }

        if(_topView)
        {
            _frontCam.SetActive(false);
            _topCam.SetActive(true);
            RotateCam(_topHolder);
            StartCoroutine(ResetCamera(_frontHolder));
            
        }
        else
        {
            _frontCam.SetActive(true);
            _topCam.SetActive(false);
            RotateCam(_frontHolder);
            StartCoroutine(ResetCamera(_topHolder));
            
        }
    }
    void RotateCam(GameObject camToRotate)
    {
        camToRotate.transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
    }

    public IEnumerator ScreenShake(CinemachineVirtualCamera camToShake, float shakeDuration, float shakeForce)
    {
        camToShake.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = shakeForce;
        yield return new WaitForSeconds(shakeDuration);
        camToShake.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
    
    IEnumerator ResetCamera(GameObject camToReset)
    {
        yield return new WaitForSeconds(0.50f);
        camToReset.transform.rotation = Quaternion.identity;
    }
}
