# Changelog

## [1.2.2] - 2026-04-11
### Fixed
- Menghapus middleware HTTPS redirection yang tidak diperlukan agar backend lebih cocok berjalan di balik reverse proxy/deployment environment.

## [1.2.1] - 2026-04-06
### Fixed
- Memperbarui konfigurasi AutoMapper dan referensi package agar startup aplikasi dan mapping tetap konsisten.

## [1.2.0] - 2026-03-23
### Added
- Menambahkan enhancement endpoint detail peminjaman agar `GetById` mengembalikan detail booking beserta riwayat status (`BookingStatusHistory`).
- Menambahkan dukungan konfigurasi koneksi database Neon melalui migration.

### Changed
- Memperbarui konfigurasi aplikasi agar nilai konfigurasi dapat diinjeksi saat runtime.
- Memperbarui kebijakan CORS untuk menambahkan origin frontend production.
- Memperbarui proses build container dengan peningkatan pada `Dockerfile` dan `.dockerignore`.
- Refaktor `RoomBookingService` untuk mendukung lazy update status dan menghapus kebutuhan `BookingStatusWorker`.

## [1.1.0] - 2026-03-06
### Added
- Menambahkan `Dockerfile` untuk build dan publish API menggunakan multi-stage image.
- Menambahkan `.dockerignore` untuk mengoptimalkan Docker build context.
- Menambahkan dokumentasi setup backend manual dan panduan otorisasi Swagger di `README.md`.

### Changed
- Memperbarui konfigurasi autentikasi di `Program.cs` dengan default JWT bearer scheme.
- Memperbarui konfigurasi CORS di `Program.cs` untuk mengizinkan origin `http://localhost:5173` dan `http://localhost:3000`.
- Memigrasikan konfigurasi OpenAPI/Swagger di `Program.cs` ke `AddOpenApi` + `MapOpenApi` dan menambahkan security scheme bearer JWT.
- Memperbarui endpoint route parameter menjadi `id:int` di `RoomBookingsController` dan `RoomsController` untuk memperjelas kontrak route integer dan memperbaiki validasi parameter `id` di Swagger UI.

### Fixed
- Memperbaiki issue Swagger UI pada endpoint berbasis path parameter (`/api/roombookings/{id}`, `/api/rooms/{id}`) yang menampilkan error `For 'id': Required field is not provided` meskipun nilai `id` sudah diisi.

## [1.0.0] - 2026-02-15
### Added
- Fitur Autentikasi menggunakan JWT (JSON Web Token).
- Role-Based Access Control (RBAC) untuk Admin dan User.
- Fitur otomatisasi status peminjaman (Approved -> OnGoing -> Completed).
- Riwayat status peminjaman (`BookingStatusHistory`).

### Changed
- Mengganti sistem identitas dari *hardcoded* ID ke *Claims* dari Token JWT.
- Update `RoomBookingService` untuk mendukung validasi kepemilikan data.
- Refaktor `AppDbContext` untuk memperbaiki relasi antar tabel.

### Removed
- Menghapus data seeder lama dari `AppDbContext`.
- Menghapus `WeatherForecastController` bawaan template.
