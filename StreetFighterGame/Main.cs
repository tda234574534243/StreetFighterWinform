using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using StreetFighterGame.GameEngine;
using StreetFighterGame.UI;
using StreetFighterGame.QuanLiTaikhoan;
using StreetFighterGame.Characters;


namespace StreetFighterGame
{
    public partial class StreetFighterGame : Form
    {
        private Timer timer;
        private bool endGame;
        private LogicGame logicGame;
        private Image background;
        private List<string> backgroundImages = new List<string>();
        private int idChar1, idChar2;
        private Timer animationTimer;
        private AnimationManager animationManager = new AnimationManager();
        private int countdownValue = 60;
        private Timer CountdownTimer;
        private Timer CountdownStart;
        private int StartTimer = 3;
        private Font font;

        // Thêm các biến kiểm tra trạng thái các phím của người chơi
        private bool player1MoveLeft, player1MoveRight, player1Jump, player1AttackJ, player1AttackK, player1Dash, player1AttackI;
        private bool player2MoveLeft, player2MoveRight, player2Jump, player2AttackJ, player2AttackK, player2Dash, player2AttackI;
        private bool isVsAI;
        private void labelWiner_Click(object sender, EventArgs e)
        {

        }
        private void InitializeCountdown()
        {
            
            CountdownTimer = new Timer { Interval = 1000 };
            CountdownTimer.Tick += (sender, e) =>
            {
                countdownValue--;
                UpdateLabel();
                if (countdownValue <= 1)
                {
                    labelWiner.Text = "Draw";
                    labelWiner.Visible = true;
                }
                if (countdownValue == 0)
                {   
                   CountdownTimer.Stop();
                   EndGame();
                   QuanLiTaiKhoan.LuuTranDau(
                        idChar1,
                        idChar2,
                        "Draw"
                   );
                }
                labelWiner.ToString();
            };
        }
        private void InitializeCountdownStart()
        {
            CountdownStart = new Timer
            {
                Interval = 1000
            };
            CountdownStart.Tick += (sender, e) =>
            {
                StartTimer--;
                if (StartTimer == -1)
                {
                    Lbstart.Visible = false;
                    logicGame.Player1.isHit = false;
                    logicGame.Player2.isHit = false;
                    CountdownTimer.Start();
                    CountdownStart.Stop();
                    labelWiner.Visible = false;
                }//oke ae
                else if (StartTimer == 0)
                {

                    Lbstart.Text = "Fight";
                    labelWiner.Text = "Fight";
                    labelWiner.Visible = true;
                    Lbstart.Location = new Point(
                         (this.ClientSize.Width - Lbstart.Width) / 2,
                         (this.ClientSize.Height - Lbstart.Height) / 2
                    );
                }
                else
                {
                    Lbstart.Text = StartTimer.ToString();
                    
                }
            };
        }
        private void UpdateLabel()
        {
            // Hiển thị giá trị đếm ngược trên Label
            LbCountdown.Text = countdownValue.ToString();
        }
        public StreetFighterGame(string s1, string s2, int idChar1, int idChar2, string mappath, bool isVsAI)
        {
            InitializeComponent();
            InitializeCountdownStart();
            InitializeCountdown();
            SetUpForm(s1, s2, idChar1, idChar2, mappath);
            this.isVsAI = isVsAI;
            this.StartPosition = FormStartPosition.CenterScreen;

            font = new Font("Times New Roman", 20, FontStyle.Regular);
        }
        private void SetUpForm(string s1, string s2, int _idChar1, int _idChar2, string mappath)
        {
            labelWiner.Visible = false;
            endGame = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);

            // Load background images
            // Kiểm tra đường dẫn bản đồ và tải hình ảnh
            if (!string.IsNullOrEmpty(mappath) && File.Exists(mappath))
            {
                background = Image.FromFile(mappath); // Gán hình ảnh vào biến background
                this.BackgroundImage = background;   // Gán ảnh nền cho Form
                this.BackgroundImageLayout = ImageLayout.Stretch; // Thiết lập chế độ căn chỉnh ảnh
            }


            // Initialize the game logic
            logicGame = new LogicGame(this.ClientSize.Width, this.ClientSize.Height);

            // Initialize the characters for both players
            string player1Choice = s1;  // Default character for player 1
            string player2Choice = s2;  // Default character for player 2
            idChar1 = _idChar1;
            idChar2 = _idChar2;
            logicGame.InitializeCharacterSelection(player1Choice, player2Choice);
            logicGame.Player2.IsFacingLeft = true;
            // Initialize Timer for game animation
            animationTimer = new Timer
            {
                Interval = 10
            };
            animationTimer.Tick += new EventHandler(OnAnimationTick);
            animationTimer.Start();

            // Key events and drawing events
            this.KeyDown += new KeyEventHandler(OnKeyDown);
            this.KeyUp += new KeyEventHandler(OnKeyUp);
            this.Paint += new PaintEventHandler(OnPaint);
            CountdownStart.Start();
        }

        private void OnAnimationTick(object sender, EventArgs e)
        {
            // Update game logic based on input
            if (!endGame/* && logicGame.Player1.CurrentState != ActionState.Standing*/)
            {
                UpdatePlayerInput();

                // Update the game logic (character positions, states)
                logicGame.Update();
            }

            // Redraw the game screen
            this.Invalidate();
        }

        private void StreetFighterGame_Load_1(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Lbstart_Click(object sender, EventArgs e)
        {

        }
        private void EndGame()
        {
           FormStart st = new FormStart();
           st.Show();
           this.Close();
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // Handle key down events for Player 1
            if (e.KeyCode == Keys.A) player1MoveLeft = true;
            if (e.KeyCode == Keys.D)
            {
                player1MoveRight = true;
                if (player1MoveLeft) player1MoveLeft = false;// đảm bảo chỉ có 1 move là true, vì khi 2 true sẽ gặp bug logic vì có 2state được change
            }

            if (e.KeyCode == Keys.W) player1Jump = true;
            if (e.KeyCode == Keys.S) logicGame.Player1.Defend(true);
            if (e.KeyCode == Keys.J) player1AttackJ = true;
            if (e.KeyCode == Keys.K) player1AttackK = true;
            if (e.KeyCode == Keys.L) player1Dash = true;
            if (e.KeyCode == Keys.I) player1AttackI = true;
            

            // Handle key down events for Player 2
            if (e.KeyCode == Keys.Left) player2MoveLeft = true;
            if (e.KeyCode == Keys.Right)
            {
                player2MoveRight = true;
                if (player2MoveLeft) player2MoveLeft = false;
            }
            if (e.KeyCode == Keys.Up) player2Jump = true;
            if (e.KeyCode == Keys.Down) logicGame.Player2.Defend(true);
            if (e.KeyCode == Keys.NumPad1) player2AttackJ = true;
            if (e.KeyCode == Keys.NumPad2) player2AttackK = true;
            if (e.KeyCode == Keys.NumPad3) player2Dash = true;
            if (e.KeyCode == Keys.NumPad5) player2AttackI = true;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            // Handle key up events for Player 1
            if (e.KeyCode == Keys.A) player1MoveLeft = false; logicGame.Player1.StopMoving();
            if (e.KeyCode == Keys.D) player1MoveRight = false; logicGame.Player1.StopMoving();
            if (e.KeyCode == Keys.W) player1Jump = false;
            if (e.KeyCode == Keys.S) logicGame.Player1.Defend(false);
            if (e.KeyCode == Keys.J) player1AttackJ = false;
            if (e.KeyCode == Keys.K) player1AttackK = false;
            if (e.KeyCode == Keys.L) player1Dash = false;
            if (e.KeyCode == Keys.I) player1AttackI = false;
            // Handle key up events for Player 2
            if (e.KeyCode == Keys.Left) player2MoveLeft = false; logicGame.Player2.StopMoving();
            if (e.KeyCode == Keys.Right) player2MoveRight = false; logicGame.Player2.StopMoving();
            if (e.KeyCode == Keys.Up) player2Jump = false;
            if (e.KeyCode == Keys.Down) logicGame.Player2.Defend(false);
            if (e.KeyCode == Keys.NumPad1) player2AttackJ = false;
            if (e.KeyCode == Keys.NumPad2) player2AttackK = false;
            if (e.KeyCode == Keys.NumPad3) player2Dash = false;
            if (e.KeyCode == Keys.NumPad5) player2AttackI = false;
        }

        private void UpdatePlayerInput()
        {
            // Cập nhật di chuyển cho Player 1
            if (!logicGame.Player1.isHit && !logicGame.Player1.DangDanhDungKo() && !logicGame.Player1.isDashing)
            {
                if (player1MoveLeft)
                {
                    if (!logicGame.Player1.IsFacingLeft) logicGame.Player1.PositionX += logicGame.Player1.charWidth;
                    logicGame.Player1.MoveLeft();
                    logicGame.Player1.IsMovingLeft = true;
                    logicGame.Player1.IsFacingLeft = true;
                }

                if (player1MoveRight)
                {
                    if (logicGame.Player1.IsFacingLeft) logicGame.Player1.PositionX -= logicGame.Player1.charWidth;
                    logicGame.Player1.MoveRight();
                    logicGame.Player1.IsMovingLeft = false;
                    logicGame.Player1.IsFacingLeft = false;
                }

                // Chỉ cập nhật hướng đối mặt nếu cả hai người chơi không di chuyển
                if (!player1MoveLeft && !player1MoveRight && !logicGame.Player1.isDashing)
                {
                    if (!logicGame.Player1.IsFacingLeft && logicGame.Player1.PositionX > logicGame.Player2.PositionX)
                    {
                        logicGame.Player1.IsFacingLeft = true;
                        logicGame.Player1.PositionX += logicGame.Player1.charWidth;
                    }
                }

                // Các hành động khác cho Player 1
                if (player1Jump) logicGame.Player1.Jump();
                if (player1AttackJ) logicGame.Player1.Attack(attackType: ActionState.AttackingJ);
                if (player1AttackK) logicGame.Player1.Attack(attackType: ActionState.AttackingK);
                if (player1Dash) logicGame.Player1.Dash();
                if (player1AttackI && logicGame.Player1.Mana >= logicGame.Player1.manaSkillI) logicGame.Player1.SpecicalSkill();
            }

            if (this.isVsAI)
            {
                ControlAIPlayer(logicGame.Player2); // Điều khiển Player 2 bằng AI
            }
            else
            {
                // Cập nhật di chuyển cho Player 2
                if (logicGame.Player2.isHit || logicGame.Player2.DangDanhDungKo() || logicGame.Player2.isDashing) return;

                if (player2MoveLeft)
                {
                    if (!logicGame.Player2.IsFacingLeft) logicGame.Player2.PositionX += logicGame.Player2.charWidth;
                    logicGame.Player2.MoveLeft();
                    logicGame.Player2.IsMovingLeft = true;
                    logicGame.Player2.IsFacingLeft = true;
                }

                if (player2MoveRight)
                {
                    if (logicGame.Player2.IsFacingLeft) logicGame.Player2.PositionX -= logicGame.Player2.charWidth;
                    logicGame.Player2.MoveRight();
                    logicGame.Player2.IsMovingLeft = false;
                    logicGame.Player2.IsFacingLeft = false;
                }

                if (!player2MoveLeft && !player2MoveRight && ! logicGame.Player1.isDashing)
                {
                    if (!logicGame.Player2.IsFacingLeft && logicGame.Player2.PositionX > logicGame.Player1.PositionX)
                    {
                        logicGame.Player2.IsFacingLeft = true;
                        logicGame.Player2.PositionX += logicGame.Player2.charWidth;
                    }
                }

                // Các hành động khác cho Player 2
                if (player2Jump) logicGame.Player2.Jump();
                if (player2AttackJ) logicGame.Player2.Attack(attackType: ActionState.AttackingJ);
                if (player2AttackK) logicGame.Player2.Attack(attackType: ActionState.AttackingK);
                if (player2Dash) logicGame.Player2.Dash();
                if (player2AttackI && logicGame.Player2.Mana >= logicGame.Player2.manaSkillI) logicGame.Player2.SpecicalSkill();
            }
        }

        private void ControlAIPlayer(Character player2)
        {
            if (player2.isHit || player2.DangDanhDungKo()) // Nếu AI bị đánh, chỉ cho phép chạy hoạt ảnh bị đánh
            {
                return; // Dừng AI khi đang bị đánh
            }

            // Kiểm tra xem Player 1 có đang tấn công không
            bool isPlayer1Attacking = logicGame.Player1.DangDanhDungKo(); // Kiểm tra trạng thái tấn công của Player 1
            float distanceToPlayer1 = Math.Abs(player2.PositionX - logicGame.Player1.PositionX); // Tính khoảng cách đến Player 1
            const float attackRange = 50.0f; // Khoảng cách để AI có thể tấn công (tùy chỉnh)

            // Nếu Player 1 đang tấn công và AI không phòng thủ, AI sẽ phòng thủ
            if (isPlayer1Attacking)
            {
                // Thực hiện hành động phòng thủ
                logicGame.Player2.Defend(false);
            }
            else
            {
                // Nếu AI không cần phòng thủ, tiếp tục hành động bình thường
                if (distanceToPlayer1 > attackRange) // Nếu khoảng cách xa hơn attackRange, di chuyển về phía Player 1
                {
                    if (player2.PositionX < logicGame.Player1.PositionX)
                    {
                        player2.MoveRight();
                        player2.IsFacingLeft = false;
                    }
                    else
                    {
                        player2.MoveLeft();
                        player2.IsFacingLeft = true;
                    }
                }
                else // Nếu khoảng cách gần, thực hiện hành động tấn công
                {
                    if (!logicGame.Player2.isHit && !logicGame.Player2.DangDanhDungKo())
                    {
                        // AI thực hiện tấn công
                        if (logicGame.Player2.Mana >= logicGame.Player2.manaSkillI) // Nếu đủ mana, dùng kỹ năng đặc biệt
                        {
                            logicGame.Player2.SpecicalSkill();
                        }
                        else
                        {
                            // Tấn công thường
                            logicGame.Player2.Attack(attackType: ActionState.AttackingJ);
                            logicGame.Player2.Attack(attackType: ActionState.AttackingK);
                            logicGame.Player2.Attack(attackType: ActionState.Dash);
                        }
                    }
                }
            }
        }

        private void DrawCharacter(PaintEventArgs e, Character character, bool flip)
        {
            if (character == null) return;
            // Sử dụng `IsMovingLeft` để quyết định có flip hình ảnh hay không
            bool shouldFlip = character.IsMovingLeft || (!character.IsMovingLeft && character.IsFacingLeft);
            character.charWidth = (int)(character.CurrentImage.Width * character.ScaleFactor);
            character.charHeight = (int)(character.CurrentImage.Height * character.ScaleFactor);
            if (!character.isJumpping) character.PositionY = Math.Min(character.BaseY - character.charHeight, this.ClientSize.Height - character.charHeight);

            // Lưu trạng thái đồ họa hiện tại
            GraphicsState state = e.Graphics.Save();
             
            // Nếu cần flip, thực hiện flip
            if (shouldFlip)
            {

                character.IsFacingLeft = true;
                e.Graphics.TranslateTransform(character.PositionX, 0);
                e.Graphics.ScaleTransform(-1, 1); // Lật trên trục X
                character.rectangle = new Rectangle(character.PositionX - character.charWidth, character.PositionY, character.charWidth, character.charHeight);
                e.Graphics.DrawImage(character.CurrentImage, new Rectangle(0, character.PositionY, character.charWidth, character.charHeight));
            }
            else
            {
                character.rectangle = new Rectangle(character.PositionX, character.PositionY, character.charWidth, character.charHeight);
                e.Graphics.DrawImage(character.CurrentImage, character.rectangle);
            }

            // Khôi phục trạng thái đồ họa
            e.Graphics.Restore(state);
        }
        private void DrawHitbox(PaintEventArgs e, ActionState attackType, Character character, bool flip, Character character2)
        {
            Rectangle rectangleHitbox;
            GraphicsState state = e.Graphics.Save();
            
            if (character.IsFacingLeft)
            {
                if (character.CurrentHitboxImage == null)
                {
                    rectangleHitbox = new Rectangle(character.PositionX - character.charWidth, character.PositionY + (character.charHeight / 2 - (character.charHeight / 2)), character.charWidth, character.charHeight/2);
                }
                else
                {
                    rectangleHitbox = new Rectangle(character.HitboxPositionXLeft, character.HitboxPositionYLeft, (int)(character.CurrentHitboxImage.Width), (int)(character.CurrentHitboxImage.Height));
                    /*using (SolidBrush redBrush = new SolidBrush(Color.Red))
                    {
                        e.Graphics.FillRectangle(redBrush, rectangleHitbox); // Tô đầy hitbox
                    }*/
                    e.Graphics.TranslateTransform(character.PositionX, 0);
                    e.Graphics.ScaleTransform(-1, 1);
                    
                    e.Graphics.DrawImage(character.CurrentHitboxImage, new Rectangle(character.HitboxPositionXRight - character.PositionX, character.HitboxPositionYLeft, (int)(character.CurrentHitboxImage.Width), (int)(character.CurrentHitboxImage.Height)));
                   
                    e.Graphics.TranslateTransform(character.PositionX, 0);
                    e.Graphics.ScaleTransform(-1, 1);
                }
            }
            else
            {
                if (character.CurrentHitboxImage == null)
                {
                    rectangleHitbox = new Rectangle(character.PositionX, character.PositionY + (character.charHeight / 2 - (character.charHeight / 2)), character.charWidth, character.charHeight / 2);
                }
                else
                {
                    rectangleHitbox = new Rectangle(character.HitboxPositionXRight, character.HitboxPositionYRight, (int)(character.CurrentHitboxImage.Width), (int)(character.CurrentHitboxImage.Height));
                    e.Graphics.DrawImage(character.CurrentHitboxImage, new Rectangle(character.HitboxPositionXRight, character.HitboxPositionYRight, (int)(character.CurrentHitboxImage.Width), (int)(character.CurrentHitboxImage.Height)));
              
                }
            }

            if(CollisionHandler.KiemTra2ThangDanhNhau(character, character2, rectangleHitbox, character2.rectangle, animationManager, this))
            {
                animationManager.DrawImage(e.Graphics);
            }

            e.Graphics.Restore(state);
            
        }


        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (background != null && background.Width > 0 && background.Height > 0)
            {
                // Vẽ nền với tỷ lệ phù hợp
                float scaleX = (float)this.ClientSize.Width / background.Width;
                float scaleY = (float)this.ClientSize.Height / background.Height;
                float scale = Math.Min(scaleX, scaleY);

                e.Graphics.DrawImage(background, 0, 0, background.Width * scale, background.Height * scale);
            }
            else
            {
                // Nếu không có nền hợp lệ, vẽ nền mặc định
                e.Graphics.Clear(Color.Gray); // Nền màu xám mặc định
            }

            // Vẽ thanh mau và năng lượng cho cả hai người chơi
            if (logicGame.Player1 != null)
            {
                // Vẽ thanh cho Player1 ở góc trái
                if (DrawHealthAndEnergy(e, logicGame.Player1, 100, 20, flip: false) <= 0f)
                {
                    //QuanLiTaiKhoan.LuuTranDau(idChar1, idChar2, "Lose");
                    SetTextWinner(logicGame.Player2.Name);
                }
                DrawCharacter(e, logicGame.Player1, flip: false);  // Vẽ nhân vật Player1
            }

            if (logicGame.Player2 != null)
            {
                // Vẽ thanh cho Player2 ở góc phải
                if (DrawHealthAndEnergy(e, logicGame.Player2, this.ClientSize.Width - 300, 20, flip: true) <= 0f)
                {
                    //QuanLiTaiKhoan.LuuTranDau(idChar1, idChar2, "Win");
                    SetTextWinner(logicGame.Player1.Name);
                };
                DrawCharacter(e, logicGame.Player2, flip: true);  // Vẽ nhân vật Player2
            }
            
            if (logicGame.Player1.DangDanhDungKo()) DrawHitbox(e, attackType: ActionState.AttackingI, logicGame.Player1, flip: false, logicGame.Player2);
            if (logicGame.Player2.DangDanhDungKo())  DrawHitbox(e, attackType: ActionState.AttackingI, logicGame.Player2, flip: false, logicGame.Player1);

            Font newFont = new Font("Arial", 24, FontStyle.Bold);

            // Tạo SolidBrush cho màu chữ và Pen cho viền
            Brush solidBrushP1 = new SolidBrush(Color.Red);
            Brush solidBrushP2 = new SolidBrush(Color.Blue);

            // Kích thước chữ và độ rộng của ô
            float rectWidthP1 = newFont.Size * 2.5f; // Kích thước ô cho Player1 (có thể điều chỉnh)
            float rectHeightP1 = newFont.Size * 1.6f; // Kích thước ô cho Player1 (có thể điều chỉnh)
            float rectWidthP2 = newFont.Size * 2.5f; // Kích thước ô cho Player2 (có thể điều chỉnh)
            float rectHeightP2 = newFont.Size * 1.6f; // Kích thước ô cho Plaeyer2 (có thể điều chỉnh)

            // Cấu hình SmoothingMode để vẽ mượt hơn
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Điều chỉnh vị trí và kích thước mũi tên cho Player1
            PointF[] arrowPoints1 = new PointF[]
            {
            new PointF(logicGame.Player1.PositionXName + (rectWidthP1 / 2) - 5, logicGame.Player1.PositionY),  // Đỉnh mũi tên (ở dưới)
            new PointF(logicGame.Player1.PositionXName + (rectWidthP1 / 2) + 15, logicGame.Player1.PositionY - 10),  // Góc trên bên phải
            new PointF(logicGame.Player1.PositionXName + (rectWidthP1 / 2) - 30, logicGame.Player1.PositionY - 10)   // Góc trên bên trái
            };
            e.Graphics.FillPolygon(Brushes.Red, arrowPoints1);  // Vẽ mũi tên với màu đỏ

            // Vẽ bóng chữ Player1
            Brush shadowBrush = new SolidBrush(Color.White); // Độ mờ lớn hơn (180)
            e.Graphics.DrawString("P1", newFont, shadowBrush, logicGame.Player1.PositionXName + 4, logicGame.Player1.PositionY - 50); // Vị trí bóng lệch chút hơn

            // Vẽ chữ Player1 với font mới
            e.Graphics.DrawString("P1", newFont, solidBrushP1, logicGame.Player1.PositionXName, logicGame.Player1.PositionY - 50);

            // Điều chỉnh vị trí và kích thước mũi tên cho Player2
            PointF[] arrowPoints2 = new PointF[]
            {
            new PointF(logicGame.Player2.PositionXName + (rectWidthP2 / 2) - 5, logicGame.Player2.PositionY),  // Đỉnh mũi tên (ở dưới)
            new PointF(logicGame.Player2.PositionXName + (rectWidthP2 / 2) + 15, logicGame.Player2.PositionY - 10),  // Góc trên bên phải
            new PointF(logicGame.Player2.PositionXName + (rectWidthP2 / 2) - 30, logicGame.Player2.PositionY - 10)   // Góc trên bên trái
            };
            e.Graphics.FillPolygon(Brushes.Blue, arrowPoints2);  // Vẽ mũi tên với màu xanh

            // Vẽ bóng chữ Player2
            e.Graphics.DrawString("P2", newFont, shadowBrush, logicGame.Player2.PositionXName + 4, logicGame.Player2.PositionY - 50); // Vị trí bóng lệch chút hơn

            // Vẽ chữ Player2 với font mới
            e.Graphics.DrawString("P2", newFont, solidBrushP2, logicGame.Player2.PositionXName, logicGame.Player2.PositionY - 50);

        }
        private void SetTextWinner(string nameWinner)
        {
            if (endGame) return; // Đảm bảo chỉ xử lý khi game chưa kết thúc

            endGame = true; // Đánh dấu trận đấu đã kết thúc
            labelWiner.Visible = true;
            labelWiner.Text = nameWinner + "  " + "WIN";

            // Lưu lịch sử trận đấu
            QuanLiTaiKhoan.LuuTranDau(
                idChar1,
                idChar2,
                nameWinner == logicGame.Player1.Name ? "Win" : "Lose"
            );

            // Hiển thị FormStart
            // Dừng và giải phóng Timer cũ (nếu có)
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
            }

            // Tạo Timer mới
            timer = new Timer
            {
                Interval = 4000
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Dispose();
            timer = null;
            FormStart mhs = new FormStart();
            mhs.Show();
            this.Dispose(); // Giải phóng form hiện tại
        }


        private float DrawHealthAndEnergy(PaintEventArgs e, Character character, int x, int y, bool flip)
        {
            if (character == null) return 0f;

            // Vẽ tên nhân vật
            using (Font font = new Font("Arial", 12, FontStyle.Bold))
            {
                if (flip)
                {
                    e.Graphics.DrawString(character.Name, font, Brushes.White, x + 160, y - 20);
                }
                else
                {
                    e.Graphics.DrawString(character.Name, font, Brushes.White, x, y - 20);
                }
            }

            // Vẽ ảnh đại diện (nếu có)
            if (character.Avatar != null)
            {
                if (flip)
                {
                    // Lật ảnh đại diện cho Player 2
                    GraphicsState state = e.Graphics.Save(); // Lưu trạng thái gốc
                    e.Graphics.ScaleTransform(-1, 1); // Lật trên trục X
                    e.Graphics.DrawImage(character.Avatar, -(x + 200 + 1), y, -60, 50); // Vẽ ảnh đã lật
                    e.Graphics.Restore(state); // Khôi phục trạng thái gốc
                }
                else
                {
                    e.Graphics.DrawImage(character.Avatar, x - 60, y, 60, 50); // Vẽ bình thường
                }
            }

            // Vẽ thanh mau
            float healthPercentage = character.PhanTramMauHienTai();
            if (flip)
            {
                // Lật thanh máu từ phải sang trái
                e.Graphics.FillRectangle(Brushes.Green, x + (1 - healthPercentage) * 200, y, healthPercentage * 200, 20);
                e.Graphics.DrawRectangle(Pens.Black, x, y, 200, 20);
            }
            else
            {
                // Vẽ bình thường
                e.Graphics.FillRectangle(Brushes.Green, x, y, healthPercentage * 200, 20);
                e.Graphics.DrawRectangle(Pens.Black, x, y, 200, 20);
            }

            // Vẽ thanh năng lượng
            float energyPercentage = character.PhanTramNangLuongHienTai();
            if (flip)
            {
                // Lật thanh năng lượng từ phải sang trái
                e.Graphics.FillRectangle(Brushes.Blue, x + (1 - energyPercentage) * 200, y + 30, energyPercentage * 200, 20);
                e.Graphics.DrawRectangle(Pens.Black, x, y + 30, 200, 20);
            }
            else
            {
                // Vẽ bình thường
                e.Graphics.FillRectangle(Brushes.Blue, x, y + 30, energyPercentage * 200, 20);
                e.Graphics.DrawRectangle(Pens.Black, x, y + 30, 200, 20);
            }
            return healthPercentage;
        }
        private void StreetFighterGame_Load(object sender, EventArgs e)
        {
            // Empty, currently no special loading logic needed
        }

    }
}
