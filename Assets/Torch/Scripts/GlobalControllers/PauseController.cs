using UnityEngine;

/// <summary>
/// Komponent odpowiadający za kontrolę pauzy
/// </summary>
public class PauseController : MonoBehaviour
{
    //Czy gra jest spauzowana?
    bool paused;

    //Właściwość do zarządzania pauzą
    public bool Paused
    {
        get { return paused; }
        set
        {
            //Zmień pauzę w kontrolerze
            paused = value;

            //Wywołaj OnPause/OnResume na wszystkich obiektach
            GameObject[] allGameObjects = Object.FindObjectsOfType<GameObject>();
            foreach (GameObject oneOfGameObjects in allGameObjects)
            {
                if (paused)
                    oneOfGameObjects.SendMessage("OnPause", SendMessageOptions.DontRequireReceiver);
                else
                    oneOfGameObjects.SendMessage("OnResume", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    void Start()
    {
        //Gra zaczyna się uruchomiona
        Paused = true;
    }

    void Update()
    {
        //Sprawdź, czy wciśnięty przycisk pauzy
        if (Input.GetButtonDown("Pause"))
        {
            //Przełącz pauzę
            Paused = !Paused;
        }
    }
}
