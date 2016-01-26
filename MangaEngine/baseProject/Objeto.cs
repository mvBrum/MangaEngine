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
		public int layer = 0;
		
		//coisas da instância
		public int x = 0;
		public int y = 0;
		public Sprite sprite;
		
		/*public Objeto(Texture2D sprite,int x,int y)
		{
			this.sprite=sprite;
			this.x=x;
			this.y=y;
		}*/
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
	    	/*
	    	//sprite ciclo
	    	if (sprite!=null){
		    	if (sprite.frameCurrent+sprite.frameSpeed<=sprite.frameCount-1  ){
		    		sprite.frameCurrent += sprite.frameSpeed/10;
		    	}
		    	else{
		    		sprite.frameCurrent = 0;
		    	}
	    	}
	    	*/
	    	sprite.Step();
	    }
	   
	    public virtual void Draw(SpriteBatch s)
	    {
	    	/*
	    	if (sprite!=null){
		    	//draw sprite
		    	int ind = (int)Math.Floor(sprite.frameCurrent);
		    	
		    	Rectangle r = new Rectangle((int)(x-sprite.origin.X),(int)(y-sprite.origin.Y),sprite.Width,sprite.Height);
		    	//Vector2.Zero
		    	s.Draw(sprite.frames[ind],r,sprite.color);
	    	}
	    	*/
	    	sprite.Draw(s,x,y);
	    }
	   
	    public virtual void Destroy()
	    {
	       /* if (!toDestroy)
	        {
	            toDestroy = true;
	            objetosDestroy.Add(this);
	        }*/
	    }
	}
	
}
