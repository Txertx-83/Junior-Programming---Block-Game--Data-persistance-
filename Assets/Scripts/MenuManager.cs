using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
    using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI bestText;


    // Start is called before the first frame update
    void Start()
    {
        //Load file and store as variable

        if (ScoreManager.instance != null)
        {
            if (ScoreManager.instance.currName != "")
                nameInput.text = ScoreManager.instance.currName;
        }

        bestText.text = ScoreManager.instance.GetBestScoreMessage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        ScoreManager.instance.currName = nameInput.text;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        ScoreManager.instance.SaveScoreToFile();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }

    public void CheckNameInput()
    {
        if (nameInput.text != "")
        {
            startButton.interactable = true;
        }
        else
        {
            startButton.interactable = false;
        }
    }
}
