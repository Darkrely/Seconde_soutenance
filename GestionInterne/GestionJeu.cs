#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
# endregion


namespace DragonTears
{
    class GestionJeu
    {
        public static void NouveauJeu(Carte map, Joueur joueur, GameWindow window) 
        {   
            map.Chargement("000");
        }

        public static void ChargerJeu(Carte map, string carte, int x, int y, Joueur joueur, GameWindow window)
        {
            map.Chargement(carte);
        }
    }
}