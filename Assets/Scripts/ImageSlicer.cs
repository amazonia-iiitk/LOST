using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ImageSlicer 
{
    public static Texture2D[,] GetSlices(Texture2D image, int blocksPerLine)
    {
        int imagesize = Mathf.Min(image.width, image.height);
        int blocksize = imagesize / blocksPerLine;

        Texture2D[,] blocks = new Texture2D[blocksPerLine, blocksPerLine];


        for(int y = 0; y < blocksPerLine; y++)
        {
            for(int x=0; x < blocksPerLine; x++)
            {
                Texture2D block = new Texture2D(blocksize, blocksize);
                block.wrapMode = TextureWrapMode.Clamp;
                block.SetPixels(image.GetPixels(x * blocksize, y * blocksize, blocksize, blocksize));
                block.Apply();
                blocks[x, y] = block;
            }
        }
        return blocks;
    }
}
