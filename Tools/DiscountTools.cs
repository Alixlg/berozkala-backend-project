using berozkala_backend.Entities.OtherEntities;

namespace berozkala_backend.Tools;

public class DiscountTools
{
    public static Task DiscountCodeValidatorAsync(DiscountCode discountCode, decimal totalAmount, int usageCount)
    {
        return Task.Run(() =>
        {
            if (!discountCode.IsActive)
            {
                throw new Exception("کد تخفیف مورد نظر فعال نیست");
            }

            if (DateTime.Now > discountCode.EndOfCredit)
            {
                throw new Exception("کد تخفیف مورد نظر منقضی شده است");
            }

            if (totalAmount < discountCode.MinProductPrice)
            {
                throw new Exception($"کد تخفیف مورد نظر برای مبالغ بالای {discountCode.MinProductPrice} تومان میباشد");
            }

            if (usageCount > discountCode.MaxUsageCount)
            {
                throw new Exception("شما بیش از حد مجاز از این کد تخفیف استفاده کردید");
            }
        });
    }
}
