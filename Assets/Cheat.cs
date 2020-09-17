using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    public void givecash()
    {
        SaveManager.SaveData.Gold += 10000000;
    }
}
