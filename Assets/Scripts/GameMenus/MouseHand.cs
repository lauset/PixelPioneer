using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class MouseHand : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public TextMeshProUGUI text;
    public GameObject gameMenuObj;
    private bool _isActiveFlag;
    private string _activeText;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (_isActiveFlag && !(_activeText == string.Empty))
                this.MouseLeftClick(text.text);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.TextContinueClick();
        }
    }

    public void OnPointerEnter(PointerEventData eventData) 
    {
        text.color = new Color(1, 1, 1, 1);
        text.fontSize = 65;
        _isActiveFlag = true;
        _activeText = text.text;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = new Color(1, 1, 1, 0.65f);
        text.fontSize = 55;
        _isActiveFlag = false;
        _activeText = "";
    }

    void MouseLeftClick(string text)
    {
        switch (text)
        {
            case "Continue":
                this.TextContinueClick();
                break;
            case "Main Menu":
                this.TextMainMenuClick();
                break;
            case "Quit Game":
                this.TextQuitGameClick();
                break;
        }
    }

    void TextContinueClick()
    {
        if (gameMenuObj != null)
            gameMenuObj.SetActive(false);
            Time.timeScale = 1.0f;
    }

    void TextMainMenuClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    void TextQuitGameClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
