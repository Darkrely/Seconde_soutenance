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
# endregion

namespace DragonTears
{
    class Joueur
    {
        public Vector2 centre_joueur;
        int largeur_ecran, hauteur_ecran;
        bool vershaut, versbas, versgauche, versdroite;

        #region Declaration gestion des textures
        bool haut, bas, gauche, droite;
        Texture2D[,] textureperso = new Texture2D[4, 3];
        int compteurtext;
        #endregion

        #region Caracteristique du joueur
        public enum Sexe
        {
            homme,
            femme,
        };
        public Sexe sexe;
        public int vieMax, manaMax;
        public int vie, mana;
        #endregion

        #region Declaration du Rectangle (contient les coordonnées du joueur)
            public Rectangle rectangle { get; set; }
            public Rectangle rectangle_femme { get; set; }
            public Rectangle rectangle_femme_face { get; set; }
        #endregion

        #region Rectangle de collision
            public Rectangle collision_total { get; set; }
            public Rectangle collisionhaut { get; set; }
            public Rectangle collisionbas { get; set; }
            public Rectangle collisiongauche { get; set; }
            public Rectangle collisiondroite { get; set; }
        #endregion

        #region Gestion de la course
            bool courseactive;
        #endregion

        #region Blocage
            public bool blocage_haut, blocage_bas, blocage_gauche, blocage_droit;
        #endregion

            public Joueur(GameWindow fenetre, Sexe sexe)
        {
            largeur_ecran = fenetre.ClientBounds.Width;
            hauteur_ecran = fenetre.ClientBounds.Height;
            centre_joueur = new Vector2(largeur_ecran / 2, hauteur_ecran / 2);
            courseactive = false;

            haut = false;
            bas = true;
            gauche = false;
            droite = false;

            blocage_haut = false;
            blocage_bas = false;
            blocage_gauche = false;
            blocage_droit = false;

            this.sexe = sexe;

            vieMax = 100;
            manaMax = 0;
            vie = 100;
            mana = 0;
        }

        public Joueur(GameWindow fenetre, Sexe sexe, int x, int y)
        {
            largeur_ecran = fenetre.ClientBounds.Width;
            hauteur_ecran = fenetre.ClientBounds.Height;
            centre_joueur = new Vector2(x, y);
            courseactive = false;

            haut = false;
            bas = true;
            gauche = false;
            droite = false;

            this.sexe = sexe;
        }

        public void LoadContent(ContentManager content)
        {
            #region femme
            if (sexe == Sexe.femme)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        textureperso[i, j] = content.Load<Texture2D>("TextureJoueur\\Femme\\persof-" + i.ToString() + "-" + j.ToString());
                    }
                }
            }
            #endregion

            #region homme
            if (sexe == Sexe.homme)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        textureperso[i, j] = content.Load<Texture2D>("TextureJoueur\\Homme\\persoh-" + i.ToString() + "-" + j.ToString());
                    }
                }
            }
            #endregion
        }

        public void Positionnement(int x, int y)
        {
            centre_joueur.X = x + 20;
            centre_joueur.Y = y - 50;
        }

        public void Update(KeyboardState clavier, Carte carte, Camera camera, EcranChargement ecran_chargement)
        {
            rectangle = new Rectangle((int)(centre_joueur.X) - 20 - camera.x, (int)centre_joueur.Y - 50 - camera.y, 40, 100);
            rectangle_femme = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width * 3 / 2, rectangle.Height);
            rectangle_femme_face = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width * 28 / 20, rectangle.Height);


            collisionhaut   = new Rectangle((int)centre_joueur.X - 15, (int)centre_joueur.Y + 22, 30, 8);
            collisionbas    = new Rectangle((int)centre_joueur.X - 15, (int)centre_joueur.Y + 50, 30, 8);
            collisiongauche = new Rectangle((int)centre_joueur.X - 21, (int)centre_joueur.Y + 30, 6, 20);
            collisiondroite = new Rectangle((int)centre_joueur.X + 15, (int)centre_joueur.Y + 30, 8, 20);
            UpdateDirection(clavier);
            Update_Deplacement(clavier, carte, ecran_chargement);
        }

        public void UpdateDirection(KeyboardState clavier)
        {
            if (clavier.IsKeyDown(Keys.Z) || clavier.IsKeyDown(Keys.S) || clavier.IsKeyDown(Keys.Q) || clavier.IsKeyDown(Keys.D))
                compteurtext++;

            #region Haut
            if (clavier.IsKeyDown(Keys.Z) && !(clavier.IsKeyDown(Keys.S) && clavier.IsKeyDown(Keys.Q) && clavier.IsKeyDown(Keys.D)))
            {
                if (!versbas && !versdroite && !versgauche)
                    vershaut = true;

                haut = true;
                bas = false;
                gauche = false;
                droite = false;
            }
            if (clavier.IsKeyUp(Keys.Z))
                vershaut = false;
            #endregion

            #region Bas
            if (clavier.IsKeyDown(Keys.S) && !(clavier.IsKeyDown(Keys.Z) && clavier.IsKeyDown(Keys.Q) && clavier.IsKeyDown(Keys.D)))
            {
                if (!vershaut && !versdroite && !versgauche)
                    versbas = true;

                haut = false;
                bas = true;
                gauche = false;
                droite = false;
            }
            if (clavier.IsKeyUp(Keys.S))
                versbas = false;
            #endregion

            #region Gauche
            if (clavier.IsKeyDown(Keys.Q) && !(clavier.IsKeyDown(Keys.S) && clavier.IsKeyDown(Keys.Z) && clavier.IsKeyDown(Keys.D)))
            {
                if (!vershaut && !versbas && !versdroite)
                    versgauche = true;

                haut = false;
                bas = false;
                gauche = true;
                droite = false;
            }
            if (clavier.IsKeyUp(Keys.Q))
                versgauche = false;
            #endregion

            #region Droite
            if (clavier.IsKeyDown(Keys.D) && !(clavier.IsKeyDown(Keys.S) && clavier.IsKeyDown(Keys.Q) && clavier.IsKeyDown(Keys.Z)))
            {
                if (!vershaut && !versbas && !versgauche)
                    versdroite = true;
                haut = false;
                bas = false;
                gauche = false;
                droite = true;
            }
            if (clavier.IsKeyUp(Keys.D))
                versdroite = false;
            #endregion
        }

        public void Update_Deplacement(KeyboardState clavier, Carte carte, EcranChargement ecran_chargement)
        {
            courseactive = clavier.IsKeyDown(Keys.LeftShift);

            #region Valeur de deplacement
            int deplacement;

            if (courseactive)
            {
                deplacement = 4;
            }

            else
            {
                deplacement = 2;
            }
            #endregion

            if (!ecran_chargement.transition)
            {
                if (!blocage_gauche && clavier.IsKeyDown(Keys.Q) && centre_joueur.X - 20 > 0)
                    centre_joueur.X -= deplacement;

                if (!blocage_droit && clavier.IsKeyDown(Keys.D) && centre_joueur.X + 20 < carte.largeur)
                    centre_joueur.X += deplacement;

                if (!blocage_haut && clavier.IsKeyDown(Keys.Z) && centre_joueur.Y - 50 > 0)
                    centre_joueur.Y -= deplacement;

                if (!blocage_bas && clavier.IsKeyDown(Keys.S) && centre_joueur.Y + 50 < carte.hauteur)
                    centre_joueur.Y += deplacement;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            #region phase1
            if (compteurtext >= 0 && compteurtext < 10)
            {
                if (haut)
                {
                    spritebatch.Draw(textureperso[0, 0], rectangle, Color.White);
                }
                if (bas)
                {
                    if (sexe == Sexe.homme)
                        spritebatch.Draw(textureperso[1, 0], rectangle, Color.White);

                    else
                        spritebatch.Draw(textureperso[1, 0], new Rectangle(rectangle_femme_face.X - 7, rectangle_femme_face.Y, rectangle_femme_face.Width, rectangle_femme_face.Height), Color.White);
                }
                if (gauche)
                {
                    if (sexe == Sexe.homme)
                        spritebatch.Draw(textureperso[2, 0], new Rectangle(rectangle.X + (int)(rectangle.Width * 0.15), rectangle.Y, (int)(rectangle.Width * 0.8), rectangle.Height), Color.White);

                    else
                        spritebatch.Draw(textureperso[2, 0], rectangle, Color.White);
                }
                if (droite)
                {
                    if (sexe == Sexe.homme)
                        spritebatch.Draw(textureperso[3, 0], new Rectangle(rectangle.X, rectangle.Y, (int)(rectangle.Width * 0.8), rectangle.Height), Color.White);

                    else
                        spritebatch.Draw(textureperso[3, 0], rectangle, Color.White);
                }
            }
            #endregion

            #region phase2
            if (compteurtext >= 10 && compteurtext < 20)
            {
                if (haut)
                {
                    spritebatch.Draw(textureperso[0, 1], rectangle, Color.White);
                }
                if (bas)
                {
                    spritebatch.Draw(textureperso[1, 1], rectangle, Color.White);
                }
                if (gauche)
                {
                    if (sexe == Sexe.homme)
                        spritebatch.Draw(textureperso[2, 1], rectangle, Color.White);

                    else
                        spritebatch.Draw(textureperso[2, 1], new Rectangle(rectangle_femme.X - 7, rectangle_femme.Y, rectangle_femme.Width, rectangle_femme.Height), Color.White);
                }
                if (droite)
                {
                    if (sexe == Sexe.homme)
                        spritebatch.Draw(textureperso[3, 1], rectangle, Color.White);

                    else
                        spritebatch.Draw(textureperso[3, 1], new Rectangle(rectangle_femme.X - 7, rectangle_femme.Y, rectangle_femme.Width, rectangle_femme.Height), Color.White);
                }
            }
            #endregion

            #region phase3
            if (compteurtext >= 20 && compteurtext < 30)
            {
                if (haut)
                {
                    spritebatch.Draw(textureperso[0, 0], rectangle, Color.White);
                }
                if (bas)
                {
                    if (sexe == Sexe.homme)
                        spritebatch.Draw(textureperso[1, 0], rectangle, Color.White);

                    else
                        spritebatch.Draw(textureperso[1, 0], new Rectangle(rectangle_femme_face.X - 7, rectangle_femme_face.Y, rectangle_femme_face.Width, rectangle_femme_face.Height), Color.White);
                }
                if (gauche)
                {
                    if (sexe == Sexe.homme)
                        spritebatch.Draw(textureperso[2, 0], new Rectangle(rectangle.X + (int)(rectangle.Width * 0.15
                            ), rectangle.Y, (int)(rectangle.Width * 0.8), rectangle.Height), Color.White);

                    else
                        spritebatch.Draw(textureperso[2, 0], rectangle, Color.White);
                }
                if (droite)
                {
                    if (sexe == Sexe.homme)
                        spritebatch.Draw(textureperso[3, 0], new Rectangle(rectangle.X, rectangle.Y, (int)(rectangle.Width * 0.8), rectangle.Height), Color.White);

                    else
                        spritebatch.Draw(textureperso[3, 0], rectangle, Color.White);
                }
            }
            #endregion

            #region phase4
            if (compteurtext >= 30 && compteurtext < 40)
            {
                if (haut)
                {
                    spritebatch.Draw(textureperso[0, 2], rectangle, Color.White);
                }
                if (bas)
                {
                    spritebatch.Draw(textureperso[1, 2], rectangle, Color.White);
                }
                if (gauche)
                {
                    if (sexe == Sexe.homme)
                        spritebatch.Draw(textureperso[2, 2], rectangle, Color.White);

                    else
                        spritebatch.Draw(textureperso[2, 2], new Rectangle(rectangle_femme.X - 7, rectangle_femme.Y, rectangle_femme.Width, rectangle_femme.Height), Color.White);
                }
                if (droite)
                {
                    if (sexe == Sexe.homme)
                        spritebatch.Draw(textureperso[3, 2], rectangle, Color.White);

                    else
                        spritebatch.Draw(textureperso[3, 2], new Rectangle(rectangle_femme.X - 7, rectangle_femme.Y, rectangle_femme.Width, rectangle_femme.Height), Color.White);
                }
            }
            if (compteurtext >= 39)
                compteurtext = 0;
            #endregion
        }
    }
}