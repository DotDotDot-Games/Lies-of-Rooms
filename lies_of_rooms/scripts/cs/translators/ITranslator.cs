using Godot;
using System;
using System.Linq;
using GDColl = Godot.Collections;

public partial interface ITranslator
{
    //public Texture2D[] Shapes { get; set; }

    public GDColl.Dictionary<string, Texture2D> Translation { get; }

    public virtual Texture2D GetShape(string letter)
    {
        return Translation[letter];
    }

    public virtual string GetCharacter(Texture2D shape)
    {
        return Translation.FirstOrDefault(translation => translation.Value == shape).Key;
    }
    
    public virtual void SetCharacter(char letter, Texture2D texture)
    {
        Translation[letter.ToString()] = texture;
    }


}
