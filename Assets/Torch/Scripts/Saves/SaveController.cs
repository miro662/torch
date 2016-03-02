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
        currentPlayerTorch = GameObject.FindGameObjectWithTag("PlayerTorch2").transform;
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

    void FillTorchData()
    {
        //Get all torches
        GameObject[] torches = GameObject.FindGameObjectsWithTag("Torch");
        data.torchData = new List<SaveData.TorchData>();
        foreach (GameObject torch in torches)
        {
            TorchController torchController = torch.GetComponent<TorchController>();
            SaveData.TorchData torchData = new SaveData.TorchData(torch.name, torchController.Status);
            if (!data.torchData.Contains(torchData))
                data.torchData.Add(torchData);
            else
            {
                data.torchData.Find(x => x.name == torchData.name).status = torchController.Status;
            }
        }
    }

    void RestoreTorchData()
    {
        foreach (SaveData.TorchData torch in data.torchData)
        {
            GameObject torchGameObject = GameObject.Find(torch.name);
            if (torchGameObject != null)
            {
                TorchController torchController = torchGameObject.GetComponent<TorchController>();
                torchController.Status = torch.status;
            }
        }
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
        print(currentPlayerTorch.name);
        data.playerTorchStatus = currentPlayerTorch.GetComponent<PlayerTorch>().IsLit;

        FillTorchData();
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
        RestoreTorchData();
    }

    public void RestoreJSON(string json)
    {
        data = JsonUtility.FromJson<SaveData>(json);
        Restore();
    }

    public string GetJSON()
    {
        return JsonUtility.ToJson(data);
    }
}
