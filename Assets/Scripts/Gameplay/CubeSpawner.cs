using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnerPos;
    [SerializeField] private float delay;
    [SerializeField] public int numCubes;
    [SerializeField] private float spawnRange = 0.5f; // Rango máximo para considerar que hay un cubo en la posición del spawner

    private List<Vector3> spawnedCubePositions = new List<Vector3>();

    void Start()
    {
        StartCoroutine(Generator());
    }

    IEnumerator Generator()
    {
        for (int i = 0; i < numCubes; i++)
        {
            int randomIndex = Random.Range(0, spawnerPos.Length);
            Vector3 spawnPosition = spawnerPos[randomIndex].transform.position;

            // Verificar si hay un cubo en la posición del spawner
            if (!CheckForCubeAtSpawner(spawnPosition))
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = spawnPosition;
                cube.transform.localScale = spawnerPos[randomIndex].transform.localScale;
                cube.tag = "Cube";
                cube.AddComponent<CubeDestroyer>();

                Debug.Log("Spawner activado: " + randomIndex);

                spawnedCubePositions.Add(spawnPosition); // Registrar la posición del cubo generado

                yield return new WaitForSeconds(delay);
            }
            else
            {
                i--; // Si ya hay un cubo en este spawner, intentar de nuevo
            }

            yield return null;
        }
    }

    public void RemoveSpawnedCubePosition(Vector3 position)
    {
        spawnedCubePositions.Remove(position); // Eliminar la posición del cubo de la lista
    }

    private bool CheckForCubeAtSpawner(Vector3 spawnerPosition)
    {
        foreach (Vector3 cubePosition in spawnedCubePositions)
        {
            if (Vector3.Distance(spawnerPosition, cubePosition) <= spawnRange)
            {
                return true; // Hay un cubo dentro del rango de spawn
            }
        }

        return false; // No hay cubo dentro del rango de spawn
    }
}
