using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class ShowPlayerScore : MonoBehaviour
{
    public GameObject EndGame;
    public List<GameObject> ScoreRows;


    void Start()
    {
        var recordScore = EndGame.GetComponent<RecordScore>();
        //recordScore._scoreTable = recordScore._scoreTable.OrderByDescending(p => p.Score).ToList();
        for (int i = 0; i < recordScore._scoreTable.Count; i++)
        {
            if (i >= ScoreRows.Count)
                break;

            ScoreRows[i].SetActive(true);
            var scoreRowTr = ScoreRows[i].transform;

            for (int j = 0; j < scoreRowTr.childCount; j++)
            {
                var child = scoreRowTr.GetChild(j);
                switch (child.gameObject.tag)
                {
                    case "Id":
                        var id = child.gameObject.GetComponent<Text>();
                        id.text = recordScore._scoreTable[i].Id.ToString();
                        break;
                    case "PlayerName":
                        var playerName = child.gameObject.GetComponent<Text>();
                        playerName.text = recordScore._scoreTable[i].PlayerName.ToString();
                        break;
                    case "Score":
                        var score = child.gameObject.GetComponent<Text>();
                        score.text = recordScore._scoreTable[i].Score.ToString();
                        break;
                    case "PlayTime":
                        var playTime = child.gameObject.GetComponent<Text>();
                        playTime.text = recordScore._scoreTable[i].PlayTime.ToString();
                        break;
                    default:
                        break;
                }
            }
        }
    }


    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }


    public void Back()
    {
        this.gameObject.SetActive(false);
        SceneManager.LoadScene("StartScene");
    }
}
