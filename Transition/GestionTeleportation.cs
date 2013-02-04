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
    class GestionTeleportation
    {
        List<Teleporteur> liste_teleporteur;
        int compteur_optimisation;
        public bool Transition_active;

        public GestionTeleportation()
        {
            compteur_optimisation = 0;
            Transition_active = false;
        }

        public void ChargementTeleporteur(string nom_map)
        {
            liste_teleporteur = new List<Teleporteur>() { };
            StreamReader sr = new StreamReader("DATA\\Transition\\" + nom_map + ".tel");
            string line = " ";
            string map_destination = " ", map_depart = " ";
            int largeur = 0, hauteur = 0, xdepart = 0, ydepart = 0, xdestination = 0, ydestination = 0, musique = 0;
            
            while ((line = sr.ReadLine()) != null)
            {
                map_destination = line;

                line = sr.ReadLine();
                xdestination = Convert.ToInt32(line);
                line = sr.ReadLine();
                ydestination = Convert.ToInt32(line);

                
                line = sr.ReadLine();
                map_depart = line;

                line = sr.ReadLine();
                xdepart = Convert.ToInt32(line);
                line = sr.ReadLine();
                ydepart = Convert.ToInt32(line);
                

                line = sr.ReadLine();
                largeur = Convert.ToInt32(line);
                line = sr.ReadLine();
                hauteur = Convert.ToInt32(line);

                line = sr.ReadLine();
                musique = Convert.ToInt32(line);

                line = sr.ReadLine();
            }

            liste_teleporteur.Add(new Teleporteur(xdepart, ydepart, xdestination, ydestination, map_destination, map_depart, largeur, hauteur, musique));

            sr.Close();
        }

        public void Desactivation_Transition(Joueur joueur)
        {
            bool trouve = false;

            foreach (Teleporteur teleporteur in liste_teleporteur)
            {
                if (Transition_active && teleporteur.Coordonnees.Intersects(new Rectangle((int)joueur.centre_joueur.X - 20, (int)joueur.centre_joueur.Y - 20, 40, 70)))
                {
                    trouve = true;
                }
            }

            if (!trouve)
                Transition_active = false;
        }

        public void TestTeleportation(Carte map, Joueur joueur, GameWindow window, EcranChargement ecran_chargement)
        {
            if (compteur_optimisation == 0)
            {
                if (liste_teleporteur.Count > 0)
                {
                    foreach (Teleporteur teleporteur in liste_teleporteur)
                    {
                        teleporteur.Teleportation(joueur, map, window, this, ecran_chargement);
                    }
                }
            }

            compteur_optimisation++;

            compteur_optimisation %= 40;
        }
    }
}
