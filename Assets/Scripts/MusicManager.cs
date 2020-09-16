using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void OnDestroy()
    {
        instance = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
