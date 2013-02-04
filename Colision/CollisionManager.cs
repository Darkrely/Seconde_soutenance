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
using System.IO;
# endregion

namespace DragonTears
{
    class Gestionnaire_collision
    {
        public static void CollisionPersoDeplacement(Carte mapManager, Joueur perso)
        {
            perso.blocage_haut = false;
            perso.blocage_bas = false;
            perso.blocage_gauche = false;
            perso.blocage_droit = false;

            foreach (List<Rectangle> rectangleligne in mapManager.collision)
            {
                foreach (Rectangle rectangle in rectangleligne)
                {
                    if (new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height).Intersects(perso.collisionhaut))
                    {
                        perso.blocage_haut = true;
                    }
                    if (new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height).Intersects(perso.collisionbas))
                    {
                        perso.blocage_bas = true;
                    }
                    if (new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height).Intersects(perso.collisiongauche))
                    {
                        perso.blocage_gauche = true;
                    }
                    if (new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height).Intersects(perso.collisiondroite))
                    {
                        perso.blocage_droit = true;
                    }
                }
            }
        }
    }
}
