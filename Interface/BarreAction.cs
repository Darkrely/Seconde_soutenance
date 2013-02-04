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
# endregion

namespace DragonTears
{
    class BarreAction
    {
        Texture2D barre;
        Rectangle position_barre;
        int[] emplacement_inv;

        public BarreAction(GameWindow window)
        {
            position_barre = new Rectangle((window.ClientBounds.Width - 429) / 2, window.ClientBounds.Height - (74 + 10), 429, 74);
        }

        public void LoadContent(ContentManager content)
        {
            barre = content.Load<Texture2D>("barre");
        }

        public void Update(Inventaire inventaire)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(barre, position_barre, Color.White);
        }
    }
}
