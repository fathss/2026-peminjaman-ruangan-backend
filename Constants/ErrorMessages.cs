namespace PeminjamanRuanganAPI.Constants
{
    public static class ErrorMessages
    {
        public const string RoomHasActiveBookings = "Ruangan tidak dapat dihapus karena masih memiliki jadwal booking aktif/menunggu persetujuan.";
        public const string RoomInactive = "Ruangan saat ini sedang tidak dapat digunakan.";
        public const string RoomNotFound = "Ruangan tidak ditemukan.";
        public const string StartTimeInPast = "Waktu mulai tidak boleh berada di masa lalu.";
        public const string InvalidTimeRange = "Waktu mulai harus lebih awal dari waktu selesai.";
        public const string BookingConflict = "Ruangan telah dipesan pada periode waktu tersebut. Silakan pilih waktu atau ruangan lain.";
        public const string CannotEditOngoing = "Booking yang sedang berlangsung tidak dapat diedit.";
        public const string CannotEditCompleted = "Booking yang sudah selesai tidak dapat diedit.";
        public const string CannotDeleteOngoing = "Booking yang sedang berlangsung tidak dapat dihapus.";
        public const string CannotApproveCancelled = "Booking yang sudah dibatalkan tidak dapat disetujui.";
        public const string CannotApproveCompleted = "Booking yang sudah selesai tidak dapat disetujui.";
        public const string AlreadyApproved = "Booking sudah disetujui.";
        public const string CannotRejectCancelled = "Booking yang sudah dibatalkan tidak dapat ditolak.";
        public const string CannotRejectCompleted = "Booking yang sudah selesai tidak dapat ditolak.";
        public const string CannotRejectOngoing = "Booking yang sedang berlangsung tidak dapat ditolak.";
        public const string AlreadyRejected = "Booking sudah ditolak.";
        public const string CannotCancelCompleted = "Booking yang sudah selesai tidak dapat dibatalkan.";
        public const string CannotCancelOngoing = "Booking yang sedang berlangsung tidak dapat dibatalkan.";
        public const string AlreadyCancelled = "Booking sudah dibatalkan.";
    }
    }