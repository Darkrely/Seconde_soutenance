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
using Microsoft.Xna.Framework.Net;
# endregion

namespace DragonTears
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Variables
            GraphicsDeviceManager graphics;
            SpriteBatch spriteBatch;
            Joueur joueur;

            #region Gestionnaire du jeu
                GameManager gameManager;
                Gestionnaire_son gestionnaire_son;
                GestionTeleportation gestion_transition;
                Environnement environnement;
                Carte carte;
                EcranChargement ecran_chargement;
            #endregion

            #region Interface
                Lancement lancement;
                Menu menu;
                Camera camera;
                Jauge jauge;
                Curseur curseur;
                BarreAction barreAction;
            #endregion
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.ToggleFullScreen();

            #region Gestionnaire
                gameManager = new GameManager();
                carte = new Carte();
                gestionnaire_son = new Gestionnaire_son();
                environnement = new Environnement(Window);
            #endregion

            #region Interface
                lancement = new Lancement();
                menu = new Menu(Window);
                camera = new Camera(Window);
                curseur = new Curseur(Content.Load<Texture2D>("Curseur"));
                jauge = new Jauge();
                barreAction = new BarreAction(Window);
            #endregion

            joueur = new Joueur(Window, Joueur.Sexe.homme);
            gestion_transition = new GestionTeleportation();
            ecran_chargement = new EcranChargement(Window);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Gestionnaire
                gestionnaire_son.LoadContent(Content);
                carte.LoadContent(Content);
                environnement.LoadContent(Content);
            #endregion

            #region Interface
                lancement.LoadContent(Content);
                menu.LoadContent(Content);
                jauge.LoadContent(Content, "Jauge//barredevie", "Jauge//santepleine", "Jauge//manapleine");
                barreAction.LoadContent(Content);
            #endregion

                ecran_chargement.LoadContent(Content);
                joueur.LoadContent(Content);
                camera.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {   
            KeyboardState clavier = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            gameManager.Update(clavier, menu);

            #region Lancement
            if (gameManager.Etat == GameManager.etat.Lancement)
            {
                lancement.Update(clavier, gameManager);
                if (lancement.lancementfini)
                {
                    gameManager.Etat = GameManager.etat.Menu;
                }
            }
            #endregion

            #region Menu
            if (gameManager.Etat == GameManager.etat.Menu || gameManager.Etat == GameManager.etat.Pause)
            {
                menu.Update(clavier, gameManager, this, carte, joueur, Window, Content);
            }
            #endregion

            #region DansLeJeu
            if (gameManager.Etat == GameManager.etat.InGame)
            {
                ecran_chargement.Update();
                Gestionnaire_collision.CollisionPersoDeplacement(carte, joueur);
                environnement.Update();
                curseur.Update();
                camera.Update(joueur, carte, clavier);
                joueur.Update(clavier, carte, camera, ecran_chargement);
                carte.Update(clavier, joueur, Window, ecran_chargement);
                jauge.Update(gameManager.combat);
                jauge.UpdateSante(joueur.vie, joueur.vieMax);
                jauge.UpdateMana(joueur.mana, joueur.manaMax);
            }
            #endregion

            #region Son
                gestionnaire_son.Update(gameManager, menu);
            #endregion

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            #region lancement
            if (gameManager.Etat == GameManager.etat.Lancement)
                lancement.Draw(spriteBatch, Window);
            #endregion

            #region menu
            if (gameManager.Etat == GameManager.etat.Menu)
                menu.Draw(spriteBatch);
            #endregion

            #region jeu
            if (gameManager.Etat == GameManager.etat.InGame)
            {
                camera.Affichage(carte, joueur, spriteBatch);
                environnement.Draw(spriteBatch, Window);
                jauge.Draw(spriteBatch);
                barreAction.Draw(spriteBatch);
                ecran_chargement.Draw(spriteBatch);
                curseur.Draw(spriteBatch);
            }
            #endregion

            #region Pause
            if (gameManager.Etat == GameManager.etat.Pause)
            {
                menu.Draw(spriteBatch);
            }
            #endregion

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}