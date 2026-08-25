using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using GDColl = Godot.Collections;


public partial interface ITranslator<[MustBeVariant] T>
{
    //public Texture2D[] Shapes { get; set; }

    public T[] AvailableTranslations { get; }

    public GDColl.Dictionary<string, T> Translation { get; }

    public virtual T GetTranslation(string letter)
    {
        return Translation[letter];
    }

    public virtual string GetCharacter(T shape)
    {
        return Translation.FirstOrDefault(translation => translation.Value.Equals(shape)).Key;
    }
    
    public virtual void SetCharacter(char letter, T texture)
    {
        Translation[letter.ToString()] = texture;
    }
}
