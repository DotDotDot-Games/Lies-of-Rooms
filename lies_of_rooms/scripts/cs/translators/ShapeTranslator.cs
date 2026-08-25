using Godot;
using GDColl = Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

[GlobalClass]
public partial class ShapeTranslator : GenericTranslator<Texture2D>
{

    private const string _shapesPath = "res://assets/images/shapes";

    public void RandomizeTranslation(int repeat = 1)
    {

        if (Translation is null)
        {
            Translation = new();
        }
        else
        {
            Translation.Clear();
        }

        List<Texture2D> shapes = new(AvailableTranslations);

        shapes = Enumerable.Repeat(shapes, repeat).SelectMany(x => x).ToList();

        List<char> letters = Enumerable
            .Range('a', 26)
            .Select(i => (char)i)
            .OrderBy(_ => Random.Shared.Next())
            .ToList();
        
        foreach (var letter in letters)
        {

            if (shapes.Count == 0)
            {
                Translation[letter.ToString()] = null;
                continue;
            }

            int idx = Random.Shared.Next(shapes.Count);

            Translation[letter.ToString()] = shapes[idx];

            shapes.RemoveAt(idx);
        }
    }

    protected override Texture2D[] LoadTranslations()
    {
        DirAccess directory = DirAccess.Open(_shapesPath);

        if (directory is null)
        {
            GD.PushError($"ShapeTranslator: Invalid folder = {_shapesPath}");
            return [];
        }

        List<Texture2D> textures = new();

        foreach (string file in directory.GetFiles())
        {
            if (!file.EndsWith(".png"))
            {
                continue;
            }

            string path = $"{_shapesPath}/{file}";
            Texture2D texture = GD.Load<Texture2D>(path);

            if (texture is not null)
            {
                textures.Add(texture);
            }
            else
            {
                GD.PushError($"ShapeTranslator: Invalid texture = {path}");
            }
        }

        return textures.ToArray();
    }
}
