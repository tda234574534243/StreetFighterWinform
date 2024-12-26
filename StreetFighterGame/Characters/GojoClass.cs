using StreetFighterGame.GameEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace StreetFighterGame.Characters
{
    public class Gojo : Character
    {
        public Gojo(int startX, int startY, float scaleFactor) : base(startX, startY, scaleFactor, 750, 2, 100)
        {
            // Tải hoạt ảnh cho Goku với ActionState
            LoadAnimations(".\\Gojo", new Dictionary<ActionState, string>
            {
                { ActionState.Standing, "Gojo_0" },
                { ActionState.WalkingFront, "Gojo_20" },
                { ActionState.WalkingBack, "Gojo_20" },
                { ActionState.Defending, "Gojo_10" },
                { ActionState.Jumping, "Gojo_40" },
                { ActionState.AttackingJ, "Gojo_200" },
                { ActionState.AttackingK, "Gojo_300" },
                { ActionState.Dash, "Gojo_61" },
                { ActionState.AttackingI, "Gojo_1000" },
                { ActionState.hit, "Gojo_6000" }
            }, new Dictionary<ActionState, int>
            {
                { ActionState.Standing, 4 },         // 4 khung hình cho Standing
                { ActionState.WalkingFront, 7 },     // 6 khung hình cho WalkingFront
                { ActionState.WalkingBack, 7 },      // 6 khung hình cho WalkingBack
                { ActionState.Defending, 1 },        // 3 khung hình cho Defending
                { ActionState.Jumping, 1 },         // 10 khung hình cho Jumping
                { ActionState.AttackingJ, 8 },       // 3 khung hình cho AttackingJ
                { ActionState.AttackingK, 8 },      // 17 khung hình cho AttackingK
                { ActionState.Dash, 1 },       // 4 khung hình cho AttackingL
                { ActionState.AttackingI, 1 },        // 4 khung hình cho AttackingI
                { ActionState.hit, 1 }
            });

            LoadHitboxAnimations(".\\Gojo", new Dictionary<ActionState, string>
            {
                { ActionState.AttackingI, "Gojo_1250" }
            }, new Dictionary<ActionState, int>
            {
                { ActionState.AttackingI, 32 }
            });

            Name = "Gojo";
            LoadAvatar(".\\Gojo\\Gojo_9000-1.png");
            manaSkillI = 22 * 4;

        }
        private float hitboxVelocityX = 10f; // Tốc độ di chuyển của chiêu thức theo hướng X
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
                HitboxPositionXLeft -= (int)hitboxVelocityX;
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
        //public override void Attack(ActionState attackType)
        //{
        //    if (CurrentState != ActionState.Jumping)
        //    {
        //        ChangeState(attackType);
        //    }
        //}

        //public override void StopAttacking()
        //{
        //    ChangeState(ActionState.Standing);
        //}
    }
}
