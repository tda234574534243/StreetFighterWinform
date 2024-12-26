using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace StreetFighterGame.GameEngine
{
    internal class AnimationManager
    {
        public List<Image> Images {  get; set; }
        public List<Image> mele {  get; set; }
        public List<Image> defense { get; set; }

        private int currentFrame;
        private int PositionX, PositionY;

        private float ScaleX = 1;
        private float ScaleY = 1;
        //private PaintEventArgs currentGraphics;
        private Control renderControl;

        private Timer DrawTimer;

        public AnimationManager()
        {
            mele = LoadImages(".\\Effect", 7, "VaCham_2");
            defense = LoadImages(".\\Effect", 5, "Ryu_7108");
            DrawTimer = new Timer { Interval = 32 };
            DrawTimer.Tick += OnMeleTimerTick;
        }
        private void OnMeleTimerTick(object sender, EventArgs e)
        {
            if (Images.Count == 0 || renderControl == null) return;

            // Yêu cầu vẽ lại control
            renderControl.Invalidate(new Rectangle(PositionX, PositionY, (int)(Images[currentFrame].Width * ScaleX), (int)(Images[currentFrame].Height * ScaleY)));

            if (currentFrame == Images.Count - 1) DrawTimer.Stop();
            // Chuyển sang frame tiếp theo
            currentFrame = (currentFrame + 1) % Images.Count;
        }
        public void DrawMele(Control control, int positionX, int positionY, float scaleX = 1, float scaleY = 1)
        {
            SetUp(control, positionX, positionY, scaleX, scaleY);
            Images = mele; 
            // Bắt đầu Timer
            DrawTimer.Start();
        }
        public void DrawDefense(Control control, int positionX, int positionY, float scaleX = 1, float scaleY = 1)
        {
            SetUp(control, positionX, positionY, scaleX, scaleX);
            Images = defense;
            // Bắt đầu Timer
            DrawTimer.Start();
        }
        private void SetUp(Control control, int positionX, int positionY, float scaleX = 1, float scaleY = 1)
        {
            renderControl = control;

            // Cập nhật vị trí vẽ
            //Console.WriteLine($"mele:  {positionX}");
            PositionX = positionX;
            PositionY = positionY;

            ScaleX = scaleX;
            ScaleY = scaleY;
        }
        public void DrawImage(Graphics g)
        {
            if (Images.Count > 0)
            {
                // Vẽ frame hiện tại
                if (currentFrame > Images.Count - 1) currentFrame = 0;
                g.DrawImage(Images[currentFrame], PositionX, PositionY, (int)(Images[currentFrame].Width * ScaleX), (int)(Images[currentFrame].Height * ScaleY));
            }
        }
        private List<Image> LoadImages(string folderPath, int count, string filePrefix)
        {
            var images = new List<Image>();
            for (int i = 0; i < count; i++)
            {
                string filePath = Path.Combine(folderPath, $"{filePrefix}-{i}.png");
                if (File.Exists(filePath))
                {
                    images.Add(Image.FromFile(filePath));
                }
            }
            return images;
        }
    }
}
