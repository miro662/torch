using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    //Obecne dane zapisu
    public SaveData data;

    //Początkowy punkt spawnu
    public SavePoint startSavePoint;

    //GameObject wskazujący na obecnego gracza
    GameObject currentPlayer;
    //GameObject wskazujący na pochodnię obecnego gracza
    Transform currentPlayerTorch;
    //Obecny punkt kontrolny
    SavePoint savePoint;

    void FindPlayerAndTorch()
    {
        //Znajdź gracza w poziomie
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
        //Znajdź pochodnię gracza
        currentPlayerTorch = currentPlayer.transform.FindChild("PlayerTorch");
    }

    //Funkcja "nowej gry"
    public void NewGame()
    {
        //Utwórz nowe dane zapisu
        data = new SaveData();

        FindPlayerAndTorch();
        UpdateData(startSavePoint);
        Restore();
    }

    //Aktualizuje dane
    public void UpdateData(SavePoint pSavePoint)
    {
        //Ustaw save point
        savePoint = pSavePoint;
        data.controlPointName = savePoint.name;

        //Ustaw nazwę poziomu
        data.levelName = SceneManager.GetActiveScene().name;

        //Ustaw stan świeczki gracza
        data.playerTorchStatus = currentPlayerTorch.GetComponent<PlayerTorch>().IsLit;
    }

    //Restartuje dane w poziomie
    public void Restore()
    {
        FindPlayerAndTorch();
        //Ustaw obecny save point
        savePoint = GameObject.Find(data.controlPointName).GetComponent<SavePoint>();

        //Przywróć pozycję gracza
        currentPlayer.transform.position = savePoint.transform.position + savePoint.spawnOffset;
        //Przywróć stan świeczki gracza
        currentPlayerTorch.GetComponent<PlayerTorch>().IsLit = data.playerTorchStatus;
    }
}
