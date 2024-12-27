## Tổng Quan
---

### 1.1 Giới thiệu về lĩnh vực
M.U.G.E.N là một game engine mạnh mẽ, đặc biệt phù hợp để phát triển các trò chơi đối kháng. Người dùng có thể tạo ra những tựa game với nhân vật tùy chỉnh, sân đấu độc đáo và cơ chế chiến đấu đa dạng,là lựa chọn lý tưởng để học hỏi cách phát triển game đối kháng, đồng thời sáng tạo các nội dung mới.

Street Fighter, tựa game đối kháng huyền thoại của Capcom, nổi bật với các nhân vật có phong cách chiến đấu độc đáo và lối chơi sâu sắc. Kết hợp M.U.G.E.N với Street Fighter là một cách tuyệt vời để tái hiện và học hỏi cách phát triển một tựa game đối kháng thành công.

---

### 1.2 Lý do chọn đề tài
Đề tài tận dụng khả năng tùy biến của M.U.G.E.N và các yếu tố thành công của Street Fighter để tạo ra một trò chơi được làm bằng Winform C# độc đáo, hấp dẫn. Dự án mang lại cơ hội thực hành lập trình, thiết kế nhân vật, tối ưu hóa gameplay và học hỏi từ cộng đồng phát triển toàn cầu.
---

### 1.3 Giới thiệu sơ lược về game
Game cung cấp các chức năng sau:
- Đăng nhập, đăng ký tài khoản, đổi mật khẩu.
- Chế độ chơi đơn với máy (AI).
- Chế độ đối kháng hai người chơi trên cùng một thiết bị.
- Xem lịch sử đấu.
- Xem bảng xếp hạng người chơi.
---

## Cách Cài Đặt và Sử Dụng

### 2.1 Yêu cầu hệ thống
- Hệ điều hành: Windows 7 trở lên.
- .NET Framework 4.8 trở lên.
- SQL Server 2019 hoặc phiên bản mới hơn.
- Visual Studio 2022 

### 2.2 Cài đặt
1. **Tải mã nguồn**: Clone dự án từ repository GitHub (or download .zip).
   ```bash
   git clone <repository_url>
   ```
2. **Cấu hình cơ sở dữ liệu**:
   - Tạo cơ sở dữ liệu trên SQL Server.
   - Nhập các file script SQL trong thư mục `Database` để tạo bảng và dữ liệu mẫu.
3. **Cài đặt dependencies**:
   - Mở dự án bằng Visual Studio 2022.
4. **Cấu hình kết nối**:
   - Sửa file thay đổi severname trong `App.config` để cập nhật thông tin kết nối đến cơ sở dữ liệu.
     ![appconfig](https://github.com/tda234574534243/StreetFighterWinform/blob/master/StreetFighterGame/Resources/appconfig.JPG)
5. **Chạy ứng dụng**:
   - Nhấn `F5` trong Visual Studio để khởi chạy ứng dụng.
---

### 2.3 Hướng dẫn sử dụng
- **Đăng nhập/Đăng ký**:
  - Người dùng có thể tạo tài khoản mới hoặc đăng nhập bằng thông tin tài khoản hiện tại.
- **Chơi đơn**:
  - Chọn nhân vật và đối đầu với AI để luyện tập kỹ năng.
- **Chơi PvP**:
  - Hai người chơi có thể thi đấu trực tiếp trên cùng một thiết bị.
- **Xem lịch sử và thứ hạng**:
  - Theo dõi kết quả các trận đấu trước và so sánh thứ hạng với người chơi khác.

---

## Mô Tả Game

### 3.1 Giao diện chính
- Màn hình đăng nhập, đăng ký và đổi mật khẩu.
- Màn hình chọn nhân vật và sân đấu.
- Màn hình chơi đơn, PvP và bảng xếp hạng.

### 3.2 Chức năng nổi bật
1. **Hệ thống tài khoản**: Đăng nhập, đăng ký, đổi mật khẩu.
2. **Chế độ chơi đơn**: Đối đầu với AI để luyện tập kỹ năng.
3. **Chế độ PvP**: Thi đấu trực tiếp trên cùng một thiết bị.
4. **Lịch sử đấu**: Lưu trữ kết quả các trận đấu.
5. **Bảng xếp hạng**: Cập nhật thứ hạng dựa trên số trận thắng.

---

## Cơ Sở Dữ Liệu

### 4.1 Các bảng chính
- **TAI_KHOAN**:
  - Thuộc tính: UserId, Username, Password, SoTranThang, SoTranThua, SoTranHoa.
- **NHAN_VAT**:
  - Thuộc tính: NhanVatId, TenNhanVat.
- **LICH_SU_TRAN_DAU**:
  - Thuộc tính: TranDauId, UserId, NhanVat1Id, NhanVat2Id, KetQua, ThoiGianTranDau.
- **XEP_HANG**:
  - Thuộc tính: UserId, SoTranThang, XepHang.

### 4.2 Diagram
![CSDL](https://github.com/tda234574534243/StreetFighterWinform/raw/f8e2063b590d5a26a000e6be15ce126fea53ba64/StreetFighterGame/Resources/140dde4ff6854bdb1294.jpg)

---
## 5. Demo
### Login form
![LoginForm](https://github.com/tda234574534243/StreetFighterWinform/blob/master/StreetFighterGame/Resources/loginsf.jpg)
### Admin Account
![Account](https://github.com/tda234574534243/StreetFighterWinform/blob/master/StreetFighterGame/Resources/Admin.JPG)
### Normal Account
![Account1](https://github.com/tda234574534243/StreetFighterWinform/blob/master/StreetFighterGame/Resources/normaluser.JPG)
### Gameplay
![Game](https://github.com/tda234574534243/StreetFighterWinform/blob/master/StreetFighterGame/Resources/gameplay.JPG)
### Select Char
![Select](https://github.com/tda234574534243/StreetFighterWinform/blob/master/StreetFighterGame/Resources/select.JPG)
### Select Map
![Map](https://github.com/tda234574534243/StreetFighterWinform/blob/master/StreetFighterGame/Resources/map.JPG)
### Find and Delete Account by Admin
![Check](https://github.com/tda234574534243/StreetFighterWinform/blob/master/StreetFighterGame/Resources/adminaccount.JPG)

---
## 6. Kết Luận

### 6.1 Mục tiêu đạt được
- Hoàn thiện cơ chế chiến đấu đối kháng.
- Cung cấp trải nghiệm chơi đơn và PvP mượt mà.
- Xây dựng giao diện người dùng thân thiện.

### 6.2 Hướng phát triển
- Tích hợp chơi online và giải đấu.
- Tạo thêm nhân vật và chế độ chơi mới.
- Phát triển AI thông minh hơn.
- Thêm tính năng tùy biến nhân vật và các yếu tố thẩm mỹ.
# Note: 
- You can download, modify, and use the code for your project.
- This project is licensed under the MIT License, which grants permission to:
- Use the code for personal, educational, or commercial purposes.
- Modify the code as needed for your specific requirements.
- Distribute the modified or original code, provided that the license and copyright notice are included.
# Disclaimer:
- The project is provided "as is" without any warranty, express or implied, including but not limited to the warranties of merchantability, fitness for a particular purpose, and noninfringement.
# Source assets character and images: 
- **Vegito**: by Fresh Prince 24 [https://www.youtube.com/@FreshPrince24_MUGEN] 🔴
- **Zenitsu**: by Dr. Animation [https://www.youtube.com/@dranimationdrcreanimationimaginationcreations]; Sebatião [https://www.youtube.com/@sebastiaovideosejogos8085]; Entah 99 [https://www.youtube.com/@entah9963] 🟡
- **GokuMUI**: by InSeph [https://www.youtube.com/@InSeph] 🔵
- **Chunli**: by InSeph [https://www.youtube.com/@InSeph] 🟣
- **Ryu**: by Planeptune Arte [https://www.youtube.com/@NepgearArte64] 🟢
- **Gojo**: by ArthurMugen [https://www.youtube.com/@arthurmugen] 🟠
- Nếu như nguồn hình ảnh, assets nhân vật không đúng tác giả, hoặc thiếu vui lòng phản hồi tại email: projectmap1234@gmail.com
- If the image sources or character assets are not from the correct author or missing, please respond to the email: projectmap1234@gmail.com.
# Teammate
- Manwithgame [https://github.com/tda234574534243]: Animation, tìm map, tìm asset, fix code, tối ưu hóa hiệu suất game, tạo và duy trì môi trường phát triển, phát triển chiến lược quảng bá dự án.
- Binh05 [https://github.com/Binh05]: Logic, vật lý, fix lỗi, phát triển các tính năng game, kiểm thử game và xử lý các lỗi liên quan đến gameplay, quản lý và giám sát tiến độ công việc nhóm, debug code.
- PhanTanHuy [https://github.com/PhanTanHuy]: Database, UI, xây dựng hệ thống lưu trữ dữ liệu, thiết kế và tối ưu giao diện người dùng, quản lý và duy trì dữ liệu.
- LamQuocViet [https://github.com/Lamquocviet]: Thuyết trình, làm slide thuyết trình, chuẩn bị tài liệu hướng dẫn người dùng, hỗ trợ thiết kế.
- HoDangKhoa [https://github.com/khoahocd]: Viết báo cáo, làm slide thuyết trình, xử lý tài liệu dự án, quản lý, tham khảo nguồn tài liệu asset nhân vật khác nhau.
