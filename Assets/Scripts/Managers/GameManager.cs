using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public GameObject finishedText;
    public GameObject restartButton;
    public GameObject mainMenuButton;
    public GameObject closeGameButton;
    public GameObject pointsText;
    public CubeSpawner numberOfCubes;
    public GameObject cubeSpawner;
    public GameObject finishedPanel;
    public GameObject pointsCanvas;

    private void OnEnable()
    {
        EventManager.onPointsUIUpdateEvent += PointsUIUpdateEvent;
        EventManager.onFinishedGameEvent += FinishedGame;
        EventManager.onStartGameEvent += StartedGame;
    }

    private void OnDisable()
    {
        EventManager.onPointsUIUpdateEvent -= PointsUIUpdateEvent;
        EventManager.onFinishedGameEvent -= FinishedGame;
        EventManager.onStartGameEvent -= StartedGame;
    }

    private void PointsUIUpdateEvent(int currentPoints, int maxPoints)
    {
        pointsText.GetComponent<Text>().text = currentPoints + " / " + maxPoints;
        if (currentPoints == maxPoints)
        {
            StartCoroutine(FinishedGame(2));
        }
    }

    private void FinishedGame(bool finished)
    {
        finishedPanel.SetActive(true);
        finishedText.SetActive(true);
        pointsCanvas.SetActive(false);

        if (finished)
        {
            restartButton.SetActive(true);
            closeGameButton.SetActive(true);
            finishedText.GetComponent<Text>().color = Color.yellow;
            finishedText.GetComponent<Text>().text = "Has Ganado!";
        }
        else
        {
            restartButton.SetActive(true);
            //mainMenuButton.SetActive(true);
            finishedText.GetComponent<Text>().color = Color.red;
            finishedText.GetComponent<Text>().text = "Has Perdido!";
        }
    }

    private void StartedGame(bool start)
    {
        if (start == true)
        {
            EventManager.PointsUIUpdate(0, numberOfCubes.numCubes);
            StartCoroutine(ActivateSpawner(2));
        }
    }

    IEnumerator ActivateSpawner(int delay)
    {
        yield return new WaitForSeconds(delay);
        cubeSpawner.SetActive(true);
    }

    IEnumerator FinishedGame(int delay)
    {
        yield return new WaitForSeconds(delay);
        EventManager.FinishedGameEvent(true);
    }
}
