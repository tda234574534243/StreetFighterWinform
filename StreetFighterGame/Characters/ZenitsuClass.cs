using StreetFighterGame.GameEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace StreetFighterGame.Characters
{
    public class Zenitsu : Character
    {
        public Rectangle rectangleHitboxRight {  get; set; } = Rectangle.Empty;
        public Rectangle rectangleHitboxLeft { get; set; } = Rectangle.Empty;
        public Zenitsu(int startX, int startY, float scaleFactor) : base(startX, startY, scaleFactor, 500, 2, 100)
        {
            // Tải hoạt ảnh cho Goku với ActionState
            LoadAnimations(".\\Zenitsu", new Dictionary<ActionState, string>
            {
                { ActionState.Standing, "Zenitsu_0" },
                { ActionState.WalkingFront, "Zenitsu_21" },
                { ActionState.WalkingBack, "Zenitsu_21" },
                { ActionState.Defending, "Zenitsu_7" },
                { ActionState.Jumping, "Zenitsu_40" },
                { ActionState.AttackingJ, "Zenitsu_201" },
                { ActionState.AttackingK, "Zenitsu_202" },
                { ActionState.Dash, "Zenitsu_8" },
                { ActionState.AttackingI, "Zenitsu_5" },
                { ActionState.hit, "Zenitsu_5" }
            }, new Dictionary<ActionState, int>
            {
                { ActionState.Standing, 5 },         // 4 khung hình cho Standing
                { ActionState.WalkingFront, 5 },     // 6 khung hình cho WalkingFront
                { ActionState.WalkingBack, 5 },      // 6 khung hình cho WalkingBack
                { ActionState.Defending, 1 },        // 3 khung hình cho Defending
                { ActionState.Jumping, 1 },         // 10 khung hình cho Jumping
                { ActionState.AttackingJ, 4 },       // 3 khung hình cho AttackingJ
                { ActionState.AttackingK, 3 },      // 17 khung hình cho AttackingK
                { ActionState.Dash, 1 },       // 4 khung hình cho AttackingL
                { ActionState.AttackingI, 5 },        // 4 khung hình cho AttackingI
                { ActionState.hit, 1 }
            });

            LoadHitboxAnimations(".\\Zenitsu", new Dictionary<ActionState, string>
            {
                { ActionState.AttackingI, "Zenitsu_1602" }
            }, new Dictionary<ActionState, int>
            {
                { ActionState.AttackingI, 25 }
            });

            Name = "Zenitsu";
            LoadAvatar(".\\Zenitsu\\Zenitsu_10000-0.png");
            manaSkillI = 50;
        }

        public override void SpecicalSkill()
        {
            Attack(ActionState.AttackingI);
            startDrawHitbox();

            HitboxPositionXLeft = PositionX - charWidth / 2 - CurrentHitboxImage.Width / 2;
            HitboxPositionXRight = PositionX - CurrentHitboxImage.Width / 2 + charWidth / 2;
            HitboxPositionYRight = HitboxPositionYLeft = BaseY - CurrentHitboxImage.Height;

            frameTimer.Stop();
            frameTimer.Tick -= OnFrameTimerTick;
            frameTimer.Tick -= OnFrameSpecicalSkillTimerTick;
            frameTimer.Tick += OnFrameSpecicalSkillTimerTick;
            frameTimer.Start();

            HitboxFrameTimer.Tick -= OnSpecicalSkillTick;
            HitboxFrameTimer.Tick += OnSpecicalSkillTick;
            HitboxFrameTimer.Start();
        }

        protected void OnSpecicalSkillTick(object sender, EventArgs e)
        {
            if (HitboxAnimations.ContainsKey(ActionState.AttackingI) && HitboxAnimations[ActionState.AttackingI].Count > 0)
            {
                var frames = HitboxAnimations[ActionState.AttackingI];
                base.currentHitboxFrame = (currentHitboxFrame + 1) % frames.Count;
                TruMana(2);

                HitboxPositionXLeft = PositionX - charWidth / 2 - CurrentHitboxImage.Width / 2;
                HitboxPositionXRight = PositionX - frames[currentFrame].Width / 2 + charWidth / 2;

                base.CurrentHitboxImage = frames[currentHitboxFrame];

                if (base.currentHitboxFrame == base.lastFrameOfHitboxAnimation || isHit)
                {
                    currentHitboxFrame = 0;
                    lastFrameOfHitboxAnimation = 0;
                    HitboxFrameTimer.Stop();
                    HitboxFrameTimer.Tick -= OnSpecicalSkillTick;
                }
            }
        }
        protected void OnFrameSpecicalSkillTimerTick(object sender, EventArgs e)
        {
            if (Animations.ContainsKey(CurrentState) && Animations[CurrentState].Count > 0)
            {
                var frames = Animations[CurrentState];
                base.currentFrame = (base.currentFrame + 1) % frames.Count;
                
                base.CurrentImage = frames[base.currentFrame];
                if (base.currentFrame == base.lastFrameOfAttackAnimation && base.currentHitboxFrame != base.lastFrameOfHitboxAnimation) base.currentFrame = 0;

                // Nếu là trạng thái tấn công, kiểm tra nếu đến frame cuối thì ngừng tấn công
                if (IsAttackAction(CurrentState) && base.currentFrame == base.lastFrameOfAttackAnimation && base.currentHitboxFrame == base.lastFrameOfHitboxAnimation || isHit)
                {
                    isAttacking = triggerAttack = false;
                    CurrentHitboxImage = null;

                    frameTimer.Stop();
                    frameTimer.Tick -= OnFrameSpecicalSkillTimerTick;
                    StopAttacking();
                    frameTimer.Tick += OnFrameTimerTick;
                    frameTimer.Start();

                }
            }
        }
    }
}
