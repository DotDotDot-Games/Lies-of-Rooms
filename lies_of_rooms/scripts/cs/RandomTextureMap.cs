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

	[Export]
	private int _repeatTries = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		translator.InitializeTranslations();
		translator.RandomizeTranslation(_repeatTries);

		if (label is null)
		{
			return;
		}

		
		label.TextureMap = translator.Translation;
	}
}
