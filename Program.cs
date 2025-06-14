// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using LibHammer.Serialization;
using LibHammer.Structs;

Console.WriteLine("Hello, World!");

List<Weapon> weapons = new();

weapons.Add(new Weapon
{
    ArmorPiercing = 2,
    Attack = 4,
    Damage = 1,
    Name = "fortnite pickaxe",
    Range = 0,
    Skill = 2,
    Strength = 5
});

weapons.Add(new Weapon
{
    ArmorPiercing = 2,
    Attack = 4,
    Damage = 1,
    Name = "forasdasdasdxe",
    Range = 0,
    Skill = 2,
    Strength = 5
});

weapons.Add(new Weapon
{
    ArmorPiercing = 2,
    Attack = 4,
    Damage = 1,
    Name = "forgdssdgkaxe",
    Range = 0,
    Skill = 2,
    Strength = 5
});

Dictionary<string, Weapon> dict = new();

foreach (var weapon in weapons)
{
    dict.Add(weapon.Name, weapon);
}

Serializer.SaveWeapons(dict);