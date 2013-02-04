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
    class EcranChargement
    {
        Texture2D text_ecran;
        Rectangle coord_ecran;
        public bool transition;
        int compteur, opacite;

        public EcranChargement(GameWindow window)
        {
            coord_ecran = new Rectangle(0, 0, window.ClientBounds.Width, window.ClientBounds.Height);
            transition = false;
            compteur = 0;
            opacite = 400;
        }

        public void LoadContent(ContentManager content)
        {
            text_ecran = content.Load<Texture2D>("dragon-tears");
        }

        public void Update()
        {
            if (transition)
            {
                compteur++;

                if (compteur > 0)
                {
                    if (opacite > 8)
                        opacite -= 8;
                    else
                        opacite = 0;
                }

                if (compteur >= 35)
                {
                    opacite = 400;
                    compteur = 0;
                    transition = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (transition)
                spriteBatch.Draw(text_ecran, new Rectangle(coord_ecran.X, coord_ecran.Y, coord_ecran.Width, coord_ecran.Height), new Color(255, 255, 255, opacite));
        }
    }
}
