using Godot;
using GDColl = Godot.Collections;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class TextureLabel : GridContainer
{

	[Signal]
	public delegate void TextChangedEventHandler();

	[Signal]
	public delegate void TextureMapChangedEventHandler();
	
	private string _text;

	[Export]
	public string Text
	{
		get => _text;
		set
		{
			_text = value;
			CallDeferred(nameof(UpdateText));
			EmitSignal(SignalName.TextChanged);
		}
	}

	[Export]
	private Vector2 _fontSize;

	private GDColl.Dictionary<string, Texture2D> _textureMap = new();

	[Export]
	public GDColl.Dictionary<string, Texture2D> TextureMap
	{
		get => _textureMap;
		set
		{
			_textureMap = value;
			CallDeferred(nameof(UpdateText));
			EmitSignal(SignalName.TextureMapChanged);
		}
	}

	private readonly LinkedList<Node> _generatedNodes = new();

	public Vector2 FontSize
	{
		get => _fontSize;
		set
		{
			_fontSize = value;
		}
	}

	public void SetMap(GDColl.Dictionary<string, Texture2D> map)
	{
		_textureMap = map;
	}

	private void UpdateText()
	{
		foreach (Node child in _generatedNodes)
		{
			child.QueueFree();
		}

		_generatedNodes.Clear();
		
		foreach (char letter in _text)
		{
			
			if (!_textureMap.TryGetValue(letter.ToString(), out Texture2D texture))
			{
				continue;
			}
			
			TextureRect textureRect = new()
			{
				Texture = texture
			};

			SetControlSize(textureRect);

			AddChild(textureRect, false, InternalMode.Front);
			_generatedNodes.AddLast(textureRect);
		}
	}

	private void UpdateFontSize()
	{

		foreach (Node child in _generatedNodes)
		{
			
			if (child is TextureRect)
			{
				TextureRect control = child as TextureRect;
				SetControlSize(control);
			}
		}
	}

	private void SetControlSize(Control control)
	{
		control.CustomMinimumSize = _fontSize;
		control.CustomMaximumSize = _fontSize;
	}
}
