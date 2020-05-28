using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Videojuego
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Fondo fondo;
        Texture2D TexturaFondo;
        private TexturaAnimada activa;
        private TexturaAnimada avanzar;
        private TexturaAnimada retroceder;
        private TexturaAnimada saltar;
        private TexturaAnimada correr;
        private TexturaAnimada estatico;
        private Vector2 spritePosicion = Vector2.Zero;
        KeyboardState EstadoActual = new KeyboardState();
        KeyboardState EstadoAnterior = new KeyboardState();
        enum State { avzr, rtcdr, stic, star, crer }
        State estado;
        List<Enemigos> enemigos = new List<Enemigos>();
        //Random random = new Random();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            spritePosicion = new Vector2(graphics.PreferredBackBufferWidth / 2, 600);
            estatico = new TexturaAnimada(spritePosicion, 3, 4);
            avanzar = new TexturaAnimada(spritePosicion, 8, 8);
            retroceder = new TexturaAnimada(spritePosicion, 8, 8);
            saltar = new TexturaAnimada(spritePosicion, 14, 20);
            correr = new TexturaAnimada(spritePosicion, 16, 8);

            activa = estatico;
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
        }

        protected override void Initialize()
        {
            estado = State.stic;
            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            TexturaFondo = Content.Load<Texture2D>("Fondo");
            fondo = new Fondo(GraphicsDevice, TexturaFondo);
            estatico.Load(Content, "Sprite13");
            avanzar.Load(Content, "Sprite4");
            retroceder.Load(Content, "Sprite4");
            saltar.Load(Content, "Sprite10");
            correr.Load(Content, "Sprite8");

            //spritePosicion.X = graphics.GraphicsDevice.Viewport.Width / 2;
            //spritePosicion.Y = graphics.GraphicsDevice.Viewport.Height;

        }

        protected override void UnloadContent()
        {

        }

        float spawn = 0;
/*
        double vi, t = 0;
        double g = 10;
        int Keystate = 0;
*/
        
        public void CargarEnemigos()
        {
            int PosicionY = 600;

            if(spawn >= 1)
            {
                spawn = 0;

                if (enemigos.Count() < 4)
                {
                    enemigos.Add(new Enemigos(Content.Load<Texture2D>("Piedra1"), new Vector2(1000, PosicionY)));
                }
            }

            for(int i = 0; i < enemigos.Count; i++)
            {
                if(!enemigos[i].esVisible)
                {
                    enemigos.RemoveAt(i);
                    i--;
                }
            }
        }

        private void ComprobarTecladoEstatico(GameTime tiempo)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {

                if (estatico.TomarFrame() == 0)
                {
                    Vector2 pos = activa.TomarPosicionSprite();
                    activa = retroceder;
                    activa.UbicarPosicionSprite(pos);
                    activa.UbicarFrame(0);
                    estado = State.rtcdr;
                }

                else if (estatico.TomarFrame() == 2 && !EstadoAnterior.IsKeyDown(Keys.Left))
                {
                    activa.UbicarFrame(1);
                }

                else if (estatico.TomarFrame() == 1 && !EstadoAnterior.IsKeyDown(Keys.Left))
                {
                    activa.UbicarFrame(0);
                }

            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {

                if (estatico.TomarFrame() == 0)
                {
                    Vector2 pos = activa.TomarPosicionSprite();
                    activa = avanzar;
                    activa.UbicarPosicionSprite(pos);
                    activa.UbicarFrame(0);
                    estado = State.avzr;
                }

                else if (estatico.TomarFrame() == 0 && !EstadoAnterior.IsKeyDown(Keys.Right))
                {
                    activa.UbicarFrame(1);

                }

                else if (estatico.TomarFrame() == 1 && !EstadoAnterior.IsKeyDown(Keys.Right))
                {
                    activa.UbicarFrame(2);
                }

            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {

                if (estatico.TomarFrame() == 0 || estatico.TomarFrame() == 1 || estatico.TomarFrame() == 2)
                {
                    Vector2 pos = activa.TomarPosicionSprite();
                    activa = saltar;
                    activa.UbicarPosicionSprite(pos);
                    activa.UbicarFrame(0);
                    estado = State.star;
                }
/*
                else if (estatico.TomarFrame() == 0 && !EstadoAnterior.IsKeyDown(Keys.Up))
                {
                    activa.UbicarFrame(1);
                }

                else if (estatico.TomarFrame() == 1 && !EstadoAnterior.IsKeyDown(Keys.Up))
                {
                    activa.UbicarFrame(2);
                }
*/
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {

                if (estatico.TomarFrame() == 0 || estatico.TomarFrame() == 1 || estatico.TomarFrame() == 2)
                {
                    Vector2 pos = activa.TomarPosicionSprite();
                    activa = correr;
                    activa.UbicarPosicionSprite(pos);
                    activa.UbicarFrame(0);
                    estado = State.crer;
                }
/*
                else if (estatico.TomarFrame() == 0 && !EstadoAnterior.IsKeyDown(Keys.Space))
                {
                    activa.UbicarFrame(1);
                }

                else if (estatico.TomarFrame() == 1 && !EstadoAnterior.IsKeyDown(Keys.Space))
                {
                    activa.UbicarFrame(2);
                }
*/
            }

        }

        private void ComprobarTecladoAvanzar(GameTime tiempo)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                activa.AumentarPosicion(2);
                float repeticion = (float)tiempo.ElapsedGameTime.TotalSeconds;
                activa.ActualizarFrame(repeticion);
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Vector2 pos = activa.TomarPosicionSprite();
                activa = estatico;
                activa.UbicarPosicionSprite(pos);
                activa.UbicarFrame(2);
                estado = State.stic;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Vector2 pos = activa.TomarPosicionSprite();
                activa = saltar;
                activa.UbicarPosicionSprite(pos);
                activa.UbicarFrame(0);
                estado = State.star;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Space) 
                || Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Vector2 pos = activa.TomarPosicionSprite();
                activa = correr;
                activa.UbicarPosicionSprite(pos);
                activa.UbicarFrame(0);
                estado = State.crer;
            }

        }

        private void ComprobarTecladoRetroceder(GameTime tiempo)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                activa.DisminuirPosicion(2);
                float repeticion = (float)tiempo.ElapsedGameTime.TotalSeconds;
                activa.ActualizarFrame(repeticion);
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Vector2 pos = activa.TomarPosicionSprite();
                activa = estatico;
                activa.UbicarPosicionSprite(pos);
                activa.UbicarFrame(0);
                estado = State.stic;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Vector2 pos = activa.TomarPosicionSprite();
                activa = saltar;
                activa.UbicarPosicionSprite(pos);
                activa.UbicarFrame(0);
                estado = State.star;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Space)
                || Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Vector2 pos = activa.TomarPosicionSprite();
                activa = correr;
                activa.UbicarPosicionSprite(pos);
                activa.UbicarFrame(0);
                estado = State.crer;
            }

        }

        private void ComprobarTecladoSaltar(GameTime tiempo)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                activa.AumentarAltura(0);
                //activa.AumentarPosicion(2);
                spritePosicion.Y = graphics.GraphicsDevice.Viewport.Height;
                float repeticion = (float)tiempo.ElapsedGameTime.TotalSeconds;
                activa.ActualizarFrame(repeticion);
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Vector2 pos = activa.TomarPosicionSprite();
                activa = avanzar;
                activa.UbicarPosicionSprite(pos);
                activa.UbicarFrame(0);
                estado = State.avzr;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Vector2 pos = activa.TomarPosicionSprite();
                activa = retroceder;
                activa.UbicarPosicionSprite(pos);
                activa.UbicarFrame(0);
                estado = State.rtcdr;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Space)
                  || Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Vector2 pos = activa.TomarPosicionSprite();
                activa = correr;
                activa.UbicarPosicionSprite(pos);
                activa.UbicarFrame(0);
                estado = State.crer;
            }

        }

        private void ComprobarTecladoCorrer(GameTime tiempo)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Right) && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                activa.AumentarPosicion(8);
                float repeticion = (float)tiempo.ElapsedGameTime.TotalSeconds;
                activa.ActualizarFrame(repeticion);
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                activa.DisminuirPosicion(8);
                float repeticion = (float)tiempo.ElapsedGameTime.TotalSeconds;
                activa.ActualizarFrame(repeticion);
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Vector2 pos = activa.TomarPosicionSprite();
                activa = avanzar;
                activa.UbicarPosicionSprite(pos);
                activa.UbicarFrame(0);
                estado = State.avzr;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Vector2 pos = activa.TomarPosicionSprite();
                activa = retroceder;
                activa.UbicarPosicionSprite(pos);
                activa.UbicarFrame(0);
                estado = State.rtcdr;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Vector2 pos = activa.TomarPosicionSprite();
                activa = saltar;
                activa.UbicarPosicionSprite(pos);
                activa.UbicarFrame(0);
                estado = State.star;
            }

        }

        void ComprobarTeclado(GameTime tiempoJuego)
        {
            switch (estado)
            {

                case State.stic:
                ComprobarTecladoEstatico(tiempoJuego);
                break;

                case State.avzr:
                ComprobarTecladoAvanzar(tiempoJuego);
                break;

                case State.rtcdr:
                ComprobarTecladoRetroceder(tiempoJuego);
                break;

                case State.star:
                ComprobarTecladoSaltar(tiempoJuego);
                break;

                case State.crer:
                ComprobarTecladoCorrer(tiempoJuego);
                break;

            }
        }

        protected override void Update(GameTime tiempoJuego)
        {

            KeyboardState EstadoTeclado = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
/*
            else 
            {
                Keystate = 1;
                vi = -100;
            }

            if (Keystate == 1)
            {
                spritePosicion.Y = (float)(vi * t + g * t * t / 2) + graphics.GraphicsDevice.Viewport.Height ;
                t = t + tiempoJuego.ElapsedGameTime.TotalSeconds;
            }

            if (spritePosicion.Y > graphics.GraphicsDevice.Viewport.Height)
            {
                spritePosicion.Y = graphics.GraphicsDevice.Viewport.Height;
                Keystate = 0;
                t = 0;
            }
*/
            
            spawn += (float)tiempoJuego.ElapsedGameTime.TotalSeconds;

            foreach(Enemigos enemigo in enemigos)
            {
                enemigo.Actualizar(graphics.GraphicsDevice);
            }

            CargarEnemigos();

            fondo.Update(EstadoTeclado);       
            EstadoActual = Keyboard.GetState();
            ComprobarTeclado(tiempoJuego);
            EstadoAnterior = EstadoActual;

            base.Update(tiempoJuego);

        }

        protected override void Draw(GameTime tiempoJuego)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach(Enemigos enemigo in enemigos)
            {
                enemigo.Dibujar(spriteBatch);
            }

            fondo.Dibujar(spriteBatch);

            activa.DibujarFrame(spriteBatch);

            spriteBatch.End();

            base.Draw(tiempoJuego);

        }
    }
}
