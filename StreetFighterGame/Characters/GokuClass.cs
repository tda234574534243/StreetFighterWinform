using StreetFighterGame.GameEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace StreetFighterGame.Characters
{
    public class Goku : Character
    {
        public Goku(int startX, int startY, float scaleFactor) : base(startX, startY, scaleFactor, 1000, 2, 100)
        {
            // Tải hoạt ảnh cho Goku với ActionState
            LoadAnimations(".\\GokuMUI", new Dictionary<ActionState, string>
            {
                { ActionState.Standing, "GokuMUI_0" },
                { ActionState.WalkingFront, "GokuMUI_20" },
                { ActionState.WalkingBack, "GokuMUI_20" },
                { ActionState.Defending, "GokuMUI_120" },
                { ActionState.Jumping, "GokuMUI_40" },
                { ActionState.AttackingJ, "GokuMUI_201" },
                { ActionState.AttackingK, "GokuMUI_202" },
                { ActionState.Dash, "GokuMUI_100" },
                { ActionState.AttackingI, "GokuMUI_203" },
                { ActionState.hit, "GokuMUI_5001" }
            }, new Dictionary<ActionState, int>
            {
                { ActionState.Standing, 5 },         // 4 khung hình cho Standing
                { ActionState.WalkingFront, 4 },     // 6 khung hình cho WalkingFront
                { ActionState.WalkingBack, 4 },      // 6 khung hình cho WalkingBack
                { ActionState.Defending, 3 },        // 3 khung hình cho Defending
                { ActionState.Jumping, 1 },         // 10 khung hình cho Jumping
                { ActionState.AttackingJ, 15 },       // 3 khung hình cho AttackingJ
                { ActionState.AttackingK, 8 },      // 17 khung hình cho AttackingK
                { ActionState.Dash, 2 },       // 4 khung hình cho AttackingL
                { ActionState.AttackingI, 6 },        // 4 khung hình cho AttackingI
                { ActionState.hit, 1 }
            });

            LoadHitboxAnimations(".\\GokuMUI", new Dictionary<ActionState, string>
            {
                { ActionState.AttackingI, "Vegeto_1405" }
            }, new Dictionary<ActionState, int>
            {
                { ActionState.AttackingI, 13 }
            });

            Name = "Goku";
            LoadAvatar(".\\GokuMUI\\GokuMUI_9000-3.png");
            base.HitboxDurian = 50;
            manaSkillI = 13 * 2;
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

        protected void OnSpecicalSkillTick(object sender, EventArgs e)
        {
            if (HitboxAnimations.ContainsKey(ActionState.AttackingI) && HitboxAnimations[ActionState.AttackingI].Count > 0)
            {
                var frames = HitboxAnimations[ActionState.AttackingI];
                base.currentHitboxFrame = (currentHitboxFrame + 1) % frames.Count;
                TruMana(2);

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
                if (base.currentFrame == base.lastFrameOfAttackAnimation && base.currentHitboxFrame != base.lastFrameOfHitboxAnimation) base.currentFrame = 2;

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
