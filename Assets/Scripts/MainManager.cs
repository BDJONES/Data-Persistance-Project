using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public string playerName;
    public InputField nameInputField;
    public TextMeshProUGUI highScore;

    [System.Serializable]
    public class SaveData
    {
        public string name = "";
        public int score = 0;
    }
    private void Start()
    {
        SaveData data = LoadHighScore();
        if (data != null)
        {
            highScore.SetText("Best Score: " + data.name + ":" + data.score);
        } else
        {
            highScore.SetText("Best Score:");
        }
        
    }
    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        SetPlayerName();
        SceneManager.LoadScene(1);
    }

    private void SetPlayerName()
    {
        playerName = nameInputField.text;
    }

    public bool SaveHighScore(string name, int score) // if return is false, then there was no change of high score. If there was a change in score, the function will return true
    {
        SaveData data = LoadHighScore();
        if (data == null)
        {
            SaveData firstSave = new SaveData();
            firstSave.score = score;
            firstSave.name = name;
            string json = JsonUtility.ToJson(firstSave);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
            return true;
        } else if (data != null && (data.score < score))
        { 
            data.score = score;
            data.name = name;
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
            return true;
        }
        return false;
    }

    public SaveData LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            return data;
        }
        return null;
    }

    public void QuitGame()
    {
        
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();           
        #endif
    }
}
