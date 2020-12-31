using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowPlayerScore : MonoBehaviour
{
    //public GameObject _startPosition;
    //BlockMovement _blockMovement;
    // Start is called before the first frame update
    void Start()
    {
        //_blockMovement = _startPosition.GetComponent<BlockMovement>();
    }

    public void Restart()
    {
        //this.gameObject.SetActive(false);
        //var blocks = GameObject.FindGameObjectsWithTag("NextBlock");
        //for (int i = 0; i < blocks.Length; i++)
        //{
        //    GameObject.Destroy(blocks[i]);
        //}
        //_blockMovement.InitGame();
        SceneManager.LoadScene("SampleScene");
    }

    public void Back()
    {
        this.gameObject.SetActive(false);
        SceneManager.LoadScene("StartScene");
    }
}
