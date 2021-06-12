using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebQuanAn.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ho = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    MatKhau = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Hinh = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SDT = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GioiTinh = table.Column<int>(type: "int", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: false),
                    Sdt = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    FacebookLink = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MatKhau = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhanLoai",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanLoai", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    MaDH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThoiGian = table.Column<DateTime>(type: "datetime", nullable: false),
                    TrangThai = table.Column<byte>(type: "tinyint", nullable: false),
                    MaKH = table.Column<int>(type: "int", nullable: false),
                    DiaChi = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.MaDH);
                    table.ForeignKey(
                        name: "FK_DonHang_KhachHang_MaKH",
                        column: x => x.MaKH,
                        principalTable: "KhachHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ThucDon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<decimal>(type: "money", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    MaLoai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThucDon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThucDon_PhanLoai_MaLoai",
                        column: x => x.MaLoai,
                        principalTable: "PhanLoai",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CTDH",
                columns: table => new
                {
                    MaDH = table.Column<int>(type: "int", nullable: false),
                    MaTD = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTDH", x => new { x.MaDH, x.MaTD });
                    table.ForeignKey(
                        name: "FK_CTDH_DonHang_MaDH",
                        column: x => x.MaDH,
                        principalTable: "DonHang",
                        principalColumn: "MaDH",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CTDH_ThucDon_MaTD",
                        column: x => x.MaTD,
                        principalTable: "ThucDon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CTDH_MaTD",
                table: "CTDH",
                column: "MaTD");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_MaKH",
                table: "DonHang",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_ThucDon_MaLoai",
                table: "ThucDon",
                column: "MaLoai");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUser");

            migrationBuilder.DropTable(
                name: "CTDH");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "ThucDon");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "PhanLoai");
        }
    }
}
