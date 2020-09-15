using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
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
