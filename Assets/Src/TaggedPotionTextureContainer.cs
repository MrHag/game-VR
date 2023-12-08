using System;
using UnityEngine;

[Serializable]
public class SidedTexture
{
    public Texture2D left;
    public Texture2D right;
}
public class TaggedPotionTextureContainer : TaggedContainer<SidedTexture>
{
}