using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

[GlobalClass]
//[Tool]
public partial class RandomTextureMap : Node
{

	[Export]
	public TextureLabel label;

	[Export]
	public ShapeTranslator translator = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		translator.RandomizeTranslation();

		if (label is null)
		{
			return;
		}

		
		label.TextureMap = translator.Translation;
	}
}
