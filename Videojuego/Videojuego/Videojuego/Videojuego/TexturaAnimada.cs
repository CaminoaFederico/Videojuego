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
    public class TexturaAnimada
    {

        private int frameContador;
        private Texture2D miTextura;
        private float TiempoPorFrame;
        private int Frame;
        private float totalRepeticion;

        public Vector2 posicion;

        public TexturaAnimada(Vector2 pos, int FrameContador, int TiempoPorSeg)
        {
            posicion = pos;
            frameContador = FrameContador;
            TiempoPorFrame = 1.0f / TiempoPorSeg;
            Frame = 0;
            totalRepeticion = 0;
        }
        public void Load(ContentManager content, string NombreDeTexto)
        {
            miTextura = content.Load<Texture2D>(NombreDeTexto);
        }

        public void ActualizarFrame(float repeticion)
        {
            totalRepeticion += repeticion;

            if (totalRepeticion > TiempoPorFrame)
            {
                Frame++;
                Frame = Frame % frameContador;
                totalRepeticion -= TiempoPorFrame;
            }
        }

        public void DibujarFrame(SpriteBatch batch)
        {
            DibujarFrame(batch, Frame);
        }

        public void DibujarFrame(SpriteBatch batch, int frame)
        {
            int FrameAncho = miTextura.Width / frameContador;

            Rectangle sourcerect = new Rectangle(FrameAncho * frame, 0, FrameAncho, miTextura.Height);

            batch.Draw(miTextura, posicion, sourcerect, Color.White);
        }

        public int TomarFrame()
        {
            return Frame;
        }

        public void UbicarFrame(int Frame)
        {
            this.Frame = Frame;
        }

        public void AumentarPosicion(float x)
        {
            posicion.X += x;
        }

        public void DisminuirPosicion(float x)
        {
            posicion.X -= x;
        }

        public void AumentarAltura(float y)
        {
            posicion.Y += y;
        }
        public Vector2 TomarPosicionSprite()
        {
            return posicion;
        }

        public void UbicarPosicionSprite(Vector2 f)
        {
            posicion = f;
        }

    }
}
