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
		public double xscale = 1;
		public double yscale = 1;
		public float angle = 0;
		public float depth = 1;
		

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
	    	sprite.Step();
	    }
	   
	    public virtual void Draw(SpriteBatch s)
	    {
	    	depth = ((float)(y)/(float)(292));//(float)(1-(1/(y+1)));//(float)(y/292);//(float)(1-(0.99/(y+0.001)))+0.01f;
	    	if (depth>1) {depth=1;}
	    	else 
	    	if (depth<0) {depth=0;}
	    	sprite.Draw(s,x,y,xscale,yscale,angle,depth);
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
