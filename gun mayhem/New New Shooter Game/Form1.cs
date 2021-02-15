using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace New_New_Shooter_Game
{
    public partial class Form1 : Form
    {
        public struct Vars
        {
            public int picNum;
            public int climbPicNum;
            public bool lookingRight;
            public bool jump;
            public int floatTime;
            public Point speed;
            public bool onLadder;
            public int numFloor;
            public bool downPress;
            public PictureBox[] bullets;
            public bool falling;
            public int bulletNum;
            public int counter;
            public bool canShoot;
        }
        Vars bVars;
        Vars jVars;
        public static bool quit;
        public static bool johnnyWon;
        public static bool bobbyWon;
        GameEnd GameEnd = new GameEnd();

        public Form1()
        {
            InitializeComponent();
        }

        private void animateBobby()
        {
            if (bVars.speed.X != 0)
            {
                animateRunBobby();
            }
            else if (bVars.speed.Y != 0 && bVars.onLadder == true && bVars.jump == false)
            {
                animateClimbBobby();
            }
            else if (bVars.speed.X == 0)
            {
                animateStandBobby();
            }
        }

        private void animateRunBobby()
        {
            if (bVars.lookingRight == true)
            {
                if (bVars.picNum == 1)
                {
                    bobby.Image = Image.FromFile("Pics/sprite move right 1.png");
                    bVars.picNum = 2;
                }
                else if (bVars.picNum == 2)
                {
                    bobby.Image = Image.FromFile("Pics/sprite move right 2.png");
                    bVars.picNum = 1;
                }
            }
            else if (bVars.lookingRight == false)
            {
                if (bVars.picNum == 1)
                {
                    bobby.Image = Image.FromFile("Pics/sprite move left 1.png");
                    bVars.picNum = 2;
                }
                else if (bVars.picNum == 2)
                {
                    bobby.Image = Image.FromFile("Pics/sprite move left 2.png");
                    bVars.picNum = 1;
                }
            }
        }

        private void animateStandBobby()
        {
            if (bVars.lookingRight == true)
            {
                bobby.Image = Image.FromFile("Pics/sprite move right 3.png");
            }
            else if (bVars.lookingRight == false)
            {
                bobby.Image = Image.FromFile("Pics/sprite move left 3.png");
            }
        }

        private void animateClimbBobby()
        {
            if (bVars.climbPicNum <= 8)
            {
                bobby.Image = Image.FromFile("Pics/sprite climb 1.png");
                bVars.climbPicNum++;
            }
            else if (bVars.climbPicNum > 8)
            {
                bobby.Image = Image.FromFile("Pics/sprite climb 2.png");
                bVars.climbPicNum++;
            }
            bVars.climbPicNum = (bVars.climbPicNum == 17) ? 1: bVars.climbPicNum;
        }

        private void animateJohnny()
        {
            if (jVars.speed.X != 0)
            {
                animateRunJohnny();
            }
            else if (jVars.speed.Y != 0 && jVars.onLadder == true && jVars.jump == false)
            {
                animateClimbJohnny();
            }
            else if (jVars.speed.X == 0)
            {
                animateStandJohnny();
            }
        }

        private void animateRunJohnny()
        {
            if (jVars.lookingRight == true)
            {
                if (jVars.picNum == 1)
                {
                    johnny.Image = Image.FromFile("Pics/johnny move right 1.png");
                    jVars.picNum = 2;
                }
                else if (jVars.picNum == 2)
                {
                    johnny.Image = Image.FromFile("Pics/johnny move right 2.png");
                    jVars.picNum = 1;
                }
            }
            else if (jVars.lookingRight == false)
            {
                if (jVars.picNum == 1)
                {
                    johnny.Image = Image.FromFile("Pics/johnny move left 1.png");
                    jVars.picNum = 2;
                }
                else if (jVars.picNum == 2)
                {
                    johnny.Image = Image.FromFile("Pics/johnny move left 2.png");
                    jVars.picNum = 1;
                }
            }
        }

        private void animateStandJohnny()
        {
            if (jVars.lookingRight == true)
            {
                johnny.Image = Image.FromFile("Pics/johnny move right 3.png");
            }
            else if (jVars.lookingRight == false)
            {
                johnny.Image = Image.FromFile("Pics/johnny move left 3.png");
            }
        }

        private void animateClimbJohnny()
        {
            if (jVars.climbPicNum <= 8)
            {
                johnny.Image = Image.FromFile("Pics/johnny climb 1.png");
                jVars.climbPicNum++;
            }
            else if (jVars.climbPicNum > 8)
            {
                johnny.Image = Image.FromFile("Pics/johnny climb 2.png");
                jVars.climbPicNum++;
            }
            jVars.climbPicNum = (jVars.climbPicNum == 17) ? 1 : jVars.climbPicNum;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mainTimer.Start();
            bulletTimer.Start();
            WindowState = FormWindowState.Maximized;
            reset();
        }

        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                bVars.speed.X = -5;
                bVars.lookingRight = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                bVars.speed.X = 5;
                bVars.lookingRight = true;
            }
            if (e.KeyCode == Keys.O && bVars.jump == false && bVars.numFloor != 0)
            {
                bVars.speed.Y = -10;
                bVars.jump = true;
                bVars.floatTime = 0;
            }
            if (e.KeyCode == Keys.Up && bVars.jump == false && bVars.onLadder == true)
            {
                bVars.speed.Y = -5;
            }
            if (e.KeyCode == Keys.Down && bVars.jump == false && bVars.onLadder == true)
            {
                bVars.speed.Y = 5;
                bVars.downPress = true;
            }
            if (e.KeyCode == Keys.P && (bVars.onLadder == false || bVars.speed.Y == 0 || bVars.jump == true))
            {
                if (bVars.canShoot == true)
                {
                    if (bVars.lookingRight == true)
                    {
                        bVars.bullets[bVars.bulletNum] = new PictureBox
                        {
                            Name = "right",
                            Image = Image.FromFile("Pics/bullet right.png"),
                            SizeMode = PictureBoxSizeMode.AutoSize,
                            Location = new Point(bobby.Location.X + 61, bobby.Location.Y + 19),
                        };
                        this.Controls.Add(bVars.bullets[bVars.bulletNum]);
                        bVars.bullets[bVars.bulletNum].BringToFront();
                        bVars.canShoot = false;
                        bVars.counter = 0;
                    }
                    else
                    {
                        bVars.bullets[bVars.bulletNum] = new PictureBox
                        {
                            Name = "left",
                            Image = Image.FromFile("Pics/bullet left.png"),
                            SizeMode = PictureBoxSizeMode.AutoSize,
                            Location = new Point(bobby.Location.X - 36, bobby.Location.Y + 19),
                        };
                        this.Controls.Add(bVars.bullets[bVars.bulletNum]);
                        bVars.bullets[bVars.bulletNum].BringToFront();
                        bVars.canShoot = false;
                        bVars.counter = 0;
                    }
                    bVars.bulletNum++;
                    if (bVars.bulletNum == 10)
                    {
                        bVars.bulletNum = 0;
                    }
                }
            }

            if (e.KeyCode == Keys.A)
            {
                jVars.speed.X = -5;
                jVars.lookingRight = false;
            }
            if (e.KeyCode == Keys.D)
            {
                jVars.speed.X = 5;
                jVars.lookingRight = true;
            }
            if (e.KeyCode == Keys.V && jVars.jump == false && jVars.numFloor != 0)
            {
                jVars.speed.Y = -10;
                jVars.jump = true;
                jVars.floatTime = 0;
            }
            if (e.KeyCode == Keys.W && jVars.jump == false && jVars.onLadder == true)
            {
                jVars.speed.Y = -5;
            }
            if (e.KeyCode == Keys.S && jVars.jump == false && jVars.onLadder == true)
            {
                jVars.speed.Y = 5;
                jVars.downPress = true;
            }
            if (e.KeyCode == Keys.B && (jVars.onLadder == false || jVars.speed.Y == 0 || jVars.jump == true))
            {
                if (jVars.canShoot == true)
                {
                    if (jVars.lookingRight == true)
                    {
                        jVars.bullets[jVars.bulletNum] = new PictureBox
                        {
                            Name = "right",
                            Image = Image.FromFile("Pics/bullet right.png"),
                            SizeMode = PictureBoxSizeMode.AutoSize,
                            Location = new Point(johnny.Location.X + 61, johnny.Location.Y + 19),
                        };
                        this.Controls.Add(jVars.bullets[jVars.bulletNum]);
                        jVars.bullets[jVars.bulletNum].BringToFront();
                        jVars.canShoot = false;
                        jVars.counter = 0;
                    }
                    else
                    {
                        jVars.bullets[jVars.bulletNum] = new PictureBox
                        {
                            Name = "left",
                            Image = Image.FromFile("Pics/bullet left.png"),
                            SizeMode = PictureBoxSizeMode.AutoSize,
                            Location = new Point(johnny.Location.X - 36, johnny.Location.Y + 19),
                        };
                        this.Controls.Add(jVars.bullets[jVars.bulletNum]);
                        jVars.bullets[jVars.bulletNum].BringToFront();
                        jVars.canShoot = false;
                        jVars.counter = 0;
                    }
                    jVars.bulletNum++;
                    if (jVars.bulletNum == 10)
                    {
                        jVars.bulletNum = 0;
                    }
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                bVars.speed.X = 0;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                bVars.speed.Y = 0;
                bVars.downPress = false;
            }

            if (e.KeyCode == Keys.A || e.KeyCode == Keys.D)
            {
                jVars.speed.X = 0;
            }
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.S)
            {
                jVars.speed.Y = 0;
                jVars.downPress = false;
            }
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            move(ref bVars, ref bobby);
            move(ref jVars, ref johnny);
            animateBobby();
            animateJohnny();
            checkLife();
            //label2.Text = Convert.ToString(bVars.speed.Y);
        }

        private void move(ref Vars var, ref PictureBox player)
        {
            checkLadder(ref var, player);
            checkFloor(ref var, player);
            if (var.jump == true)
            {
                jumping(ref var);
            }
            else if (var.onLadder == false)
            {
                if (var.numFloor == 0)
                {
                    var.speed.Y = 10;
                    var.falling = true;
                }
                else if (var.numFloor != 0)
                {
                    var.speed.Y = 0;
                    var.falling = false;
                }
                if (var.speed.Y != 0 && (player.Location.Y - 5) % 10 != 0)
                {
                    player.Location = new Point(player.Location.X, player.Location.Y + 5);
                }
            }
            else if (var.onLadder == true)
            {
                if ((var.numFloor == 1 && var.speed.Y == 5) || (var.numFloor == 5 && var.speed.Y == -5) || (var.numFloor == 3 && var.speed.Y == -5) || (var.numFloor == 6 && var.speed.Y == -5) || (var.numFloor == 2 && var.speed.Y == 5) || (var.numFloor == 4 && var.speed.Y == 5))
                {
                    var.speed.Y = 0;
                }
                if ((player.Location.Y - 5) % 10 != 0 && var.speed.Y == 0)
                {
                    player.Location = new Point(player.Location.X, player.Location.Y + 5);
                }
                if (var.speed.Y != 0)
                {
                    var.speed.X = 0;
                }
            }
            if ((var.onLadder == true && var.downPress == false && var.numFloor == 3 && var.jump == false))
            {
                var.speed.Y = 0;
            }
            player.Location = new Point(player.Location.X + var.speed.X, player.Location.Y + var.speed.Y);
        }

        private void jumping(ref Vars var)
        {
            if (var.floatTime == 10)
            {
                if (var.speed.Y == -10)
                {
                    var.speed.Y = 10;
                }
                else
                {
                    var.speed.Y = 0;
                    var.jump = false;
                }
                var.floatTime = 1;
            }
            else
            {
                var.floatTime++;
            }
        }

        private void checkLadder(ref Vars var, PictureBox player)
        {
            if (var.numFloor != 0)
            {
                if ((player.Left >= ladder0.Left && player.Right <= ladder0.Right && player.Bottom <= ladder0.Bottom && player.Bottom >= ladder0.Top) || (player.Left >= ladder1.Left && player.Right <= ladder1.Right && player.Bottom <= ladder1.Bottom && player.Bottom >= ladder1.Top) || (player.Left >= ladder2.Left && player.Right <= ladder2.Right && player.Bottom <= ladder2.Bottom && player.Bottom >= ladder2.Top) || (player.Left >= ladder3.Left && player.Right <= ladder3.Right && player.Bottom <= ladder3.Bottom && player.Bottom >= ladder3.Top))
                {
                    var.onLadder = true;
                }
                else
                {
                    var.onLadder = false;
                }
            }
            else if (var.falling == false)
            {
                if ((player.Left <= ladder0.Right && player.Right >= ladder0.Left && player.Bottom <= ladder0.Bottom && player.Bottom >= ladder0.Top) || (player.Left <= ladder1.Right && player.Right >= ladder1.Left && player.Bottom <= ladder1.Bottom && player.Bottom >= ladder1.Top) || (player.Left <= ladder2.Right && player.Right >= ladder2.Left && player.Bottom <= ladder2.Bottom && player.Bottom >= ladder2.Top) || (player.Left <= ladder3.Right && player.Right >= ladder3.Left && player.Bottom <= ladder3.Bottom && player.Bottom >= ladder3.Top))
                {
                    var.onLadder = true;
                }
                else
                {
                    var.onLadder = false;
                }
            }
        }

        private void checkFloor(ref Vars var, PictureBox player)
        {
            if (player.Location.Y == 165 && player.Location.X + 61 > 1255 && player.Location.X < 1775)
            {
                var.numFloor = 6;
            }
            else if (player.Location.Y == 165 && player.Location.X + 61 > 175 && player.Location.X < 690)
            {
                var.numFloor = 5;
            }
            else if (player.Location.Y == 445 && player.Location.X + 61 > 1018 && player.Location.X < 1368)
            {
                var.numFloor = 4;
            }
            else if (player.Location.Y == 445 && player.Location.X + 61 > 587 && player.Location.X < 937)
            {
                var.numFloor = 3;
            }
            else if (player.Location.Y == 735 && player.Location.X + 61 > 1255 && player.Location.X < 1775)
            {
                var.numFloor = 2;
            }
            else if (player.Location.Y == 735 && player.Location.X + 61 > 175 && player.Location.X < 690)
            {
                var.numFloor = 1;
            }
            else
            {
                var.numFloor = 0;
            }
        }

        private void bulletTimer_Tick(object sender, EventArgs e)
        {
            bVars.counter++;
            if (bVars.counter >= 20)
            {
                bVars.canShoot = true;
            }
            try
            {
                foreach (PictureBox bullet in bVars.bullets)
                {
                    if (bullet.Name == "right")
                    {
                        bullet.Location = new Point(bullet.Location.X + 10, bullet.Location.Y);
                    }
                    else if (bullet.Name == "left")
                    {
                        bullet.Location = new Point(bullet.Location.X - 10, bullet.Location.Y);
                    }
                    if (bullet.Location.X >= 1940 || bullet.Location.X <= -30)
                    {
                        this.Controls.Remove(bullet);
                        bullet.Dispose();
                    }
                    if (bullet.Bounds.IntersectsWith(johnny.Bounds) && bullet.Visible == true)
                    {
                        johnnyLife.Size = new Size(johnnyLife.Size.Width - 20, johnnyLife.Size.Height);
                        bullet.Visible = false;
                    }
                }
            }
            catch(NullReferenceException)
            {

            }

            jVars.counter++;
            if (jVars.counter >= 20)
            {
                jVars.canShoot = true;
            }
            try
            {
                foreach (PictureBox bullet in jVars.bullets)
                {
                    if (bullet.Name == "right")
                    {
                        bullet.Location = new Point(bullet.Location.X + 10, bullet.Location.Y);
                    }
                    else if (bullet.Name == "left")
                    {
                        bullet.Location = new Point(bullet.Location.X - 10, bullet.Location.Y);
                    }
                    if (bullet.Location.X >= 1940 || bullet.Location.X <= -30)
                    {
                        this.Controls.Remove(bullet);
                        bullet.Dispose();
                    }
                    if (bullet.Bounds.IntersectsWith(bobby.Bounds) && bullet.Visible == true)
                    {
                        bobbyLife.Size = new Size(bobbyLife.Size.Width - 20, bobbyLife.Size.Height);
                        bobbyLife.Location = new Point(bobbyLife.Location.X + 20, bobbyLife.Location.Y);
                        bullet.Visible = false;
                    }
                }
            }
            catch (NullReferenceException)
            {

            }
        }

        private void reset()
        {
            quit = false;
            bobbyWon = false;
            johnnyWon = false;
            bVars.picNum = 1;
            bVars.climbPicNum = 1;
            bVars.lookingRight = true;
            bVars.jump = false;
            bVars.floatTime = 0;
            bVars.speed = new Point(0,0);
            bVars.onLadder = false;
            bVars.numFloor = 1;
            bVars.downPress = false;
            bVars.falling = false;
            bVars.bulletNum = 0;
            bVars.counter = 20;
            bVars.canShoot = true;
            bobby.Location = new Point(1410, 165);
            bobbyLife.Size = new Size(200, 20);
            bobbyLife.Location = new Point(1712, 12);
            try
            {
                foreach (PictureBox bullet in bVars.bullets)
                {
                    this.Controls.Remove(bullet);
                    bullet.Dispose();
                }
            }
            catch (NullReferenceException)
            {
                bVars.bullets = new PictureBox[10];
            }

            jVars.picNum = 1;
            jVars.climbPicNum = 1;
            jVars.lookingRight = false;
            jVars.jump = false;
            jVars.floatTime = 0;
            jVars.speed = new Point(0, 0);
            jVars.onLadder = false;
            jVars.numFloor = 1;
            jVars.downPress = false;
            jVars.falling = false;
            jVars.bulletNum = 0;
            jVars.counter = 20;
            jVars.canShoot = true;
            johnny.Location = new Point(434, 735);
            johnnyLife.Size = new Size(200, 20);
            try
            {
                foreach (PictureBox bullet in jVars.bullets)
                {
                    this.Controls.Remove(bullet);
                    bullet.Dispose();
                }
            }
            catch (NullReferenceException)
            {
                jVars.bullets = new PictureBox[10];
            }
        }

        private void checkLife()
        {
            if (johnnyLife.Size.Width == 0 || johnny.Location.Y >= 1100)
            {
                mainTimer.Stop();
                bulletTimer.Stop();
                bobbyWon = true;
                GameEnd.ShowDialog();
                if (quit == true) this.Close();
                mainTimer.Start();
                bulletTimer.Start();
                reset();
            }
            if (bobbyLife.Size.Width == 0 || bobby.Location.Y >= 1100)
            {
                mainTimer.Stop();
                bulletTimer.Stop();
                johnnyWon = true;
                GameEnd.ShowDialog();
                if (quit == true) this.Close();
                mainTimer.Start();
                bulletTimer.Start();
                reset();
            }
        }
    }
}
