# Peminjaman Ruangan API

Sistem manajemen peminjaman ruangan berbasis Web API yang memungkinkan pengguna untuk memesan ruangan secara efisien dengan sistem persetujuan admin.

## Description
Project ini dibuat untuk mempermudah proses reservasi ruangan di lingkungan kampus. Sistem ini menangani konflik jadwal secara otomatis dan memberikan riwayat status peminjaman yang transparan bagi pengguna maupun administrator.

## Features
* **User Authentication & Authorization**
    * Registrasi dan Login menggunakan JWT (JSON Web Token).
    * Role-Based Access Control (Admin & User).
    * Password hashing menggunakan BCrypt.
* **Room Management**
    * **Admin:** Akses penuh (Create, Read, Update, Delete).
    * **User:** Akses terbatas (Read-only) untuk melihat daftar ruangan dan detail ruangan.
* **Booking System**
    * Pemesanan ruangan dengan validasi.
    * Pemesanan dengan status `Pending`, `Approved`, `Rejected`, `Cancelled`, `OnGoing`, dan `Completed`
    * Mekanisme pembatalan (Cancel) dengan pengecekan kepemilikan data.
* **Approval Workflow**
    * Admin dapat menyetujui (Approve) atau menolak (Reject) pesanan.
    * Pencatatan riwayat perubahan status.
* **Automated Status Transitions**
    * Background Worker yang secara otomatis mengubah status booking:
        * `Approved` → `OnGoing`.
        * `OnGoing` → `Completed`.

## Tech Stack
* **Framework:** ASP.NET Core 10.0
* **Database:** Entity Framework Core (SQLite)
* **Security:** JWT Authentication, BCrypt.Net
* **Mapping Tools:** AutoMapper
* **API Documentation:** Swagger UI & Thunder Client

## Installation
1. Clone Repository
   ```bash
   git clone https://github.com/fathss/2026-peminjaman-ruangan-backend.git
   ```
2. Restore Dependencies
   ```bash
   dotnet restore
   ```
3. Apply Migrations
   ```bash
   dotnet ef database update
   ```

## Usage
1. Run Server
   ```bash
   dotnet run
   ```
2. Access Swagger UI
   Buka browser dan arahkan ke `http://localhost:5145/swagger` (Port mungkin berbeda tergantung konfigurasi `launchSettings.json`).

## Environment Variables
Tambahkan konfigurasi berikut pada file `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=campus_room_booking.db"
  },

  "Jwt": {
    "Key": "FSGC8kL0At93x7DH8yvzEeltrG44g-yDlBKKsYyamcsQhGtCFlBMWU5318sqctowyOVrSQVux1dzdhkhiCm2xg",
    "Issuer": "PeminjamanRuanganAPI",
    "Audience": "PeminjamanRuanganApp"
  },
}
