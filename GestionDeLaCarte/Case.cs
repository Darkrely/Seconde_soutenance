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
    class Case : Sprite
    {
        #region Declaration
        public enum Typetext{
            Herbe,
            Sable,
            Solbois,
            Murbois,
            Eau,
            Solpierre,
            Murpierre,
            SableEau,
            EauSable,
            Noir,
            Terre,
        };
        public Typetext text;
        #endregion

        public Case(Vector2 position, char type) : base(position)
        {
            Attribuertype(type);   
        }

        public Case(int x, int y, char type) : base(x, y)
        {
            Attribuertype(type);
        }

        public void Attribuertype(char type)
        {
            switch (type)
            {
                case '~':
                    text = Typetext.Eau;
                    break;
                case 't':
                    text = Typetext.Terre;
                    break;
                case '#':
                    text = Typetext.Herbe;
                    break;
                case '.':
                    text = Typetext.Sable;
                    break;
                case '_':
                    text = Typetext.Solbois;
                    break;
                case '-':
                    text = Typetext.Murbois;
                    break;
                case 'p':
                    text = Typetext.Solpierre;
                    break;
                case '=':
                    text = Typetext.Murpierre;
                    break;
                case '\\':
                    text = Typetext.EauSable;
                    break;
                case '/':
                    text = Typetext.SableEau;
                    break;
                default:
                    text = Typetext.Noir;
                    break;
            }
        }
    }
}
