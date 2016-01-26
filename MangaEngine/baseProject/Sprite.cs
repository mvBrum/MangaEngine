/*
 * Criado por SharpDevelop.
 * Usuário: Usuario
 * Data: 25/01/2016
 * Hora: 23:12
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace baseProject
{
	/// <summary>
	/// Description of Sprite.
	/// </summary>
	
	public class Sprite
	{
		public Texture2D[] frames;
		public int frameCount = 0;
		public float frameCurrent = 0;
		public float frameSpeed = 1;
		public Vector2 origin;
		public int Width;
		public int Height;
		public Color color = Color.White;
				
		public Sprite(params Texture2D[] frames) //passa a lista de imagens
		{
			this.frames = frames;	
			this.frameCount = frames.Length;
			this.Width = frames[0].Width;
			this.Height = frames[0].Height;
		}
		
		public Sprite(ContentManager content,params String[] files) //passa a lista de imagens
		{
			this.frames = new Texture2D[files.Length];
			for (int i = 0; i < files.Length; i++)
	        {
				this.frames[i] = content.Load<Texture2D>(files[i]);
	        }
			//this.frames = frames;	
			this.frameCount = frames.Length;
			this.Width = frames[0].Width;
			this.Height = frames[0].Height;
		}
		
		public void Draw(SpriteBatch s,int x,int y){
			//if (this!=null){
		    	//draw sprite
		    	int ind = (int)Math.Floor(frameCurrent);
		    	
		    	Rectangle r = new Rectangle((int)(x-origin.X),(int)(y-origin.Y),Width,Height);
		    	//Vector2.Zero
		    	s.Draw(frames[ind],r,color);
	    	//}
		}
		
		public void Step(){
			//if (sprite!=null){
		    	if (frameCurrent+frameSpeed<=frameCount-1  ){
		    		frameCurrent += frameSpeed/10;
		    	}
		    	else{
		    		frameCurrent = 0;
		    	}
	    	//}
		}
		
	}
	
	
	
	
	/*
	 //Sprisheet
	public class Sprite
	{
		public Texture2D texture;
		public int frameCount;
		public float frameCurrent = 0;
		private int framesHor;
		private int frameVer;
		public Vector2 origin;
		
		
		public Sprite(Texture2D texture, int framesHor,int framesVer, Vector2 origin)
		{
			this.texture = texture;
			this.frameCount = frameCount;
			this.frameWidth = texture.Width/frameCount;
			
			this.origin = origin;
		}
		
	}
	*/
}
