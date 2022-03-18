using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRun : MonoBehaviour
{
    public GameObject gameMenuObj;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIEnable();
        }
    }

    void UIEnable()
    {
        gameMenuObj.SetActive(true);
        Time.timeScale = 0;
    }
    
}
