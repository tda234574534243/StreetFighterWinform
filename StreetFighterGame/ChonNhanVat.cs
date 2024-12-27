using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StreetFighterGame.QuanLiTaikhoan;

namespace StreetFighterGame
{
    public partial class ChonNhanVat : Form
    {
        private string tenNhanVatDuocChon1, tenNhanVatDuocChon2;
        private int idChar1, idChar2;
        private int soLanChonNhanVat;
        private Dictionary<string, Image> characterImages;
        private List<string> mapPaths;
        private int currentInDexMapSelect;
        private Dictionary<string, List<Image>> characterAnimations;
        private Timer animationTimer;
        private int currentFrameChar1, currentFrameChar2;
        public bool isVsAI; // Biến để kiểm tra chế độ đấu với máy
        public ChonNhanVat(bool isVsAI)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.isVsAI = isVsAI;

            buttonFight.Visible = true;
            labelName.Visible = false;
            pictureBoxMap.Visible = false;
            buttonNextMap.Visible = buttonPreMap.Visible = false;
            this.Controls.Add(pictureBoxMap);

            int x = (this.ClientSize.Width - pictureBoxMap.Width) / 2;
            int y = (this.ClientSize.Height - pictureBoxMap.Height) / 2;
            pictureBoxMap.Location = new Point(x, y);
            
            LoadMapInFo();
            InitializeCharacterImages();
        }
        private void LoadMapInFo()
        {
            currentInDexMapSelect = 0;
            string mapFolder = @".\Map"; // Đường dẫn thư mục chứa các bản đồ
            // Lấy tất cả các file PNG trong thư mục Map
            mapPaths = Directory.GetFiles(mapFolder, "*.png").ToList();
        }
        private void InitializeCharacterImages()
        {
            characterAnimations = new Dictionary<string, List<Image>>();
            characterImages = new Dictionary<string, Image>(); // Khởi tạo dictionary

            // Nạp hình ảnh tĩnh cho từng nhân vật
            characterImages["chunli"] = Image.FromFile(".\\Chunli\\ChunLi_0-0.png");
            characterImages["goku"] = Image.FromFile(".\\GokuMUI\\GokuMUI_0-0.png");
            characterImages["ryu"] = Image.FromFile(".\\Ryu\\Ryu_0-0.png");
            characterImages["vegeto"] = Image.FromFile(".\\Vegeto\\Vegeto_0-0.png");
            characterImages["zenitsu"] = Image.FromFile(".\\Zenitsu\\Zenitsu_0-1.png");
            characterImages["gojo"] = Image.FromFile(".\\Gojo\\Gojo_0-0.png");

            // Load các khung hình cho từng nhân vật
            characterAnimations["chunli"] = LoadAnimationFrames(".\\Chunli\\", "ChunLi_0-{0}.png", 4);
            characterAnimations["goku"] = LoadAnimationFrames(".\\GokuMUI\\", "GokuMUI_0-{0}.png", 3);
            characterAnimations["ryu"] = LoadAnimationFrames(".\\Ryu\\", "Ryu_0-{0}.png", 5);
            characterAnimations["vegeto"] = LoadAnimationFrames(".\\Vegeto\\", "Vegeto_0-{0}.png", 6);
            characterAnimations["zenitsu"] = LoadAnimationFrames(".\\Zenitsu\\", "Zenitsu_0-{0}.png", 5);
            characterAnimations["gojo"] = LoadAnimationFrames(".\\Gojo\\", "Gojo_0-{0}.png", 3);

            pictureBoxChar1.Visible = pictureBoxChar2.Visible = false;

            // Initialize Timer
            animationTimer = new Timer();
            animationTimer.Interval = 100; // Đổi ảnh mỗi 100ms
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        // Hàm hỗ trợ để load các khung hình
        private List<Image> LoadAnimationFrames(string folderPath, string filePattern, int frameCount)
        {
            var frames = new List<Image>();
            for (int i = 0; i < frameCount; i++)
            {
                string filePath = Path.Combine(folderPath, string.Format(filePattern, i));
                if (File.Exists(filePath))
                {
                    frames.Add(Image.FromFile(filePath));
                }
            }
            return frames;
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tenNhanVatDuocChon1) && characterAnimations.ContainsKey(tenNhanVatDuocChon1.ToLower()))
            {
                var frames = characterAnimations[tenNhanVatDuocChon1.ToLower()];
                pictureBoxChar1.BackgroundImage = frames[currentFrameChar1];
                currentFrameChar1 = (currentFrameChar1 + 1) % frames.Count; // Lặp lại khung hình
            }

            if (!string.IsNullOrEmpty(tenNhanVatDuocChon2) && characterAnimations.ContainsKey(tenNhanVatDuocChon2.ToLower()))
            {
                var frames = characterAnimations[tenNhanVatDuocChon2.ToLower()];
                pictureBoxChar2.BackgroundImage = frames[currentFrameChar2];
                currentFrameChar2 = (currentFrameChar2 + 1) % frames.Count; // Lặp lại khung hình
            }
        }

        public void buttonFight_Click(object sender, EventArgs e)
        {
            // Kiểm tra chế độ đấu với AI (thực ra cái chọn này tượng tự như khi chơi 2 người nhưng mà tui gắn cờ là AI để dễ truyển vào gameplay main:VVVVVVV)
            if (isVsAI)
            {
                // Nếu chế độ không phải đấu với AI, tiếp tục xử lý như bình thường
                if (soLanChonNhanVat == 1)
                {
                    buttonFight.Text = "Start"; // Đổi chữ của button khi đã chọn đủ 2 nhân vật
                    panel1.Visible = false;
                    labelName.Visible = false;
                    pictureBoxMap.Visible = true;
                    buttonNextMap.Visible = buttonPreMap.Visible = true;
                    ShowCurrentInDexMapOnPictureBox();
                }
                else if (soLanChonNhanVat == 2)
                {
                    // Bắt đầu trận đấu sau khi chọn đủ 2 nhân vật
                    StreetFighterGame game = new StreetFighterGame(tenNhanVatDuocChon1, tenNhanVatDuocChon2, idChar1, idChar2, mapPaths[currentInDexMapSelect], true);
                    game.Show(); // Mở game
                    this.Close(); // Đóng form hiện tại
                }

                // Ẩn/Hiện các control khi chọn nhân vật
                if (soLanChonNhanVat < 1) buttonFight.Visible = false;  // Ẩn nút Fight khi người chơi đã chọn xong
                else buttonFight.Visible = true;
                labelName.Visible = false;
                // Tăng số lần chọn nhân vật để chuyển sang bước tiếp theo
                soLanChonNhanVat++;
            }
            else
            {
                // Nếu chế độ không phải đấu với AI, tiếp tục xử lý như bình thường
                if (soLanChonNhanVat == 1)
                {
                    buttonFight.Text = "Start"; // Đổi chữ của button khi đã chọn đủ 2 nhân vật
                    panel1.Visible = false;
                    labelName.Visible = false;

                    pictureBoxMap.Visible = true;
                    buttonNextMap.Visible = buttonPreMap.Visible = true;
                    ShowCurrentInDexMapOnPictureBox();
                }
                else if (soLanChonNhanVat == 2)
                {
                    // Bắt đầu trận đấu sau khi chọn đủ 2 nhân vật
                    StreetFighterGame game = new StreetFighterGame(tenNhanVatDuocChon1, tenNhanVatDuocChon2, idChar1, idChar2, mapPaths[currentInDexMapSelect], false);
                    game.Show(); // Mở game
                    this.Close(); // Đóng form hiện tại
                }

                // Ẩn/Hiện các control khi chọn nhân vật
                if (soLanChonNhanVat < 1) buttonFight.Visible = false;  // Ẩn nút Fight khi người chơi đã chọn xong
                else buttonFight.Visible = true;
                labelName.Visible = false;
                // Tăng số lần chọn nhân vật để chuyển sang bước tiếp theo
                soLanChonNhanVat++;
            }
        }
        private void ShowCurrentInDexMapOnPictureBox()
        {
            if (File.Exists(mapPaths[currentInDexMapSelect]))
            {
                // Tải hình ảnh từ tệp và hiển thị trong PictureBox
                pictureBoxMap.BackgroundImage = Image.FromFile(mapPaths[currentInDexMapSelect]);
            }
            else
            {
                // Thông báo nếu không tìm thấy tệp
                MessageBox.Show("Không tìm thấy tệp hình ảnh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ChonNhanVat_Load(object sender, EventArgs e)
        {
            tenNhanVatDuocChon1 = tenNhanVatDuocChon2 = "";
            soLanChonNhanVat = 0;
        }

        private void HandlePictureBoxClick(string nameNV)
        {

            if (soLanChonNhanVat == 0)
            {
                pictureBoxChar1.Visible = true;
                tenNhanVatDuocChon1 = nameNV;
                currentFrameChar1 = 0;
                idChar1 = QuanLiTaiKhoan.GetNhanVatIdByName(nameNV);
                pictureBoxChar1.BackgroundImage = characterImages[nameNV.ToLower()];
                pictureBoxChar2.Visible = false;
            }
            else if (soLanChonNhanVat == 1)
            {
                pictureBoxChar2.Visible = true;
                tenNhanVatDuocChon2 = nameNV;
                currentFrameChar2 = 0;
                idChar2 = QuanLiTaiKhoan.GetNhanVatIdByName(nameNV);
                pictureBoxChar2.BackgroundImage = characterImages[nameNV.ToLower()];
            }
            labelName.Visible = true;
            labelName.Text = nameNV;
            buttonFight.Visible = true;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            HandlePictureBoxClick("chunli");

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            HandlePictureBoxClick("goku");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            HandlePictureBoxClick("ryu");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            HandlePictureBoxClick("vegeto");
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            HandlePictureBoxClick("gojo");
        }
        private void buttonNextMap_Click(object sender, EventArgs e)
        {
            currentInDexMapSelect++;
            if (currentInDexMapSelect > mapPaths.Count - 1) currentInDexMapSelect = 0;
            ShowCurrentInDexMapOnPictureBox();
        }

        private void buttonPreMap_Click(object sender, EventArgs e)
        {
            currentInDexMapSelect--;
            if (currentInDexMapSelect < 0) currentInDexMapSelect = mapPaths.Count - 1;
            ShowCurrentInDexMapOnPictureBox();
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            FormStart st = new FormStart();
            st.Show();
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            HandlePictureBoxClick("zenitsu");
        }
    }
}
