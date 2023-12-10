using UnityEngine;

public class Displays : MonoBehaviour
{
    private void Start()
    {
       for (int i = 0; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }
}
