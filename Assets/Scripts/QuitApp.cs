using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApp : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("ESC is pressed"); 
            Application.Quit(); 
        }
    }
}
