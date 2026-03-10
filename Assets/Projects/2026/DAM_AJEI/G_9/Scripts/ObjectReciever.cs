using UnityEngine;

public class ObjectReceiver : MonoBehaviour
{
    public bool objectInserted = false;
    public GameObject storedObject;
    public Transform teleportTarget;

    void OnTriggerEnter(Collider other)
    {
        if (!objectInserted)
        {
            storedObject = other.gameObject;
            objectInserted = true;

            storedObject.SetActive(false);

            Debug.Log("Objeto insertado correctamente.");
        }
    }

    // Ahora devolvemos un bool indicando si la activación fue exitosa
    public bool Activate()
    {
        if (objectInserted && storedObject != null)
        {
            storedObject.transform.position = teleportTarget.position;
            storedObject.SetActive(true);

            objectInserted = false;
            storedObject = null;

            Debug.Log("Sistema activado correctamente. Objeto movido.");
            return true;
        }
        else
        {
            Debug.Log("No puedes activar el sistema. No hay objeto.");
            return false;
        }
    }
}