# Changelog

## [1.2.0] - 2026-03-22
### Added
- Menambahkan dukungan runtime configuration injection untuk menyesuaikan konfigurasi aplikasi saat deploy.
- Menambahkan koneksi database PostgreSQL Neon melalui migrasi terbaru.
- Menambahkan dukungan origin frontend production pada kebijakan CORS.

### Changed
- Refactor `RoomBookingService` untuk menggunakan lazy status update pada alur service.
- Menghapus ketergantungan update status otomatis melalui `BookingStatusWorker`.
- Memperbarui Docker build flow (`Dockerfile` dan `.dockerignore`) agar proses build lebih optimal.

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
