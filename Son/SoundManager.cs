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
    class Gestionnaire_son
    {
        SoundEffect[] sons = new SoundEffect[5];
        SoundEffectInstance sonmanager;
        bool jeu;
        int piste;

        public Gestionnaire_son()
        {
            jeu = true;
            piste = 1;
        }

        public void LoadContent(ContentManager content)
        {
            sons[0] = content.Load<SoundEffect>("debut");
            sons[1] = content.Load<SoundEffect>("01-yasuharu_takanashi-fairy_tail_main_theme-cocmp3");
            sons[2] = content.Load<SoundEffect>("taverne");
            sons[3] = content.Load<SoundEffect>("festif"); 
            sons[4] = content.Load<SoundEffect>("aventure");
            sonmanager = sons[0].CreateInstance();
            sonmanager.IsLooped = true;
            sonmanager.Play();
        }

        public void Update(GameManager gameManager, Menu menu)
        {
            if (jeu && gameManager.Etat == GameManager.etat.InGame)
            {
                sonmanager.Stop();
                sonmanager = sons[2].CreateInstance();
                sonmanager.IsLooped = false;
                sonmanager.Play();
                jeu = false;
            }

            if (gameManager.Etat == GameManager.etat.InGame && sonmanager.State == SoundState.Stopped)
            {
                piste++;
                if (piste == 1)
                {
                    sonmanager.Stop();
                    sonmanager = sons[2].CreateInstance();
                    sonmanager.IsLooped = false;
                    sonmanager.Play();
                }
                else if (piste == 2)
                {
                    sonmanager.Stop();
                    sonmanager = sons[3].CreateInstance();
                    sonmanager.IsLooped = false;
                    sonmanager.Play();
                }
                else if (piste == 3)
                {
                    sonmanager.Stop();
                    sonmanager = sons[4].CreateInstance();
                    sonmanager.IsLooped = false;
                    sonmanager.Play();
                }
                else
                {
                    piste = 0;
                }
            }

            if (!menu.pauseactive)
            {
                if (menu.sound == Menu.Son.On && sonmanager.State == SoundState.Paused)
                {
                    sonmanager.Play();
                }

                if (menu.sound == Menu.Son.Off && sonmanager.State == SoundState.Playing)
                {
                    sonmanager.Pause();
                }
            }
            
            if(menu.pauseactive)
            {
                if (sonmanager.State == SoundState.Playing)
                    sonmanager.Pause();
            }
        }
    }
}
