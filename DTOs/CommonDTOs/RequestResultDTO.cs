namespace berozkala_backend.DTOs.CommonDTOs
{
    public class RequestResultDto<T>
    {
        public required bool IsSuccess { get; set; }
        public required int StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Body { get; set; }
        public RequestResultDto()
        {
            Thread.Sleep(700);
        }
    }
}