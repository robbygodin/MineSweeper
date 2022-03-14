using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace MineSweeper
{
    public partial class Form1 : Form
    {
        // System.Drawing.ColorS USED FOR THE PICTUREBOXES
        // from: https://flatuicolors.com/palette/de
        public System.Drawing.Color lightCovered = System.Drawing.Color.FromArgb(69, 170, 242); // high blue
        public System.Drawing.Color darkCovered = System.Drawing.Color.FromArgb(45, 152, 218); // boyzone
        public System.Drawing.Color lightOpen = System.Drawing.Color.FromArgb(209, 216, 224); // twinkle blue
        public System.Drawing.Color darkOpen = System.Drawing.Color.FromArgb(165, 177, 194); // innuendo
        public System.Drawing.Color flag = System.Drawing.Color.FromArgb(235, 59, 90); // desire
        public System.Drawing.Color one = System.Drawing.Color.FromArgb(75, 123, 236); // C64NTS
        public System.Drawing.Color two = System.Drawing.Color.FromArgb(56, 103, 214); // royal blue
        public System.Drawing.Color three = System.Drawing.Color.FromArgb(136, 84, 208); // gloomy purple
        public System.Drawing.Color four = System.Drawing.Color.FromArgb(165, 94, 234); // lighter purple
        public System.Drawing.Color five = System.Drawing.Color.FromArgb(254, 211, 48); // flirtatious
        public System.Drawing.Color six = System.Drawing.Color.FromArgb(247, 183, 49); // nyc taxi
        public System.Drawing.Color seven = System.Drawing.Color.FromArgb(253, 150, 68); // orange hibiscus
        public System.Drawing.Color eight = System.Drawing.Color.FromArgb(250, 130, 49); // beniukon bronze

        MediaPlayer lossSound = new MediaPlayer();

        int time = 0;

        bool end = false;
        bool revealBombs = false;

        Stopwatch stopwatch = new Stopwatch();

        // array of the boxes
        public Box[,] boxes = new Box[30, 10];

        // bool for if the user has clicked a box yet or not
        bool alrClicked = false;

        // random variable
        public Random rand = new Random((int)DateTime.Now.Ticks);

        // list of the bombs
        List<Box> bombs = new List<Box>();

        public Form1()
        {
            InitializeComponent();

            flagLabelLabel.Text = "\u26ff";

            int r = 0;
            int c = 0;
            while (r < 30)
            {
                while (c < 10)
                {
                    Box box = new Box(r, c);
                    this.Controls.Add(box.label);
                    // create the mouse click event for this picturebox
                    box.label.MouseClick += new MouseEventHandler(boxClick);
                    boxes[r, c] = box;
                    c++;
                }
                r++;
                c = 0;
            }

            showBombs.RunWorkerAsync();
            lineLabel.SendToBack();
        }

        MediaPlayer clickSound = new MediaPlayer();
        MediaPlayer flagSound = new MediaPlayer();

        private void boxClick(object sender, MouseEventArgs e)
        {

            Label label = (Label)sender;
            Box box = new Box(label.Location.X / label.Width, (label.Location.Y - 100) / label.Height);
            int x = box.getRow();
            int y = box.getCol();
            // find which box was clicked and save it
            foreach (Box box2 in boxes)
            {
                if (box2.label == label)
                    box = box2;
            }



            if (label.Text != "" && !box.isFlagged)
            {
                return;
            }
            // if the user hasn't clicked yet, generate the map
            if (!alrClicked && e.Button != MouseButtons.Right)
            {
                clickSound.Open(new Uri(Application.StartupPath + "\\Sounds\\Ding.wav"));
                clickSound.Play();
                alrClicked = true;
                timer.Enabled = true;
                timer.Start();
                stopwatch.Start();
                genMap(box);
                if (box.label.BackColor == darkCovered)
                    box.label.BackColor = darkOpen;
                else if (box.label.BackColor == lightCovered)
                    box.label.BackColor = lightOpen;
                clear(x, y);
                visited.Clear();
            }
            else
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (box.isFlagged)
                        label.Text = "";
                    else
                        label.Text = "\u26ff";
                    box.isFlagged = !box.isFlagged;
                    box.setForeColor();
                    flagSound.Volume += .25;
                    flagSound.Open(new Uri(Application.StartupPath + "\\Sounds\\Flag.wav"));
                    flagSound.Play();

                }
                else if (!box.isFlagged)
                {
                    if (box.getBomb())
                    {
                        explode(box);
                        box.label.BackColor = flag;
                        revealBombs = true;
                        lossSound.Open(new Uri(Application.StartupPath + "\\Sounds\\Death Sound.wav"));
                        lossSound.Play();
                    }
                    else if (box.getNum() != 0)
                    {
                        label.Text = box.getNum().ToString();
                        box.setForeColor();

                        if (box.label.BackColor == darkCovered)
                            box.label.BackColor = darkOpen;
                        else if (box.label.BackColor == lightCovered)
                            box.label.BackColor = lightOpen;

                        clickSound.Open(new Uri(Application.StartupPath + "\\Sounds\\Ding.wav"));
                        clickSound.Play();
                    }
                    else if (box.getNum() == 0)
                    {
                        if (box.label.BackColor == darkCovered)
                            box.label.BackColor = darkOpen;
                        else if (box.label.BackColor == lightCovered)
                            box.label.BackColor = lightOpen;
                        clear(x, y);
                        visited.Clear();

                        clickSound.Open(new Uri(Application.StartupPath + "\\Sounds\\Ding.wav"));
                        clickSound.Play();
                    }
                    else if (box.isFlagged)
                    {
                        label.Text = "\u1F6A9";
                        box.setForeColor();

                        flagSound.Open(new Uri(Application.StartupPath + "\\Sounds\\Flag.wav"));
                        flagSound.Play();
                    }
                }
            }
        }

        List<Box> visited = new List<Box>();


        private void clear(int x, int y)
        {
            // if this one has been visited
            if (visited.Contains(boxes[x, y]))
                return;

            visited.Add(boxes[x, y]);

            if (boxes[x, y].label.BackColor == darkCovered)
                boxes[x, y].label.BackColor = darkOpen;
            else if (boxes[x, y].label.BackColor == lightCovered)
                boxes[x, y].label.BackColor = lightOpen;

            boxes[x, y].setForeColor();

            if (x < 29)
            {
                if (boxes[x + 1, y].getNum() == 0)
                {
                    clear(x + 1, y);
                }
                else if(!boxes[x + 1, y].getBomb())
                {
                    if (boxes[x + 1, y].label.BackColor == darkCovered)
                        boxes[x + 1, y].label.BackColor = darkOpen;
                    else if (boxes[x + 1, y].label.BackColor == lightCovered)
                        boxes[x + 1, y].label.BackColor = lightOpen;

                    boxes[x + 1, y].setForeColor();
                    boxes[x + 1, y].label.Text = boxes[x + 1, y].getNum().ToString();
                }
            }

            if (x > 0)
            {
                if (boxes[x - 1, y].getNum() == 0)
                {
                    clear(x - 1, y);
                }
                else if (!boxes[x - 1, y].getBomb())
                {
                    if (boxes[x - 1, y].label.BackColor == darkCovered)
                        boxes[x - 1, y].label.BackColor = darkOpen;
                    else if (boxes[x - 1, y].label.BackColor == lightCovered)
                        boxes[x - 1, y].label.BackColor = lightOpen;

                    boxes[x - 1, y].setForeColor();
                    boxes[x - 1, y].label.Text = boxes[x - 1, y].getNum().ToString();

                }
            }

            if (y < 9)
            {
                if (boxes[x, y + 1].getNum() == 0)
                {
                    clear(x, y + 1);
                }
                else if (!boxes[x, y + 1].getBomb())
                {
                    if (boxes[x, y + 1].label.BackColor == darkCovered)
                        boxes[x, y + 1].label.BackColor = darkOpen;
                    else if (boxes[x, y + 1].label.BackColor == lightCovered)
                        boxes[x, y + 1].label.BackColor = lightOpen;

                    boxes[x, y + 1].setForeColor();
                    boxes[x, y + 1].label.Text = boxes[x, y + 1].getNum().ToString();
                }
            }

            if (y > 0)
            {
                if (boxes[x, y - 1].getNum() == 0)
                {
                    clear(x, y - 1);
                }
                else if(!boxes[x, y - 1].getBomb())
                {
                    if (boxes[x, y - 1].label.BackColor == darkCovered)
                        boxes[x, y - 1].label.BackColor = darkOpen;
                    else if (boxes[x, y - 1].label.BackColor == lightCovered)
                        boxes[x, y - 1].label.BackColor = lightOpen;

                    boxes[x, y - 1].setForeColor();
                    boxes[x, y - 1].label.Text = boxes[x, y - 1].getNum().ToString();
                }
            }

            if (x < 29 && y < 9)
            {
                if (boxes[x + 1, y + 1].getNum() != 0)
                {
                    if (boxes[x + 1, y + 1].label.BackColor == darkCovered)
                        boxes[x + 1, y + 1].label.BackColor = darkOpen;
                    else if (boxes[x + 1, y + 1].label.BackColor == lightCovered)
                        boxes[x + 1, y + 1].label.BackColor = lightOpen;

                    boxes[x + 1, y + 1].setForeColor();
                    boxes[x + 1, y + 1].label.Text = boxes[x + 1, y + 1].getNum().ToString();
                }
            }

            if (x < 29 && y > 0)
            {
                if (boxes[x + 1, y - 1].getNum() != 0)
                {
                    if (boxes[x + 1, y - 1].label.BackColor == darkCovered)
                        boxes[x + 1, y - 1].label.BackColor = darkOpen;
                    else if (boxes[x + 1, y - 1].label.BackColor == lightCovered)
                        boxes[x + 1, y - 1].label.BackColor = lightOpen;

                    boxes[x + 1, y - 1].setForeColor();
                    boxes[x + 1, y - 1].label.Text = boxes[x + 1, y - 1].getNum().ToString();
                }
            }

            if (x > 0 && y < 9)
            {
                if (boxes[x - 1, y + 1].getNum() != 0)
                {
                    if (boxes[x - 1, y + 1].label.BackColor == darkCovered)
                        boxes[x - 1, y + 1].label.BackColor = darkOpen;
                    else if (boxes[x - 1, y + 1].label.BackColor == lightCovered)
                        boxes[x - 1, y + 1].label.BackColor = lightOpen;

                    boxes[x - 1, y + 1].setForeColor();
                    boxes[x - 1, y + 1].label.Text = boxes[x - 1, y + 1].getNum().ToString();
                }
            }

            if (x > 0 && y > 0)
            {
                if (boxes[x - 1, y - 1].getNum() != 0)
                {
                    if (boxes[x - 1, y - 1].label.BackColor == darkCovered)
                        boxes[x - 1, y - 1].label.BackColor = darkOpen;
                    else if (boxes[x - 1, y - 1].label.BackColor == lightCovered)
                        boxes[x - 1, y - 1].label.BackColor = lightOpen;


                    boxes[x - 1, y - 1].label.Text = boxes[x - 1, y - 1].getNum().ToString();
                    boxes[x - 1, y - 1].setForeColor();
                }
            }

            return;
        }


        private void explode(Box box)
        {
            Console.WriteLine("BOOM");
        }

        private bool surroundingIsBomb(Box box)
        {
            int row = box.getRow();
            int col = box.getCol();

            // increase the number of the boxes around this bomb
            if (box.getRow() > 0)
            {
                // top middle
                if (boxes[row - 1, col].getBomb())
                {
                    return true;
                }

                if (box.getCol() > 0)
                {
                    // top left
                    if (boxes[row - 1, col - 1].getBomb())
                    {
                        return true;
                    }

                    // middle left
                    if (boxes[row, col - 1].getBomb())
                    {
                        return true;
                    }
                }

                if (box.getCol() < 9)
                {
                    // top right
                    if (boxes[row - 1, col + 1].getBomb())
                    {
                        return true;
                    }

                    // middle right
                    if (boxes[row, col + 1].getBomb())
                    {
                        return true;
                    }
                }
            }

            if (box.getRow() < 29)
            {
                // bottom middle
                if (boxes[row + 1, col].getBomb())
                {
                    return true;
                }

                if (box.getCol() > 0)
                {
                    // bottom left
                    if (boxes[row + 1, col - 1].getBomb())
                    {
                        return true;
                    }
                }

                if (box.getCol() < 9)
                {
                    // bottom right
                    if (boxes[row + 1, col + 1].getBomb())
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        private bool surrounding(Box clicked, Box box)
        {
            int row = box.getRow();
            int col = box.getCol();

            // increase the number of the boxes around this bomb
            if (box.getRow() > 0)
            {
                // top middle
                if (boxes[row - 1, col] == clicked)
                {
                    return true;
                }

                if (box.getCol() > 0)
                {
                    // top left
                    if (boxes[row - 1, col - 1] == clicked)
                    {
                        return true;
                    }

                    // middle left
                    if (boxes[row, col - 1] == clicked)
                    {
                        return true;
                    }
                }

                if (box.getCol() < 9)
                {
                    // top right
                    if (boxes[row - 1, col + 1] == clicked)
                    {
                        return true;
                    }

                    // middle right
                    if (boxes[row, col + 1] == clicked)
                    {
                        return true;
                    }
                }
            }

            if (box.getRow() < 29)
            {
                // bottom middle
                if (boxes[row + 1, col] == clicked)
                {
                    return true;
                }

                if (box.getCol() > 0)
                {
                    // bottom left
                    if (boxes[row + 1, col - 1] == clicked)
                    {
                        return true;
                    }
                }

                if (box.getCol() < 9)
                {
                    // bottom right
                    if (boxes[row + 1, col + 1] == clicked)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void genMap(Box clickedBox)
        {
            // set the clicked box to blank
            clickedBox.setNum(0);

            // randomly place bombs around the map
            foreach (Box box in boxes)
            {
                if (box != clickedBox && rand.Next(0, 5) == 0)
                {
                    // save the row and column into variables
                    int row = box.getRow();
                    int col = box.getCol();

                    if (!surroundingIsBomb(box) && !surrounding(clickedBox, box)) 
                    {
                        // turn this box into a bomb
                        box.setBomb();
                        bombs.Add(box);

                        // increase the number of the boxes around this bomb
                        if (box.getRow() > 0)
                        {
                            // top middle
                            if (!boxes[row - 1, col].getBomb())
                            {
                                if (boxes[row - 1, col] != clickedBox)
                                    boxes[row - 1, col].addNum();
                            }

                            if (box.getCol() > 0)
                            {
                                // top left
                                if (!boxes[row - 1, col - 1].getBomb())
                                {
                                    if (boxes[row - 1, col - 1] != clickedBox)
                                        boxes[row - 1, col - 1].addNum();
                                }

                                // middle left
                                if (!boxes[row, col - 1].getBomb())
                                {
                                    if (boxes[row, col - 1] != clickedBox)
                                        boxes[row, col - 1].addNum();
                                }
                            }

                            if (box.getCol() < 9)
                            {
                                // top right
                                if (!boxes[row - 1, col + 1].getBomb())
                                {
                                    if (boxes[row - 1, col + 1] != clickedBox)
                                        boxes[row - 1, col + 1].addNum();
                                }

                                // middle right
                                if (!boxes[row, col + 1].getBomb())
                                {
                                    if (boxes[row, col + 1] != clickedBox)
                                        boxes[row, col + 1].addNum();
                                }
                            }
                        }

                        if (box.getRow() < 29)
                        {
                            // bottom middle
                            if (!boxes[row + 1, col].getBomb())
                            {
                                if (boxes[row + 1, col] != clickedBox)
                                    boxes[row + 1, col].addNum();
                            }

                            if (box.getCol() > 0)
                            {
                                // bottom left
                                if (!boxes[row + 1, col - 1].getBomb())
                                {
                                    if (boxes[row + 1, col - 1] != clickedBox)
                                        boxes[row + 1, col - 1].addNum();
                                }
                            }

                            if (box.getCol() < 9)
                            {
                                // bottom right
                                if (!boxes[row + 1, col + 1].getBomb())
                                {
                                    if (boxes[row + 1, col + 1] != clickedBox)
                                        boxes[row + 1, col + 1].addNum();
                                }
                            }
                        }
                    }
                }

                box.setForeColor();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timerLabel.Text = ((int) stopwatch.Elapsed.TotalMinutes).ToString() + ":";
            if ((int)stopwatch.Elapsed.TotalSeconds - ((int)stopwatch.Elapsed.TotalMinutes * 60) < 10)
                timerLabel.Text += "0";
            timerLabel.Text += ((int)stopwatch.Elapsed.TotalSeconds - ((int)stopwatch.Elapsed.TotalMinutes * 60)).ToString();
        }

        private void showBombs_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!end)
            {
                while (revealBombs)
                {
                    showBombs.ReportProgress(1);
                    Thread.Sleep(250);
                }
            }
        }

        // int for which bomb needs to be shown
        int showBombNum = 0;

        private void showBombs_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            timer.Stop();
            bombs[showBombNum].label.BackColor = flag;
            showBombNum++;

            if (showBombNum >= bombs.Count() - 1)
            {
                revealBombs = false;
                lossSound.Stop();
            }
        }
    }






    public class Box
    {
        // System.Drawing.ColorS USED FOR THE PICTUREBOXES
        // from: https://flatuicolors.com/palette/de
        public System.Drawing.Color lightCovered = System.Drawing.Color.FromArgb(69, 170, 242); // high blue
        public System.Drawing.Color darkCovered = System.Drawing.Color.FromArgb(45, 152, 218); // boyzone
        public System.Drawing.Color lightOpen = System.Drawing.Color.FromArgb(209, 216, 224); // twinkle blue
        public System.Drawing.Color darkOpen = System.Drawing.Color.FromArgb(165, 177, 194); // innuendo
        public System.Drawing.Color flag = System.Drawing.Color.FromArgb(235, 59, 90); // desire
        public System.Drawing.Color one = System.Drawing.Color.FromArgb(75, 123, 236); // C64NTS
        public System.Drawing.Color two = System.Drawing.Color.FromArgb(56, 103, 214); // royal blue
        public System.Drawing.Color three = System.Drawing.Color.FromArgb(136, 84, 208); // gloomy purple
        public System.Drawing.Color four = System.Drawing.Color.FromArgb(165, 94, 234); // lighter purple
        public System.Drawing.Color five = System.Drawing.Color.FromArgb(254, 211, 48); // flirtatious
        public System.Drawing.Color six = System.Drawing.Color.FromArgb(247, 183, 49); // nyc taxi
        public System.Drawing.Color seven = System.Drawing.Color.FromArgb(253, 150, 68); // orange hibiscus
        public System.Drawing.Color eight = System.Drawing.Color.FromArgb(250, 130, 49); // beniukon bronze

        bool bomb;
        int number = 0; // if bomb, number = 9, if blank, number = 0
        public Label label = new Label();
        int x;
        int y;
        public bool isFlagged; 


        public Box(int inRow, int inCol)
        {

            x = inRow;
            y = inCol;

            // set the settings for the picture box
            label.Size = new Size(50, 50);
            label.Location = new Point(x * label.Width, 100 + y * label.Height);
            label.Font = new Font("Arial Rounded MT Bold", 28);
            label.TextAlign = ContentAlignment.MiddleCenter;

            // set the System.Drawing.Color
            if (x % 2 == 0)
            {
                if (y % 2 == 0)
                    label.BackColor = darkCovered;
                else
                    label.BackColor = lightCovered;
            }
            else if (y % 2 == 0)
                label.BackColor = lightCovered;
            else
                label.BackColor = darkCovered;
        }



        public void setBomb()
        {
            bomb = true;
            number = 9;
        }


        public bool getBomb()
        {
            return bomb;
        }


        public void setNum(int amount)
        {
            number = amount;
        }

        public void addNum(int amount)
        {
            number += amount;
        }

        public void addNum()
        {
            number++;
        }

        public int getNum()
        {
            return number;
        }


        public int getRow()
        {
            return x;
        }


        public int getCol()
        {
            return y;
        }


        public void setForeColor()
        {
            if (isFlagged)
            {
                label.ForeColor = flag;
                return;
            }

            switch (number)
            {
                case (0): label.ForeColor = one;  break;
                case (1): label.ForeColor = one; break;
                case (2): label.ForeColor = two; break;
                case (3): label.ForeColor = three; break;
                case (4): label.ForeColor = four; break;
                case (5): label.ForeColor = five; break;
                case (6): label.ForeColor = six; break;
                case (7): label.ForeColor = seven; break;
                case (8): label.ForeColor = eight; break;
                case (9): label.ForeColor = flag; break;
            }


        }
    }
}
