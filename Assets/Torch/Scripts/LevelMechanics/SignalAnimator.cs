using System;
using UnityEngine;

/// <summary>
/// Komponent służący do animowania obiektów z użyciem signali
/// </summary>
[RequireComponent(typeof(Animator))]
public class SignalAnimator : MonoBehaviour
{
    //Dane slotu sygnału
    [Serializable]
    public class SignalField
    {
        //Nazwa slotu
        public string name;
        //Slot na sygnał
        public Signal signal;

        //Referencja do używanego animatora
        Animator _animator;

        //Inicjalizacja pola
        public void Initialize(Animator animator)
        {
            //Przekaż referencję do animatora
            _animator = animator;

            //Podepnij metody do sygnału
            signal.OnSignalOn += OnSignalOn;
            signal.OnSignalOff += OnSignalOff;
        }

        //Metody służące do zmiany wartości w Animatorze
        void OnSignalOn()
        {
            _animator.SetBool(name, true);
        }
        void OnSignalOff()
        {
            _animator.SetBool(name, false);
        }
    }

    //Tablica zawierająca obsługiwane sygnały
    public SignalField[] usedSignals;

    //Komponenty
    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        InitializeSlots();
    }
    
    void OnValidate()
    {
        InitializeSlots();
    }

    //Inicjalizacja slotów
    void InitializeSlots()
    {
        for (int i = 0; i != usedSignals.Length; ++i)
        {
            //Zainicjalizuj slot i przekaż mu referencję do animatora
            usedSignals[i].Initialize(_animator);
        }
    }
}