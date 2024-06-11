using UnityEngine;

public class FixationManager : MonoBehaviour
{
    [SerializeField] private ActionsManager actions;
    [SerializeField] private float selectionTimeThreshhold = 2.0f;
    [SerializeField] private FixationInterface fixationInterface;

    private bool uiElementSeen = false;
    private float startTimeFixation;

    public void Fixation(GameObject hitObject)
    {
        if (!uiElementSeen)
        {
            StartFixation();
        }
        else
        {
            FinishFixation(hitObject);
        }


        if (uiElementSeen && ((Time.time - startTimeFixation) >= selectionTimeThreshhold))
        {
            ResetFixation();
        }
    }

    private void StartFixation()
    {
        startTimeFixation = Time.time;
        uiElementSeen = true;

        fixationInterface.Activar();
    }

    private void FinishFixation(GameObject hitObject)
    {
        float elapsedFixingTime = Time.time - startTimeFixation;

        fixationInterface.Actualizar(elapsedFixingTime / selectionTimeThreshhold);

        if (elapsedFixingTime > selectionTimeThreshhold)
        {
            fixationInterface.Desactivar();
            actions.Actions(hitObject);
        }
        else
        {
            // Debemos seguir mirando
            // Aquí puedes actualizar la barra de tiempo si necesitas mostrar visualmente el progreso
        }
    }

    private void ResetFixation()
    {
        uiElementSeen = false;

        // Reinicia la interfaz de fijación
        fixationInterface.Reiniciar();
    }
}
