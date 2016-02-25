using System.Collections.Generic;
using System;

[Serializable]
public class SaveData
{
    [Serializable]
    public class TorchData
    {
        public string name;
        public bool status;

        public TorchData(string n, bool s)
        {
            name = n;
            status = s;
        }

        public TorchData() { }
    }
    public string levelName;
    public string controlPointName;
    public List<TorchData> torchData;
    public bool playerTorchStatus;
    public int gameVersion = 0;
}