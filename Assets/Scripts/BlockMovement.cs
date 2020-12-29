using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BlockMovement : MonoBehaviour
{
    public int _width,_height;
    public float _maxSpeed;
    public List<GameObject> _allPrefabs;
    public List<GameObject> _nextBlocks;
    public GameObject _scoreText;
    public Text _playerScore;
    public GameObject _gameOverPanel;
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
        _grid = new Transform[_height, _width];
        _nextBlockId = Random.Range(0, _allPrefabs.Count - 1);
        _text = _scoreText.GetComponent<Text>();
        CreateNewBlock();
        _levelSpeed = _defaultSpeed;
    }

    private void CreateNewBlock()
    {
        _text.text = _score.ToString();
        _newBlock = Instantiate(_allPrefabs[_nextBlockId], this.transform.position, Quaternion.identity);
        _newBlockScrpit = _newBlock.GetComponent<Block>();
        _newBlockScrpit._grid = _grid;
        _newBlockScrpit._width = _width;
        _nextBlockId = Random.Range(0, _allPrefabs.Count - 1);
        _nextBlocks.ForEach(p => p.SetActive(false));
        _nextBlocks[_nextBlockId].SetActive(true);
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
            _speed = _maxSpeed;
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
                if(!SaveBlock())
                {
                    Debug.Log("游戏结束");
                    _playerScore.text = _score.ToString();
                    _gameOverPanel.SetActive(true);
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
            if (y >= _height)
                return false;
            _grid[y, x] = childTransform;
        }
        return true;
    }

    private void RemoveFullRow()
    {
        int removeCount = 0;
        for (int i = _height - 1; i >= 0; i--)
        {
            if(IsRowFull(i))
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
        if (_levelSpeed < _maxSpeed)
            _levelSpeed = _maxSpeed;
    }

    private bool IsRowFull(int rowCount)
    {
        for (int i = 0; i < _width; i++)
        {
            if(_grid[rowCount,i] == null)
            {
                return false;
            }
        }
        return true;
    }

    private void RemoveFullRow(int rowCount)
    {
        for (int i = 0; i < _width; i++)
        {
            GameObject.Destroy(_grid[rowCount, i].gameObject);
            _grid[rowCount, i] = null;
        }
        RowDown(rowCount);
    }

    private void RowDown(int rowCount)
    {
        for (int i = rowCount; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                if(_grid[i,j] != null)
                {
                    _grid[i, j].transform.position += Vector3.down;
                    _grid[i - 1, j] = _grid[i, j];
                    _grid[i, j] = null;
                }
            }
        }
    }
}
