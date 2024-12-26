using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreetFighterGame.GameEngine
{
    public class LogicGame
    {
        public Character Player1 { get; private set; }
        public Character Player2 { get; private set; }
        public float gameWidth;
        public float gameHeight;

        public LogicGame(float width, float height)
        {
            gameWidth = width;
            gameHeight = height;
        }

        // Cho phép người chơi chọn nhân vật
        public void InitializeCharacterSelection(string character1Name, string character2Name)
        {
            // Tạo nhân vật cho Player 1
            Player1 = CharacterFactory.CreateCharacter(character1Name, startX: 200, startY: 400);
            // Tạo nhân vật cho Player 2
            Player2 = CharacterFactory.CreateCharacter(character2Name, startX: 750, startY: 400);
        }

        public void Update()
        {

            // Cập nhật trạng thái của từng nhân vật
            Player1.Update(Player2.rectangle);
            Player2.Update(Player1.rectangle);
            //CollisionHandler.KiemTra2ThangDanhNhau(Player1, Player2);
            //Console.WriteLine("vi tri x nguoi choi 1:" + Player1.PositionX);
            //Console.WriteLine("vi tri x nguoi choi 2:" + Player2.PositionX);
        }

        /*public void Draw(Graphics g)
        {
            // Vẽ các thành phần của game như nhân vật, thanh máu, năng lượng
            Player1.Draw(g); // Pass the Graphics object to Player1's Draw method
            Player2.Draw(g); // Pass the Graphics object to Player2's Draw method


        }*/

    }
}
