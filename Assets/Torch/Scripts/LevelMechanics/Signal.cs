using UnityEngine;

/// <summary>
/// Klasa bazowa "sygnałów" - komponentów wysyłających sygnały
/// </summary>
abstract public class Signal : MonoBehaviour
{
    //Zmienna przechowująca status
    protected bool status;

    //Delegat zdarzeń sygnału
    public delegate void OnSignalChange();

    //Zdarzenie wywoływane przy "włączeniu" sygnału
    public event OnSignalChange OnSignalOn;
    //Zdarzenie wywoływane przy "wyłączeniu" sygnału
    public event OnSignalChange OnSignalOff;

    //Właściwość służąca do zmiany statusu sygnału
    public bool Status
    {
        get { return status; }
        set
        {
            //Zmień status
            status = value;

            //Jeżeli do zdarzeń podpięte są jakieś metody, wywołaj odpowiednie zdarzenie
            if (status && OnSignalOn != null)
                OnSignalOn();
            else if ((!status) && OnSignalOff != null)
                OnSignalOff();
        }
    }
}