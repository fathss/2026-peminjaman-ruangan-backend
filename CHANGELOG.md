# Changelog
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
