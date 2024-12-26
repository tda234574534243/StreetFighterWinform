using StreetFighterGame.GameEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace StreetFighterGame.Characters
{
    public class Ryu : Character
    {
        public Ryu(int startX, int startY, float scaleFactor) : base(startX, startY, scaleFactor, 400, 2)
        {
            // Tải các ảnh cho từng trạng thái với số lượng khung hình khác nhau
            LoadAnimations(".\\Ryu", new Dictionary<ActionState, string>
            {
                { ActionState.Standing, "Ryu_0" },
                { ActionState.WalkingFront, "Ryu_20" },
                { ActionState.WalkingBack, "Ryu_20" },
                { ActionState.Jumping, "Ryu_40" },
                { ActionState.Defending, "Ryu_10" },
                { ActionState.AttackingJ, "Ryu_200" },
                { ActionState.AttackingK, "Ryu_210" },
                { ActionState.Dash, "Ryu_30" },
                { ActionState.AttackingI, "Ryu_400" },
                { ActionState.hit, "Ryu_5000" }
            }, new Dictionary<ActionState, int>
            {
                { ActionState.Standing, 5 },       // 5 khung hình cho Standing
                { ActionState.WalkingFront, 7 },   // 7 khung hình cho WalkingFront
                { ActionState.WalkingBack, 7 },    // 7 khung hình cho WalkingBack
                { ActionState.Jumping, 7 },        // 7 khung hình cho Jumping
                { ActionState.Defending, 1 },      // 3 khung hình cho Defending
                { ActionState.AttackingJ, 7 },     // 7 khung hình cho AttackingJ
                { ActionState.AttackingK, 10 },    // 10 khung hình cho AttackingK
                { ActionState.Dash, 1 },    // 18 khung hình cho AttackingL
                { ActionState.AttackingI, 9 },      // 9 khung hình cho AttackingI
                { ActionState.hit, 1 }
            });

            LoadHitboxAnimations(".\\Ryu", new Dictionary<ActionState, string>
            {
                { ActionState.AttackingI, "Ryu_7173" }
            }, new Dictionary<ActionState, int>
            {
                { ActionState.AttackingI, 10 }
            });

            Name = "Ryu";
            LoadAvatar(".\\Ryu\\Ryu_9000-1.png");
        }
        private float hitboxVelocityX = 5f; // Tốc độ di chuyển của chiêu thức theo hướng X
        public override void SpecicalSkill()
        {
            Attack(ActionState.AttackingI);
            startDrawHitbox();

            HitboxPositionXLeft = PositionX - charWidth - CurrentHitboxImage.Width;
            HitboxPositionXRight = charWidth + PositionX;
            HitboxPositionYRight = HitboxPositionYLeft = PositionY + (charHeight / 2 - CurrentHitboxImage.Height / 2);

            frameTimer.Stop();
            frameTimer.Tick -= OnFrameTimerTick;
            frameTimer.Tick -= OnFrameSpecicalSkillTimerTick;
            frameTimer.Tick += OnFrameSpecicalSkillTimerTick;
            frameTimer.Start();

            HitboxFrameTimer.Tick -= OnSpecicalSkillTick;
            HitboxFrameTimer.Tick += OnSpecicalSkillTick;
            HitboxFrameTimer.Start();

        }

        public void OnSpecicalSkillTick(object sender, EventArgs e)
        {
            if (HitboxAnimations.ContainsKey(ActionState.AttackingI) && HitboxAnimations[ActionState.AttackingI].Count > 0)
            {
                var frames = HitboxAnimations[ActionState.AttackingI];
                base.currentHitboxFrame = (currentHitboxFrame + 1) % frames.Count;
                TruMana(4);

                // Di chuyển hitbox theo hướng xa
                HitboxPositionXLeft += (int)hitboxVelocityX;
                HitboxPositionXRight += (int)hitboxVelocityX;

                HitboxPositionYRight = HitboxPositionYLeft = PositionY + (charHeight / 2 - frames[currentHitboxFrame].Height / 2);

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
                if (base.currentFrame == 6 && base.currentHitboxFrame != base.lastFrameOfHitboxAnimation) base.currentFrame = 4;

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
