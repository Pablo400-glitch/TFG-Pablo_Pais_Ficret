using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class GazeManager : MonoBehaviour
{
    [SerializeField] private InputActionReference eyeGazeInput;
    [SerializeField] private InputActionReference isGazeTrackedInput;
    [SerializeField] private LineRenderer combinedRenderer;
    [SerializeField] private GraphicRaycaster graphicRaycaster;
    [SerializeField] private FixationManager fixationManager;

    private void Awake()
    {
        if (combinedRenderer == null)
        {
            Debug.LogError("No LineRenderer component found on this game object. Please add one.");
            return;
        }

        combinedRenderer.positionCount = 2;
    }

    void Update()
    {
        if (eyeGazeInput == null)
        {
            return;
        }

        var gaze = eyeGazeInput.action.ReadValue<PoseState>();

        if (gaze.rotation.x == 0 && gaze.rotation.y == 0 && gaze.rotation.w == 0) return;

        //Debug.Log("Rotation: " + gaze.rotation);
        //Debug.Log("Position: " + gaze.position);

        // OpenXR usa un sistema de referencia -Z forward right-hand, por lo tanto para que el gaze este correcto debemos invertir la Z para que este en +Z
        Vector3 invertZ = new Vector3(1.0f, 1.0f, -1.0f);
        Matrix4x4 gazeToCameraMatrix = Matrix4x4.TRS(gaze.position, new Quaternion(-gaze.rotation.x, gaze.rotation.y, gaze.rotation.z, gaze.rotation.w), invertZ);
        Matrix4x4 gazeToWorldMatrix = Camera.main.transform.localToWorldMatrix * gazeToCameraMatrix;

        var start = Vector3.zero;
        var end = Vector3.forward * 100;

        Vector3 worldStart = gazeToWorldMatrix.MultiplyPoint3x4(start);
        Vector3 worldEnd = gazeToWorldMatrix.MultiplyPoint3x4(end);

        RaycastCubeDestroyer(worldStart, worldEnd);

        RaycastUI(worldStart, worldEnd);

        combinedRenderer.SetPosition(0, worldStart);
        combinedRenderer.SetPosition(1, worldEnd);
    }

    private void RaycastUI(Vector3 worldStart, Vector3 worldEnd)
    {
        Ray ray = new Ray(Camera.main.transform.position, worldEnd - worldStart);

        PointerEventData eventData = new PointerEventData(null);
        eventData.position = Camera.main.WorldToScreenPoint(worldEnd);
        var results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);

        if (results.Count > 0)
        {
            foreach (var result in results)
            {
                GameObject hitObject = result.gameObject;
                fixationManager.Fixation(hitObject);
            }
        }
    }

    void RaycastCubeDestroyer(Vector3 worldStart, Vector3 worldEnd)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.transform.position, worldEnd - worldStart, out hitInfo))
        {
            GameObject hitObject = hitInfo.collider.gameObject;
            if (hitObject.tag == Tags.CUBE)
            {
                StartCoroutine(DestroyGameObject(hitObject, 1));
            }
        }
    }

    private IEnumerator DestroyGameObject(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
}
