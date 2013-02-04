﻿#region Using
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
    class GameManager
    {
        public enum etat{
            Lancement,
            Menu,
            InGame,
            Pause,
            Credits
        };
        public etat Etat;
        bool pauseactive, combatactive;
        public bool combat;

        public GameManager()
        {
            Etat = etat.Lancement;
            combat = false;
            combatactive = false;
        }

        public void Update(KeyboardState clavier, Menu menu)
        {
            #region GestionPause
            if (clavier.IsKeyDown(Keys.Escape))
                pauseactive = true;

            if (clavier.IsKeyUp(Keys.Escape) && pauseactive)
            {
                if (Etat == etat.InGame)
                {
                    menu.mode = Menu.Mode.Pause;
                    Etat = etat.Pause;
                }

                pauseactive = false;
            }
            #endregion

            #region Gestion Combat
            if (clavier.IsKeyDown(Keys.Space))
                combatactive = true;

            if (clavier.IsKeyUp(Keys.Space) && combatactive)
            {
                if (Etat == etat.InGame)
                {
                    combat = !combat;
                }

                combatactive = false;
            }
            #endregion
        }
    }
}
