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
		public String name = "homem";

		public override void Draw(SpriteBatch s)
	    {
			base.Draw(s);    	
			s.DrawString(GameBase.FontMain, "Frames-> Count:"+sprite.frameCount+" Frame:"+sprite.frameCurrent+" x:"+x+" y:"+y+" Depth:"+depth, new Vector2(10, 10), Color.Black);
	    }
		
		public override void Step()
	    {
			base.Step();   
						
			//controle
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
			
			sprite.frameSpeed = speed/2;
	    }
	}
	
 
	
}
