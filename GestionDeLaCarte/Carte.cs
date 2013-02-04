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
    class Carte
    {
        #region Variables
        //Textures
        Texture2D textureeau;
        Texture2D textureherbe;
        Texture2D texturesolbois;
        Texture2D texturemurbois;
        Texture2D texturesable;
        Texture2D texturesolpierre;
        Texture2D texturemurpierre;
        Texture2D textureeausable;
        Texture2D texturesableeau;
        Texture2D coltext;
        Texture2D textureterre;
        //Carte
        public List<List<char>> carte;
        List<char> ligne;
        public string carte_actuelle { get; set; }
        //Coordonnées
        public int largeur;
        public int hauteur;
        //Collision
        public List<List<Rectangle>> collision;
        public List<Rectangle> collisionligne;
        //Transition
        GestionTeleportation gestionTeleportation;
        #endregion

        public Carte()
        {
            largeur = 0;
            hauteur = 0;
            //
            carte = new List<List<char>>();
            ligne = new List<char>();
            //
            collision = new List<List<Rectangle>>();
            collisionligne = new List<Rectangle>();
            //
            gestionTeleportation = new GestionTeleportation();
        }

        public void LoadContent(ContentManager content)
        {
            textureeau = content.Load<Texture2D>("Décors\\Mursetsols\\eau");
            textureherbe = content.Load<Texture2D>("Décors\\Mursetsols\\herbe");
            texturesolbois = content.Load<Texture2D>("Décors\\Mursetsols\\solbois");
            texturemurbois = content.Load<Texture2D>("Décors\\Mursetsols\\murbois");
            texturesable = content.Load<Texture2D>("Décors\\Mursetsols\\sable");
            texturesolpierre = content.Load<Texture2D>("Décors\\Mursetsols\\solpierre");
            texturemurpierre = content.Load<Texture2D>("Décors\\Mursetsols\\murpierre");
            textureeausable = content.Load<Texture2D>("Décors\\Mursetsols\\eausable");
            texturesableeau = content.Load<Texture2D>("Décors\\Mursetsols\\sableeau");
            textureterre = content.Load<Texture2D>("Décors\\Mursetsols\\terre");
            coltext = content.Load<Texture2D>("collision");            
        }

        public void Update(KeyboardState clavier, Joueur joueur, GameWindow window, EcranChargement ecran_chargement)
        {
            gestionTeleportation.Desactivation_Transition(joueur);
            gestionTeleportation.TestTeleportation(this, joueur, window, ecran_chargement);
        }

        public void Chargement(string adress)
        {
            ChargementMap(adress);
            ChargementCollisionCarte(adress);
            gestionTeleportation.ChargementTeleporteur(adress);
        }

        public void ChargementMap(string adress)
        {
            carte_actuelle = adress;
            largeur = 0;
            hauteur = 0;
            carte = new List<List<char>>() { };
            ligne = new List<char>() { };
            try
            {
                StreamReader monStreamReader = new StreamReader("DATA\\Carte\\" + adress + ".map");
                string line = monStreamReader.ReadLine();

                while (line != null)
                {
                    hauteur++;
                    if (largeur < line.Length)
                    {
                        largeur = line.Length;
                    }
                    ligne = new List<char>() { };
                    for (int i = 0; i < line.Length + 20; i++)
                    {
                        if (i < line.Length)
                        {
                            ligne.Add(line[i]);
                        }
                    }
                    carte.Add(ligne);
                    line = monStreamReader.ReadLine();
                }
                monStreamReader.Close();
                largeur *= 40;
                hauteur *= 40;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ChargementCollisionCarte(string adress)
        {
            int y = 0;
            collision = new List<List<Rectangle>>() { };
            collisionligne = new List<Rectangle>() { };

            try
            {
                StreamReader monStreamReader = new StreamReader("DATA\\Carte\\" + adress + ".map");
                string line = monStreamReader.ReadLine();

                while (line != null)
                {
                    collisionligne = new List<Rectangle>() { };

                    for (int i = 0; i < line.Length + 20; i++)
                    {
                        if (i < line.Length)
                        {
                            if (line[i] == '~')
                            {
                                collisionligne.Add(new Rectangle(40 * i, 40 * y, 40, 40));
                            }
                            else if (line[i] == '_')
                            {
                            }
                            else if (line[i] == 't')
                            {
                            }
                            else if (line[i] == 'p')
                            {
                            }
                            else if (line[i] == '.')
                            {
                            }
                            else if (line[i] == '_')
                            {
                            }
                            else if (line[i] == '#')
                            {
                            }
                            else if (line[i] == '=')
                            {
                                collisionligne.Add(new Rectangle(40 * i, 40 * y, 40, 40));
                            }
                            else if (line[i] == '-')
                            {
                                collisionligne.Add(new Rectangle(40 * i, 40 * y, 40, 40));
                            }
                            else if (line[i] == '/')
                            {
                                collisionligne.Add(new Rectangle(40 * i + 20, 40 * y + 20, 20, 20));
                            }
                            else if (line[i] == '\\')
                            {
                                collisionligne.Add(new Rectangle(40 * i, 40 * y + 20, 20, 20));
                            }
                            else
                            {
                                collisionligne.Add(new Rectangle(40 * i, 40 * y, 40, 40));
                            }
                        }
                        else
                        {
                            collisionligne.Add(new Rectangle(40 * i, 40 * y, 40, 40));
                        }
                    }
                    collision.Add(collisionligne);
                    line = monStreamReader.ReadLine();
                    y++;
                }
                monStreamReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AffichageCase(int x, int y, SpriteBatch spriteBatch, char texture)
        {
            Case casee;

            casee = new Case(x, y, texture);

            if (casee.text == Case.Typetext.Eau)
            {
                spriteBatch.Draw(textureeau, casee.rectangle, Color.White);
            }
            else if (casee.text == Case.Typetext.Herbe)
            {
                spriteBatch.Draw(textureherbe, casee.rectangle, Color.White);
            }
            else if (casee.text == Case.Typetext.Murbois)
            {
                spriteBatch.Draw(texturemurbois, casee.rectangle, Color.White);
            }
            else if (casee.text == Case.Typetext.Terre)
            {
                spriteBatch.Draw(textureterre, casee.rectangle, Color.White);
            }
            else if (casee.text == Case.Typetext.Murpierre)
            {
                spriteBatch.Draw(texturemurpierre, casee.rectangle, Color.White);
            }
            else if (casee.text == Case.Typetext.Sable)
            {
                spriteBatch.Draw(texturesable, casee.rectangle, Color.White);
            }
            else if (casee.text == Case.Typetext.SableEau)
            {
                spriteBatch.Draw(texturesableeau, casee.rectangle, Color.White);
            }
            else if (casee.text == Case.Typetext.EauSable)
            {
                spriteBatch.Draw(textureeausable, casee.rectangle, Color.White);
            }
            else if (casee.text == Case.Typetext.Solbois)
            {
                spriteBatch.Draw(texturesolbois, casee.rectangle, Color.White);
            }
            else if (casee.text == Case.Typetext.Solpierre)
            {
                spriteBatch.Draw(texturesolpierre, casee.rectangle, Color.White);
            }
            else
            {

            }
        }
    }
}
