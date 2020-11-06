using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public int shufflelength = 20;
    public int blocksPerLine = 4;
    public Texture2D image;
    Block emptyblock;
    Block[,] blocks;

    void Start()
    {
        CreatePuzzle();
    }

    void CreatePuzzle()
    {
        blocks = new Block [blocksPerLine, blocksPerLine];
        Texture2D[,] image_slices= ImageSlicer.GetSlices(image, blocksPerLine);
        for (int y = 0; y < blocksPerLine; y++)
        {
            for (int x = 0; x < blocksPerLine; x++)
            {
                GameObject blockObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                blockObject.transform.position = -Vector2.one * (blocksPerLine - 1) * .5f + new Vector2(x, y);
                blockObject.transform.parent = transform;

                Block block = blockObject.AddComponent<Block>();
                block.Init(new Vector2Int(x,y),image_slices[x,y]);
                block.OnBlockPressed += PlayerBlockMoveInput;
                blocks[x, y] = block;

            //Hiding last block
            if(y==0 && x== blocksPerLine - 1)
                {
                    blockObject.SetActive(false);
                    emptyblock = block;
                }
            }
        }
        Camera.main.orthographicSize = blocksPerLine * .55f;
        Shuffle();
        for(int x = 0; x < blocksPerLine; ++x)
        {
            for (int y = 0; y < blocksPerLine; ++y)
            {
                blocks[x, y].game_start = true;
            }
        }

    }

    void PlayerBlockMoveInput(Block targetblock)
    {
        if ((targetblock.coord - emptyblock.coord).sqrMagnitude == 1)//checking if blocks are adjacent
        {   //swapping cordinates of clicked black and empty block
            Vector2Int dummy = emptyblock.coord;
            emptyblock.coord = targetblock.coord;
            targetblock.coord = dummy;
            //Switching target and empty block
            Vector2 dummyposition = emptyblock.transform.position;
            emptyblock.transform.position = targetblock.transform.position;
            targetblock.MovetoPosition(dummyposition, .3f);
        }
        if (blocks[0, 0].game_start == true) {
            bool game_solved = true;
            for (int x = 0; x < blocksPerLine; x++)
            {
                for (int y = 0; y < blocksPerLine; y++)
                {
                    if (!blocks[x, y].isSolution())
                    {
                        game_solved = false;
                        break;
                    }
                }
            }
            if (game_solved)
            {
                emptyblock.gameObject.SetActive(true);
            }
        }
        
    }
    void Shuffle()
    {   
        Vector2Int[] offsets = { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1) };
        for(int n = 0; n < shufflelength; ++n)
        {
            int randomIndex = Random.Range(0, offsets.Length);
            for (int i = 0; i < offsets.Length; ++i)
            {
                Vector2Int offset = offsets[(randomIndex + i) % offsets.Length];
                Vector2Int shuffleblock_coord = emptyblock.coord + offset;
                if (shuffleblock_coord.x >= 0 && shuffleblock_coord.x < blocksPerLine && shuffleblock_coord.y >= 0 && shuffleblock_coord.y < blocksPerLine)
                {
                    PlayerBlockMoveInput(blocks[shuffleblock_coord.x, shuffleblock_coord.y]);
                    break;
                }
            }
        }
    }
}