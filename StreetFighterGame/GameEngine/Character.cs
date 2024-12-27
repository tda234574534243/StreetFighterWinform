using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Media;
using System.Security.Cryptography;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;


namespace StreetFighterGame.GameEngine
{
    public enum ActionState
    {
        Standing,
        WalkingFront,
        WalkingBack,
        Defending,
        Jumping,
        AttackingJ,
        AttackingK,
        Dash,
        AttackingI,
        hit
    }
    public struct ChiSoSucManh
    {
        public float mauHienTai, mauToiDa;
        public float nangLuongHienTai, nangLuonToiDa;
        public float dame;
        public ChiSoSucManh(int mauTd, int d)
        {
            mauHienTai = mauTd;
            mauToiDa = mauTd;
            nangLuongHienTai = 100;
            nangLuonToiDa = 100;
            dame = d;
        }
    }
    public abstract class Character
    {
        public bool IsFacingLeft { get; set; }
        public bool IsMovingLeft { get; set; }
        public bool stun { get; set; }
        public string Name { get; set; }
        public float Energy { get; set; }
        public float Health { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int BaseY { get; set; }
        public float ScaleFactor { get; set; }
        public Image CurrentImage { get; protected set; }
        public Image CurrentHitboxImage { get; protected set; }
        public Image Avatar { get; set; }  // Ảnh đại diện cho nhân vật
        public int charWidth { get; set; }
        public int charHeight { get; set; }
        public int HitboxPositionXLeft { get; set; }
        public int HitboxPositionXRight { get; set; }
        public int HitboxPositionYLeft { get; set; }
        public int HitboxPositionYRight { get; set; }
        public int hitboxWidth { get; set; }
        public int hitboxHeight { get; set; }
        protected int HitboxDurian { get; set; }
        public Rectangle rectangle { get; set; }
        public Rectangle rectangleEneme { get; set; }
        public int manaSkillI { get; protected set; }
        public int PositionXName { get; set; }
        public bool isDashing { get; private set; }
        //public Rectangle rectangleHitbox {  get; set; }

        protected Dictionary<ActionState, List<Image>> Animations { get; set; }
        protected Dictionary<ActionState, List<Image>> HitboxAnimations { get; set; }
        public ActionState CurrentState { get; private set; } = ActionState.Standing;

        protected int currentFrame = 0;
        public int currentHitboxFrame = 0;
        private int frameDelay = 0;
        //  private int frameCounter = 0;
        protected int lastFrameOfAttackAnimation;
        public int lastFrameOfHitboxAnimation;
        private float jumpSpeed = 0f;
        private const float Gravity = 16f;
        // private int DefendYOffset = 25;
        public bool isDefense, triggerAttack; // biến để kiểm tra có đang trong trạng thái attack hay ko
        public bool isAttacking, isJumpping, isHit;

        ChiSoSucManh cssm;

        protected Timer frameTimer; // Timer để thay đổi frame
        protected int frameInterval = 6; // Khoảng thời gian giữa các frame (ms)

        protected Timer HitboxFrameTimer;

        private Timer hitTimer;
        private int hitDuration = 300;

        private Timer dashTimer;
        private int dashSpeed = 100;
        private int dashDirection = 1;
        private int dashDistance = 300;

        //private List<Hitbox> hitboxes = new List<Hitbox>();



        protected Character(int startX, int startY, float scaleFactor, int mau, int d, int hitboxDurian = 6)
        {
            isHit = true;
            PositionX = startX;
            BaseY = startY;
            PositionY = startY;
            ScaleFactor = scaleFactor;
            Animations = new Dictionary<ActionState, List<Image>>();
            HitboxAnimations = new Dictionary<ActionState, List<Image>>();
            isAttacking = isJumpping = isDefense = triggerAttack = false;
            CurrentHitboxImage = null;
            HitboxDurian = hitboxDurian;
            isDashing = false;

            frameTimer = new Timer { Interval = frameInterval };
            frameTimer.Tick += OnFrameTimerTick;
            frameTimer.Start();

            HitboxFrameTimer = new Timer { Interval = HitboxDurian };
            //HitboxFrameTimer.Tick += OnHitboxFrameTimerTick;

            hitTimer = new Timer { Interval = hitDuration };
            hitTimer.Tick += OnHitTimerTick;

            dashTimer = new Timer { Interval = 6 };
            dashTimer.Tick += OnDashTick;

            SetChiSoSucManh(mau, d);
            //LoadHitSound();
        }
        public abstract void SpecicalSkill();
        protected void OnFrameTimerTick(object sender, EventArgs e)
        {
            // Chỉ xử lý nếu trạng thái hiện tại có animation
            if (Animations.ContainsKey(CurrentState) && Animations[CurrentState].Count > 0)
            {
                var frames = Animations[CurrentState];
                currentFrame = (currentFrame + 1) % frames.Count;
                CurrentImage = frames[currentFrame];

                // Nếu là trạng thái tấn công, kiểm tra nếu đến frame cuối thì ngừng tấn công
                if (IsAttackAction(CurrentState) && currentFrame == lastFrameOfAttackAnimation)
                {
                    isAttacking = triggerAttack = false;
                    CurrentHitboxImage = null;
                    StopAttacking();
                }
            }
        }
        private void OnDashTick(object sender, EventArgs e)
        {
            PositionX += dashSpeed * dashDirection;
            dashSpeed = Math.Max(dashSpeed - 2, 5);
            dashDistance -= dashSpeed;

            if (dashDistance <= 0)
            {

                dashSpeed = 100;
                dashDistance = 300;
                dashTimer.Stop();
                if (!IsOnGround()) ChangeState(ActionState.Jumping);
                else ChangeState(ActionState.Standing);
                isDashing = false;
            }
        }
        public void Dash()
        {
            if (IsFacingLeft) dashDirection = -1;
            else dashDirection = 1;
            isDashing = true;
            ChangeState(ActionState.Dash);
            dashTimer.Start();
            jumpSpeed = 0;
        }
        public void startDrawHitbox()
        {

            CurrentHitboxImage = HitboxAnimations[ActionState.AttackingI][0];
            lastFrameOfHitboxAnimation = HitboxAnimations[ActionState.AttackingI].Count - 1; // lấy frame để kiểm tra tấn công

            //HitboxFrameTimer.Start();
        }
        private void OnHitTimerTick(object sender, EventArgs e)
        {

            isHit = false;
            ChangeState(ActionState.Standing);
            hitTimer.Stop();
        }
        protected void LoadAnimations(string folderPath, Dictionary<ActionState, string> stateToPrefix, Dictionary<ActionState, int> stateToFrameCount)
        {
            foreach (var state in stateToPrefix)
            {
                // Lấy số lượng khung hình từ dictionary stateToFrameCount
                int frameCount = stateToFrameCount.ContainsKey(state.Key) ? stateToFrameCount[state.Key] : 1;
                Animations[state.Key] = LoadImages(folderPath, frameCount, state.Value);
            }

            if (Animations.ContainsKey(ActionState.Standing))
            {
                CurrentImage = Animations[ActionState.Standing][0];
            }
        }
        protected void LoadHitboxAnimations(string folderPath, Dictionary<ActionState, string> statetoPrefix, Dictionary<ActionState, int> statetoFrameCounr)
        {
            foreach (var state in statetoPrefix)
            {
                int frameCount = statetoFrameCounr.ContainsKey(state.Key) ? statetoFrameCounr[state.Key] : 1;
                HitboxAnimations[state.Key] = LoadImages(folderPath, frameCount, state.Value);
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
        public virtual void Update(Rectangle eneme)
        {
            charWidth = (int)(CurrentImage.Width * ScaleFactor);
            charHeight = (int)(CurrentImage.Height * ScaleFactor);
            if (IsFacingLeft) PositionXName = PositionX - charWidth + CurrentImage.Width / 5;
            else PositionXName = PositionX + CurrentImage.Width / 5;
            PositionX = Math.Min(PositionX, 1000);
            PositionX = Math.Max(PositionX, IsFacingLeft ? charWidth : 0);
            rectangleEneme = eneme;
            if (isHit || isDashing) return;
            if (isJumpping) HandleJumping();
        }
        public void Attack(ActionState attackType)
        {
            ChangeState(attackType);
            PositionX = Math.Min(PositionX + (IsFacingLeft ? -2 : 2), 900);
            isAttacking = true;
            triggerAttack = true;
        }
        public bool DangDanhDungKo()
        {
            bool b = isAttacking && triggerAttack;
            //triggerAttack = false;
            return b;
        }
        public void XuLiKhiBiDanh()
        {
            if (cssm.mauHienTai <= 0) return;

            ChangeState(ActionState.hit); // Chuyển trạng thái sang Hit

            // Bắt đầu đếm ngược thời gian bị đánh
            hitTimer.Start();
            isHit = true;
        }
        public void ChangeState(ActionState newState)
        {
            if (CurrentState == newState || DangDanhDungKo() || isHit) return;


            CurrentState = newState;
            frameDelay = newState == ActionState.WalkingFront || newState == ActionState.WalkingBack ? 1 : 1;
            currentFrame = 0;

            if (Animations.ContainsKey(newState))
            {
                CurrentImage = Animations[newState][0];
            }

            // Đặt khoảng thời gian giữa các frame dựa trên hành động
            frameInterval = 6;
            frameTimer.Interval = frameInterval; // Cập nhật tốc độ Timer

            /// neu state la tan cong
            if (IsAttackAction(newState))
            {
                lastFrameOfAttackAnimation = Animations[newState].Count - 1; // lấy frame để kiểm tra tấn công
            }
        }
        public bool IsAttackAction(ActionState ac)
        {
            return ac == ActionState.AttackingJ || ac == ActionState.AttackingK || ac == ActionState.AttackingI;
        }
        public void StopAttacking()
        {
            if (!isAttacking) ChangeState(ActionState.Standing);
        }
        private void HandleJumping()
        {
            PositionY -= (int)jumpSpeed;
            if (isJumpping) jumpSpeed -= Gravity;

            if (PositionY > BaseY - charHeight)  // Đảm bảo không đi qua mặt đất
            {
                PositionY = BaseY - charHeight;
                jumpSpeed = 0;
                isJumpping = false;
                ChangeState(ActionState.Standing);
            }
        }
        public bool IsOnGround() // kiem tra co tren mat dat ko
        {
            //Console.WriteLine("vua goi IsOnGround");
            //if (PositionY == BaseY) Console.WriteLine("dang tren dat");
            return jumpSpeed == 0 && !isJumpping;
        }
        public void MoveRight()
        {
            if (isDefense) return;
            //if (CurrentState == ActionState.Jumping) return; // nếu đang nhảy mà retun thì nhân vật ko thể di chuyển khi nhảy
            //ChangeState(ActionState.WalkingFront);
            //PositionX = Math.Min(PositionX + 12, 1000);  // Kiểm tra vị trí để tránh đi ra ngoài
            /*** code sữa ****/
            if (IsOnGround()) ChangeState(ActionState.WalkingFront);
            PositionX = Math.Min(PositionX + 30, 900);  // Kiểm tra vị trí để tránh đi ra ngoài
        }
        public void MoveLeft()
        {
            if (isDefense) return;
            if (IsOnGround()) ChangeState(ActionState.WalkingBack);
            PositionX = Math.Max(PositionX - 30, 0);  // Kiểm tra vị trí để tránh đi ra ngoài
        }
        public void StopMoving()
        {
            if (IsOnGround() && !isDashing) // chỉ chuyển sang standing khi ngừng, và ở trên đất
            {
                ChangeState(ActionState.Standing);
            }
        }
        public void Jump()
        {
            if (IsOnGround()) // Kiểm tra nếu đang ở mặt đất
            {
                jumpSpeed = 80f;
                ChangeState(ActionState.Jumping);
                isJumpping = true;
            }
        }
        public void Defend(bool isDefen)
        {
            //ChangeState(ActionState.Defending);
            //PositionY = BaseY + DefendYOffset;
            /**** code sữa *****/
            if (IsOnGround() && isDefen) // chỉ ở trên đất mới được phòng thủ
            {
                ChangeState(ActionState.Defending);
                //PositionY = BaseY + DefendYOffset;
                isDefense = true;
            }
            else if (IsOnGround())
            {
                PositionY = BaseY;
                ChangeState(ActionState.Standing);
                isDefense = false;
            }
        }

        protected void LoadAvatar(string avatarPath)
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, avatarPath);
            Console.WriteLine($"Loading avatar from: {fullPath}"); // In đường dẫn

            if (File.Exists(fullPath))
            {
                Avatar = Image.FromFile(fullPath);
            }
            else
            {
                Console.WriteLine("Avatar file not found!");
                Avatar = new Bitmap(50, 50); // Ảnh mặc định
                using (Graphics g = Graphics.FromImage(Avatar))
                {
                    g.Clear(Color.Gray);
                    g.DrawString("?", new Font("Arial", 24), Brushes.White, 10, 10);
                }
            }
        }
        public float PhanTramMauHienTai()
        {
            return cssm.mauHienTai / cssm.mauToiDa;
        }
        public float PhanTramNangLuongHienTai()
        {

            //Console.WriteLine(cssm.nangLuongHienTai / cssm.nangLuonToiDa);
            return (float)(cssm.nangLuongHienTai) / (float)(cssm.nangLuonToiDa);
        }
        public void SetChiSoSucManh(int mau, int d)
        {
            cssm = new ChiSoSucManh(mau, d);
        }
        public void TruMau(float dame)
        {
            /* Console.WriteLine(dame);
             Console.WriteLine(cssm.mauHienTai);
             Console.WriteLine(cssm.mauToiDa);
             Console.WriteLine(cssm.mauHienTai / cssm.mauToiDa);*/
            cssm.mauHienTai -= dame;
        }
        public void HoiMana(float mana)
        {
            cssm.nangLuongHienTai = Math.Min(cssm.nangLuongHienTai += mana, cssm.nangLuonToiDa);
        }
        public void TruMana(float mana)
        {
            cssm.nangLuongHienTai = Math.Max(cssm.nangLuongHienTai -= mana, 0);
        }
        public ActionState AttackType
        {
            get { return CurrentState; }
        }
        public float Dame
        {
            get { return cssm.dame; }
        }
        public float Mana
        {
            get { return cssm.nangLuongHienTai; }
        }
        public bool Colliding()
        {
            return CollisionHandler.Colliding(rectangle, rectangleEneme);
        }
    }
}
