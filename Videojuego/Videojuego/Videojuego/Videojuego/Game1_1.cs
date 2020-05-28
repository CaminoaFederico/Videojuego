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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Texture2D texZorro;
        //Vector2 PosicionPersonaje = new Vector2(50, 100);
        //int vidas = 3;
        private Fondo fondo;
        Texture2D TexturaFondo;
        // Texture2D d;
        private TexturaAnimada activa;
        //private TexturaAnimada girar;
        private TexturaAnimada avanzar;
        private TexturaAnimada retroceder;
        private TexturaAnimada saltar;
        private TexturaAnimada correr;
//private TexturaAnimada SpriteTexture;
        private Vector2 spritePosicion;
        KeyboardState EstadoActual = new KeyboardState();
        KeyboardState EstadoAnterior = new KeyboardState();
        enum State { avzr, rtcdr, star, crer }
        State estado;
        /*private const int FRAMES = 2;
        private const int FRAMESPORSEGUNDO = 10;*/

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            spritePosicion = new Vector2(graphics.PreferredBackBufferWidth / 2, 625);
            //girar = new TexturaAnimada(spritePosicion, 3, 8);
            avanzar = new TexturaAnimada(spritePosicion, 8, 8);
            retroceder = new TexturaAnimada(spritePosicion, 8, 8);
            saltar = new TexturaAnimada(spritePosicion, 14, 10);
            correr = new TexturaAnimada(spritePosicion, 16, 8);
            activa = avanzar;
            //SpriteTexture = new TexturaAnimada(spritePosicion, FRAMES, FRAMESPORSEGUNDO);//
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            estado = State.avzr;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            TexturaFondo = Content.Load<Texture2D>("Fondo");
            fondo = new Fondo(GraphicsDevice, TexturaFondo);
            //d = Content.Load<Texture2D>("Sprite4");
            //SpriteTexture.Load(Content, "Sprite11");
            //girar.Load(Content, "Sprite11");
            avanzar.Load(Content, "Sprite4");
            //izquierda.Load(Content, "Sprite5");
            saltar.Load(Content, "Sprite10");
            correr.Load(Content, "Sprite8");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        /// 
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>   
        
        private void ComprobarTecladoAvanzar(GameTime tiempo)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Left))
            {

                    Vector2 pos = activa.TomarPosicionSprite();
                    activa = retroceder;
                    activa.UbicarPosicionSprite(pos);
                    activa.UbicarFrame(0);
                    estado = State.rtcdr;

            }

            else if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {

                    Vector2 pos = activa.TomarPosicionSprite();
                    activa = avanzar;
                    activa.UbicarPosicionSprite(pos);
                    activa.UbicarFrame(0);
                    estado = State.avzr;

                activa.AumentarPosicion(2);
                float repeticion = (float)tiempo.ElapsedGameTime.TotalSeconds;
                activa.ActualizarFrame(repeticion);

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

        private void ComprobarTecladoRetroceder(GameTime tiempo)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Vector2 pos = activa.TomarPosicionSprite();
                activa = avanzar;
                activa.UbicarPosicionSprite(pos);
                activa.UbicarFrame(0);
                estado = State.avzr;
            }

            else if(Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                activa.DisminuirPosicion(2);
                float repeticion = (float)tiempo.ElapsedGameTime.TotalSeconds;
                activa.ActualizarFrame(repeticion);
            }
        }

        private void ComprobarTecladoSaltar(GameTime tiempo)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Up) && Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Vector2 pos = activa.TomarPosicionSprite();
                activa = saltar;
                activa.UbicarPosicionSprite(pos);
                activa.UbicarFrame(0);
                estado = State.avzr;
            }
        }

        private void ComprobarTecladoCorrer(GameTime tiempo)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.RightShift) && Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Vector2 pos = activa.TomarPosicionSprite();
                activa = correr;
                activa.UbicarPosicionSprite(pos);
                activa.UbicarFrame(0);
                estado = State.avzr;
            }
        }

        void ComprobarTeclado(GameTime tiempoJuego)
        {
            switch (estado)
            {
                /*case State.gira:
                ComprobarTecladoGirar(tiempoJuego);
                break;*/

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

            /* if(keyboardState.IsKeyDown(Keys.Right))
             { PosicionPersonaje.X += 1; }*/

            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            fondo.Update(EstadoTeclado);       
            EstadoActual = Keyboard.GetState();
            ComprobarTeclado(tiempoJuego);
            EstadoAnterior = EstadoActual;
               
            /*float repeticion = (float)tiempoJuego.ElapsedGameTime.TotalSeconds;
            SpriteTexture.ActualizarFrame(repeticion);*/
             
            // TODO: Add your update logic here

            base.Update(tiempoJuego);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime tiempoJuego)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Opaque);
            spriteBatch.Begin();

            fondo.Dibujar(spriteBatch);
            //spriteBatch.Draw(texZorro, PosicionPersonaje, Color.White);

            //SpriteTexture.DibujarFrame(spriteBatch);

            //spriteBatch.Draw(d, new Vector2(0, 0),Color.White);
            activa.DibujarFrame(spriteBatch);

            //spriteBatch.DrawString(SpriteFont, "Vidas: " + vidas, new Vector2(50, 100), Color.White);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(tiempoJuego);
        }
    }
}
