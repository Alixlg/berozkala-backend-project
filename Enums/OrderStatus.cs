namespace berozkala_backend.Enums
{
    public enum OrderStatus
    {
        PendingToPay,  // در انتظار پرداخت
        PendingConfirmation,     // در انتظار
        Processing,  // در حال پردازش
        Shipped,     // ارسال شده
        Completed,   // انجام شده
        Cancelled,   // لغو شده
        Returned   // مرجوعی
    }
}