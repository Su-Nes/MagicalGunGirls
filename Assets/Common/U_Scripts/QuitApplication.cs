using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    public void QuitGame()
    {
        print("Quit!");
        Application.Quit();
    }
}
