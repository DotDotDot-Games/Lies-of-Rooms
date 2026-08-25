using Godot;
using GDColl = Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

[GlobalClass]
public partial class ShapeTranslator : Resource, ITranslator
{

    private const string _shapesPath = "res://assets/images/shapes";
    private static readonly Texture2D[] _availableShapes = LoadShapes();

    private readonly GDColl.Dictionary<string, Texture2D> _translation = new();

    public GDColl.Dictionary<string, Texture2D> Translation => _translation;

    public Texture2D GetShape(string letter)
    {
        return _translation[letter];
    }

    public string GetCharacter(Texture2D shape)
    {
        return _translation.FirstOrDefault(translation => translation.Value == shape).Key;
    }

    public void SetCharacter(string letter, Texture2D texture)
    {
        _translation[letter] = texture;
    }

    public void RandomizeTranslation()
    {
        _translation.Clear();

        List<Texture2D> shapes = new(_availableShapes);

        List<char> letters = Enumerable
            .Range('a', 26)
            .Select(i => (char)i)
            .OrderBy(_ => Random.Shared.Next())
            .ToList();
        
        foreach (var letter in letters)
        {

            if (shapes.Count == 0)
            {
                _translation[letter.ToString()] = null;
                continue;
            }

            int idx = Random.Shared.Next(shapes.Count);

            _translation[letter.ToString()] = shapes[idx];

            shapes.RemoveAt(idx);
        }
    }

    private static Texture2D[] LoadShapes()
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
