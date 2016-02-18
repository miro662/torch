using UnityEngine;

/// <summary>
/// Klasa pomocnicza, zawierająca właściwości ułatwiające dostanie się do różnych ważnych obiektów
/// </summary>
public class Torch
{
    /// <summary>
    /// Znajduje obiekt Game Controller i zwraca odpowiednią klasę
    /// </summary>
    public GameController GameController
    {
        get
        {
            GameObject gameControllerGameObject = GameObject.FindGameObjectWithTag("Game Controller");
            GameController gameController = gameControllerGameObject.GetComponent<GameController>();
            return gameController;
        }
    }
}