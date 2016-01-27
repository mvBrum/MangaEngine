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

namespace baseProject
{
	/// <summary>
	/// Description of Objeto.
	/// </summary>
	public abstract class Objeto
	{
		//Objeto
		public String name = "unnamed";
		
		//coisas da instância
		public int x = 0;
		public int y = 0;
		public Sprite sprite;
		public double xscale = 1 ;//{get;}
		public double yscale = 1 ;//{get;}
		public float angle = 0;//0a360
		public float layer = 0;
		public Color color = Color.White;
		public Rectangle box;
		
		public Objeto(){
			GameBase.objetos.Add(this);
			Create();
		}
					
		
		 //======== Method templates
	    
		 public virtual void Create()
	    {

	    }
	   
	    public virtual void Step()
	    {
	    	//atualiza a box do player	    	
	    	box = new Rectangle(x,y,Convert.ToInt16(sprite.Width*xscale),Convert.ToInt16(sprite.Height*yscale));
	    	sprite.Step();
	    }
	   
	    public virtual void Draw(SpriteBatch s)
	    {	    	
	    	DrawSelf(s);
	    }
	   
	    public virtual void Destroy()
	    {
	       /* if (!toDestroy)
	        {
	            toDestroy = true;
	            objetosDestroy.Add(this);
	        }*/
	    }
	    /*
	    public void setSprite(Sprite sprite){
	    	this.sprite = sprite;
	    	setBox();
	    }
	    
	    public void setScale(double xscale,double yscale){
	    	this.xscale = xscale;
	    	this.yscale = yscale;
	    	setBox();
	    }
	    
	    private void setBox(){
	    	if (sprite!=null){
	    		//box = new Rectangle(x,y,Convert.ToInt16(sprite.Width*xscale),Convert.ToInt16(sprite.Height*yscale));	    		
	    	}
	    }
	    */
	   
	   //Timer
	    public Boolean timerOk(int timer){
	    	if (timer>0 && GameBase.framesElapsed % timer == 0){
	    		return true;
	    	}
	    	else return false;
	    }
	    
	   //Colission
	   public Boolean CollisionOk(Objeto obj1,Objeto obj2){
	   		return obj1.box.Intersects(obj2.box);
	   		/*if ((obj1.box.X>obj2.box.X && obj1.box.X<obj2.box.X+obj2.box.Width && obj1.box.Y>obj2.box.Y && obj1.box.Y<obj2.box.Y+obj2.box.Height)
	   			||
	   			(obj1.box.X+obj1.box.Width>obj2.box.X && obj1.box.X+obj1.box.Width<obj2.box.X+obj2.box.Width && obj1.box.Y>obj2.box.Y && obj1.box.Y<obj2.box.Y+obj2.box.Height)
	   			||
	   			(obj1.box.X>obj2.box.X && obj1.box.X<obj2.box.X+obj2.box.Width && obj1.box.Y+obj1.box.Height>obj2.box.Y && obj1.box.Y+obj1.box.Height<obj2.box.Y+obj2.box.Height)
	   			||
	   			(obj1.box.X+obj1.box.Width>obj2.box.X && obj1.box.X+obj1.box.Width<obj2.box.X+obj2.box.Width && obj1.box.Y+obj1.box.Height>obj2.box.Y && obj1.box.Y+obj1.box.Height<obj2.box.Y+obj2.box.Height)
	   		){
	   			return true;
	   		}
	   		else return false;	*/
	   		
	   		/*
	   		double o1x1 = obj1.x-obj1.sprite.origin.X;
	   		double o1x2 = o1x1+(obj1.sprite.Width*xscale);
			double o1y1 = obj1.y-obj1.sprite.origin.Y;
	   		double o1y2 = o1y1+(obj1.sprite.Height*yscale);

			double o2x1 = obj2.x-obj2.sprite.origin.X;
	   		double o2x2 = o2x1+(obj2.sprite.Width*xscale);
			double o2y1 = obj2.y-obj2.sprite.origin.Y;
	   		double o2y2 = o2y1+(obj2.sprite.Height*yscale);	   		
	   		
	   		Console.WriteLine(o1x1+","+o1x2+","+o1y1+","+o1y2+" - "+o2x1+","+o2x2+","+o2y1+","+o2y2);
	   		*/
	   		/*
	   		if ((o1x1>o2x1 && o1x1<o2x2 && o1y1>o2y1 && o1y1<o2y2 )
	   			||
	   			(o1x2>o2x1 && o1x2<o2x2 && o1y1>o2y1 && o1y1<o2y2 )
	   			||
	   			(o1x1>o2x1 && o1x1<o2x2 && o1y2>o2y1 && o1y2<o2y2 )
	   			||
	   			(o1x2>o2x1 && o1x2<o2x2 && o1y2>o2y1 && o1y2<o2y2 )
	   		)*/
	   		/*
 			if ((o1y1>o2y1 || o1y1<o2y2 || o1y2>o2y1 || o1y2<o2y2)
	   			&&
	   			(o1x1>o2x1 || o1x1<o2x2 || o1x2>o2x1 || o1x2<o2x2))
	   		{
	   			return true;
	   		}
	   		else return false;
	   		*/
	   		
	    }
	   
	    public void DrawSelf(SpriteBatch s){	    	
	    	sprite.Draw(s,x,y,box,angle,layer,color);
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
			
			x = Convert.ToInt32(pos.X);//Convert.ToInt32(dir.X*speed);
			y = Convert.ToInt32(pos.Y);//Convert.ToInt32(dir.Y*speed);
		}
	    
	}
	
}
