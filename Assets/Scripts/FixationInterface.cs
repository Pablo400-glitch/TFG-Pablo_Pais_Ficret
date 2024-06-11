using UnityEngine;
using UnityEngine.UI;

public class FixationInterface : MonoBehaviour
{
    [SerializeField] private Image fillImage; // Referencia al componente Image que muestra el progreso

    void Start()
    {
        // Desactivamos la interfaz de fijación al inicio
        Desactivar();
    }

    // Activa la interfaz de fijación
    public void Activar()
    {
        gameObject.SetActive(true);
    }

    // Desactiva la interfaz de fijación
    public void Desactivar()
    {
        gameObject.SetActive(false);
    }

    // Actualiza el valor de progreso de la fijación
    public void Actualizar(float valorProgresoFijacion)
    {
        fillImage.fillAmount = valorProgresoFijacion;
    }

    // Reinicia el progreso de la fijación
    public void Reiniciar()
    {
        fillImage.fillAmount = 0f;
    }
}