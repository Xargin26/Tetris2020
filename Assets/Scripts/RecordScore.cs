using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RecordScore : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField _input;
    public Text _score;
    public GameObject _playerScorePanel;
    public List<ScoreRow> _scoreTable;

    void Start()
    {
        _scoreTable = XmlHelper.LoadScoreTable();
    }

    public void OnClick()
    {
        if (string.IsNullOrEmpty(_input.text))
        {
            Debug.Log("请输入玩家昵称！");
        }
        else
        {
            int score = int.Parse(_score.text);
            string playerName = _input.text;
            var scoreRow = new ScoreRow(playerName, score);
            if (_scoreTable.Count < 10)
            {
                _scoreTable.Add(scoreRow);
                _scoreTable.OrderByDescending(p => p.Score);
                for (int i = 0; i < _scoreTable.Count; i++)
                {
                    _scoreTable[i].Id = i + 1;
                }
            }
            else
            {
                if (_scoreTable.Any(p => p.Score < score))
                {
                    _scoreTable.RemoveAt(_scoreTable.Count - 1);
                    _scoreTable.Add(scoreRow);
                    _scoreTable.OrderByDescending(p => p.Score);
                    for (int i = 0; i < _scoreTable.Count; i++)
                    {
                        _scoreTable[i].Id = i + 1;
                    }
                }
            }

            XmlHelper.SaveScoreTable(_scoreTable);

            this.transform.parent.gameObject.SetActive(false);
            _playerScorePanel.SetActive(true);
        }
    }
}
