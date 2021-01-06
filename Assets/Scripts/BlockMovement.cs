using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BlockMovement : MonoBehaviour
{
    public int Width, Height;
    public float MaxSpeed;
    public List<GameObject> AllPrefabs;
    public List<GameObject> NextBlocks;
    public GameObject ScoreText;
    public Text PlayerScore;
    public GameObject GameOverPanel;
    Text _text;
    int _score;
    int _nextBlockId = 0;
    GameObject _newBlock;
    Block _newBlockScrpit;
    float _speed = 1f;
    float _defaultSpeed = 1f;
    float _levelSpeed;
    float _afterTime = 0;
    Transform[,] _grid;
    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        _grid = new Transform[Height, Width];
        _nextBlockId = Random.Range(0, AllPrefabs.Count - 1);
        _text = ScoreText.GetComponent<Text>();
        CreateNewBlock();
        _levelSpeed = _defaultSpeed;
    }

    private void CreateNewBlock()
    {
        _text.text = _score.ToString();
        _newBlock = Instantiate(AllPrefabs[_nextBlockId], this.transform.position, Quaternion.identity);
        _newBlock.tag = "NextBlock";
        _newBlockScrpit = _newBlock.GetComponent<Block>();
        _newBlockScrpit.Grid = _grid;
        _newBlockScrpit.Width = Width;
        _newBlockScrpit.Height = Height;
        _nextBlockId = Random.Range(0, AllPrefabs.Count - 1);
        NextBlocks.ForEach(p => p.SetActive(false));
        NextBlocks[_nextBlockId].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (_newBlock == null)
        {
            CreateNewBlock();
            return;
        }

        _afterTime += Time.deltaTime;

        if (Input.GetKey(KeyCode.DownArrow))
        {
            _speed = MaxSpeed;
        }
        else
        {
            _speed = _levelSpeed;
        }

        if (_afterTime > _speed)
        {
            _afterTime = 0;
            if (!_newBlockScrpit.Move(Vector3.down))
            {
                if (!SaveBlock())
                {
                    Debug.Log("游戏结束");
                    Time.timeScale = 0;
                    GameOverPanel.SetActive(true);
                    PlayerScore.text = _score.ToString();
                    return;
                }
                RemoveFullRow();
                _newBlock = null;
                _newBlockScrpit = null;
                CreateNewBlock();
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _newBlockScrpit.Move(Vector3.left);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _newBlockScrpit.Move(Vector3.right);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _newBlockScrpit.Rotate();
        }
    }

    private bool SaveBlock()
    {
        for (int i = 0; i < _newBlock.transform.childCount; i++)
        {
            var childTransform = _newBlock.transform.GetChild(i);
            int x = Mathf.RoundToInt(childTransform.position.x);
            int y = Mathf.RoundToInt(childTransform.position.y);
            if (y >= Height)
                return false;
            _grid[y, x] = childTransform;
        }
        return true;
    }

    private void RemoveFullRow()
    {
        int removeCount = 0;
        for (int i = Height - 1; i >= 0; i--)
        {
            if (IsRowFull(i))
            {
                removeCount++;
                RemoveFullRow(i);
            }
        }
        AddScore(removeCount);
    }

    private void AddScore(int removeCount)
    {
        if (removeCount == 1)
        {
            _score += 100;
        }
        if (removeCount == 2)
        {
            _score += 400;
        }
        if (removeCount == 3)
        {
            _score += 700;
        }
        if (removeCount == 4)
        {
            _score += 1000;
        }

        _levelSpeed = _defaultSpeed - Mathf.RoundToInt(_score / 2000) * 0.1f;
        if (_levelSpeed < MaxSpeed)
            _levelSpeed = MaxSpeed;
    }

    private bool IsRowFull(int rowCount)
    {
        for (int i = 0; i < Width; i++)
        {
            if (_grid[rowCount, i] == null)
            {
                return false;
            }
        }
        return true;
    }

    private void RemoveFullRow(int rowCount)
    {
        for (int i = 0; i < Width; i++)
        {
            GameObject.Destroy(_grid[rowCount, i].gameObject);
            _grid[rowCount, i] = null;
        }
        RowDown(rowCount);
    }

    private void RowDown(int rowCount)
    {
        for (int i = rowCount; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (_grid[i, j] != null)
                {
                    _grid[i, j].transform.position += Vector3.down;
                    _grid[i - 1, j] = _grid[i, j];
                    _grid[i, j] = null;
                }
            }
        }
    }
}
