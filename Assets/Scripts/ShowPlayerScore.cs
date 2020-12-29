using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlayerScore : MonoBehaviour
{
    public GameObject _startPosition;
    BlockMovement _blockMovement;
    // Start is called before the first frame update
    void Start()
    {
        _blockMovement = _startPosition.GetComponent<BlockMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        this.gameObject.SetActive(false);
        var blocks = GameObject.FindGameObjectsWithTag("Block");
        for (int i = 0; i < blocks.Length; i++)
        {
            GameObject.Destroy(blocks[i]);
        }
        _blockMovement.InitGame();
    }

    public void Back()
    {
        this.gameObject.SetActive(false);
        
    }
}
