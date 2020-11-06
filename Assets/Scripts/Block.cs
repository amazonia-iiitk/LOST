using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection;
using UnityEngine;

public class Block : MonoBehaviour
{
    public event System.Action<Block> OnBlockPressed;

    public Vector2Int coord;
    public Vector2Int solution;
    public bool game_start = false;

    public void Init(Vector2Int init_coords,Texture2D patch)//Initializes each puzzle block with the given image patch 
    {
        coord = init_coords;
        solution = init_coords;
        GetComponent<MeshRenderer>().material.shader = Shader.Find("Unlit/Texture");
        GetComponent<MeshRenderer>().material.mainTexture = patch;
    }

    void OnMouseDown()
    {
        if(OnBlockPressed!= null)
        {
            OnBlockPressed(this);
        }
    }

    public void MovetoPosition(Vector2 target,float duration)
    {
        StartCoroutine(AnimateMove(target, duration));
    }

    IEnumerator AnimateMove(Vector2 target, float duration)//sets smooth animation for movement
    {
        Vector2 init_pos = transform.position;
        float percent=0;
        while (percent < 1)
        {
            percent += Time.deltaTime / duration;
            transform.position = Vector2.Lerp(init_pos,target,percent);
            yield return null;
        }
    }
    public bool isSolution()
    {
        return coord == solution;

    }
}
