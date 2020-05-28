using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;

namespace Videojuego
{
    class Enemigos
    {
        public Texture2D Textura;
        public Vector2 Posicion;
        public Vector2 Velocidad;

        public bool esVisible = true;

        //Random random = new Random();

        int PosicionY, PosicionX;

        public Enemigos(Texture2D nuevaTextura, Vector2 nuevaPosicion)
        {
            Textura = nuevaTextura;
            Posicion = nuevaPosicion;

            PosicionY = 600;
            PosicionX = 1000;

            Velocidad = new Vector2(-8, 0);
        }

        public void Actualizar(GraphicsDevice graficos)
        {
            Posicion += Velocidad;

            if(Posicion.Y <= 0 || Posicion.Y >= graficos.Viewport.Height - Textura.Height)
            {
                Velocidad.Y = -Velocidad.Y;
            }

            if(Posicion.X < 0 - Textura.Width)
            {
                esVisible = false;
            }
        }

        public void Dibujar(SpriteBatch spriteBach)
        {
            spriteBach.Draw(Textura, Posicion, Color.White);
        }
    }
}
