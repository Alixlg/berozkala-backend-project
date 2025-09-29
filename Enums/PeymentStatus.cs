namespace berozkala_backend.Enums
{
    public enum PeymentStatus
    {
        Pending = 0,        // در انتظار پرداخت
        Paid = 1,           // پرداخت شده
        Failed = 2,         // پرداخت ناموفق
        Refunded = 3,       // بازگشت وجه
        Cancelled = 4      // لغو شده
    }
}