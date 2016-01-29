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
		public Color[][] textureDatas;
		public int frameCount = 0;
		public double frameCurrent = 0;
		public double frameSpeed = 1;
		private int imageIndex = 0;	
		public Vector2 origin;
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
		
		public int ImageIndex {
			get { return imageIndex; }
		}
		
		public int width() {
			return box[ImageIndex].Width;
		}
		public int height() {
			return box[ImageIndex].Height;
		}
		
		//criar com lista de arquivos
		public Sprite(ContentManager content,Boolean autoboundingbox,params String[] files) 
		{
			this.frames = new Texture2D[files.Length];
			this.textureDatas = new Color[files.Length][];
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
			
			setOrigin(Sprite.Bounds.CENTERDOWN);//GameBase.fps=0;
			//setOrigin(200,200);
		}
		
		public void Draw(SpriteBatch s,int x,int y,double xscale,double yscale,float angle,float depth,Color color){

			//draw sprite
	    	   			    	
	    	//Rectangle box = new Rectangle(Convert.ToInt32(x-(widthOrig-width())),Convert.ToInt32(y-(heightOrig-height)),Convert.ToInt32(widthOrig*xscale),Convert.ToInt32(heightOrig*yscale));//Convert.ToInt32(x-origin.X),Convert.ToInt32(y-origin.Y)
	    	//Rectangle box = new Rectangle((int)(x-(widthOrig-width())/2*xscale)-1,(int)(y-(HeightOrig-height())/2*xscale)-1,Convert.ToInt32(widthOrig*xscale+1),Convert.ToInt32(heightOrig*yscale+1));//Convert.ToInt32(x-origin.X),Convert.ToInt32(y-origin.Y)
	    	Rectangle boxdraw = new Rectangle((int)(x-(box[imageIndex].X)*xscale),(int)(y-(box[imageIndex].Y)*xscale),Convert.ToInt32(widthOrig*xscale),Convert.ToInt32(heightOrig*yscale));//Convert.ToInt32(x-origin.X),Convert.ToInt32(y-origin.Y)
	    	s.Draw(frames[imageIndex],boxdraw,null,color,angle/360,origin,SpriteEffects.None,depth);
	    	//s.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList,0,10);	    	
	    	/*
 			if (box[imageIndex].X>0){
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
			
			//arredondando o índice:	
			imageIndex = (int)Math.Floor(frameCurrent); 	
			 
		}
		
		public void setOrigin(Bounds bound){
			switch(bound){
				case Bounds.LEFTUP:
					origin = new Vector2(0,0);					
				break;
				case Bounds.LEFTDOWN:
					origin = new Vector2(0,height());						
				break;
				case Bounds.RIGHTUP:
					origin = new Vector2(width(),0);						
				break;
				case Bounds.RIGHTDOWN:
					origin = new Vector2(width(),height());						
				break;
				case Bounds.CENTER:
					origin = new Vector2(width()/2,height()/2);						
				break;
				case Bounds.CENTERUP:
					origin = new Vector2(width()/2,0);						
				break;
				case Bounds.CENTERDOWN:
					origin = new Vector2(width()/2,height());						
				break;
			
			}
			/*origin.X += (widthOrig-width())/2;
			origin.Y += (heightOrig-height)/2;*/
		}
		
		public void setOrigin(int xoffset,int yoffset){
			origin = new Vector2(xoffset,yoffset);			
		}
		
		private void setTextureData(int ind,Boolean autoboundingbox){			
			//inicializa a array da subimage:
			textureDatas[ind] = new Color[frames[ind].Width*frames[ind].Height];
			//tamanho da img: textureDatas[ind] = new Color[box[ind] frames[ind].Width*frames[ind].Height];
			//preencher a array com um bi de cores da subimage:
			textureDatas[ind] = TextureToArray(frames[ind]);
			//guardar a box reduzida da subimage:
			if (autoboundingbox){
				box[ind] = GetSmallestRectangleFromTexture(frames[ind],textureDatas[ind]);								
				//otimizar, reduzindo boundingbox
				//textureDatas[ind] = TextureToArrayInBoundBoxing(frames[ind],box[ind]);
			}
			else {
				box[ind] = new Rectangle(0,0,frames[0].Width,frames[0].Height);
			}
			
		}
		
	

    //Get smallest rectangle from Texture, cased on color
    public Rectangle GetSmallestRectangleFromTexture(Texture2D Texture,Color[] Colors)
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
                if (Colors[a+b*Texture.Width].A != 0)
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
    private static Color[] TextureToArray(Texture2D texture)
    {
        //Texture.GetData returns a 1D array
        Color[] colors1D = new Color[texture.Width * texture.Height];
        texture.GetData(colors1D);
        return colors1D;
   	 }
    
    //convert texture to 2d array dentro de boundingbox
    private static Color[] TextureToArrayInBoundBoxing(Texture2D texture,Rectangle rect)
    {
        //Texture.GetData returns a 1D array
        Color[] colors1D = new Color[texture.Width * texture.Height];
        texture.GetData(colors1D);
 		
        if (rect.X>texture.Width){rect.X=0;}
        if (rect.Y>texture.Height){rect.Y=0;}
        if (rect.X+rect.Width>texture.Width){rect.Width=texture.Width-rect.X;}
        if (rect.Y+rect.Height>texture.Height){rect.Height=texture.Height-rect.Y;}
        
        //Retira apenas a array dentro do boundingbox:
        Color[] colorsInBoundbox = new Color[rect.Width*rect.Height];
       	for (int x = 0; x < rect.Width; x++)
       		for (int y =  0; y <  rect.Height; y++)       	            
                colorsInBoundbox[x + y * rect.Width] = colors1D[x + y * rect.Width];
 
        	return colorsInBoundbox;
   	 }
    
    
	}
	
	
	
	
}
