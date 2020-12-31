using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour, IMove, IRotate
{
    public bool _rotateable;
    public Transform[,] _grid;
    public int _width;
    public int _height;
    public bool Move(Vector3 vector)
    {
        this.transform.position += vector;
        if(IsCrash())
        {
            this.transform.position -= vector;
            return false;
        }
        return true;
    }

    public void Rotate()
    {
        if(_rotateable)
        {
            this.transform.Rotate(Vector3.forward, -90);
            if(IsCrash())
            {
                this.transform.Rotate(Vector3.forward, 90);
            }
        }
    }

    public bool IsCrash()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            var blockTransform = this.transform.GetChild(i);
            int x = Mathf.RoundToInt(blockTransform.position.x);
            int y = Mathf.RoundToInt(blockTransform.position.y);
            if (x < 0 || x >= _width || y < 0 || (y < _height && _grid[y, x] != null))
                return true;
        }
        return false;
    }    
}
