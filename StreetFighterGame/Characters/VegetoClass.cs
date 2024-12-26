using StreetFighterGame.GameEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace StreetFighterGame.Characters
{
    public class Vegeto : Character
    {
        public Vegeto(int startX, int startY, float scaleFactor) : base(startX, startY, scaleFactor, 750, 2, 100)
        {
            // Tải hoạt ảnh cho Goku với ActionState
            LoadAnimations(".\\Vegeto", new Dictionary<ActionState, string>
            {
                { ActionState.Standing, "Vegeto_2" },
                { ActionState.WalkingFront, "Vegeto_20" },
                { ActionState.WalkingBack, "Vegeto_21" },
                { ActionState.Defending, "Vegeto_120" },
                { ActionState.Jumping, "Vegeto_40" },
                { ActionState.AttackingJ, "Vegeto_201" },
                { ActionState.AttackingK, "Vegeto_202" },
                { ActionState.Dash, "Vegeto_100" },
                { ActionState.AttackingI, "Vegeto_204" },
                { ActionState.hit, "Vegeto_220" }
            }, new Dictionary<ActionState, int>
            {
                { ActionState.Standing, 8 },         // 4 khung hình cho Standing
                { ActionState.WalkingFront, 4 },     // 6 khung hình cho WalkingFront
                { ActionState.WalkingBack, 4 },      // 6 khung hình cho WalkingBack
                { ActionState.Defending, 1 },        // 3 khung hình cho Defending
                { ActionState.Jumping, 1 },         // 10 khung hình cho Jumping
                { ActionState.AttackingJ, 9 },       // 3 khung hình cho AttackingJ
                { ActionState.AttackingK, 7 },      // 17 khung hình cho AttackingK
                { ActionState.Dash, 2 },       // 4 khung hình cho AttackingL
                { ActionState.AttackingI, 3 },        // 4 khung hình cho AttackingI
                { ActionState.hit, 1 }
            });

            LoadHitboxAnimations(".\\Vegeto", new Dictionary<ActionState, string>
            {
                { ActionState.AttackingI, "Vegeto_1000" }
            }, new Dictionary<ActionState, int>
            {
                { ActionState.AttackingI, 22 }
            });

            Name = "Vegeto";
            LoadAvatar(".\\Vegeto\\Vegeto_9000-4.png");
            manaSkillI = 22 * 4;
            
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
