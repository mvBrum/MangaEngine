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
		public Color[][,] textureDatas;
		public int frameCount = 0;
		public double frameCurrent = 0;
		public double frameSpeed = 1;
		public Vector2 origin;
		public int width;
		public int height;
		private int widthOrig;
		private int heightOrig;		
		public Rectangle[] box;
				
		public enum Bounds{ 
			LEFTUP = 0,
			LEFTDOWN = 1,
			RIGHTUP = 2,
			RIGHTDOWN = 3,
			CENTER = 4,
			CENTERUP = 5,
			CENTERDOWN = 6			
		}
		
		public int WidthOrig {
			get { return widthOrig; }
		}
		
		
		public int HeightOrig {
			get { return heightOrig; }
		}
		
		//criar com lista de arquivos
		public Sprite(ContentManager content,Boolean autoboundingbox,params String[] files) 
		{
			this.frames = new Texture2D[files.Length];
			this.textureDatas = new Color[files.Length][,];
			this.box = new Rectangle[files.Length];
			
			for (int i = 0; i < files.Length; i++)
	        {
				this.frames[i] = content.Load<Texture2D>(files[i]);
				setTextureData(i,autoboundingbox);
	        }
			
			this.frameCount = frames.Length;
			//guarda o tamanho da 1ªimagem:
			this.widthOrig = frames[0].Width;//box[0].Width;//
			this.heightOrig = frames[0].Height;//box[0].Height;//
			this.width = box[0].Width;//frames[0].Width;//
			this.height = box[0].Height;//frames[0].Height;//
			
			setOrigin(Sprite.Bounds.CENTERDOWN);//GameBase.fps=0;
			//setOrigin(200,200);
		}
		
		public void Draw(SpriteBatch s,int x,int y,double xscale,double yscale,float angle,float depth,Color color){
		    	
			
			
			//draw sprite
	    	int ind = (int)Math.Floor(frameCurrent);//arredondando o índice	    			    	
	    	//Rectangle box = new Rectangle(Convert.ToInt32(x-(widthOrig-width)),Convert.ToInt32(y-(heightOrig-height)),Convert.ToInt32(widthOrig*xscale),Convert.ToInt32(heightOrig*yscale));//Convert.ToInt32(x-origin.X),Convert.ToInt32(y-origin.Y)
	    	//Rectangle box = new Rectangle((int)(x-(widthOrig-width)/2*xscale)-1,(int)(y-(HeightOrig-height)/2*xscale)-1,Convert.ToInt32(widthOrig*xscale+1),Convert.ToInt32(heightOrig*yscale+1));//Convert.ToInt32(x-origin.X),Convert.ToInt32(y-origin.Y)
	    	Rectangle boxdraw = new Rectangle((int)(x-(box[ind].X)*xscale),(int)(y-(box[ind].Y)*xscale),Convert.ToInt32(widthOrig*xscale),Convert.ToInt32(heightOrig*yscale));//Convert.ToInt32(x-origin.X),Convert.ToInt32(y-origin.Y)
	    	s.Draw(frames[ind],boxdraw,null,color,angle/360,origin,SpriteEffects.None,depth);
	    	//s.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList,0,10);	    	
	    	/*
 			if (box[ind].X>0){
				//nada
			}*/
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
					origin = new Vector2(0,height);						
				break;
				case Bounds.RIGHTUP:
					origin = new Vector2(width,0);						
				break;
				case Bounds.RIGHTDOWN:
					origin = new Vector2(width,height);						
				break;
				case Bounds.CENTER:
					origin = new Vector2(width/2,height/2);						
				break;
				case Bounds.CENTERUP:
					origin = new Vector2(width/2,0);						
				break;
				case Bounds.CENTERDOWN:
					origin = new Vector2(width/2,height);						
				break;
			
			}
			/*origin.X += (widthOrig-width)/2;
			origin.Y += (heightOrig-height)/2;*/
		}
		
		public void setOrigin(int xoffset,int yoffset){
			origin = new Vector2(xoffset,yoffset);			
		}
		
		private void setTextureData(int ind,Boolean autoboundingbox){			
			//inicializa a array da subimage:
			textureDatas[ind] = new Color[frames[ind].Width,frames[ind].Height];
			//preencher a array com um bi de cores da subimage:
			textureDatas[ind] = TextureTo2DArray(frames[ind]);
				//frames[ind].GetData(textureDatas[ind]);
			//guardar a box reduzida da subimage:
			if (autoboundingbox){
				box[ind] = GetSmallestRectangleFromTexture(frames[ind],textureDatas[ind]);								
			}
			else box[ind] = new Rectangle(0,0,frames[0].Width,frames[0].Height);
			
		}
		
	

    //Get smallest rectangle from Texture, cased on color
    public Rectangle GetSmallestRectangleFromTexture(Texture2D Texture,Color[,] Colors)
    {
        //Create our index of sprite frames
        //Color[,] Colors = TextureTo2DArray(Texture);
 
        //determine the min/max bounds
        int x1 = 9999999, y1 = 9999999;
        int x2 = -999999, y2 = -999999;
 
        for (int a = 0; a < Texture.Width; a++)
        {
            for (int b = 0; b < Texture.Height; b++)
            {
                //If we find a non transparent pixel, update bounds if required
                if (Colors[a, b].A != 0)
                {
                	if (x1 > a) {x1 = a;}
                	if (x2 < a) x2 = a;
 
                	if (y1 > b) {y1 = b;}
                    if (y2 < b) y2 = b;
                }
            }
        }
        //We now have our smallest possible rectangle for this texture
        return new Rectangle(x1, y1, x2 - x1 + 1, y2 - y1 + 1);
    }
 
    //convert texture to 2d array
    private static Color[,] TextureTo2DArray(Texture2D texture)
    {
        //Texture.GetData returns a 1D array
        Color[] colors1D = new Color[texture.Width * texture.Height];
        texture.GetData(colors1D);
 
        //convert the 1D array to 2D for easier processing
        Color[,] colors2D = new Color[texture.Width, texture.Height];
        for (int x = 0; x < texture.Width; x++)
            for (int y = 0; y < texture.Height; y++)
                colors2D[x, y] = colors1D[x + y * texture.Width];
 
        	return colors2D;
   	 	}
	}
	
	
	
	
}
