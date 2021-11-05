using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Item", menuName= "Gameplay/New Item", order = 51)]

public class ItemsData : ScriptableObject
{
    [Tooltip("Уникальное имя объекта.")]
    [SerializeField] private string _name;
    public string Name => this._name;

    [Tooltip("Ширина спрайта объекта.")]
    [SerializeField] private float _width;
    public float Width => this._width;

    [Tooltip("Высота спрата объекта.")]
    [SerializeField] private float _height;
    public float Height => this._height;


    [Tooltip("Спрайт объекта.")]
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;
}
