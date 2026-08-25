using Godot;
using Godot.Collections;
using System;
using System.Linq;

[GlobalClass]
[Tool]
public partial class CopyLabelTextNode : Node
{

	[Export]
	public Node label;
	
	[Export]
	public Node target;

	public override void _Process(double delta)
	{
		
		if (label is null || target is null)
		{
			return;
		}

		if (label.GetType().GetProperty("Text") is null || target.GetType().GetProperty("Text") is null)
		{
			return;
		}
		
		target.Set("Text", label.Get("Text"));
	}

    public override void _ValidateProperty(Dictionary property)
    {
        base._ValidateProperty(property);


		string[] props = ["label", "target"];
		if (props.Contains(property["name"].AsString())) {
			property["hint_string"] = "Label,TextureLabel";
		}
    }
}
