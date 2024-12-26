using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using StreetFighterGame.GameEngine;

namespace StreetFighterGame.UI
{
    public class EnergyBar
    {
        private readonly Character character;  // Marked as readonly

        public EnergyBar(Character character)
        {
            this.character = character;
        }

        public void Draw(Graphics g)
        {
            // Giả sử `Energy` là thuộc tính trong `Character` với giá trị từ 0 đến 100
            float energyPercentage = character.Energy / 100f;

            // Vẽ thanh năng lượng với chiều dài thay đổi theo mức năng lượng
            g.FillRectangle(Brushes.Blue, 10, 40, energyPercentage * 200, 20); // Thanh năng lượng màu xanh dương
        }
    }
}