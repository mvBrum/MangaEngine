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
		public double frameCurrent = 0;
		public double frameSpeed = 1;
		public Vector2 origin;
		public int Width;
		public int Height;
		//private double xscale = 1;
		//private double yscale = 1;
		
		public Color color = Color.White;
				
		public Sprite(params Texture2D[] frames) //passa a lista de imagens
		{
			this.frames = frames;	
			this.frameCount = frames.Length;
			this.Width = frames[0].Width;
			this.Height = frames[0].Height;
		}
		
		public Sprite(ContentManager content,params String[] files) //passa a lista de arquivos
		{
			this.frames = new Texture2D[files.Length];
			for (int i = 0; i < files.Length; i++)
	        {
				this.frames[i] = content.Load<Texture2D>(files[i]);
	        }
			this.frameCount = frames.Length;
			this.Width = frames[0].Width;
			this.Height = frames[0].Height;
		}
		
		public void Draw(SpriteBatch s,int x,int y,double xscale,double yscale,float angle,float depth){
		    	//draw sprite
		    	int ind = (int)Math.Floor(frameCurrent);		    	
		    	//Rectangle r = new Rectangle((int)(x-(origin.X*xscale)),(int)(y-(origin.Y*yscale)),Convert.ToInt16(Width*xscale),Convert.ToInt16(Height*yscale));
		    	//s.Draw(frames[ind],r,color,angle);
		    	Rectangle pos = new Rectangle(x,y,Convert.ToInt16(Width*xscale),Convert.ToInt16(Height*yscale));
		    	//Rectangle ori = new Rectangle(x,y,Width,Height);		    	 
		    	s.Draw(frames[ind],pos,null,color,angle,origin,SpriteEffects.None,depth);//Vector2.One SpriteEffects.None		    	
		}
		
		public void Step(){
			double fator = frameSpeed/25;//+fator
			
			if (frameCurrent<frameCount){
				frameCurrent += fator;
				if (frameCurrent>=frameCount) {frameCurrent=0;}//resetar extouro
			}			
			else 
			frameCurrent += 0;			
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
