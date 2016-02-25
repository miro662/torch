using UnityEngine;

/// <summary>
/// Główny komponent, odpowiada za kontrolę nad grą
/// </summary>
public class GameController : MonoBehaviour
{
    SavePoint lastSavePoint;

    void Start()
    {
        GetComponent<SaveController>().NewGame();
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
    }
}
