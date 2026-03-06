# Peminjaman Ruangan API

Sistem manajemen peminjaman ruangan berbasis Web API yang memungkinkan pengguna untuk memesan ruangan secara efisien dengan sistem persetujuan admin.

## Description
Project ini dibuat untuk mempermudah proses reservasi ruangan di lingkungan kampus. Sistem ini menangani konflik jadwal secara otomatis dan memberikan riwayat status peminjaman yang transparan bagi pengguna maupun administrator.

## Features
- **User Authentication & Authorization**
  - Registrasi dan Login menggunakan JWT (JSON Web Token).
  - Role-Based Access Control (Admin & User).
  - Password hashing menggunakan BCrypt.
- **Room Management**
  - **Admin:** Akses penuh (Create, Read, Update, Delete).
  - **User:** Akses terbatas (Read-only) untuk melihat daftar ruangan dan detail ruangan.
- **Booking System**
  - Pemesanan ruangan dengan validasi.
  - Pemesanan dengan status `Pending`, `Approved`, `Rejected`, `Cancelled`, `OnGoing`, dan `Completed`.
  - Mekanisme pembatalan (Cancel) dengan pengecekan kepemilikan data.
- **Approval Workflow**
  - Admin dapat menyetujui (Approve) atau menolak (Reject) pesanan.
  - Pencatatan riwayat perubahan status.
- **Automated Status Transitions**
  - Background Worker yang secara otomatis mengubah status booking:
  - `Approved` -> `OnGoing`.
  - `OnGoing` -> `Completed`.

## Tech Stack
- **Framework:** ASP.NET Core 10.0
- **Database:** PostgreSQL
- **Security:** JWT Authentication, BCrypt.Net
- **Mapping Tools:** AutoMapper
- **API Docs/Testing:** OpenAPI + Swagger UI

## Manual Backend Setup (Local)

Ikuti langkah berikut untuk setup backend secara manual di local environment.

1. Clone Repository
   ```bash
   git clone https://github.com/fathss/2026-peminjaman-ruangan-backend.git
   cd 2026-peminjaman-ruangan-backend/backend/PeminjamanRuangan.API
   ```

2. Siapkan Prasyarat
   * Install .NET SDK 10.0
   * Install PostgreSQL 15+
   * Pastikan service PostgreSQL berjalan

3. Buat Database PostgreSQL
   Buat database baru, contoh nama:
   ```sql
   CREATE DATABASE "PeminjamanRuanganDB";
   ```

4. Salin dan Atur Konfigurasi Aplikasi
   `appsettings.json.example` menjadi `appsettings.json`, lalu sesuaikan koneksi database dan JWT key.

   Contoh isi `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=PeminjamanRuanganDB;Username=postgres;Password=PASSWORD"
     },
     "Jwt": {
       "Key": "MINIMAL_32_KARAKTER_RAHASIA",
       "Issuer": "PeminjamanRuanganAPI",
       "Audience": "PeminjamanRuanganApp"
     }
   }
   ```

5. Restore Dependencies
   ```bash
   dotnet restore
   ```

6. Apply Migrations
   ```bash
   dotnet ef database update
   ```

7. Jalankan API
   ```bash
   dotnet run
   ```

8. Verifikasi API
   Buka Swagger UI di `http://localhost:5145/swagger` (port bisa berbeda sesuai `launchSettings.json`).

## Installation
Gunakan bagian **Manual Backend Setup (Local)** di atas untuk proses instalasi lengkap.

## Usage
1. Run Server
   ```bash
   dotnet run
   ```
2. Access Swagger UI
   Buka browser dan arahkan ke `http://localhost:5145/swagger` (port mungkin berbeda tergantung konfigurasi `launchSettings.json`).

## Swagger Authorization

Untuk endpoint yang membutuhkan autentikasi:

1. Login lewat `POST /api/auth/login`.
2. Copy nilai `token` dari response (Tanpa kurung kurawal).
3. Klik tombol `Authorize` di Swagger.
4. Paste token mentah `eyJ...`

## Default Seed User (Development)

Jika data seeder aktif, akun default:

- Admin
  - `username`: `admin`
  - `password`: `admin123`
- User
  - `username`: `user`
  - `password`: `user123`

## Environment Variables
Edit `appsettings.json` dan masukkan `Password` PostgreSQL serta `Jwt:Key` seperti contoh:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=PeminjamanRuanganDB;Username=postgres;Password=PASSWORD"
  },

  "Jwt": {
    "Key": "MINIMAL_32_KARAKTER_RAHASIA",
    "Issuer": "PeminjamanRuanganAPI",
    "Audience": "PeminjamanRuanganApp"
   }
}
```
