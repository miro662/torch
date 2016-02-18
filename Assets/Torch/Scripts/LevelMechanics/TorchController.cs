using UnityEngine;

/// <summary>
/// Komponent odpowiadający za zarządzanie tzw. zwykłą świeczką
/// </summary>
[RequireComponent(typeof(Animator))]
public class TorchController : Signal
{
    #region Animator
    //Referencja do animatora
    Animator _animator;
    void Awake()
    {
        _animator = GetComponent<Animator>();

        //Dodaj funkcje zmieniającą animację do zdarzeń
        OnSignalOff += UpdateAnimation;
        OnSignalOn += UpdateAnimation;
    }

    //Funkcja zmieniająca animację
    void UpdateAnimation()
    {
        _animator.SetBool("Lit", Status);
    }
    #endregion

    #region Initialization
    //Czy świeczka świeci się na początku?
    public bool isLitOnStart;

    void Start()
    {
        Status = isLitOnStart;
    }
    #endregion
}
