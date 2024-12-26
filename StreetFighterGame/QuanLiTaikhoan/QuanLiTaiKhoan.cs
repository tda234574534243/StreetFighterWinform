using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreetFighterGame.QuanLiTaikhoan
{
    public class LichSuTranDauViewModel
    {
        public string TenNguoiChoi { get; set; }
        public string NhanVat1 { get; set; }
        public string NhanVat2 { get; set; }
        public string KetQua { get; set; }
        public DateTime? ThoiGianTranDau { get; set; } 
    }

    public class TaiKhoan
    {
        public string Username;
        public string Password;
        public TaiKhoan(string us, string ps)
        {
            Username = us;
            Password = ps;
        }
    }
    public class TopPlayer
    {
        public string Username { get; set; }
        public int Wins { get; set; }
    }

    public static class QuanLiTaiKhoan
    {
        public static TaiKhoan taiKhoanDangChoi = new TaiKhoan(string.Empty, string.Empty);
        public static TaiKhoan Admin = new TaiKhoan("Admin", "TanHuyVippro05");
        public static int GetNhanVatIdByName(string tenNhanVat)
        {
            var db = new DoAnGameLoEntities();
             var nhanVat = db.NHAN_VAT
                                .FirstOrDefault(nv => nv.TenNhanVat == tenNhanVat);

                return nhanVat.NhanVatId; // Trả về Id hoặc null nếu không tìm thấy
        }
        public static int GetUserIdByUsername(string username)
        {
            using (var db = new DoAnGameLoEntities())
            {
                return db.TAI_KHOAN
                         .Where(u => u.Username == username)
                         .Select(u => u.UserId)
                         .FirstOrDefault();
            }
        }

        public static void LuuTranDau(int nhanVat1Id, int nhanVat2Id, string ketQua)
        {
            var db = new DoAnGameLoEntities();
                // Tạo đối tượng mới cho lịch sử trận đấu
                var tranDauMoi = new LICH_SU_TRAN_DAU
                {
                    UserId = GetUserIdByUsername(taiKhoanDangChoi.Username),
                    NhanVat1Id = nhanVat1Id,
                    NhanVat2Id = nhanVat2Id,
                    KetQua = ketQua,
                    ThoiGianTranDau = DateTime.Now // Lấy thời gian hiện tại
                };

                db.LICH_SU_TRAN_DAU.Add(tranDauMoi);

                // Cập nhật số trận thắng hoặc số trận thua
                var user = db.TAI_KHOAN.FirstOrDefault(u => u.Username == taiKhoanDangChoi.Username);
                if (user != null)
                {
                    if (ketQua == "Win") // để kQua là Win mới tính
                    {
                        user.SoTranThang = (user.SoTranThang ?? 0) + 1; // Tăng số trận thắng
                    }
                    else if (ketQua == "Lose") // để name là Lose mới tính
                    {
                        user.SoTranThua = (user.SoTranThua ?? 0) + 1; // Tăng số trận thua
                    }
                    else if (ketQua == "Draw")
                    {
                        user.SoTranHoa = (user.SoTranHoa ?? 0) + 1; // Tăng số trận hòa
                    }
                    
                }

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();
                Console.WriteLine($"Trận thắng: {user.SoTranThang}, Trận thua: {user.SoTranThua}, Trận Hòa: {user.SoTranHoa}");
        }

        public static List<LichSuTranDauViewModel> LayLichSuTranDau()
        {
            using (var db = new DoAnGameLoEntities())
            {
                var lichSu = db.LICH_SU_TRAN_DAU
                .Join(db.TAI_KHOAN, ls => ls.UserId, tk => tk.UserId, (ls, tk) => new { ls, tk })
                .Join(db.NHAN_VAT, lsTk => lsTk.ls.NhanVat1Id, nv1 => nv1.NhanVatId, (lsTk, nv1) => new { lsTk, nv1 })
                .Join(db.NHAN_VAT, lsTkNv1 => lsTkNv1.lsTk.ls.NhanVat2Id, nv2 => nv2.NhanVatId, (lsTkNv1, nv2) => new
                {
                    TenNguoiChoi = lsTkNv1.lsTk.tk.Username,
                    NhanVat1 = lsTkNv1.nv1.TenNhanVat,
                    NhanVat2 = nv2.TenNhanVat,
                    lsTkNv1.lsTk.ls.KetQua,
                    lsTkNv1.lsTk.ls.ThoiGianTranDau
                })
                .Select(x => new LichSuTranDauViewModel
                {
                    TenNguoiChoi = x.TenNguoiChoi,
                    NhanVat1 = x.NhanVat1,
                    NhanVat2 = x.NhanVat2,
                    KetQua = x.KetQua,
                    ThoiGianTranDau = x.ThoiGianTranDau ?? DateTime.MinValue
                })
                // Sắp xếp theo thời gian trận đấu từ mới nhất đến cũ nhất
                .OrderByDescending(x => x.ThoiGianTranDau)
                .ToList();

                return lichSu;
            }
        }

        public static void HienThiLichSuTranDau(DataGridView dataGridView)
        {
            var lichSu = LayLichSuTranDau();

            dataGridView.DataSource = lichSu;

            dataGridView.Columns["TenNguoiChoi"].HeaderText = "Người chơi";
            dataGridView.Columns["NhanVat1"].HeaderText = "Nhân vật 1";
            dataGridView.Columns["NhanVat2"].HeaderText = "Nhân vật 2";
            dataGridView.Columns["KetQua"].HeaderText = "Kết quả";
            dataGridView.Columns["ThoiGianTranDau"].HeaderText = "Thời gian";
        }
        public static List<LichSuTranDauViewModel> TimKiemLichSu(string tenNguoiChoi)
        {
            using (DoAnGameLoEntities db = new DoAnGameLoEntities())
            {
                var lichSu = LayLichSuTranDau()
                    .Where(ls => ls.TenNguoiChoi.Contains(tenNguoiChoi))
                    .ToList();

                return lichSu;
            }
        }

        public static List<TopPlayer> LayTop3NguoiChoi()
        {
            using (var db = new DoAnGameLoEntities())
            {
                var top3Players = db.TAI_KHOAN
                .OrderByDescending(x => x.SoTranThang) // Sắp xếp theo số trận thắng giảm dần
                .Take(3) // Lấy 3 tài khoản đầu tiên
                .ToList() // Lấy kết quả từ database về bộ nhớ
                .Select(x => new TopPlayer // Chuyển đổi sang danh sách đối tượng TopPlayer
                {
                    Username = x.Username,
                    Wins = x.SoTranThang ?? 0 // Nếu SoTranThang null, gán mặc định là 0
                })
                .ToList();

                return top3Players;
            }
        }


        public static bool DoiMatKhau(string username, string oldPassword, string newPassword, string confirmPassword, Label message)
        {
                // Tìm tài khoản
            using(var db = new DoAnGameLoEntities())
            {
                var account = db.TAI_KHOAN.FirstOrDefault(a => a.Username == username && a.Password == oldPassword);

                if (account == null)
                {
                    message.Visible = true;
                    message.Text = "Tên tài khoản hoặc mật khẩu cũ không chính xác!";
                    return false;
                }

                // Kiểm tra mật khẩu mới có hợp lệ không
                if (newPassword.Length < 8)
                {
                    message.Visible = true;
                    message.Text = "Mật khẩu mới phải có ít nhất 8 ký tự!";
                    return false;
                }

                if (newPassword != confirmPassword)
                {
                    message.Visible = true;
                    message.Text = "Mật khẩu mới và xác nhận mật khẩu không khớp!";
                    return false;
                }

                // Cập nhật mật khẩu mới
                account.Password = newPassword;
                db.SaveChanges();
                MessageBox.Show("Đổi mật khẩu thành công!");
                message.Visible = false;
                return true;
            }
        }

        public static bool XoaTaiKhoan(string username)
        {
            if (username == Admin.Username)
            {
                MessageBox.Show("Không thể xóa tài khoản admin");
                return false;
            }
            using (var db = new DoAnGameLoEntities())
            {
                // Tìm tài khoản cần xóa
                var account = db.TAI_KHOAN.FirstOrDefault(a => a.Username == username);

                if (account != null)
                {
                    // Xóa các bản ghi trong bảng XEP_HANG liên quan đến tài khoản này
                    var xepHang = db.XEP_HANG.Where(xh => xh.UserId == account.UserId).ToList();
                    db.XEP_HANG.RemoveRange(xepHang);

                    // Xóa các bản ghi trong bảng LICH_SU_TRAN_DAU liên quan đến tài khoản này
                    var lichSuTranDau = db.LICH_SU_TRAN_DAU.Where(ls => ls.UserId == account.UserId).ToList();
                    db.LICH_SU_TRAN_DAU.RemoveRange(lichSuTranDau);

                    // Xóa tài khoản
                    db.TAI_KHOAN.Remove(account);

                    // Lưu thay đổi
                    db.SaveChanges();
                    MessageBox.Show($"Tài khoản '{username}' đã được xóa thành công!");
                    return true;
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy tài khoản '{username}'!");
                    return false;
                }
            }
        }


        public static void TimKiemTaiKhoan(DataGridView dataGridViewAccounts, string keyword)
        {
            using (var db = new DoAnGameLoEntities())
            {
                if (keyword == string.Empty)
                {
                    LoadDanhSachTaiKhoanLenGridView(dataGridViewAccounts);
                    return;
                }
                var searchResult = db.TAI_KHOAN
               .Where(a => a.Username.Contains(keyword)) // Lọc theo từ khóa
               .Select(a => new
               {
                   a.UserId,
                   a.Username,
                   a.Tien,
                   a.SoTranThang,
                   a.SoTranThua,
                   a.SoTranHoa
               }).ToList();

                // Gán kết quả vào DataGridView
                dataGridViewAccounts.DataSource = searchResult;

                if (searchResult.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy tài khoản nào phù hợp!");
                }
            }
        }
        public static void LoadDanhSachTaiKhoanLenGridView(DataGridView dataGridViewAccounts)
        {
            using (var db = new DoAnGameLoEntities())
            {
                var accounts = db.TAI_KHOAN
                .Select(a => new
                {
                    a.UserId,
                    a.Username,
                    a.Tien,
                    a.SoTranThang,
                    a.SoTranThua,
                    a.SoTranHoa
                }).ToList();
                dataGridViewAccounts.Dock = DockStyle.None; // kích thước tùy chỉnh cho Admin
                dataGridViewAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewAccounts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                // Hiển thị danh sách lên DataGridView
                dataGridViewAccounts.DataSource = accounts;
            }
        }
        public static bool CheckIsAdmin()
        {
            Console.WriteLine(taiKhoanDangChoi.Username);
            return taiKhoanDangChoi.Username == "Admin";
        }
        public static void DangNhap(string us, string ps)
        {
            using (var db = new DoAnGameLoEntities())
            {
                string username = us;
                string password = ps;
                var user = db.TAI_KHOAN
                     .FirstOrDefault(u => u.Username == username && u.Password == password);
                if (user != null)
                {
                    taiKhoanDangChoi.Username = us;
                    taiKhoanDangChoi.Password = ps;
                    FormStart mhs = new FormStart();
                    mhs.Show();
                    FormLogincs.instance.Hide();
                }
                else
                {
                    // Đăng nhập thất bại
                    MessageBox.Show("Tài khoản hoặc mật khẩu sai!");
                }
            }
        }
        public static bool CreatAccount(string usn, string ps, string crUs, string crPs1, string crPs2, Label lb)
        {
            if (CheckUsername(lb, crUs) && CheckPassword(lb, crPs1, crPs2))
            {
                using (DoAnGameLoEntities db = new DoAnGameLoEntities())
                {
                    TAI_KHOAN newUser = new TAI_KHOAN
                    {
                        Username = usn,
                        Password = ps,
                        Tien = 100
                    };

                    db.TAI_KHOAN.Add(newUser);

                    db.SaveChanges();
                }
                MessageBox.Show("Tạo tài khoản thành công");

                return true;
            }
            return false;
        }
        public static bool CheckUsername(Label lb, string crUs)
        {
            using (var db = new DoAnGameLoEntities())
            {
                var user = db.TAI_KHOAN.FirstOrDefault(u => u.Username == crUs);

                if (user != null)
                {
                    lb.Visible = true;
                    lb.Text = "Tài khoản đã tồn tại";
                    return false;  // Username tồn tại, trả về false (không hợp lệ khi đăng ký)
                }
                else
                {
                    return true;   // Username không tồn tại, trả về true (hợp lệ khi đăng ký)
                }
            }
        }
        public static bool CheckPassword(Label lb, string crPs1, string crPs2)
        {
            if (crPs1 == crPs2 && crPs1.Length >= 8)
            {
                return true;
            }
            else
            {
                lb.Visible = true;
                lb.Text = "Mật khẩu phải dài hơn 8 kí tự";
                return false;
            }
        }
    }
}
