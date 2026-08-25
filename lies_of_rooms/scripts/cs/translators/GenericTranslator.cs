using Godot;
using System;
using GDCollections = Godot.Collections;

public abstract partial class GenericTranslator<[MustBeVariant] T> : Resource, ITranslator<T>
{
    
    public GDCollections.Dictionary<string, T> Translation { get; protected set; }

    public T[] AvailableTranslations { get; protected set; } = [];

    public void InitializeTranslations()
    {
        AvailableTranslations = LoadTranslations();
    }

    protected abstract T[] LoadTranslations();
}
