using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIManager : MonoBehaviour
{
    [SerializeField] protected const int MAINMENU = 0;
    [SerializeField] protected const int MAINGAME = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Exit the application, stops play mode if in the unity editor
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif !UNITY_EDITOR
        Application.Quit();
#endif
    }
}
