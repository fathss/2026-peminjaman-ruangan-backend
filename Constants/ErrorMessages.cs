namespace PeminjamanRuanganAPI.Constants
{
    public static class ErrorMessages
    {
        public const string RoomHasActiveBookings = "Ruangan tidak dapat dihapus karena masih memiliki jadwal booking aktif/menunggu persetujuan.";
        public const string RoomInactive = "Ruangan saat ini sedang tidak dapat digunakan.";
        public const string RoomNotFound = "Ruangan tidak ditemukan.";
        public const string InvalidTimeRange = "Waktu mulai harus lebih awal dari waktu selesai.";
        public const string BookingConflict = "Ruangan telah dipesan pada periode waktu tersebut. Silakan pilih waktu atau ruangan lain.";
    }
    }