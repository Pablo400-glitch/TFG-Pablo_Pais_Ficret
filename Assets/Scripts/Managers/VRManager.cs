using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Management;


public class VRManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    private bool isVRActive = false;
    public RestartScene restartGame;

    void OnEnable()
    {
        if (actionAsset != null)
        {
            actionAsset.Enable();
        }
    }

    void OnDisable()
    {
        if (actionAsset != null)
        {
            actionAsset.Disable();
        }
    }

    public void OnEnableXR(InputAction.CallbackContext context)
    {
        if (!isVRActive)
        {
            StartCoroutine(StartXR());
            isVRActive = true;
            EventManager.StartGameEvent(true);
        }
    }

    public void OnDisableXR(InputAction.CallbackContext context)
    {
        if (isVRActive)
        {
            StopXR();
            isVRActive = false;
            restartGame.RestartGame();
        }
    }

    void OnApplicationQuit()
    {
        StopXR();
    }

    public IEnumerator StartXR()
    {
        Debug.Log("Initializing XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
        }
        else
        {
            Debug.Log("Starting XR...");
            XRGeneralSettings.Instance.Manager.StartSubsystems();
        }
    }

    public void StopXR()
    {
        Debug.Log("Stopping XR...");

        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("XR stopped completely.");
    }
}
