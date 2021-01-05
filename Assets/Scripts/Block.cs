using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Block : MonoBehaviour, IMove, IRotate
{
    public bool Rotateable;
    public Transform[,] Grid;
    public int Width;
    public int Height;
    private List<Transform> Children;


    private void Start()
    {
        Children = new List<Transform>(this.GetComponentsInChildren<Transform>());
    }


    public bool Move(Vector3 vector)
    {
        this.transform.position += vector;
        if (IsCrash())
        {
            this.transform.position -= vector;
            return false;
        }
        return true;
    }


    public void Rotate()
    {
        if (Rotateable)
        {
            this.transform.Rotate(Vector3.forward, -90);
            if (IsCrash())
            {
                float minX = 0.0f, maxX = 0.0f;
                minX = Children.Min(p => p.position.x);
                maxX = Children.Max(p => p.position.x);

                if (minX < 0)
                {
                    Move(new Vector3(-minX, 0, 0));
                }

                if (maxX >= Width)
                {
                    Move(new Vector3(Width - maxX - 1, 0, 0));
                }

                this.transform.Rotate(Vector3.forward, 90);
            }
        }
    }


    public bool IsCrash()
    {
        for (int i = 0; i < Children.Count; i++)
        {
            var blockTransform = Children[i];
            int x = Mathf.RoundToInt(blockTransform.position.x);
            int y = Mathf.RoundToInt(blockTransform.position.y);
            if (x < 0 || x >= Width || y < 0 || (y < Height && Grid[y, x] != null))
                return true;
        }
        return false;
    }
}
