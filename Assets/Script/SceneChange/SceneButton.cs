using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    public static string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(GameObject button)
    {
        if (SceneManager.GetActiveScene().name == button.name) return;
        SceneManager.LoadScene(button.name);
        sceneName = button.name;
    }
}
