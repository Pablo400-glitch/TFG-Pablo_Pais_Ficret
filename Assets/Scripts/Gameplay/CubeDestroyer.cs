using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    private Points m_Points;
    private CubeSpawner cubeSpawner;
    private Vector3 cubePosition;

    private void Awake()
    {
        // Buscar el GameObject que tiene el componente Points por su nombre
        GameObject pointsGameObject = GameObject.Find("PlayerPointsRegister");
        cubeSpawner = FindObjectOfType<CubeSpawner>(); // Encontrar el componente CubeSpawner
        cubePosition = transform.position;

        // Obtener el componente Points si se encuentra
        if (pointsGameObject != null)
        {
            m_Points = pointsGameObject.GetComponent<Points>(); 
        }
        else
        {
            Debug.LogWarning("Not found the GameObject 'PlayerPointsRegister'");
        }
    }

    private void OnDestroy()
    {
        if (m_Points != null)
        {
            m_Points.AddPoints();
        }
        else
        {
            Debug.LogWarning("The component Points is not Assigned");
        }

        if (cubeSpawner != null)
        {
            cubeSpawner.RemoveSpawnedCubePosition(cubePosition); // Eliminar la posición del cubo de la lista del CubeSpawner
        }
        else
        {
            Debug.LogWarning("CubeSpawner not found.");
        }
    }
}
