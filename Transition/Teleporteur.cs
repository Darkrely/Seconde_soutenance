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
    class Teleporteur
    {
        public string Destination { get; set; }
        public string Arrive { get; set; }
        int xdestination, ydestination;
        int xarrive, yarrive, musique;
        Rectangle zoneteleporteuse;
        public Rectangle Coordonnees;

        public Teleporteur(int xdepart, int ydepart, int xdest, int ydest, string cartedest, string carteactuel, int width, int height, int musique)
        {
            xarrive = xdepart;
            yarrive = ydepart;
            xdestination = xdest;
            ydestination = ydest;
            Destination = cartedest;
            Arrive = carteactuel;
            zoneteleporteuse = new Rectangle(xarrive, yarrive, width, height);
            Coordonnees = zoneteleporteuse;
            this.musique = musique;
        }

        public void Teleportation(Joueur joueur, Carte carte, GameWindow window, GestionTeleportation gestion_teleportation, EcranChargement ecran_chargement)
        {
            if (!gestion_teleportation.Transition_active && Coordonnees.Intersects(new Rectangle((int)joueur.centre_joueur.X - 20, (int)joueur.centre_joueur.Y - 20, 40, 70)))
            {
                gestion_teleportation.Transition_active = true;
                ecran_chargement.transition = true;
                carte.Chargement(Destination);
                joueur.Positionnement(xdestination, ydestination);
            }
        }
    }
}
