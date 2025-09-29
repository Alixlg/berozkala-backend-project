namespace berozkala_backend.Enums
{
    public enum OrderStatus
    {
        PendingConfirmation = 0,     // در انتظار
        Processing = 1,  // در حال پردازش
        Shipped = 2,     // ارسال شده
        Completed = 3,   // انجام شده
        Cancelled = 4,   // لغو شده
        Returned = 5     // مرجوعی
    }
}