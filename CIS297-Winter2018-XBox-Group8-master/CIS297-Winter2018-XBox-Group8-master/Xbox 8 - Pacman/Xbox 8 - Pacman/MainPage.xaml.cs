//using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Gaming.Input;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Xbox_8___Pacman {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Gamepad controller;
        private DispatcherTimer timer;

        static string pPath = "Assets/Pacman/p.png", pr1Path = "Assets/Pacman/pr1.png", pr2Path = "Assets/Pacman/pr2.png",
         pl1Path = "Assets/Pacman/pl1.png", pl2Path = "Assets/Pacman/pl2.png", pu1Path = "Assets/Pacman/pu1.png",
         pu2Path = "Assets/Pacman/pu2.png", pd1Path = "Assets/Pacman/pd1.png", pd2Path = "Assets/Pacman/pd2.png";

        bool right = false, left = false, up = false, down = false;
        bool p1 = false, p2 = false, p3 = false;
        bool previousRight = false;
        bool PreviousLeft = false;
        bool previousTop = false;
        bool PreviousBottom = false;
        Image tempcyan,temppink,tempred,tempyellow;
        public int score = 0, timeGhosts = 0;
        public bool ghostsBlue = false;

        public bool cright = false, cleft = false, cup = true, cdown = false;
        public bool pright = false, pleft = false, pup = true, pdown = false;
        public bool rright = false, rleft = false, rup = true, rdown = false;
        public bool oright = false, oleft = false, oup = true, odown = false;
        public List<Rectangle> ListOfRectangles = new List<Rectangle>();

        int countDots = 0;
        bool GameDone = false, teleRight = false, teleLeft = false;

        public MainPage()
        {
            this.InitializeComponent();
            //  Thread t = new Thread();\
            GameOver.Visibility = Visibility.Collapsed;
            Win.Visibility = Visibility.Collapsed;

            ghostSound.Volume = 0.1;
            blueghostSound.Volume = 0.1;

            ListOfRectangles.Add(a20);
            ListOfRectangles.Add(a);
            ListOfRectangles.Add(b);
            ListOfRectangles.Add(c);
            ListOfRectangles.Add(d);
            ListOfRectangles.Add(e);
            ListOfRectangles.Add(f);
            ListOfRectangles.Add(g);
            ListOfRectangles.Add(h);
            ListOfRectangles.Add(i);
            ListOfRectangles.Add(j);
            ListOfRectangles.Add(k);
            ListOfRectangles.Add(l);
            ListOfRectangles.Add(m);
            ListOfRectangles.Add(n);
            ListOfRectangles.Add(o);
            ListOfRectangles.Add(p);
            ListOfRectangles.Add(q);
            ListOfRectangles.Add(r);
            ListOfRectangles.Add(s);
            ListOfRectangles.Add(t);
            ListOfRectangles.Add(u);
            ListOfRectangles.Add(v);
            ListOfRectangles.Add(x);
            ListOfRectangles.Add(y);
            ListOfRectangles.Add(r1);
            ListOfRectangles.Add(a1);
            ListOfRectangles.Add(a2);
            ListOfRectangles.Add(a3);
            ListOfRectangles.Add(a4);
            ListOfRectangles.Add(a5);
            ListOfRectangles.Add(a6);
            ListOfRectangles.Add(GhostHolder);
            ListOfRectangles.Add(a8);
            ListOfRectangles.Add(a9);
            ListOfRectangles.Add(a10);
            //   Canvas.SetTop(Cyan, Canvas.GetTop(Cyan) - 5);
            tempcyan = Cyan;



            // setup game timer
            timer = new DispatcherTimer();
            // add event handler to the Tick event
            timer.Tick += DispatcherTimer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();
        }

        //------------------Teleport Pacman------------------
        public void TeleportPacman()
        {
            double pacright = Canvas.GetLeft(Pacman) + Pacman.Width;
            double pacleft = Canvas.GetLeft(Pacman);
            double pactop = Canvas.GetTop(Pacman);
            double pacbot = Canvas.GetTop(Pacman) + Pacman.Height;

            if ((pactop > 175 && pactop < 285) && (pacleft <= 2))
            {
                Canvas.SetTop(Pacman, pactop);
                Canvas.SetLeft(Pacman, 635);
            }

            if ((pactop > 175 && pactop < 285) && (pacleft >= 635 || pacright >= Pacman.Width + 640))
            {
                Canvas.SetTop(Pacman, pactop);
                Canvas.SetLeft(Pacman, 10);
            }
        }

        //-----------------Checks If Pacman Wins-------------
        public void PacmanWins()
        {
            foreach(UIElement dots in Dots.Children)
            {
                if(dots.Visibility == Visibility.Visible)
                {
                    countDots++;
                }
            }
            foreach(UIElement d in BigDots.Children)
            {
                if (d.Visibility == Visibility.Visible)
                {
                    countDots++;
                }
            }
            if(countDots == 0 && !GameDone)
            {
                //Pacman Wins ... Game Over ... Kill Everything
                foreach (UIElement gh in Ghosts.Children)
                {
                    gh.Visibility = Visibility.Collapsed;
                }
                for (int i = 0; i < ListOfRectangles.Count; i++)
                {
                    ListOfRectangles[i].Visibility = Visibility.Collapsed;
                }
                Pacman.Visibility = Visibility.Collapsed;
                Win.Visibility = Visibility.Visible;
                endSound.Play();
            }
            else
            {
                countDots = 0;
            }
        }

        //-----------------Cyan Ghost AI---------------------
        public void CyanGhostAI()
        {

            if (cup)
            {
                if (Canvas.GetTop(Cyan) <= 10)
                {
                    cup = false;
                    //cdown = true;
                    cleft = true;
                    cright = true;
                    //cdown = true;
                    cyanUp();
                }
                else
                {
                    if (cyanUp())
                    {
                        cup = false;
                        Random rand = new Random();
                        int randNum = rand.Next(1, 3);
                        if (randNum == 1)
                        {
                            cleft = true;
                        }
                        else if (randNum == 2)
                        {
                            cright = true;
                        }
                        //else if(randNum==3)
                        //{
                        //    cdown = true;
                        //}
                    }
                    else
                    {
                        Canvas.SetTop(Cyan, Canvas.GetTop(Cyan) - 5);
                        if (cyanUp())
                        {
                            cup = false;
                            Random rand = new Random();
                            int randNum = rand.Next(1, 3);
                            if (randNum == 1)
                            {
                                cleft = true;
                            }
                            else if (randNum == 2)
                            {
                                cright = true;
                            }
                            //else if(randNum==3)
                            //{
                            //    cdown = true;
                            //}
                        }
                        //     cyanUp();
                    }
                }
            }
            else if (cdown)
            {
                if (Canvas.GetTop(Cyan) + Cyan.Height >= 531)
                {
                    // cleft = true;
                    //   cright = true; cleft = true;
                    cdown = false;
                    cup = true;
                }
                else
                {
                    if (cyandown())
                    {
                        cdown = false;
                        Random rand = new Random();
                        int randNum = rand.Next(1, 3);
                        if (randNum == 1)
                        {
                            cleft = true;
                        }
                        if (randNum == 2)
                        {
                            cright = true;
                        }
                    }
                    else
                    {
                        Canvas.SetTop(Cyan, Canvas.GetTop(Cyan) + 5);
                        if (cyandown())
                        {
                            cdown = false;
                            Random rand = new Random();
                            int randNum = rand.Next(1, 3);
                            if (randNum == 1)
                            {
                                cleft = true;
                            }
                            if (randNum == 2)
                            {
                                cright = true;
                            }
                        }
                    }
                }
            }
            else if (cright)
            {
                //    if (Canvas.getrigh)
                if (Canvas.GetLeft(Cyan) + Cyan.Width >= 664)
                {
                    cright = false;
                    cleft = true;
                   // cup = true;

                    //1cdown = true;
                }
                else
                {
                    if (cyanRight())
                    {
                        cright = false;

                        Random rand = new Random();
                        int randNum = rand.Next(1, 3);
                        if (randNum == 1)
                        {
                            cdown = true;
                        }
                        if (randNum == 2)
                        {
                            cup = true;
                        }
                    }
                    else
                    {
                        Canvas.SetLeft(Cyan, Canvas.GetLeft(Cyan) + 5);
                        if (cyanRight())
                        {
                            cright = false;

                            Random rand = new Random();
                            int randNum = rand.Next(1, 3);
                            if (randNum == 1)
                            {
                                cdown = true;
                            }
                            if (randNum == 2)
                            {
                                cup = true;
                            }
                        }
                    }
                }
            }
            else if (cleft)
            {
                if (Canvas.GetLeft(Cyan)<=10)
                {
                    cright = true;
                    cleft = false;
                }
                if (cyanLeft())
                {
                    cleft = false;
                    Random rand = new Random();
                    int randNum = rand.Next(1, 3);
                    if (randNum == 1)
                    {
                        cdown = true;
                    }
                    if (randNum == 2)
                    {
                        cup = true;
                    }
                }
                else
                {
                    Canvas.SetLeft(Cyan, Canvas.GetLeft(Cyan) - 5);
                    if (cyanLeft())
                    {
                        cleft = false;
                        Random rand = new Random();
                        int randNum = rand.Next(1, 3);
                        if (randNum == 1)
                        {
                            cdown = true;
                        }
                        if (randNum == 2)
                        {
                            cup = true;
                        }
                    }
                }
            }




            ////When the ghost is in the box its only direction to go is up
            ////    Canvas.SetTop(Cyan, Canvas.GetTop(Cyan) - 5);
            ////     cup = true;
            //if (cup == false && cdown == false && cleft == false && cright == false)
            //{
            //    Random rand = new Random();
            //    int randNum = rand.Next(0, 5);

            //    if (randNum == 1)
            //    {


            //        //      Canvas.SetTop(Cyan, Canvas.GetTop(Cyan) - 5);
            //        cup = true;
            //        if (cyanUp())
            //        {
            //            cup = false;
            //        }


            //    }

            //    if (randNum == 2)
            //    {
            //        cdown = true;
            //        if (cyandown())
            //        {
            //            cdown = false;
            //        }
            //        //Canvas.SetTop(Cyan, Canvas.GetTop(Cyan) + 5);

            //    }
            //    if (randNum == 4)
            //    {
            //        cleft = true;
            //        if (cyanLeft())
            //        {
            //            cleft = false;
            //        }
            //    }
            //    if (randNum == 3)
            //    {
            //        //Canvas.SetLeft(Cyan, Canvas.GetLeft(Cyan) + 5);
            //        cright = true;
            //        if (cyanRight())
            //        {
            //            cright = false;
            //        }
            //    }
            //}

            ////If direction of Cyan Ghost is up
            //if (cup)
            //{
            //    if (cyanUp())
            //    {

            //        cup = false;

            //        Random rand = new Random();
            //        int randNum = rand.Next(0, 4);

            //        if (randNum == 1)
            //        {
            //            cdown = true;
            //            //Direction of ghost changes to down
            //            //   Canvas.SetTop(Cyan, Canvas.GetTop(Cyan) + 5);
            //            if (cyandown())
            //            { cdown = false; }
            //        }
            //        else if (randNum == 2)
            //        {
            //            cright = true;
            //            //Direction of ghost changes to right
            //            //  Canvas.SetLeft(Cyan, Canvas.GetLeft(Cyan) + 5);
            //            if (cyanRight())
            //            { cright = false; }
            //        }
            //        else if (randNum == 3)
            //        {
            //            cleft = true;
            //            //Direction of ghost changes to left
            //            //  Canvas.SetLeft(Cyan, Canvas.GetLeft(Cyan) - 5);
            //            if (cyanLeft())
            //            { cleft = false ; }
            //        }
            //    }
            //    else
            //    {
            //        Canvas.SetTop(Cyan, Canvas.GetTop(Cyan) - 5);
            //      //  cup = false;
            //        cyanUp();
            //    }
            //}

            ////If direction of Cyan Ghost is down
            //if (cdown)
            //{
            //    if (cyandown())
            //    {

            //        //   Canvas.SetTop(Cyan, top - Cyan.Height - 1);
            //        cdown = false;

            //        Random rand = new Random();
            //        int randNum = rand.Next(0, 4);

            //        if (randNum == 1)
            //        {

            //            //    Canvas.SetTop(Cyan, Canvas.GetTop(Cyan) - 5);
            //            cup = true;
            //            if (cyanUp())
            //            {
            //                cup = false;
            //            }

            //        }

            //        if (randNum == 2)
            //        {
            //            //  Canvas.SetLeft(Cyan, Canvas.GetLeft(Cyan) + 5);
            //            cright = true;
            //            if (cyanRight())
            //            {
            //                cright = false;
            //            }

            //        }

            //        if (randNum == 3)
            //        {
            //            //Canvas.SetLeft(Cyan, Canvas.GetLeft(Cyan) - 5);
            //            cleft = true;
            //            if (cyanLeft())
            //            {
            //                cleft = false;
            //            }
            //        }

            //    }
            //    else
            //    {
            //        Canvas.SetTop(Cyan, Canvas.GetTop(Cyan) + 5);
            //    //    cdown = false;

            //        cyandown();
            //    }
            //}

            ////If direction of Cyan Ghost is right
            //if (cright)
            //{
            //    if (Canvas.GetLeft(Cyan) + Cyan.Width >= 664)
            //    {
            //        cright = false;
            //    }
            //    else
            //    {
            //        if (cyanRight())
            //        {

            //            //   Canvas.SetLeft(Cyan, left - Cyan.Width - 2);
            //            cright = false;

            //            Random rand = new Random();
            //            int randNum = rand.Next(0, 4);

            //            if (randNum == 1)
            //            {

            //                cup = true;
            //                //Canvas.SetTop(Cyan, Canvas.GetTop(Cyan) - 5);
            //                if (cyanUp())
            //                {
            //                    cup = false;
            //                }

            //            }

            //            if (randNum == 2)
            //            {
            //                cdown = true;
            //                //  Canvas.SetTop(Cyan, Canvas.GetTop(Cyan) + 5);
            //                if (cyandown())
            //                {
            //                    cdown = false;
            //                }
            //            }

            //            if (randNum == 3)
            //            {
            //                //  Canvas.SetLeft(Cyan, Canvas.GetLeft(Cyan) - 5);
            //                cleft = true;
            //                if (cyanLeft())
            //                {
            //                    cleft = false;
            //                }
            //            }

            //        }
            //        else
            //        {
            //            //   double leftie=    Canvas.GetLeft(Cyan);
            //            Canvas.SetLeft(Cyan, Canvas.GetLeft(Cyan) + 5);
            //      //      cright = false;
            //            cyanRight();
            //        }
            //    }
            //}

            ////If direction of Cyan Ghost is left
            //if (cleft)
            //{
            //    if (cyanLeft())
            //    {
            //        // if ((cyanleft <= right && cyanright >= right))
            //        // {
            //        //   Canvas.SetLeft(Cyan, right + 1);
            //        cleft = false;

            //        Random rand = new Random();
            //        int randNum = rand.Next(0, 4);

            //        if (randNum == 1)
            //        {
            //            cup = true;
            //            if (cyanUp())

            //            {
            //                cup = false;
            //            }
            //            //      Canvas.SetTop(Cyan, Canvas.GetTop(Cyan) - 5);


            //        }

            //        if (randNum == 2)
            //        {
            //            //Canvas.SetTop(Cyan, Canvas.GetTop(Cyan) + 5);
            //            cdown = true;
            //            if (cyandown())
            //            {
            //                cdown = false;
            //            }
            //        }

            //        if (randNum == 3)
            //        {
            //            //Canvas.SetLeft(Cyan, Canvas.GetLeft(Cyan) + 5);
            //            cright = true;
            //            if (cyanLeft())
            //            { cright = false; }
            //        }

            //    }
            //    else
            //    {
            //        Canvas.SetLeft(Cyan, Canvas.GetLeft(Cyan) - 5);
            //        cyanLeft();
            //     //   cleft = false;
            //    }
            //}

        }

        //Turns the ghosts blue if a big dot is eaten
        public void MakeGhostsBlue()
        {
            foreach (UIElement dot in BigDots.Children)
            {
                Ellipse dots = dot as Ellipse;
                double pacright = Canvas.GetLeft(Pacman) + Pacman.Width;
                double pacleft = Canvas.GetLeft(Pacman);
                double pactop = Canvas.GetTop(Pacman);
                double pacbot = Canvas.GetTop(Pacman) + Pacman.Height;
                double left = Canvas.GetLeft(dot);
                double right = Canvas.GetLeft(dots) + dots.Width;
                double top = Canvas.GetTop(dots);
                double bottom = Canvas.GetTop(dots) + dots.Height;

                if ((pactop <= top && pacbot >= bottom) && (pacleft <= left && pacright > left))
                {
                    if (dots.Visibility != Visibility.Collapsed)
                    {
                        dots.Visibility = Visibility.Collapsed;
                        ghostsBlue = true;
                        timeGhosts = 0;
                        score++;
                        if (ghostsBlue)
                        {
                            Cyan.Source = new BitmapImage(new Uri(Cyan.BaseUri, "Assets/Ghosts/blue.png"));
                            Orange.Source = new BitmapImage(new Uri(Orange.BaseUri, "Assets/Ghosts/blue.png"));
                            Pink.Source = new BitmapImage(new Uri(Pink.BaseUri, "Assets/Ghosts/blue.png"));
                            Red.Source = new BitmapImage(new Uri(Red.BaseUri, "Assets/Ghosts/blue.png"));
                        }
                    }
                }
            }
        }

        //Pacman can kill ghosts if they are blue
        public void KillGhosts()
        {
            foreach (UIElement ghost in Ghosts.Children)
            {
                Image g = ghost as Image;
                double pacright = Canvas.GetLeft(Pacman) + Pacman.Width;
                double pacleft = Canvas.GetLeft(Pacman);
                double pactop = Canvas.GetTop(Pacman);
                double pacbot = Canvas.GetTop(Pacman) + Pacman.Height;
                double left = Canvas.GetLeft(g);
                double right = Canvas.GetLeft(g) + g.Width;
                double top = Canvas.GetTop(g);
                double bottom = Canvas.GetTop(g) + g.Height;

                if ((pactop <= top && pacbot >= bottom) && (pacleft <= left && pacright > left))
                {
                    if (ghostsBlue)
                    {
                        eatghostSound.Play();
                        if (g.Name == "Cyan")
                        {
                            Canvas.SetLeft(g, 270);
                            Canvas.SetTop(g, 243);
                            score += 200;
                        }

                        if (g.Name == "Orange")
                        {
                            Canvas.SetLeft(g, 306);
                            Canvas.SetTop(g, 243);
                            score += 200;
                        }

                        if (g.Name == "Red")
                        {
                            Canvas.SetLeft(g, 378);
                            Canvas.SetTop(g, 243);
                            score += 200;
                        }

                        if (g.Name == "Pink")
                        {
                            Canvas.SetLeft(g, 342);
                            Canvas.SetTop(g, 243);
                            score += 200;
                        }
                    }
                    else
                    {
                        dieSound.Play();
                        ghostSound.Stop();
                        //Pacman Dies ... Game Over ... Kill Everything
                        foreach (UIElement gh in Ghosts.Children)
                        {
                            gh.Visibility = Visibility.Collapsed;
                        }
                        foreach (UIElement dot in Dots.Children)
                        {
                            Dots.Visibility = Visibility.Collapsed;
                        }
                        foreach (UIElement d in BigDots.Children)
                        {
                            d.Visibility = Visibility.Collapsed;
                        }
                        for(int i = 0; i < ListOfRectangles.Count; i++)
                        {
                            ListOfRectangles[i].Visibility = Visibility.Collapsed;
                        }
                        Pacman.Visibility = Visibility.Collapsed;
                        GameOver.Visibility = Visibility.Visible;
                        GameDone = true;
                    }
                }
            }

            //Timer for ghosts to stay blue
            if (ghostsBlue)
            {
                timeGhosts++;
            }

            if (timeGhosts == 100)
            {
               
                ghostsBlue = false;
            }

            //Once timer runs out ghosts change back to their original colors
            if (!ghostsBlue)
            {
                Cyan.Source = new BitmapImage(new Uri(Cyan.BaseUri, "Assets/Ghosts/cyan.png"));
               
                
                Orange.Source = new BitmapImage(new Uri(Orange.BaseUri, "Assets/Ghosts/orange.png"));
                Pink.Source = new BitmapImage(new Uri(Pink.BaseUri, "Assets/Ghosts/pink.png"));
                Red.Source = new BitmapImage(new Uri(Red.BaseUri, "Assets/Ghosts/red.png"));
                timeGhosts = 0;
                tempred = Red;
                temppink = Pink;
                tempyellow = Orange;
                ghostsBlue = false;
                ghostSound.Play();
            }
        }

        //Pacman eats the dots
        public void KillDots()
        {
            foreach (UIElement dot in Dots.Children)
            {
                Ellipse dots = dot as Ellipse;
                double pacright = Canvas.GetLeft(Pacman) + Pacman.Width;
                double pacleft = Canvas.GetLeft(Pacman);
                double pactop = Canvas.GetTop(Pacman);
                double pacbot = Canvas.GetTop(Pacman) + Pacman.Height;
                double left = Canvas.GetLeft(dot);
                double right = Canvas.GetLeft(dots) + dots.Width;
                double top = Canvas.GetTop(dots);
                double bottom = Canvas.GetTop(dots) + dots.Height;

                if ((pactop <= top && pacbot >= bottom) && (pacleft <= left && pacright > left))
                {
                    if (dots.Visibility != Visibility.Collapsed)
                    {
                        //Makes dots invisible and increments the score
                        dots.Visibility = Visibility.Collapsed;
                        score++;
                        eatSound.Play();
                    }
                }
            }
        }

        public void ScoreBoardText()
        {
            TextScore.Text = Convert.ToString(score);
        }
        //Image temp=Cyan;
        private void DispatcherTimer_Tick(object sender, object e)
        {
            MovePacman();

            MakeGhostsBlue();
            KillGhosts();
            KillDots();
            ScoreBoardText();
            PacmanWins();
            TeleportPacman();

            PinkGhostAI();
            CyanGhostAI();
            RedGhostAI();
            OrangeGhostAI();

            if (right || previousRight)
            {
                if ((Canvas.GetLeft(Pacman) + Pacman.Width) < 664)
                {
                    if (PacRight())
                    {
                        right = false;
                    }
                    else
                    {
                        Canvas.SetLeft(Pacman, Canvas.GetLeft(Pacman) + 5);
                        PacRight();
                        //   previousRight = true;
                        previousTop = false;
                        PreviousBottom = false;
                        PreviousLeft = false;

                        if (p3 && !p1 && !p2)
                        {
                            Pacman.Source = new BitmapImage(new Uri(Pacman.BaseUri, pr1Path));
                            p1 = true;
                            p3 = false;
                        }
                        else if (p1 && !p2 && !p3)
                        {
                            Pacman.Source = new BitmapImage(new Uri(Pacman.BaseUri, pr2Path));
                            p1 = false;
                            p2 = true;
                        }
                        else if (p2 && !p1 && !p3)
                        {
                            Pacman.Source = new BitmapImage(new Uri(Pacman.BaseUri, pPath));
                            p3 = true;
                            p2 = false;
                        }
                    }
                }
            }
            else if (left || PreviousLeft)
            {
                if (Canvas.GetLeft(Pacman) > 2)
                {
                    if (PacLeft())
                    {
                        //left = false;
                        //up = false;
                        //previousTop = false;
                        PreviousLeft = false;
                    }
                    else
                    {
                        //up = false;
                        //previousTop = false;
                        //PreviousBottom = false;
                        ////left = false;
                        //// PreviousLeft = true;
                        //previousRight = false;
                        Canvas.SetLeft(Pacman, Canvas.GetLeft(Pacman) - 5);
                        PacLeft();
                    }
                }

                if (p3 && !p1 && !p2)
                {
                    Pacman.Source = new BitmapImage(new Uri(Pacman.BaseUri, pl1Path));
                    p1 = true;
                    p3 = false;
                }
                else if (p1 && !p2 && !p3)
                {
                    Pacman.Source = new BitmapImage(new Uri(Pacman.BaseUri, pl2Path));
                    p1 = false;
                    p2 = true;
                }
                else if (p2 && !p1 && !p3)
                {
                    Pacman.Source = new BitmapImage(new Uri(Pacman.BaseUri, pPath));
                    p3 = true;
                    p2 = false;
                }
            }
            else if (up || previousTop)
            {
                if (pacUp())
                {

                }
                else
                {
                    if (Canvas.GetTop(Pacman) < 15)
                    {
                        up = false;
                        previousTop = false;
                    }
                    else
                    {
                        Canvas.SetTop(Pacman, Canvas.GetTop(Pacman) - 5);
                        pacUp();
                    }
                }
                //   Height = "540" Width = "560"
                //stops packman if he gets close to the border(x value) left of the map

                if (p3 && !p1 && !p2)
                {
                    Pacman.Source = new BitmapImage(new Uri(Pacman.BaseUri, pu1Path));
                    p1 = true;
                    p3 = false;
                }
                else if (p1 && !p2 && !p3)
                {
                    Pacman.Source = new BitmapImage(new Uri(Pacman.BaseUri, pu2Path));
                    p1 = false;
                    p2 = true;
                }
                else if (p2 && !p1 && !p3)
                {
                    Pacman.Source = new BitmapImage(new Uri(Pacman.BaseUri, pPath));
                    p3 = true;
                    p2 = false;
                }
            }
            else if (down || PreviousBottom)
            {
                if (Canvas.GetTop(Pacman) + Pacman.Height <= 531)
                    if (pacdown())
                    {
                        down = false;
                    }
                    else
                    {
                        Canvas.SetTop(Pacman, Canvas.GetTop(Pacman) + 5);
                        previousTop = false;
                        PreviousBottom = false;
                        PreviousLeft = false;
                        previousRight = false;
                        pacdown();

                    }
                //   Height = "540" Width = "560"
                //stops packman if he gets close to the border(x value) left of the map

                if (p3 && !p1 && !p2)
                {
                    Pacman.Source = new BitmapImage(new Uri(Pacman.BaseUri, pd1Path));
                    p1 = true;
                    p3 = false;
                }
                else if (p1 && !p2 && !p3)
                {
                    Pacman.Source = new BitmapImage(new Uri(Pacman.BaseUri, pd2Path));
                    p1 = false;
                    p2 = true;
                }
                else if (p2 && !p1 && !p3)
                {
                    Pacman.Source = new BitmapImage(new Uri(Pacman.BaseUri, pPath));
                    p3 = true;
                    p2 = false;
                }
            }
        }

        //Pacman image changed every millisecond based on its bool status to create animation
        public async Task DoSomethingEveryTenSeconds()
        {
            while (true)
            {
                await Task.Delay(100);
            }
        } //commit test

        public bool PacLeft()
        {
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double left = Canvas.GetLeft(ListOfRectangles[i]);
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                double pacright = Canvas.GetLeft(Pacman) + Pacman.Width;
                double pacleft = Canvas.GetLeft(Pacman);
                double pactop = Canvas.GetTop(Pacman);
                double pacbot = Canvas.GetTop(Pacman) + Pacman.Height;
                if (((pactop >= top && pactop <= bottom) || (pacbot <= bottom && pacbot >= top)))
                {
                    if ((pacleft <= right && pacright >= right))
                    {
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                        mySolidColorBrush.Color = Color.FromArgb(0, 255, 255, 100);
                     //   ListOfRectangles[i].Fill = mySolidColorBrush;
                        Canvas.SetLeft(Pacman, right + 1);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool cyanLeft()
        {
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double left = Canvas.GetLeft(ListOfRectangles[i]);
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                double pacright = Canvas.GetLeft(Cyan) + Cyan.Width;
                double pacleft = Canvas.GetLeft(Cyan);
                double pactop = Canvas.GetTop(Cyan);
                double pacbot = Canvas.GetTop(Cyan) + Cyan.Height;
                if (((pactop >= top && pactop <= bottom) || (pacbot <= bottom && pacbot >= top)))
                {
                    if ((pacleft <= right && pacright >= right))
                    {
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                        mySolidColorBrush.Color = Color.FromArgb(0, 255, 255, 100);
                       // ListOfRectangles[i].Fill = mySolidColorBrush;
                        Canvas.SetLeft(Cyan, right + 1);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool cyanRight()
        {
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double pacright = Canvas.GetLeft(Cyan) + Cyan.Width;
                double pacleft = Canvas.GetLeft(Cyan);
                double pactop = Canvas.GetTop(Cyan);
                double pacbot = Canvas.GetTop(Cyan) + Cyan.Height;
                ListOfRectangles[i].StrokeThickness = 1;
                double left = Canvas.GetLeft(ListOfRectangles[i]);
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                if ((pactop >= top && pactop <= bottom) || (pacbot <= bottom && pacbot >= top))
                {
                    if (pacright >= left && pacright <= right)
                    {
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                        mySolidColorBrush.Color = Color.FromArgb(0, 255, 255, 0);
                      //  ListOfRectangles[i].Fill = mySolidColorBrush;
                        Canvas.SetLeft(Cyan, left - Cyan.Width - 2);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PacRight()
        {
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double pacright = Canvas.GetLeft(Pacman) + Pacman.Width;
                double pacleft = Canvas.GetLeft(Pacman);
                double pactop = Canvas.GetTop(Pacman);
                double pacbot = Canvas.GetTop(Pacman) + Pacman.Height;
                ListOfRectangles[i].StrokeThickness = 1;
                double left = Canvas.GetLeft(ListOfRectangles[i]);
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                if ((pactop >= top && pactop <= bottom) || (pacbot <= bottom && pacbot >= top))
                {
                    if (pacright >= left && pacright <= right)
                    {
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                        mySolidColorBrush.Color = Color.FromArgb(0, 255, 255, 0);
                      //  ListOfRectangles[i].Fill = mySolidColorBrush;
                        Canvas.SetLeft(Pacman, left - Pacman.Width - 2);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool pacdown()
        {
            double pacright = Canvas.GetLeft(Pacman) + Pacman.Width;
            double pacleft = Canvas.GetLeft(Pacman);
            double pactop = Canvas.GetTop(Pacman);
            double pacbot = Canvas.GetTop(Pacman) + Pacman.Height;
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double left = Canvas.GetLeft(ListOfRectangles[i]) - Pacman.Width;
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width + Pacman.Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
             //   ListOfRectangles[i].Fill = mySolidColorBrush;

                if ((Canvas.GetLeft(Pacman) >= left && (Canvas.GetLeft(Pacman) + Pacman.Width) <= right))
                {
                    if (pacbot >= top && pactop < top)
                    {
                        Canvas.SetTop(Pacman, top - Pacman.Height - 2);
                        return true;
                    }
                }
            }

            // up = true;
            return false;
        }

        public bool cyandown()
        {
            double pacright = Canvas.GetLeft(Cyan) + Cyan.Width;
            double pacleft = Canvas.GetLeft(Cyan);
            double pactop = Canvas.GetTop(Cyan);
            double pacbot = Canvas.GetTop(Cyan) + Cyan.Height;
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double left = Canvas.GetLeft(ListOfRectangles[i]) - Cyan.Width;
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width + Cyan.Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
             //   ListOfRectangles[i].Fill = mySolidColorBrush;

                if ((Canvas.GetLeft(Cyan) >= left && (Canvas.GetLeft(Cyan) + Cyan.Width) <= right))
                {
                    if (pacbot >= top && pactop < top)
                    {
                        Canvas.SetTop(Cyan, top - Cyan.Height - 2);
                        return true;

                    }
                }
            }
            // up = true;
            return false;
        }

        public bool pacUp()
        {
            //  a.wi
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double left = Canvas.GetLeft(ListOfRectangles[i]) - Pacman.Width;
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width + Pacman.Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
              //  ListOfRectangles[i].Fill = mySolidColorBrush;

                if ((Canvas.GetTop(Pacman) <= bottom + 1 && Canvas.GetTop(Pacman) >= top) && (Canvas.GetLeft(Pacman) >= left && (Canvas.GetLeft(Pacman) + Pacman.Width) <= right))
                {
                    Canvas.SetTop(Pacman, bottom + 1);
                    return true;
                }
            }
            // up = true;
            return false;
        }

        public bool cyanUp()
        {
            //  a.wi
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double left = Canvas.GetLeft(ListOfRectangles[i]) - Cyan.Width;
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width + Cyan.Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
               // ListOfRectangles[i].Fill = mySolidColorBrush;

                if ((Canvas.GetTop(Cyan) <= bottom + 1 && Canvas.GetTop(Cyan) >= top) && (Canvas.GetLeft(Cyan) >= left && (Canvas.GetLeft(Cyan) + Cyan.Width) <= right))
                {
                    Canvas.SetTop(Cyan, bottom + 1);
                    return true;
                }
            }
            // up = true;
            return false;
        }

        public bool RedUp()
        {
            //  a.wi
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double left = Canvas.GetLeft(ListOfRectangles[i]) - Red.Width;
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width + Red.Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
              //  ListOfRectangles[i].Fill = mySolidColorBrush;

                if ((Canvas.GetTop(Red) <= bottom + 1 && Canvas.GetTop(Red) >= top) && (Canvas.GetLeft(Red) >= left && (Canvas.GetLeft(Red) + Red.Width) <= right))
                {
                    Canvas.SetTop(Red, bottom + 1);
                    return true;
                }
            }
            // up = true;
            return false;
        }

        public bool Reddown()
        {
            double parright = Canvas.GetLeft(Red) + Red.Width;
            double parleft = Canvas.GetLeft(Red);
            double pactop = Canvas.GetTop(Red);
            double pacbot = Canvas.GetTop(Red) + Red.Height;
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double left = Canvas.GetLeft(ListOfRectangles[i]) - Red.Width;
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width + Red.Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
             //   ListOfRectangles[i].Fill = mySolidColorBrush;

                if ((Canvas.GetLeft(Red) >= left && (Canvas.GetLeft(Red) + Red.Width) <= right))
                {
                    if (pacbot >= top && pactop < top)
                    {
                        Canvas.SetTop(Red, top - Red.Height - 2);
                        return true;
                    }
                }
            }
            // up = true;
            return false;
        }
        
        public bool RedRight()
        {
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double parright = Canvas.GetLeft(Red) + Red.Width;
                double parleft = Canvas.GetLeft(Red);
                double pactop = Canvas.GetTop(Red);
                double pacbot = Canvas.GetTop(Red) + Red.Height;
                ListOfRectangles[i].StrokeThickness = 1;
                double left = Canvas.GetLeft(ListOfRectangles[i]);
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                if ((pactop >= top && pactop <= bottom) || (pacbot <= bottom && pacbot >= top))
                {
                    if (parright >= left && parright <= right)
                    {
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                        mySolidColorBrush.Color = Color.FromArgb(0, 255, 255, 0);
                    //    ListOfRectangles[i].Fill = mySolidColorBrush;
                        Canvas.SetLeft(Red, left - Red.Width - 2);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool RedLeft()
        {
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double left = Canvas.GetLeft(ListOfRectangles[i]);
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                double parright = Canvas.GetLeft(Red) + Red.Width;
                double parleft = Canvas.GetLeft(Red);
                double pactop = Canvas.GetTop(Red);
                double pacbot = Canvas.GetTop(Red) + Red.Height;
                if (((pactop >= top && pactop <= bottom) || (pacbot <= bottom && pacbot >= top)))
                {
                    if ((parleft <= right && parright >= right))
                    {
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                        mySolidColorBrush.Color = Color.FromArgb(0, 255, 255, 100);
                      //  ListOfRectangles[i].Fill = mySolidColorBrush;
                        Canvas.SetLeft(Red, right + 1);
                        return true;
                    }
                }
            }
            return false;
        }

        public void RedGhostAI()
        {
            if (rup)
            {
                if (Canvas.GetTop(Red) <= 10)
                {
                    rup = false;
                    //rdown = true;
                    rleft = true;
                    rright = true;
                    //rdown = true;
                    RedUp();
                }
                else
                {
                    if (RedUp())
                    {
                        rup = false;
                        Random rand = new Random();
                        int randNum = rand.Next(1, 3);
                        if (randNum == 1)
                        {
                            rleft = true;
                        }
                        else if (randNum == 2)
                        {
                            rright = true;
                        }
                        //else if(randNum==3)
                        //{
                        //    rdown = true;
                        //}
                    }
                    else
                    {
                        Canvas.SetTop(Red, Canvas.GetTop(Red) - 5);

                        if (RedUp())
                        {
                            rup = false;
                            Random rand = new Random();
                            int randNum = rand.Next(1, 3);
                            if (randNum == 1)
                            {
                                rleft = true;
                            }
                            else if (randNum == 2)
                            {
                                rright = true;
                            }
                            //else if(randNum==3)
                            //{
                            //    rdown = true;
                            //}
                        }
                        //     RedUp();
                    }
                }
            }
            else if (rdown)
            {
                if (Canvas.GetTop(Red) + Red.Height >= 531)
                {
                    // rleft = true;
                    //   rright = true; rleft = true;
                    rdown = false;
                    rup = true;
                }
                else
                {
                    if (Reddown())
                    {
                        rdown = false;
                        Random rand = new Random();
                        int randNum = rand.Next(1, 3);
                        if (randNum == 1)
                        {
                            rleft = true;
                        }
                        if (randNum == 2)
                        {
                            rright = true;
                        }
                    }
                    else
                    {
                        Canvas.SetTop(Red, Canvas.GetTop(Red) + 5);
                        if (Reddown())
                        {
                            rdown = false;
                            Random rand = new Random();
                            int randNum = rand.Next(1, 3);
                            if (randNum == 1)
                            {
                                rleft = true;
                            }
                            if (randNum == 2)
                            {
                                rright = true;
                            }
                        }
                        //  Reddown();
                    }
                }
            }
            else if (rright)
            {
                //    if (Canvas.getrigh)
                if (Canvas.GetLeft(Red) + Red.Width >= 664)
                {
                    rright = false;
                    rleft = true;
                    // rup = true;
                    //1rdown = true;
                }
                else
                {
                    if (RedRight())
                    {
                        rright = false;

                        Random rand = new Random();
                        int randNum = rand.Next(1, 3);
                        if (randNum == 1)
                        {
                            rdown = true;
                        }
                        if (randNum == 2)
                        {
                            rup = true;
                        }
                    }
                    else
                    {
                        Canvas.SetLeft(Red, Canvas.GetLeft(Red) + 5);
                        if (RedRight())
                        {
                            rright = false;

                            Random rand = new Random();
                            int randNum = rand.Next(1, 3);
                            if (randNum == 1)
                            {
                                rdown = true;
                            }
                            if (randNum == 2)
                            {
                                rup = true;
                            }
                        }
                        //    RedLeft();
                    }
                }
            }
            else if (rleft)
            {
                if (Canvas.GetLeft(Red) <=10)
                {
                    rright = true;
                    rleft = false;
                }
                if (RedLeft())
                {
                    rleft = false;
                    Random rand = new Random();
                    int randNum = rand.Next(1, 3);
                    if (randNum == 1)
                    {
                        rdown = true;
                    }
                    if (randNum == 2)
                    {
                        rup = true;
                    }
                }
                else
                {
                    Canvas.SetLeft(Red, Canvas.GetLeft(Red) - 5);
                    if (RedLeft())
                    {
                        rleft = false;
                        Random rand = new Random();
                        int randNum = rand.Next(1, 3);
                        if (randNum == 1)
                        {
                            rdown = true;
                        }
                        if (randNum == 2)
                        {
                            rup = true;
                        }
                    }
                }
            }
        }

        private void MovePacman()
        {
            controller = Gamepad.Gamepads.FirstOrDefault();
            if (controller != null)
            {
                var control = controller.GetCurrentReading();

                if (control.Buttons == GamepadButtons.DPadRight)
                {
                    right = true;
                    left = false;
                    up = false;
                    down = false;
                    p3 = true;
                    p1 = false;
                    p2 = false;
                    previousTop = false;
                    previousRight = false;
                    PreviousBottom = false;
                    PreviousLeft = false;
                }
                else if (control.Buttons == GamepadButtons.DPadLeft)
                {
                    left = true;
                    right = false;
                    up = false;
                    down = false;
                    p3 = true;
                    p1 = false;
                    p2 = false;
                    previousTop = false;
                    previousRight = false;
                    PreviousBottom = false;
                    PreviousLeft = true;
                }
                else if (control.Buttons == GamepadButtons.DPadUp)
                {
                    up = true;
                    right = false;
                    left = false;
                    down = false;
                    p3 = true;
                    p1 = false;
                    p2 = false;
                    previousTop = false;
                    previousRight = false;
                    PreviousBottom = false;
                    PreviousLeft = false;
                }
                else if (control.Buttons == GamepadButtons.DPadDown)
                {
                    down = true;
                    right = false;
                    left = false;
                    up = false;
                    p3 = true;
                    p1 = false;
                    p2 = false;
                    previousTop = false;
                    previousRight = false;
                    PreviousBottom = false;
                    PreviousLeft = false;
                }
            }
        }

        //Decides what to do based on user keypress
        private void HandleKeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (args.VirtualKey == Windows.System.VirtualKey.Right || args.VirtualKey == Windows.System.VirtualKey.GamepadDPadRight || args.VirtualKey == Windows.System.VirtualKey.GamepadLeftThumbstickRight)
            {
                right = true;
                left = false;
                up = false;
                down = false;
                p3 = true;
                p1 = false;
                p2 = false;
                previousTop = false;
                previousRight = false;
                PreviousBottom = false;
                PreviousLeft = false;
            }
            else if (args.VirtualKey == Windows.System.VirtualKey.Left || args.VirtualKey == Windows.System.VirtualKey.GamepadDPadLeft || args.VirtualKey == Windows.System.VirtualKey.GamepadLeftThumbstickLeft)
            {
                left = true;
                right = false;
                up = false;
                down = false;
                p3 = true;
                p1 = false;
                p2 = false;
                previousTop = false;
                previousRight = false;
                PreviousBottom = false;
                PreviousLeft = true;
            }
            else if (args.VirtualKey == Windows.System.VirtualKey.Up || args.VirtualKey == Windows.System.VirtualKey.GamepadDPadUp || args.VirtualKey == Windows.System.VirtualKey.GamepadLeftThumbstickUp)
            {
                up = true;
                right = false;
                left = false;
                down = false;
                p3 = true;
                p1 = false;
                p2 = false;
                previousTop = false;
                previousRight = false;
                PreviousBottom = false;
                PreviousLeft = false;
            }
            else if (args.VirtualKey == Windows.System.VirtualKey.Down || args.VirtualKey == Windows.System.VirtualKey.GamepadDPadDown || args.VirtualKey == Windows.System.VirtualKey.GamepadLeftThumbstickDown)
            {
                down = true;
                right = false;
                left = false;
                up = false;
                p3 = true;
                p1 = false;
                p2 = false;
                previousTop = false;
                previousRight = false;
                PreviousBottom = false;
                PreviousLeft = false;
            }
        }

        //Override for handlekeydown function
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            Window.Current.CoreWindow.KeyDown -= HandleKeyDown;
        }

        //Override for handlekeydown function
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Window.Current.CoreWindow.KeyDown += HandleKeyDown;
        }

        public bool OrangeUp()
        {
            //  a.wi
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double left = Canvas.GetLeft(ListOfRectangles[i]) - Orange.Width;
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width + Orange.Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
               // ListOfRectangles[i].Fill = mySolidColorBrush;

                if ((Canvas.GetTop(Orange) <= bottom + 1 && Canvas.GetTop(Orange) >= top) && (Canvas.GetLeft(Orange) >= left && (Canvas.GetLeft(Orange) + Orange.Width) <= right))
                {
                    Canvas.SetTop(Orange, bottom + 1);
                    return true;
                }
            }
            // up = true;
            return false;
        }

        public bool Orangedown()
        {
            double paoright = Canvas.GetLeft(Orange) + Orange.Width;
            double paoleft = Canvas.GetLeft(Orange);
            double pactop = Canvas.GetTop(Orange);
            double pacbot = Canvas.GetTop(Orange) + Orange.Height;
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double left = Canvas.GetLeft(ListOfRectangles[i]) - Orange.Width;
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width + Orange.Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
             //   ListOfRectangles[i].Fill = mySolidColorBrush;

                if ((Canvas.GetLeft(Orange) >= left && (Canvas.GetLeft(Orange) + Orange.Width) <= right))
                {
                    if (pacbot >= top && pactop < top)
                    {
                        Canvas.SetTop(Orange, top - Orange.Height - 2);
                        return true;
                    }
                }
            }
            // up = true;
            return false;
        }

        public bool OrangeRight()
        {
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double paoright = Canvas.GetLeft(Orange) + Orange.Width;
                double paoleft = Canvas.GetLeft(Orange);
                double pactop = Canvas.GetTop(Orange);
                double pacbot = Canvas.GetTop(Orange) + Orange.Height;
                ListOfRectangles[i].StrokeThickness = 1;
                double left = Canvas.GetLeft(ListOfRectangles[i]);
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                if ((pactop >= top && pactop <= bottom) || (pacbot <= bottom && pacbot >= top))
                {
                    if (paoright >= left && paoright <= right)
                    {
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                        mySolidColorBrush.Color = Color.FromArgb(0, 255, 255, 0);
                //        ListOfRectangles[i].Fill = mySolidColorBrush;
                        Canvas.SetLeft(Orange, left - Orange.Width - 2);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool OrangeLeft()
        {
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double left = Canvas.GetLeft(ListOfRectangles[i]);
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                double paoright = Canvas.GetLeft(Orange) + Orange.Width;
                double paoleft = Canvas.GetLeft(Orange);
                double pactop = Canvas.GetTop(Orange);
                double pacbot = Canvas.GetTop(Orange) + Orange.Height;
                if (((pactop >= top && pactop <= bottom) || (pacbot <= bottom && pacbot >= top)))
                {
                    if ((paoleft <= right && paoright >= right))
                    {
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                        mySolidColorBrush.Color = Color.FromArgb(0, 255, 255, 100);
                    //    ListOfRectangles[i].Fill = mySolidColorBrush;
                        Canvas.SetLeft(Orange, right + 1);
                        return true;
                    }
                }
            }
            return false;
        }

        public void OrangeGhostAI()
        {

            if (oup)
            {
                if (Canvas.GetTop(Orange) <= 10)
                {
                    oup = false;
                    //odown = true;
                    oleft = true;
                    oright = true;
                    //odown = true;
                    OrangeUp();
                }
                else
                {
                    if (OrangeUp())
                    {
                        oup = false;
                        Random rand = new Random();
                        int randNum = rand.Next(1, 3);
                        if (randNum == 1)
                        {
                            oleft = true;
                        }
                        else if (randNum == 2)
                        {
                            oright = true;
                        }
                        //else if(randNum==3)
                        //{
                        //    odown = true;
                        //}
                    }
                    else
                    {
                        Canvas.SetTop(Orange, Canvas.GetTop(Orange) - 5);
                          if ( OrangeUp())
                        {
                            oup = false;
                            Random rand = new Random();
                            int randNum = rand.Next(1, 3);
                            if (randNum == 1)
                            {
                                oleft = true;
                            }
                            else if (randNum == 2)
                            {
                                oright = true;
                            }
                        }
                    }
                }
            }
            else if (odown)
            {
                if (Canvas.GetTop(Orange) + Orange.Height >= 531)
                {
                    // oleft = true;
                    //   oright = true; oleft = true;
                    odown = false;
                    oup = true;
                }
                else
                {
                    if (Orangedown())
                    {
                        odown = false;
                        Random rand = new Random();
                        int randNum = rand.Next(1, 3);
                        if (randNum == 1)
                        {
                            oleft = true;
                        }
                        if (randNum == 2)
                        {
                            oright = true;
                        }
                    }
                    else
                    {
                        Canvas.SetTop(Orange, Canvas.GetTop(Orange) + 5);
                          if (Orangedown())
                        {
                            odown = false;
                            Random rand = new Random();
                            int randNum = rand.Next(1, 3);
                            if (randNum == 1)
                            {
                                oleft = true;
                            }
                            if (randNum == 2)
                            {
                                oright = true;
                            }
                        }
                    }
                }
            }
            else if (oright)
            {
                //    if (Canvas.getrigh)
                if (Canvas.GetLeft(Orange) + Orange.Width >= 664)
                {
                    oright = false;
                    oleft = true;
                    // oup = true;
                    //1odown = true;
                }
                else
                {
                    if (OrangeRight())
                    {
                        oright = false;

                        Random rand = new Random();
                        int randNum = rand.Next(1, 3);
                        if (randNum == 1)
                        {
                            odown = true;
                        }
                        if (randNum == 2)
                        {
                            oup = true;
                        }
                    }
                    else
                    {
                        Canvas.SetLeft(Orange, Canvas.GetLeft(Orange) + 5);
                       
                         if ( OrangeRight())
                        {
                            oright = false;
                            Random rand = new Random();
                            int randNum = rand.Next(1, 3);
                            if (randNum == 1)
                            {
                                odown = true;
                            }
                            if (randNum == 2)
                            {
                                oup = true;
                            }
                        }
                    }
                }
            }
            else if (oleft)
            {
                if (Canvas.GetLeft(Orange) <= 10)
                {
                    oright = true;
                    oleft = false;
                }
                if (OrangeLeft())
                {
                    oleft = false;
                    Random rand = new Random();
                    int randNum = rand.Next(1, 3);
                    if (randNum == 1)
                    {
                        odown = true;
                    }
                    if (randNum == 2)
                    {
                        oup = true;
                    }
                }
                else
                {
                    Canvas.SetLeft(Orange, Canvas.GetLeft(Orange) - 5);
                     if (OrangeLeft())
                    {
                        oleft = false;
                        Random rand = new Random();
                        int randNum = rand.Next(1, 3);
                        if (randNum == 1)
                        {
                            odown = true;
                        }
                        if (randNum == 2)
                        {
                            oup = true;
                        }
                    }
                }
            }
        }

        public bool PinkUp()
        {
            //  a.wi
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double left = Canvas.GetLeft(ListOfRectangles[i]) - Pink.Width;
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width + Pink.Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
              //  ListOfRectangles[i].Fill = mySolidColorBrush;

                if ((Canvas.GetTop(Pink) <= bottom + 1 && Canvas.GetTop(Pink) >= top) && (Canvas.GetLeft(Pink) >= left && (Canvas.GetLeft(Pink) + Pink.Width) <= right))
                {
                    Canvas.SetTop(Pink, bottom + 1);
                    return true;
                }
            }
            // up = true;
            return false;
        }

        public bool Pinkdown()
        {
            double papright = Canvas.GetLeft(Pink) + Pink.Width;
            double papleft = Canvas.GetLeft(Pink);
            double pactop = Canvas.GetTop(Pink);
            double pacbot = Canvas.GetTop(Pink) + Pink.Height;
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double left = Canvas.GetLeft(ListOfRectangles[i]) - Pink.Width;
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width + Pink.Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0);
           //     ListOfRectangles[i].Fill = mySolidColorBrush;

                if ((Canvas.GetLeft(Pink) >= left && (Canvas.GetLeft(Pink) + Pink.Width) <= right))
                {
                    if (pacbot >= top && pactop < top)
                    {
                        Canvas.SetTop(Pink, top - Pink.Height - 2);
                        return true;
                    }
                }
            }
            // up = true;
            return false;
        }

        public bool PinkRight()
        {
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double papright = Canvas.GetLeft(Pink) + Pink.Width;
                double papleft = Canvas.GetLeft(Pink);
                double pactop = Canvas.GetTop(Pink);
                double pacbot = Canvas.GetTop(Pink) + Pink.Height;
                ListOfRectangles[i].StrokeThickness = 1;
                double left = Canvas.GetLeft(ListOfRectangles[i]);
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                if ((pactop >= top && pactop <= bottom) || (pacbot <= bottom && pacbot >= top))
                {
                    if (papright >= left && papright <= right)
                    {
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                        mySolidColorBrush.Color = Color.FromArgb(0, 255, 255, 0);
                     //   ListOfRectangles[i].Fill = mySolidColorBrush;
                        Canvas.SetLeft(Pink, left - Pink.Width - 2);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PinkLeft()
        {
            for (int i = 0; i < ListOfRectangles.Count; i++)
            {
                double left = Canvas.GetLeft(ListOfRectangles[i]);
                double right = Canvas.GetLeft(ListOfRectangles[i]) + ListOfRectangles[i].Width;
                double top = Canvas.GetTop(ListOfRectangles[i]);
                double bottom = Canvas.GetTop(ListOfRectangles[i]) + ListOfRectangles[i].Height;
                double papright = Canvas.GetLeft(Pink) + Pink.Width;
                double papleft = Canvas.GetLeft(Pink);
                double pactop = Canvas.GetTop(Pink);
                double pacbot = Canvas.GetTop(Pink) + Pink.Height;
                if (((pactop >= top && pactop <= bottom) || (pacbot <= bottom && pacbot >= top)))
                {
                    if ((papleft <= right && papright >= right))
                    {
                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                        mySolidColorBrush.Color = Color.FromArgb(0, 255, 255, 100);
                     //   ListOfRectangles[i].Fill = mySolidColorBrush;
                        Canvas.SetLeft(Pink, right + 1);
                        return true;
                    }
                }
            }
            return false;
        }
        public void PinkGhostAI()
        {
            if (pup)
            {
                if (Canvas.GetTop(Pink) <= 10)
                {
                    pup = false;
                    //pdown = true;
                    pleft = true;
                    pright = true;
                    //pdown = true;
                    PinkUp();
                }
                else
                {
                    if (PinkUp())
                    {
                        pup = false;
                        Random rand = new Random();
                        int randNum = rand.Next(1, 3);
                        if (randNum == 1)
                        {
                            pleft = true;
                        }
                        else if (randNum == 2)
                        {
                            pright = true;
                        }
                        //else if(randNum==3)
                        //{
                        //    pdown = true;
                        //}
                    }
                    else
                    {
                        Canvas.SetTop(Pink, Canvas.GetTop(Pink) - 5);
                       if (PinkUp())
                        {
                            pup = false;
                            Random rand = new Random();
                            int randNum = rand.Next(1, 3);
                            if (randNum == 1)
                            {
                                pleft = true;
                            }
                            else if (randNum == 2)
                            {
                                pright = true;
                            }
                        }
                    }
                }
            }
            else if (pdown)
            {
                if (Canvas.GetTop(Pink) + Pink.Height >= 531)
                {
                    // pleft = true;
                    //   pright = true; pleft = true;
                    pdown = false;
                    pup = true;
                }
                else
                {
                    if (Pinkdown())
                    {
                        pdown = false;
                        Random rand = new Random();
                        int randNum = rand.Next(1, 3);
                        if (randNum == 1)
                        {
                            pleft = true;
                        }
                        if (randNum == 2)
                        {
                            pright = true;
                        }
                    }
                    else
                    {
                        Canvas.SetTop(Pink, Canvas.GetTop(Pink) + 5);
                      if( Pinkdown())
                        {
                            pdown = false;
                            Random rand = new Random();
                            int randNum = rand.Next(1, 3);
                            if (randNum == 1)
                            {
                                pleft = true;
                            }
                            if (randNum == 2)
                            {
                                pright = true;
                            }
                        }
                    }
                }
            }
            else if (pright)
            {
                //    if (Canvas.getrigh)
                if (Canvas.GetLeft(Pink) + Pink.Width >= 664)
                {
                    pright = false;
                    pleft = true;
                    // pup = true;
                    //1pdown = true;
                }
                else
                {
                    if (PinkRight())
                    {
                        pright = false;

                        Random rand = new Random();
                        int randNum = rand.Next(1, 3);
                        if (randNum == 1)
                        {
                            pdown = true;
                        }
                        if (randNum == 2)
                        {
                            pup = true;
                        }
                    }
                    else
                    {
                        Canvas.SetLeft(Pink, Canvas.GetLeft(Pink) + 5);
                        if (PinkRight())
                        {
                            pright = false;

                            Random rand = new Random();
                            int randNum = rand.Next(1, 3);
                            if (randNum == 1)
                            {
                                pdown = true;
                            }
                            if (randNum == 2)
                            {
                                pup = true;
                            }
                        }
                    }
                }
            }
            else if (pleft)
            {
                if (Canvas.GetLeft(Pink) <= 10)
                {
                    pright = true;
                    pleft = false;
                }
                if (PinkLeft())
                {
                    pleft = false;
                    Random rand = new Random();
                    int randNum = rand.Next(1, 3);
                    if (randNum == 1)
                    {
                        pdown = true;
                    }
                    if (randNum == 2)
                    {
                        pup = true;
                    }
                }
                else
                {
                    Canvas.SetLeft(Pink, Canvas.GetLeft(Pink) - 5);
                    if (PinkLeft())
                    {
                        pleft = false;
                        Random rand = new Random();
                        int randNum = rand.Next(1, 3);
                        if (randNum == 1)
                        {
                            pdown = true;
                        }
                        if (randNum == 2)
                        {
                            pup = true;
                        }
                    }
                }
            }
        }
    }
}











