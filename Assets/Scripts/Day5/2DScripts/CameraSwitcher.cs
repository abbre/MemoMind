using System.Collections;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras;
    public int currentCameraIndex = 0;
    [CanBeNull] [SerializeField] private GameObject pilot3Sprite;

    private void OnEnable()
    {
        if (pilot3Sprite)
            pilot3Sprite.SetActive(false);
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
        pilot3Sprite.SetActive(true);
        cameras[currentCameraIndex].enabled = true;
        cameras[currentCameraIndex - 1].enabled = false;
    }
}