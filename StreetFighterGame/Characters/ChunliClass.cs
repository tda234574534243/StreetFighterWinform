using StreetFighterGame.GameEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace StreetFighterGame.Characters
{
    public class Chunli : Character
    {
        public Chunli(int startX, int startY, float scaleFactor) : base(startX, startY, scaleFactor, 300, 2)
        {
            // Tải hoạt ảnh cho Chunli với ActionState
            LoadAnimations(".\\Chunli", new Dictionary<ActionState, string>
            {
                { ActionState.Standing, "ChunLi_0" },
                { ActionState.WalkingFront, "ChunLi_20" },
                { ActionState.WalkingBack, "ChunLi_20" },
                { ActionState.Defending, "ChunLi_10" },
                { ActionState.Jumping, "ChunLi_41" },
                { ActionState.AttackingJ, "ChunLi_205" },
                { ActionState.AttackingK, "ChunLi_201" },
                { ActionState.Dash, "ChunLi_110" },
                { ActionState.AttackingI, "ChunLi_2003" },
                { ActionState.hit, "ChunLi_6000"}
            }, new Dictionary<ActionState, int>
            {
                { ActionState.Standing, 4 },         // 4 khung hình cho Standing
                { ActionState.WalkingFront, 6 },     // 6 khung hình cho WalkingFront
                { ActionState.WalkingBack, 6 },      // 6 khung hình cho WalkingBack
                { ActionState.Defending, 3 },        // 3 khung hình cho Defending
                { ActionState.Jumping, 10 },         // 10 khung hình cho Jumping
                { ActionState.AttackingJ, 5 },       // 3 khung hình cho AttackingJ
                { ActionState.AttackingK, 17 },      // 17 khung hình cho AttackingK
                { ActionState.Dash, 1 },       // 4 khung hình cho AttackingL
                { ActionState.AttackingI, 4 },
                { ActionState.hit, 1}// 4 khung hình cho AttackingI
            });

            LoadHitboxAnimations(".\\Chunli", new Dictionary<ActionState, string>
            {
                { ActionState.AttackingI, "Chunli_1015" }
            }, new Dictionary<ActionState, int>
            {
                { ActionState.AttackingI, 12 }
            });
            Name = "Chunli";
            LoadAvatar(".\\Chunli\\ChunLi_9000-1.png");
        }
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

                HitboxPositionXLeft = PositionX - charWidth - frames[currentHitboxFrame].Width;
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


    }
}
