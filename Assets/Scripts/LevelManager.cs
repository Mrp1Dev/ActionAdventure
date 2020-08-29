using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int sceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
