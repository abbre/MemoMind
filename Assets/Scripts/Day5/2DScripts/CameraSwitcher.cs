using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras;
    public int currentCameraIndex = 0;


    private void OnEnable()
    {
        foreach (CinemachineVirtualCamera camera in cameras)
        {
            camera.enabled = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (cameras.Length > 0)
        {
            EnableCamera(currentCameraIndex);
        }
    }


    public void EnableCamera(int index)
    {
        cameras[index].enabled = true;
    }

    public void DisableCamera(int prevIndex)
    {
        cameras[prevIndex].enabled = false;
    }

    public void StartDelayThenEnableNextCamera()
    {
        StartCoroutine(DelayThenEnableNextCamera());
    }
    public IEnumerator DelayThenEnableNextCamera()
    {
        yield return new WaitForSeconds(2f);
        currentCameraIndex++;
        cameras[currentCameraIndex].enabled = true;
        cameras[currentCameraIndex-1].enabled = false;

    }
}