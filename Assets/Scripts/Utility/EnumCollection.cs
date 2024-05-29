using System;

namespace Utility
{
    public enum Element
    {
        Wind, Fire, Water, Soil, Wood,
        Rock,    // Fire + Soil
        Ember,   // Fire + Wood
        Steam,   // Fire + Water
        Wetland  // Soil + Water
    }

    public enum ECommand
    {
        Move, Interact
    }
    
    public enum SceneType
    {
        Level, Menu
    }
}
