/*
 * Criado por SharpDevelop.
 * Usuário: Usuario
 * Data: 25/01/2016
 * Hora: 23:07
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace baseProject.PacMan
{
	/// <summary>
	/// Description of Man.
	/// </summary>
	public class Man : Objeto{	
		protected static String IDENTIFY = "man";//para identificar a classe		
		public String name = "homem";
		int timer0 = 180;
		public int xstart;
		public int ystart;				
			
		public Man(){
			
		}
		
		public Man(String name,int x,int y,Sprite spr,double xscale,double yscale){
			this.name = name;
			this.x=x;
			this.y=y;
			this.sprite=spr;
			this.xscale=xscale;
			this.yscale=yscale;
			this.xstart = x;
			this.ystart = y;
			identify = IDENTIFY;
		}
		
		public override void Create(){
			//base.Create();
			
		}
		
		public override void Draw(SpriteBatch s)
	    {
			setDepthByY();
			//base.Draw(s);
			DrawSelf(s);						
			//s.DrawString(GameBase.FontMain, "FPS:"+GameBase.fps+" Frames-> Count:"+sprite.frameCount+" Frame:"+sprite.frameCurrent, new Vector2(10, 10), Color.Black);
			s.DrawString(GameBase.FontMain, name+" x:"+x+" y:"+y+" Depth:"+layer+" Angle:"+angle+" Collide?:"+col, new Vector2(x,y), Color.Black);
			//s.DrawString(GameBase.FontMain, " x:"+box.X+" y:"+box.Y+" w:"+(box.X+box.Width)+" h:"+(box.Y+box.Height), new Vector2(x,y+20), Color.Black);
			//MouseState mouse = new MouseState();			
			//s.DrawString(GameBase.FontMain, "Dir:"+GameBase.PointDirection(new Vector2(0,0),new Vector2(100, 100)), new Vector2(10, 40), Color.Black);
			//s.DrawString(GameBase.FontMain, "ORI*GIN",new Vector2(x+sprite.origin.X,y+sprite.origin.Y), Color.Black);
			DrawRectangle(boxCollision,Color.Fuchsia,s);
	    }
		
		Boolean col = false;
		public override void Step()
	    {
			base.Step();   
			
			//Timers
	    	if (timerOk(timer0)){
				x = 350-(new Random(x).Next(100,300));
	    		y = 350;
	    		timer0 = 0;
	    	}
			
			//Colisions
			//if (CollisionInstancesOk(GameBase.joao,GameBase.jose)){
			if (CollisionOk(Man.IDENTIFY)){
				col=true;
				x=xstart;
				y=ystart;
			}
			else col=false;
			
			//Controls
			if (ClickOk()){
				if (color==Color.Black){
					color=Color.White;
				}
				else color=Color.Black;
			}
			
			if (color!=Color.Black){			
			int speed = 0;
			if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
				speed=4;
				x += speed;	
				sprite=GameBase.Spr_right;				
			}
			else
			if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
				speed=4;
				x -= speed;
				sprite=GameBase.Spr_left;
			}
			
			if (Keyboard.GetState().IsKeyDown(Keys.Up)) {
				speed=4;
				y -= speed;
				sprite=GameBase.Spr_up;
			}
			else
			if (Keyboard.GetState().IsKeyDown(Keys.Down)) {
				speed=4;
				y += speed;
				sprite=GameBase.Spr_down;
			}
			
			if (sprite!=null){
				sprite.frameSpeed = speed/2;
			}
			
			//girar: 0 a 6,28
			if (Keyboard.GetState().IsKeyDown(Keys.Z)) {
				angle -= 5;//(float)1/(float)25;
			}
			else
			if (Keyboard.GetState().IsKeyDown(Keys.X)) {
				angle += 5;//(float)1/(float)25;
			}
			
			//click
			if(GameBase.mouse.RightButton == ButtonState.Pressed)
			{
				float dir = GameBase.PointDirection(new Vector2(x,y),new Vector2(GameBase.mouse.X,GameBase.mouse.Y));//x += Math.Cos(
				MoveInDirection(dir,4);
			}									
			
			}
			
			//mover a pos da câmera:
			//GameBase.cameraPosition.X += (Keyboard.GetState().IsKeyDown(Keys.D)? 3 : 0)-(Keyboard.GetState().IsKeyDown(Keys.A)? 3 : 0);
			//GameBase.cameraPosition.Y += (Keyboard.GetState().IsKeyDown(Keys.S)? 3 : 0)-(Keyboard.GetState().IsKeyDown(Keys.W)? 3 : 0);
			
			
			
	    }
	}
	
 
	
}
