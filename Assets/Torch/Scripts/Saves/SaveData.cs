using System.Collections.Generic;
using System;

[Serializable]
public class SaveData
{
    public string levelName;
    public string controlPointName;
    public Dictionary<string, bool> torchStatus;
    public bool playerTorchStatus;
}