using Godot;
using Godot.Collections;
using System;
using System.Linq;

[GlobalClass]
[Tool]
public partial class PropertyCopier : Node
{

	[Signal]
	public delegate void CopiedEventHandler();

	private Node _node;

	[Export]
	public virtual Node MainNode
	{
		get => _node;
		set
		{
			_node = value;
			NotifyPropertyListChanged();
		}
	}

	[Export]
	private StringName nodeProp;

	public Node _target;

	[Export]
	public virtual Node Target
	{
		get => _target;
		set
		{
			_target = value;
			NotifyPropertyListChanged();
		}
	}

	[Export]
	public StringName targetProp;

    public override void _Process(double delta)
	{
		if (MainNode is null || Target is null)
		{
			return;
		}

		Variant propValue = MainNode.Get(nodeProp);
		Variant targetPropValue = Target.Get(targetProp);

		if (propValue.Equals(targetPropValue))
		{
			return;
		}

		Target.Set(targetProp, propValue);
		EmitSignal(SignalName.Copied);
	}

    public override void _ValidateProperty(Dictionary property)
    {
        base._ValidateProperty(property);

		if (property["name"].ToString() == "nodeProp")
		{
			SetPropertyHint(MainNode, property);
		}
		else if (property["name"].ToString() == "targetProp")
		{
			SetPropertyHint(Target, property);
		}
    }

	public static void SetPropertyHint<T>(T node, Dictionary property) where T: Node
	{
		if (node is null)
		{
			return;
		}

		string[] props = node.GetPropertyList().Select(prop => prop["name"].AsString()).ToArray();

		property["hint"] = Variant.CreateFrom((int)PropertyHint.Enum);
		property["hint_string"] = props.Join(",");
	}
}
