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
    class Camera
    {
        bool animationactive, affichage_collision, affichage_collision_joueur, touchecol, touchecol_joueur;
        public int x, y;
        int largeur_ecran, hauteur_ecran;
        Texture2D texture_collision;

        public Camera(GameWindow fenetre)
        {
            x = 0;
            y = 0;
            largeur_ecran = fenetre.ClientBounds.Width;
            hauteur_ecran = fenetre.ClientBounds.Height;
            animationactive = false;
        }

        public Camera(GameWindow fenetre, int x, int y)
        {
            this.x = x;
            this.y = y;
            largeur_ecran = fenetre.ClientBounds.Width;
            hauteur_ecran = fenetre.ClientBounds.Height;
            animationactive = false;
        }

        public void LoadContent(ContentManager content)
        {
            texture_collision = content.Load<Texture2D>("collision");
        }

        public void Update(Joueur joueur, Carte carte, KeyboardState clavier)
        {
            #region Affichage collision
            if (clavier.IsKeyDown(Keys.F4))
            {
                touchecol = true;
            }

            if (clavier.IsKeyUp(Keys.F4) && touchecol)
            {
                touchecol = false;
                affichage_collision = !affichage_collision;
            }
            #endregion

            #region Affichage collision joueur
            if (clavier.IsKeyDown(Keys.F3))
            {
                touchecol_joueur = true;
            }

            if (clavier.IsKeyUp(Keys.F3) && touchecol_joueur)
            {
                touchecol_joueur = false;
                affichage_collision_joueur = !affichage_collision_joueur;
            }
            #endregion

            if (!animationactive)
            {
                if (joueur.centre_joueur.X >= carte.largeur - largeur_ecran / 2 && joueur.centre_joueur.X <= carte.largeur)
                {
                    x = carte.largeur - largeur_ecran;
                }

                else if (joueur.centre_joueur.X <= largeur_ecran / 2 && joueur.centre_joueur.X >= 0)
                {
                    x = 0;
                }

                else
                {
                    x = (int)(joueur.centre_joueur.X - largeur_ecran / 2);
                }

                if (joueur.centre_joueur.Y >= carte.hauteur - hauteur_ecran / 2 && joueur.centre_joueur.Y <= carte.hauteur)
                {
                    y = carte.hauteur - hauteur_ecran;
                }

                else if (joueur.centre_joueur.Y <= hauteur_ecran / 2 && joueur.centre_joueur.Y >= 0)
                {
                    y = 0;
                }

                else
                {
                    y = (int)(joueur.centre_joueur.Y - hauteur_ecran / 2);
                }

                
            }
        }

        public void Deplacement(int trans_x, int trans_y)
        {
            x += trans_x;
            y += trans_y;
        }

        public void Affichage(Carte carte, Joueur joueur, SpriteBatch spriteBatch)
        {
            for (int i = y / 40; i < carte.carte.Count; i++)
            {
                for (int j = x / 40; j < carte.carte[i].Count && i < (largeur_ecran + 40) / 40; j++)
                {
                    carte.AffichageCase(40 * j - x, 40 * i - y, spriteBatch, carte.carte[i][j]);
                }
            }

            if (affichage_collision)
            {
                foreach (List<Rectangle> rect_ligne in carte.collision)
                {
                    foreach (Rectangle rect in rect_ligne)
                    {
                        spriteBatch.Draw(texture_collision, new Rectangle(rect.X - x, rect.Y - y, rect.Width, rect.Height), Color.White);
                    }
                }
            }

            joueur.Draw(spriteBatch);
        }
    }
}
