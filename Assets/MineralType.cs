using System.Collections.Generic;
using UnityEngine;

public record MineralType
{
    public string Name { get; }
    public double Value { get; }
    public double Density { get; }
    public Color Color { get; }
    public float HitPoints { get; }
    public bool Dangerous { get; }
    public float DamagePerSecond { get; }
    public bool Mineable { get; }
    public bool Collision { get; }
    public bool Immolate { get; }

    public MineralType() { }

    private MineralType(string name, double value, double density, Color color, float hitPoints) : 
        this(name, value, density, color, hitPoints, dangerous: false, damagePerSecond: 0, mineable: true, collision: true, immolate: false) { }

    private MineralType(string name, double value, double density, Color color, float hitPoints, bool dangerous, float damagePerSecond, bool mineable, bool collision, bool immolate) {
        Name = name;
        Value = value;
        Density = density;
        Color = color;
        HitPoints = hitPoints;
        Dangerous = dangerous;
        DamagePerSecond = damagePerSecond;
        Mineable = mineable;
        Collision = collision;
        Immolate = immolate;
    }

    public static readonly MineralType Gold = new(
        name: "Gold",
        value: 300.1,
        density: 2.0,
        color: new Color(r: 0.7f, g: 0.5f, b: 0.0f),
        hitPoints: 12
    );

    public static readonly MineralType Dirt = new(
        name: "Dirt",
        value: 0.0,
        density: 1.0,
        color: new Color(r: 0.5f, g: 0.3f, b: 0.2f),
        hitPoints: 10
    );

    public static readonly MineralType Stone = new(
        name: "Stone",
        value: 0.0,
        density: 3.0,
        color: new Color(r: 0.4f, g: 0.4f, b: 0.4f),
        hitPoints: 15
    );

    public static readonly MineralType Uranium = new(
        name: "Uranium",
        value: 0.0,
        density: 3.0,
        color: new Color(r: 0.2f, g: 0.9f, b: 0.6f),
        hitPoints: 15
    );

    public static readonly MineralType Lava = new(
        name: "Lava",
        value: 0.0,
        density: 3.0,
        color: new Color(r: 1.0f, g: 0.0f, b: 0.0f),
        hitPoints: 15,
        dangerous: true,
        damagePerSecond: 10.0f,
        mineable: false,
        collision: false,
        immolate: true
    );
}