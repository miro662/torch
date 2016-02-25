using UnityEngine;

/// <summary>
/// Główny komponent, odpowiada za kontrolę nad grą
/// </summary>
public class GameController : MonoBehaviour
{
    SavePoint lastSavePoint;

    void Start()
    {
        //Search for playerpref SaveData
        if (!PlayerPrefs.HasKey("SaveData"))
        {
            print("New Game");
            GetComponent<SaveController>().NewGame();
        }
        else
        {
            print("Saved Game");
            string saveData = PlayerPrefs.GetString("SaveData");
            GetComponent<SaveController>().RestoreJSON(saveData);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GetComponent<SaveController>().Restore();
        }
    }

    public void RegisterSavePoint(SavePoint savePoint)
    {
        if (lastSavePoint != null && lastSavePoint != savePoint) lastSavePoint.Deactivate();
        GetComponent<SaveController>().UpdateData(savePoint);
        lastSavePoint = savePoint;
        PlayerPrefs.SetString("SaveData", GetComponent<SaveController>().GetJSON());
        PlayerPrefs.Save();
    }
}
