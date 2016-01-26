#region Using Statements
using System;
using System.Collections.Generic;
using baseProject.PacMan;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

#endregion

namespace baseProject
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class GameBase : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		
		//Geral
		public static List<Objeto> objetos = new List<Objeto>();
		public static List<Objeto> objetosToDestroy = new List<Objeto>();
		
		public GameBase()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	            
			//graphics.IsFullScreen = true;		
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			// TODO: Add your initialization logic here						
			base.Initialize ();
				
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);

			//TODO: use this.Content to load your game content here 
			LoadResources(Content);
			GameStart();//iniciar após carregar os resources
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
			    Keyboard.GetState ().IsKeyDown (Keys.Escape)) {
				Exit ();
			}
			#endif
			// TODO: Add your update logic here		
			UpdateAll();
			
			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);
		
			//TODO: Add your drawing code here
			spriteBatch.Begin();
				DrawAll(spriteBatch);
				//spriteBatch.Draw(Objeto.spr_man,Vector2.Zero,Color.White);				
			spriteBatch.End();
			base.Draw (gameTime);
		}
		
		////////
		
		
		public static void UpdateAll()
	    {
	        //Call objetos Update
	        foreach(Objeto current in objetos)
	        {
	            current.Step();
	        }
	       
	        //Destroy objetos listed to be destroyed
	        for(int i=0; i<objetosToDestroy.Count; i++)
	        {
	        	objetos.Remove(objetos[i]);
	            objetos.Remove(objetosToDestroy[i]);
	            i--;
	        }
	    }
		
		public static void DrawAll(SpriteBatch s)
	    {
	       /* //update camera boundaries
	        Amber.cameraBoundaries = new Rectangle(Amber.cameraBoundaries.X, Amber.cameraBoundaries.Y, Amber.cameraWidth, Amber.cameraHeight);
	       */
	        foreach(Objeto current in objetos)
	        {
	            current.Draw(s);
	        }
	    }
		
		public static void SortDrawOrder() //organizar lista por layer
	    {
	        objetos.Sort((x, y) => x.layer.CompareTo(y.layer));
	    }
		
		/////////////////// /// /////////////////// 
		
		//Carregue os resources aqui:
		public static Sprite Spr_man;
		
		public static void LoadResources(ContentManager content) //load resources
	    {
			//Texture2D res_man = content.Load<Texture2D>("run/1");
			//Spr_man = new Sprite(res_man);						
			Spr_man = new Sprite(content,"run/1","run/2","run/3","run/4");
	    }
		
		//Crie as instâncias aqui:
		public static void GameStart() //Início do jogo, crie as instâncias aqui!
	    {
			//Instâncias
			Man joao = new Man();			
			joao.sprite = Spr_man;
	    }
		
		
	}
}

