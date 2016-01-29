#region Using Statements
using System;
using System.Collections.Generic;
using System.IO.Ports;
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
		TimeSpan updateFrameRate;			
		public static  int frameCounter,framesElapsed = 0;
		public static Texture2D rect;
		//Geral
		public static List<Objeto> objetos = new List<Objeto>();
		public static List<Objeto> objetos_deactived = new List<Objeto>();
		public static Rectangle cameraBoundaries = new Rectangle(0, 0, 0, 0);
		public static Vector2 cameraPosition = new Vector2(0, 0);
		public static int TelaWidth = 640, TelaHeight = 480;
		public static MouseState mouse;
		public static int fps = 1;
		
		public GameBase()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	            
			//graphics.IsFullScreen = true;	
			IsMouseVisible = true;
			Window.AllowUserResizing = true;
			graphics.PreferredBackBufferWidth = TelaWidth;
			graphics.PreferredBackBufferHeight = TelaHeight;			
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
			//rect
			rect = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
		    rect.SetData(new[] { Color.Gray });
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
		
			//update get pos mouse
			mouse = Mouse.GetState();			
			//medir fps
			setFps(gameTime);
						
			//test criar instancia:
			if(Objeto.mouseRightCheck())
			{
				Man man = new Man("new",mouse.X,mouse.Y,Spr_down,0.1,0.1);//GameBase.mouse.X,GameBase.mouse.Y,GameBase.Spr_up);
			}
			
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
			spriteBatch.Begin(SpriteSortMode.FrontToBack);//
				DrawAll(spriteBatch);
				
				spriteBatch.DrawString(GameBase.FontMain, "FPS:"+GameBase.fps+" rate:"+frameCounter+" Instancias:"+objetos.Count, new Vector2(10, 10), Color.Black);
				spriteBatch.DrawString(GameBase.FontMain, "mouse:"+GameBase.mouse.X+","+GameBase.mouse.Y, new Vector2(10, 40), Color.Black);
				
			spriteBatch.End();
			base.Draw (gameTime);
		}
		
		////////
		
		private void setFps(GameTime gameTime){
			//Timer FPS
			TimeSpan timer,timerFrameRate;
			timer = gameTime.ElapsedGameTime;//tempo decorrido de jogo			
			timerFrameRate = TimeSpan.FromSeconds(1);//quantos ms tem 1s
			updateFrameRate += timer;
			
			if (updateFrameRate > timerFrameRate)
	        {
				updateFrameRate -= timerFrameRate;
				fps = frameCounter;
	            frameCounter = 0;
	        }
			
			frameCounter++;
			framesElapsed++;
		}

		
		public static void UpdateAll()
	    {
	        //Call objetos Update
	       	for(int i=0;i<objetos.Count;i++){
	       		Objeto current = objetos[i];
	       		if (current.toDestroy==true){
	        		objetos.Remove(current);
	        		i--;
	        	}
	        	else
	        	if (current.Active==false){
	        		objetos_deactived.Add(current);
					objetos.Remove(current);
					i--;
	        	}
	        	else
	        	current.Step();
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
		
		public static float PointDirection(Vector2 pos1,Vector2 pos2)
		{
			return (MathHelper.ToDegrees((float)Math.Atan2(pos1.Y-pos2.Y, pos1.X-pos2.X))+180f);
		}
		
		/////////////////// /// /////////////////// 
		
		//Carregue os resources aqui:
		public static Sprite Spr_right,Spr_left,Spr_up,Spr_down;
		public static SpriteFont FontMain;
		
		public static void LoadResources(ContentManager content) //load resources
	    {
			/*
			//Spr_man = new Sprite(content.Load<Texture2D>("run/1"));						
			Spr_right = new Sprite(content,"run/right/1","run/right/2","run/right/3","run/right/2");
			//Spr_right.setOrigin(Sprite.Bounds.CENTER);"x","x","x","x");//
			Spr_left = new Sprite(content,"run/left/1","run/left/2","run/left/3","run/left/2");
			//Spr_left.setOrigin(Sprite.Bounds.CENTER);"x","x","x","x");//
			Spr_up = new Sprite(content,"run/up/1","run/up/2","run/up/3","run/up/2");
			//Spr_up.setOrigin(Sprite.Bounds.CENTER);"x","x","x","x");//			
			Spr_down = new Sprite(content,"run/down/1","run/down/2","run/down/3","run/down/2");
			//Spr_down.setOrigin(Sprite.Bounds.CENTER);"x","x","x","x");//
			*/
						
			Spr_right = new Sprite(content,"man","man","man","man");//"run/down/1","run/down/1","run/down/1","run/down/1");
			Spr_left = new Sprite(content,"man","man","man","man");//"run/down/1","run/down/1","run/down/1","run/down/1");
			Spr_up = new Sprite(content,"man","man","man","man");//"run/down/1","run/down/1","run/down/1","run/down/1");
			Spr_down = new Sprite(content,"man","man","man","man");//"run/down/1","run/down/1","run/down/1","run/down/1");

			FontMain = content.Load<SpriteFont>("corbel");
	    }
		
		//Crie as instâncias aqui:
		public static Man joao;
		public static Man jose;
		public static Man maria;
		public static void GameStart() //Início do jogo, crie as instâncias aqui!
	    {
			//Instâncias
			joao = new Man("Joao",100,300,Spr_down,1,1);
			joao.solid = true;	
			//joao.precise=true;			
			//joao.sprite = Spr_down;
			//joao.sprite.frameSpeed = 0.5;
			//joao.xscale = 1;
			//joao.yscale = 1;
			//joao.setScale(1,1);
			//joao.x = 100;
			//joao.y = 300;
			//joao.color = Color.Black;
			//joao.setSprite(Spr_right);
			//joao.sprite.setOrigin(Sprite.Bounds.CENTER);
			
			jose = new Man("Jose",300,300,Spr_down,1,1);
			jose.solid = true;
			//jose.xscale = 1;
			//jose.yscale = 1;
			//jose.sprite = Spr_down;			
			//jose.setScale(1,1);
			//jose.x = 200;
			//jose.y = 300;
			//jose.setSprite(Spr_right);
			//jose.angle = 1;
			//jose.color = Color.Black;
			
			maria = new Man("Maria",500,300,Spr_down,1,1);
			maria.solid=true;
			//maria.precise=true;
	    }
		
		
	}
}

