using Godot;
using System;
using System.Linq;

[GlobalClass]
[Tool]
public partial class TextCode : Node
{

	private string _code = "";

	[Export]
	public int codeLength { get; private set; } = 4;

	[Export]
	public string Code
	{
		get => _code;
		private set
		{
			_code = value[..Mathf.Min(value.Length, codeLength)];
		}
	}

	[ExportToolButton("Generate Code")]
	public Callable generateCode => Callable.From(SetRandomCode);


    public override void _Ready()
    {
        base._Ready();

		if (Code.Length == 0)
		{
			SetRandomCode();
		}
    }

	public virtual bool CompareCode(string input) => Code == input;

	private void SetRandomCode()
	{
		Code = GenerateRandomCode(codeLength);
	}

	public static string GenerateRandomCode(int length)
	{
		string ableCharacters = new string(
			Enumerable.Range('a', 26)
				.Select(i => (char)i)
				.ToArray()
		);

		char[] result = new char[length];

		for (int i = 0; i < length; i++)
		{
			result[i] = ableCharacters[Random.Shared.Next(ableCharacters.Length)];
		}

		return new string(result);
	}
}
