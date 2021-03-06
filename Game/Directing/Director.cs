using System.Collections.Generic;
using Unit04.Game.Casting;
using Unit04.Game.Services;
using System;

using System.IO;
using System.Linq;

using Unit04.Game.Directing;



namespace Unit04.Game.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService keyboardService = null;
        private VideoService videoService = null;
        int score = 500;
        private bool buying = false;

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            Actor banner = cast.GetFirstActor("banner");
            
            
            banner.SetText("Money = "+ score);
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the robot.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor robot = cast.GetFirstActor("robot");
            Point velocity = keyboardService.GetDirection();
            robot.SetVelocity(velocity);     
         buying = keyboardService.buy();
        }

        /// <summary>
        /// Updates the robot's position and resolves any collisions with artifacts.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
Random random = new Random();
            string text = ((char)random.Next(42, 43)).ToString();
                

                int x = random.Next(1, 60);
                int y = random.Next(0, 5);
                Point position = new Point(x, y);
                position = position.Scale(15);

                int r = random.Next(0, 256);
                int g = random.Next(0, 256);
                int b = random.Next(0, 256);
                Color color = new Color(r, g, b);

                Artifact newGuy = new Artifact();
                newGuy.SetText(text);
                newGuy.SetFontSize(15);
                newGuy.SetColor(color);
                newGuy.SetPosition(position);
                
            
                cast.AddActor("Attack", newGuy);
            


            Actor banner = cast.GetFirstActor("banner");
            Actor robot = cast.GetFirstActor("robot");
            
//adding the towers.
            robot.GetPosition();
            if (buying == true){
                Console.WriteLine("It works!");
                if (score >= 100){
                    Console.WriteLine("Truly!!");
                Tower Built = new Tower();
                Built.SetText("X");
                Built.SetFontSize(15);
                int t = random.Next(0, 256);
                int h = random.Next(0, 256);
                int n = random.Next(0, 256);
                Color new_color = new Color(t, h, n);
                Built.SetColor(new_color);
                Built.SetPosition(robot.GetPosition());
                score = score - 100;
                banner.SetText("Money = "+ score);
                cast.AddTower("Defense", Built);
                }
            }
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            robot.MoveNext(maxX, maxY);
Point bottom = new Point(0, 600);
Point top = new Point(0, 0);
List<Actor> artifacts = cast.GetActors("Attack");
            List<Tower> Towers = cast.GetTowers("Defense");
List<Bullet> Bullets = cast.GetBullets("Response");

            foreach (Actor actor in artifacts)
            {
                actor.Fall();
                 Point Fall = actor.GetPosition();
                if(Fall.Equals(bottom)){
                    cast.RemoveActor("Attack", actor);

                }
                

                if (robot.GetPosition().Equals(actor.GetPosition()))
                {
                   
                    
                        score += 20;
                        banner.SetText("Money = "+ score);
                        cast.RemoveActor("Attack", actor);
                    
                    
                    
                   
                }
                

                foreach(Bullet bullet in Bullets){
                     Point Rise = bullet.GetPosition();
                if(Rise.Equals(top)){
                    cast.RemoveBullet("Response", bullet);
                    Console.WriteLine("huh");

                }
                    
                    bullet.Fall();
                    
                    
                if(bullet.GetPosition().Equals(actor.GetPosition())){
                    score += 20;
                    banner.SetText("Money = "+ score);
                    cast.RemoveActor("Attack", actor);
                    
                    
                }
                
                }
                foreach (Tower tower in Towers){
                    if((actor.GetPosition()).Equals(tower.GetPosition())){
                    score += 20;
                    banner.SetText("Money = "+ score);
                    cast.RemoveActor("Attack", actor);
                    Console.WriteLine("Got em!");
                }
    bool fire = tower.shoot();
    if (fire==true){ 
        string ztext = ((char)random.Next(43, 44)).ToString();
                

                int zx = random.Next(1, 60);
                int zy = random.Next(0, 5);
                Point zposition = tower.GetPosition();
                

                int zr = random.Next(0, 256);
                int zg = random.Next(0, 256);
                int zb = random.Next(0, 256);
                Color zcolor = new Color(r, g, b);

                Bullet znewGuy = new Bullet();
                znewGuy.SetText(ztext);
                znewGuy.SetFontSize(15);
                znewGuy.SetColor(zcolor);
               znewGuy.SetPosition(zposition);
               cast.AddBullet("Response", znewGuy);

    }
                
                
                
                }
                







            } 
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            List<Tower> Towers = cast.GetAllTowers();
            List<Bullet> Bullets = cast.GetAllBullets();
            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.DrawTowers(Towers);
            videoService.DrawBullets(Bullets);
            videoService.FlushBuffer();
        }

    }
}