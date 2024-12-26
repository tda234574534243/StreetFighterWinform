using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using StreetFighterGame.GameEngine;

namespace StreetFighterGame.UI
{
    public class HealthBar
    {
        private readonly Character character;
        public HealthBar(Character character)
        {
            this.character = character;
        }

        public void Draw(Graphics g)
        {
            // Tính tỷ lệ phần trăm máu
            float healthPercentage = character.Health / 100f;

            // Vẽ thanh máu: màu đỏ, chiều dài thay đổi tùy theo mức máu
            g.FillRectangle(Brushes.Red, 10, 10, healthPercentage * 200, 20);
        }
    }
}