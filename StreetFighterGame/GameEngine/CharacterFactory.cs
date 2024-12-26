using System;
using StreetFighterGame.Characters;

namespace StreetFighterGame.GameEngine
{
    public static class CharacterFactory
    {
        public static Character CreateCharacter(string name, int startX, int startY)
        {
            switch (name.ToLower())
            {
                case "ryu":
                    return new Ryu(startX, startY, scaleFactor: 2.5f);
                case "chunli":
                    return new Chunli(startX, startY, scaleFactor: 2.5f);
                case "goku":
                    return new Goku(startX, startY, scaleFactor: 2.5f);
                case "zenitsu":
                    return new Zenitsu(startX, startY, scaleFactor: 2.5f);
                case "vegeto":
                    return new Vegeto(startX, startY, scaleFactor: 2.5f);
                case "gojo":
                    return new Gojo(startX, startY, scaleFactor: 2.5f);
                default:
                    return new Ryu(startX, startY, scaleFactor: 2.5f);
            }
        }
    }
}
