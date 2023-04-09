using System;
using UnityEngine;

namespace FlatVillage.Controls
{
    public interface IMainControlActionsProvider
    {
        event Action<Vector2> Clicked;
        event Action<Vector2> Moved;
        event Action<float> Zoom;

        public Vector2 LastActionScreenPoint { get; }
    }
}