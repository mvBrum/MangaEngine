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
			setIdentify(IDENTIFY);
			Console.WriteLine(name);
		}
				
		
		public override void Draw(SpriteBatch s)
	    {
			setDepthByY();
			DrawRectangle(boxCollision,Color.Fuchsia,s);
			DrawSelf(s);									
			s.DrawString(GameBase.FontMain, name+" "+ok+" x:"+x+" y:"+y+" Depth:"+layer+" Angle:"+angle+" Collide?:"+col, new Vector2(x,y), Color.Black);			
	    }
		
		Boolean col = false;
		public override void Step()
	    {
			base.Step();   
			
			//Timers
	    	/*if (timerOk(timer0)){
				x = 350-(new Random(x).Next(100,300));
	    		y = 350;
	    		timer0 = 0;
	    	}
			*/
			
			//Colisions
			//if (CollisionInstancesOk(GameBase.joao,GameBase.jose)){
			
			if (CollisionOk(Man.IDENTIFY)){
				col=true;
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
				angle -= 5;
			}
			else
			if (Keyboard.GetState().IsKeyDown(Keys.X)) {
				angle += 5;
			}
			
			//click
			if(mouseRightCheck())// GameBase.mouse.RightButton == ButtonState.Pressed)
			{
				float dir = GameBase.PointDirection(new Vector2(x,y),new Vector2(GameBase.mouse.X,GameBase.mouse.Y));//x += Math.Cos(
				MoveInDirection(dir,4);
			}		
			
			//testes						
			if (keyboardCheck(Keys.Delete)){// Keyboard.GetState().IsKeyDown(Keys.Delete)) {
				Console.WriteLine("Delete: "+name);
				Destroy();
			}
			else
			if (keyboardCheck(Keys.D)){//Keyboard.GetState().IsKeyDown(Keys.D)) {
				Deactivate();
				Console.WriteLine("Desative: "+name);
			}
<<<<<<< HEAD
=======
			
>>>>>>> a92d0a59e8d69702f04ce63494fdfc48e0b098e3
			
			
			}
			
			if (keyboardCheck(Keys.O)){// Keyboard.GetState().IsKeyDown(Keys.Delete)) {
				ok=true;	
				Console.WriteLine("Ok: "+name);				
			}
			
<<<<<<< HEAD
=======
			if (keyboardCheck(Keys.O)){// Keyboard.GetState().IsKeyDown(Keys.Delete)) {
				ok=true;	
				Console.WriteLine("Ok: "+name);				
			}
			
>>>>>>> a92d0a59e8d69702f04ce63494fdfc48e0b098e3
			if (keyboardCheck(Keys.A)){//Keyboard.GetState().IsKeyDown(Keys.A)) {				
				GameBase.maria.Activate();
				Console.WriteLine("Ative: "+name);				
			}
			//mover a pos da câmera:
			//GameBase.cameraPosition.X += (Keyboard.GetState().IsKeyDown(Keys.D)? 3 : 0)-(Keyboard.GetState().IsKeyDown(Keys.A)? 3 : 0);
			//GameBase.cameraPosition.Y += (Keyboard.GetState().IsKeyDown(Keys.S)? 3 : 0)-(Keyboard.GetState().IsKeyDown(Keys.W)? 3 : 0);
			
			
			
	    }
		Boolean ok=false;//test
		
		
	}
	
 
	
}
