using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{

    [Header("UI组件")]
    public Text talkText;
    public Image talkAvatar;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;
    List<string> textList = new List<string>();
    public float printSpeed = 0.1f;
    bool printFinish;

    [Header("头像")]
    public Sprite face01, face02;

    // Start is called before the first frame update
    void Awake()
    {
        GetTextFromFile(textFile);
    }

    private void OnEnable() 
    {
        // talkText.text = textList[index];
        // index ++;
        StartCoroutine(PrintTextUI());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        if (Input.GetKeyDown(KeyCode.R) && printFinish)
        {
            // talkText.text = textList[index];
            // index ++;
            StartCoroutine(PrintTextUI());
        }
    }

    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');
        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    }

    IEnumerator PrintTextUI()
    {
        printFinish = false;
        talkText.text = "";
        
        switch (textList[index])
        {

            case "A":
                talkAvatar.sprite = face01;
                index++;
                break;
            case "B":
                talkAvatar.sprite = face02;
                index++;
                break;
        }

        for (int i = 0; i < textList[index].Length; i++)
        {
            talkText.text += textList[index][i];
            yield return new WaitForSeconds(printSpeed);
        }
        if (printFinish != true)
        {
            printFinish = true;
            index ++;
        }
    }


}
