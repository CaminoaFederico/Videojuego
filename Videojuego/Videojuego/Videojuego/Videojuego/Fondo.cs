using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;

namespace Videojuego
{
    class Fondo
    {
        Texture2D fondo;
        Vector2 posicion, TamañoFondo, TamañoPantalla;
        int desplazamiento = 3;

        public Fondo(GraphicsDevice dispositivo, Texture2D textura)
        {
            fondo = textura;
            TamañoPantalla = new Vector2(dispositivo.Viewport.Width, dispositivo.Viewport.Height);
            TamañoFondo = new Vector2(fondo.Width, fondo.Height);
            posicion = Vector2.Zero;
        }

        public void Update(KeyboardState teclado)
        {
            if(teclado.IsKeyDown(Keys.Right))
            {
                posicion.X -= desplazamiento;
            }

            if(teclado.IsKeyDown(Keys.Left))
            {
                posicion.X += desplazamiento;
            }

            //Comprobación

            if(Math.Abs(posicion.X) + TamañoPantalla.X >= TamañoFondo.X)
            {
                posicion.X = -TamañoFondo.X + TamañoPantalla.X;
            }

            if(posicion.X >= 0)
            {
                posicion.X = 0;
            }
        }

        public void Dibujar(SpriteBatch batch)
        {
            batch.Draw(fondo, new Rectangle(((int)posicion.X), ((int)posicion.Y), fondo.Width, fondo.Height), Color.White);
        }
    }
}
