using UnityEngine;

/// <summary>
/// Komponent służący do tworzenia bramek logicznych z sygnałów
/// </summary>
public class SignalGate : Signal
{
    //Typ bramki logicznej
    public enum GateType
    {
        And,
        Or,
        Xor
    };
    public GateType type;

    //Sloty na sygnały
    public Signal[] slots;

    //Czy negować wejście
    public bool negate;

    void Start()
    {
        UpdateSlots();
    }

    void OnValidate()
    {
        UpdateSlots();
    }

    //Aktualizacja slotów eventami
    void UpdateSlots()
    {
        foreach (Signal slot in slots)
        {
            slot.OnSignalOff += PreformGate;
            slot.OnSignalOn += PreformGate;
        }
        PreformGate();
    }

    void PreformGate()
    {
        bool newSignal = slots[0].Status;
        
        //Wykonaj operację na każdym kolejnym sygnale
        for(int i = 1; i != slots.Length; ++i)
        {
            newSignal = Operation(newSignal, slots[i].Status);
        }

        //Zaneguj, jeżeli jest ustawiona opcja
        if (negate) newSignal = !newSignal;

        //Zmień status sygnału
        Status = newSignal;
    }

    //Przeprowadza daną operację na 2 operandach
    bool Operation(bool op1, bool op2)
    {
        bool result = false;
        //Wybierz operację w zależności od typu
        switch (type)
        {
            case GateType.And:
                result = op1 && op2;
                break;
            case GateType.Or:
                result = op1 || op2;
                break;
            case GateType.Xor:
                result = op1 != op2;
                break;
        }
        return result;
    }
}