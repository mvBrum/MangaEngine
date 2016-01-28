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
		public String identify ;//= "unnamed";//para identificar a classe
		
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
		private Boolean active = true;
		
		
		public Objeto(){
			GameBase.objetos.Add(this);						
		}
					
		
		public void setIdentify(String identify){
			this.identify = identify;
		}
		
		/*
		private Vector2 _rotatePoint(Vector2 PointToRotate,Vector2 OriginOfRotation,float ThetaInRads)
		{
		    Vector2 RotationVector = PointToRotate - OriginOfRotation;
		    Vector2 RotatedVector = new Vector2()
		    {
		        X = (float)(RotationVector.X * Math.Cos(ThetaInRads) - RotationVector.Y * Math.Sin(ThetaInRads)),
		        Y = (float)(RotationVector.X * Math.Sin(ThetaInRads) + RotationVector.Y * Math.Cos(ThetaInRads))
		    };
		
		    return OriginOfRotation + RotatedVector;
		}
		*/
		public void setRotateBox(){
			//float nx = x * (float)Math.Cos(angle/360) - y * (float)Math.Sin(angle/360);
			//float ny = x * (float)Math.Sin(angle/360) + y * (float)Math.Cos(angle/360);
			//boxCollision = new Rectangle(Convert.ToInt32(nx),Convert.ToInt32(ny),Convert.ToInt32(sprite.Width*xscale),Convert.ToInt32(sprite.Height*yscale));
		}
		
		 //======== Method templates
	    
	   
	    public virtual void Step()
	    {
	    	//atualiza a box do player				
			if (sprite!=null){
				boxCollision = new Rectangle(Convert.ToInt32(x-(sprite.origin.X)*xscale),Convert.ToInt32(y-(sprite.origin.Y)*yscale),Convert.ToInt16(sprite.Width*xscale),Convert.ToInt16(sprite.Height*yscale));
				setRotateBox();
				sprite.Step();
			}
	    	
	    }
	   
	    public abstract void Draw(SpriteBatch s);
	   
	    public Boolean toDestroy=false;
	    public virtual void Destroy()
	    {
	        toDestroy=true;
	    }
	   
	    //// Functions
	    public bool Active {
			get { return active; }
		}
		
	    /*
	    public void Activate(Objeto obj){	    	
	    	if (GameBase.objetos_deactived.Remove(obj)){
	    		GameBase.objetos.Add(obj);
	    		obj.active=true;
	    	}			
	    }
	    */
	   public void Activate(){	
			//Objeto o = this;	   	
	    	if (GameBase.objetos_deactived.Remove(this)){
	    		GameBase.objetos.Add(this);
	    		this.active=true;
	    	}			
	    }
	   
	    public void Deactivate(){
	    	active=false;	    	
	    }
	    
	    public void ActiveAll(){
			//for corrige o erro de remoção no foreach:
			for(int i=0;i<GameBase.objetos_deactived.Count;i++){
	       		Objeto current = GameBase.objetos_deactived[i];
	       		GameBase.objetos.Add(current);
				GameBase.objetos_deactived.Remove(current);
				current.active=true;				
	      	} 
		}	    
	    
	   //Timer
	    public Boolean timerOk(int timer){
	    	if (timer>0 && GameBase.framesElapsed % timer == 0){
	    		return true;
	    	}
	    	else return false;
	    }
	   
	   //Control	   
	   public static Boolean keyboardCheck(Keys key){
	   	if (GameBase.framesElapsed % 5 == 0 && Keyboard.GetState().IsKeyDown(key)){
	    		return true;
	    	}
	    	else return false;
	    }
	   
	   public static Boolean mouseLeftCheck(){
	   		if (GameBase.framesElapsed % 5 == 0 && GameBase.mouse.LeftButton == ButtonState.Pressed){
	    		return true;
	    	}
	    	else return false;
	    }
	   
	   public static Boolean mouseRightCheck(){
	   		if (GameBase.framesElapsed % 5 == 0 && GameBase.mouse.RightButton == ButtonState.Pressed){
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
	   		if(mouseLeftCheck())
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
		    s.Draw(GameBase.rect, coords, color);
		}
	    
	}
	
}
