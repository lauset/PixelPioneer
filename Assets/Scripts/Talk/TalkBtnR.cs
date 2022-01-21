using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkBtnR : MonoBehaviour
{

    public GameObject Button;
    public GameObject talkUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Button.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
      if (Button.activeSelf && Input.GetKeyDown(KeyCode.R))
      {
          talkUI.SetActive(true);
      }
    }

}
