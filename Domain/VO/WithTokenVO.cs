namespace Domain.VO
{
    public class WithTokenVO<T>
    {
        public string Token { get; set; }
        public T Dados { get; set; }
    }
}
