/*
 * Created by SharpDevelop.
 * User: Usuario
 * Date: 25/01/2016
 * Time: 00:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace baseProject
{
	/// <summary>
	/// Description of Objeto.
	/// </summary>
	public abstract class Objeto
	{
		//Objeto
		protected String identify ;//= "unnamed";//para identificar a classe
		
		//coisas da instância
		public int x = 0;
		public int y = 0;
		public Sprite sprite;
		public double xscale = 1 ;//{get;}
		public double yscale = 1 ;//{get;}
		public float angle = 0;//0a360
		public float layer = 0;
		public Color color = Color.Black;
		public Rectangle boxCollision = new Rectangle(0,0,0,0);
		
		public Objeto(){
			GameBase.objetos.Add(this);			
			//Create();
		}
					
		
		
		 //======== Method templates
	    
		 public virtual void Create()
	    {
			//GameBase.objetos.Add(this);
	    }
	   
	    public virtual void Step()
	    {
	    	//atualiza a box do player	
			//Convert.ToInt32(x-sprite.origin.X),Convert.ToInt32(y-sprite.origin.Y)	    	
			if (sprite!=null){
				boxCollision = new Rectangle(Convert.ToInt32(x-(sprite.origin.X)*xscale),Convert.ToInt32(y-(sprite.origin.Y)*yscale),Convert.ToInt16(sprite.Width*xscale),Convert.ToInt16(sprite.Height*yscale));
				sprite.Step();
			}
	    	
	    }
	   
	    public virtual void Draw(SpriteBatch s)
	    {	    	
	    	
	    }
	   
	    public virtual void Destroy()
	    {
	       /* if (!toDestroy)
	        {
	            toDestroy = true;
	            objetosDestroy.Add(this);
	        }*/
	    }
	   
	    //// Functions
	    
	   //Timer
	    public Boolean timerOk(int timer){
	    	if (timer>0 && GameBase.framesElapsed % timer == 0){
	    		return true;
	    	}
	    	else return false;
	    }
	    
	   //Collision
	   public Boolean CollisionOk(String identify2){
	   		Boolean col=false;
	   		foreach(Objeto current in GameBase.objetos)
	        {
	   			if (current.identify==identify2 && this!=current && col==false){
	            	col = this.boxCollision.Intersects(current.boxCollision);
	   			}
	        }	
			return col;	   		
	   }
	   
	   public Boolean CollisionInstancesOk(Objeto obj1,Objeto obj2){
	   		return obj1.boxCollision.Intersects(obj2.boxCollision);	   		
	   }
	   
	   
	   
	    public void DrawSelf(SpriteBatch s){	    	
		   	if (sprite!=null){
		   		sprite.Draw(s,x,y,xscale,yscale,angle,layer,color);
		   	}
	    }
	    
	   //Click
	   public Boolean ClickOk(){
	  		if(GameBase.mouse.LeftButton == ButtonState.Pressed)
			{
	  			Rectangle mbox = new Rectangle(GameBase.mouse.X,GameBase.mouse.Y,1,1);
	  			return mbox.Intersects(boxCollision);
			}
	  		else return false;
	   }
	   
	    public void setDepthByY(){
	    	layer = ((float)(y)/(float)(GameBase.TelaHeight));
	    	if (layer>1) {layer=1;}
	    	else 
	    	if (layer<0) {layer=0;}
	    }
	    
	    public void MoveInDirection(float direction,float speed)
		{
			float dirRads = MathHelper.ToRadians(direction);
			
			Vector2 dir = new Vector2((float)Math.Cos(dirRads), (float)Math.Sin(dirRads));
			Vector2 pos = new Vector2(x,y);
			pos += dir*speed;
			
			x = Convert.ToInt32(pos.X);
			y = Convert.ToInt32(pos.Y);
		}
	    
	    public void DrawRectangle(Rectangle coords, Color color,SpriteBatch s)
		{
	    	//DrawRectangle(new Rectangle((int)playerPos.X, (int)playerPos.Y, 5, 5), Color.Fuchsia);
	    	var rect = new Texture2D(s.GraphicsDevice, 1, 1);
		    rect.SetData(new[] { color });
		    s.Draw(rect, coords, color);
		}
	    
	}
	
}
