using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras;
    private int currentCameraIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (cameras.Length > 0)
        {
            EnableCamera(currentCameraIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SwitchToNextCamera()
    {
        
    }
    
    private void EnableCamera(int index)
    {
        cameras[index].enabled = true;
    }

    private void onTriggerEnter()
    {
        
    }
    
}
