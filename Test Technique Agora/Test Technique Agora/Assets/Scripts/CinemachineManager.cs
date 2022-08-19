using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    bool _topView;
    [SerializeField] GameObject _frontCam;
    [SerializeField] GameObject _topCam;

    [SerializeField] GameObject _frontHolder;
    [SerializeField] GameObject _topHolder;
 
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            _topView = !_topView;
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
    
    IEnumerator ResetCamera(GameObject camToReset)
    {
        yield return new WaitForSeconds(0.50f);
        camToReset.transform.rotation = Quaternion.identity;
    }
}
