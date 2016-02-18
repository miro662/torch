using UnityEngine;
using System.Collections;

public class PauseTester : MonoBehaviour
{
	void OnPause()
    {
        print("Paused");
    }
    void OnResume()
    {
        print("Resumed");
    }
}
