using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using StreetFighterGame.GameEngine;
using StreetFighterGame.UI;

namespace StreetFighterGame.GameEngine
{
    public class InputHandle
    {
        public Character Player { get; private set; }
        public readonly HashSet<Keys> pressedKey = new HashSet<Keys>(); // HashSet to track pressed keys

        private Keys moveRightKey;
        private Keys moveLeftKey;
        private Keys jumpKey;
        private Keys defendKey;
        private Keys attackJKey;
        private Keys attackKKey;
        private Keys attackLKey;
        private Keys attackIKey;

        public InputHandle(string characterName, Keys moveRightKey = Keys.D, Keys moveLeftKey = Keys.A,
                           Keys jumpKey = Keys.W, Keys defendKey = Keys.S,
                           Keys attackJKey = Keys.J, Keys attackKKey = Keys.K,
                           Keys attackLKey = Keys.L, Keys attackIKey = Keys.I)
        {
            InitializeGame(characterName);
            this.moveRightKey = moveRightKey;
            this.moveLeftKey = moveLeftKey;
            this.jumpKey = jumpKey;
            this.defendKey = defendKey;
            this.attackJKey = attackJKey;
            this.attackKKey = attackKKey;
            this.attackLKey = attackLKey;
            this.attackIKey = attackIKey;
        }

        public void InitializeGame(string characterName)
        {
            Player = CharacterFactory.CreateCharacter(characterName, startX: 100, startY: 400);
        }

        /*public void OnKeyDown(Keys key)
        {
            if (Player.isHit) return;
            pressedKey.Add(key);

            // Handle movement
            if (key == moveRightKey) // Move right
            {
                Player.MoveRight();
            }
            else if (key == moveLeftKey) // Move left
            {
                Player.MoveLeft();
            }
            else if (key == jumpKey) // Jump
            {
                Player.Jump();
            }
            else if (key == defendKey) // Defend
            {
                Player.ChangeState(ActionState.Defending);
            }
            else if (key == attackJKey) // Attack J
            {
                Player.Attack(ActionState.AttackingJ);
            }
            else if (key == attackKKey) // Attack K
            {
                Player.Attack(ActionState.AttackingK);
            }
            else if (key == attackLKey) // Attack L
            {
                Player.Attack(ActionState.AttackingL);
            }
            else if (key == attackIKey) // Attack I
            {
                Player.Attack(ActionState.AttackingI);
            }
        }*/

        public void OnKeyUp(Keys key)
        {
            pressedKey.Remove(key);

            // Stop movement or attack
            if (key == moveRightKey || key == moveLeftKey)
            {
                Player.StopMoving();
            }
            else if (key == attackJKey || key == attackKKey || key == attackLKey || key == attackIKey)
            {
                Player.StopAttacking();
            }
            else if (key == defendKey)
            {
                Player.ChangeState(ActionState.Standing);  // Stop defending
            }
        }

      /*  public void Update()
        {
            // Process input
            //ProcessInput();
            Player.Update();

        }*/

        /*public void ProcessInput()
        {
            // Handle movement and attacks
            if (pressedKey.Contains(moveRightKey)) Player.MoveRight();
            if (pressedKey.Contains(moveLeftKey)) Player.MoveLeft();
            if (pressedKey.Contains(jumpKey)) Player.Jump();

            if (pressedKey.Contains(defendKey))
            {
                Player.ChangeState(ActionState.Defending);
            }

            if (pressedKey.Contains(attackJKey)) Player.Attack(ActionState.AttackingJ);
            if (pressedKey.Contains(attackKKey)) Player.Attack(ActionState.AttackingK);
            if (pressedKey.Contains(attackLKey)) Player.Attack(ActionState.AttackingL);
            if (pressedKey.Contains(attackIKey)) Player.Attack(ActionState.AttackingI);
        }*/
    }
}
