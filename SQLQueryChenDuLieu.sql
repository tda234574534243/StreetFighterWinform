
use DoAnGameLo

go
INSERT INTO TAI_KHOAN (Username, Password, Tien, SoTranThang, SoTranThua)
VALUES 
('Admin', 'TanHuyVippro05', 100000, 500, 0),
('DucAnhGaVCL', 'pass1235', 100, 5, 2),
('HuyVipproPlayer', 'pass4567', 200, 3, 4),
('BinhGaVc', 'pass7891', 50, 1, 5),
('KhoaIT', 'pass789123', 50, 8, 5),
('HuyVipproPlayer1', 'pass4567', 200, 3, 4),
('BinhGaVc1', 'pass7891', 50, 1, 5),
('KhoaIT1', 'pass789123', 50, 8, 5),
('HuyVipproPlayer2', 'pass4567', 200, 3, 4),
('BinhGaVc2', 'pass7891', 50, 1, 5),
('KhoaIT2', 'pass789123', 50, 8, 5),
('HuyVipproPlayer3', 'pass4567', 200, 3, 4),
('BinhGaVc3', 'pass7891', 50, 1, 5),
('KhoaIT3', 'pass789123', 50, 8, 5),
('QuocViet32', 'pass1012201', 300, 10, 0);

go
INSERT INTO NHAN_VAT (TenNhanVat)
VALUES 
('chunLi'),
('ryu'),
('goku'),
('vegeto'),
('zenitsu'),
('gojo');


go

INSERT INTO XEP_HANG (UserId, SoTranThang, XepHang)
VALUES 
    (5, 10, 1),
    (4, 8, 2),
    (1, 5, 3),
    (2, 3, 4),
    (3, 1, 5);
	
go

INSERT INTO LICH_SU_TRAN_DAU (UserId, NhanVat1Id, NhanVat2Id, KetQua, ThoiGianTranDau)
VALUES 
    (1,  1, 2, 'Win', '2024-12-01 15:30:00'),
    (3,  3, 4, 'Win', '2024-12-01 16:00:00'),
    (5,  5, 1, 'Lose', '2024-12-01 17:00:00'),
    (2,  2, 3, 'Win', '2024-12-01 18:00:00'),
    (4,  4, 5, 'Win', '2024-12-01 19:00:00');
