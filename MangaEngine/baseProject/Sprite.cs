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
		
		
		public enum Bounds{ 
			LEFTUP = 0,
			LEFTDOWN = 1,
			RIGHTUP = 2,
			RIGHTDOWN = 3,
			CENTER = 4,
			CENTERUP = 5,
			CENTERDOWN = 6			
		}
		
		//criar com lista de textures
		public Sprite(params Texture2D[] frames) 
		{
			this.frames = frames;	
			this.frameCount = frames.Length;
			this.Width = frames[0].Width;
			this.Height = frames[0].Height;
		}
		
		//criar com lista de arquivos
		public Sprite(ContentManager content,params String[] files) 
		{
			this.frames = new Texture2D[files.Length];
			for (int i = 0; i < files.Length; i++)
	        {
				this.frames[i] = content.Load<Texture2D>(files[i]);
	        }
			this.frameCount = frames.Length;
			this.Width = frames[0].Width;
			this.Height = frames[0].Height;
			
			setOrigin(Sprite.Bounds.CENTERDOWN);GameBase.fps=0;
			//setOrigin(200,200);
		}
		
		public void Draw(SpriteBatch s,int x,int y,double xscale,double yscale,float angle,float depth,Color color){
		    	
			//draw sprite
	    	int ind = (int)Math.Floor(frameCurrent);//arredondando o índice	    			    	
	    	Rectangle box = new Rectangle(x,y,Convert.ToInt16(Width*xscale),Convert.ToInt16(Height*yscale));    	 		    	
	    	s.Draw(frames[ind],box,null,color,angle/360,origin,SpriteEffects.None,depth);
	    	//s.Draw(frames[ind],pos,null,color,angle/360,origin,new Vector2(xscale,yscale),SpriteEffects.None,depth);
	    	//s.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList,0,10);	    	
		}
		
		public void Step(){
			double fator = frameSpeed/(GameBase.fps+0.01);//+fator  25;
			
			if (frameCurrent<frameCount){
				frameCurrent += fator;
				if (frameCurrent>=frameCount) {frameCurrent=0;}//resetar extouro
			}			
			else 
			frameCurrent += 0;			
		}
		
		public void setOrigin(Bounds bound){
			switch(bound){
				case Bounds.LEFTUP:
					origin = new Vector2(0,0);					
				break;
				case Bounds.LEFTDOWN:
					origin = new Vector2(0,Height);						
				break;
				case Bounds.RIGHTUP:
					origin = new Vector2(Width,0);						
				break;
				case Bounds.RIGHTDOWN:
					origin = new Vector2(Width,Height);						
				break;
				case Bounds.CENTER:
					origin = new Vector2(Width/2,Height/2);						
				break;
				case Bounds.CENTERUP:
					origin = new Vector2(Width/2,0);						
				break;
				case Bounds.CENTERDOWN:
					origin = new Vector2(Width/2,Height);						
				break;
			
			}
		}
		
		public void setOrigin(int xoffset,int yoffset){
			origin = new Vector2(xoffset,yoffset);			
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
