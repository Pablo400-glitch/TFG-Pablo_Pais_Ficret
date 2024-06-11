using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ActionsManager : MonoBehaviour
{
    [SerializeField] private RestartScene restartScene;
    [SerializeField] private CloseGame close;
    [SerializeField] private VRManager vrManager;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject credits;
    [SerializeField] private ChangeScene changeScene;

    public void Actions(GameObject hitObject)
    {
        if (hitObject.tag == Tags.RESTART)
        {
            vrManager.StopXR();
            restartScene.RestartGame();
        }
        if (hitObject.tag == Tags.CLOSE)
        {
            close.ExitGame();
        }
        if (hitObject.tag == Tags.PLAY)
        {
            mainMenu.SetActive(false);
            tutorial.SetActive(true);
        }
        if (hitObject.tag == Tags.START_GAME)
        {
            tutorial.SetActive(false);
            changeScene.MoveToScene(1);
        }
        if (hitObject.tag == Tags.CREDITS)
        {
            mainMenu.SetActive(false);
            credits.SetActive(true);
        }
        if (hitObject.tag == Tags.RETURN_TO_MAIN_MENU)
        {
            mainMenu.SetActive(true);
            credits.SetActive(false);
        }
    }
}
